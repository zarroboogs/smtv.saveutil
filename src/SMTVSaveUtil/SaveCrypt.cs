using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace SMTVSaveUtil
{
    class SaveCrypt
    {
        private static readonly string sKey = "MDEyMzQ1Njc4OWFiY2RlZj";

        static SaveCrypt()
        {
            sKey = Encoding.UTF8.GetString(Convert.FromBase64String(sKey + "AxMjM0NTY3ODlhYmNkZWY="));
        }

        public static bool IsEncrypted(byte[] buffer)
            => Encoding.ASCII.GetString(buffer[0..4]) != "GVAS";

        public static byte[] Encrypt(byte[] buffer)
            => Crypt(buffer, true);

        public static byte[] Decrypt(byte[] buffer)
            => Crypt(buffer, false);

        public static byte[] Crypt(byte[] buffer, bool encrypt)
        {
            var engine = new AesEngine();
            var cipher = new BufferedBlockCipher(engine);

            var keyBytes = Encoding.ASCII.GetBytes(sKey);
            var keyP = new KeyParameter(keyBytes);

            cipher.Init(encrypt, keyP);

            var res = new byte[cipher.GetOutputSize(buffer.Length)];
            var len = cipher.ProcessBytes(buffer, res, 0);
            len += cipher.DoFinal(res, len);

            return res[..len];
        }

        public static void CryptFile(string path, string pathOut = "")
        {
            var data = File.ReadAllBytes(path);
            var encBefore = IsEncrypted(data);

            Console.WriteLine(encBefore ? "Decrypting..." : "Encrypting...");
            data = Crypt(data, !encBefore);

            if (encBefore && IsEncrypted(data))
            {
                Console.WriteLine("Decryption failed, the file might not be a valid SMTV save");
                return;
            }

            if (string.IsNullOrWhiteSpace(pathOut))
            {
                pathOut = encBefore ? $"{path}_dec" : $"{path}_enc";
            }

            Console.WriteLine($"Writing to {pathOut}...");
            File.WriteAllBytes(pathOut, data);

            Console.WriteLine("Done");
        }
    }
}