using System;
using System.IO;
using System.Text;

namespace vanish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Shellcode XOR Obfuscator (0xFA key)");
            Console.WriteLine("===================================");

            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ShellcodeObfuscator <path_to_shellcode_file>");
                Console.WriteLine("The shellcode file should contain bytes in the format: 0xfc,0x48,0x83,...");
                return;
            }

            try
            {
                string filePath = args[0];
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    return;
                }

                string shellcodeText = File.ReadAllText(filePath);

                // Parse the shellcode bytes
                byte[] shellcodeBytes = ParseShellcode(shellcodeText);
                Console.WriteLine($"Original shellcode size: {shellcodeBytes.Length} bytes");

                // Encrypt the shellcode with XOR 0xFA
                byte[] encryptedShellcode = XorEncrypt(shellcodeBytes, 0x02);

                // Generate output code
                string output = GenerateOutputCode(encryptedShellcode, shellcodeBytes.Length);

                // Write to file
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(filePath),
                    Path.GetFileNameWithoutExtension(filePath) + "_obfuscated.txt");

                File.WriteAllText(outputPath, output);
                Console.WriteLine($"Obfuscated shellcode saved to: {outputPath}");
                Console.WriteLine();
                Console.WriteLine("Copy the generated code into your shellcode runner.");

                // Also show the decryption function
                Console.WriteLine();
                Console.WriteLine("Decryption function to use in your runner:");
                Console.WriteLine(GetDecryptionFunction());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static byte[] ParseShellcode(string shellcodeText)
        {
            // Remove any whitespace and split by commas
            string[] byteStrings = shellcodeText
                .Replace(" ", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("{", "")
                .Replace("}", "")
                .Split(',');

            byte[] bytes = new byte[byteStrings.Length];
            for (int i = 0; i < byteStrings.Length; i++)
            {
                string byteStr = byteStrings[i].Trim();
                if (string.IsNullOrEmpty(byteStr))
                    continue;

                if (byteStr.StartsWith("0x"))
                    byteStr = byteStr.Substring(2);

                bytes[i] = Convert.ToByte(byteStr, 16);
            }

            return bytes;
        }

        static byte[] XorEncrypt(byte[] data, byte xorKey)
        {
            byte[] encrypted = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                encrypted[i] = (byte)(data[i] ^ xorKey);
            }
            return encrypted;
        }

        static string GenerateOutputCode(byte[] encryptedShellcode, int originalLength)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("// XOR Encrypted Shellcode (Key: 0xFA)");
            sb.AppendLine($"// Original length: {originalLength} bytes");
            sb.AppendLine($"// Encrypted length: {encryptedShellcode.Length} bytes");
            sb.AppendLine();

            sb.AppendLine("private static byte[] GetEncryptedPayload()");
            sb.AppendLine("{");
            sb.AppendLine("    // XOR encrypted shellcode with key 0xFA");

            // Format the encrypted shellcode array
            sb.AppendLine($"    byte[] encryptedShellcode = new byte[{encryptedShellcode.Length}]");
            sb.AppendLine("    {");
            sb.AppendLine("        " + FormatByteArray(encryptedShellcode, 12));
            sb.AppendLine("    };");
            sb.AppendLine();
            sb.AppendLine("    return encryptedShellcode;");
            sb.AppendLine("}");

            return sb.ToString();
        }

        static string GetDecryptionFunction()
        {
            return @"private static byte[] DecryptShellcode(byte[] encryptedData)
{
    // XOR decrypt with key 0xFA
    byte[] decrypted = new byte[encryptedData.Length];
    for (int i = 0; i < encryptedData.Length; i++)
    {
        decrypted[i] = (byte)(encryptedData[i] ^ 0xFA);
    }
    return decrypted;
}";
        }

        static string FormatByteArray(byte[] bytes, int bytesPerLine = 12)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                if (i > 0)
                {
                    if (i % bytesPerLine == 0)
                        sb.AppendLine(",");
                    else
                        sb.Append(", ");
                }

                if (i % bytesPerLine == 0)
                    sb.Append("        ");

                sb.Append($"0x{bytes[i]:X2}");
            }

            return sb.ToString();
        }
    }
}