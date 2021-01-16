using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Helpers
{
    public class AES
    {
        public static byte[] Key = new byte[] { 215, 210, 125, 32, 68, 139, 179, 190, 60, 187, 104, 236, 178, 179, 159, 132, 23, 93, 110, 121, 46, 192, 140, 231, 79, 191, 154, 164, 65, 207, 40, 109 };
        public static byte[] IV = new byte[] { 39, 131, 103, 245, 37, 195, 2, 41, 159, 247, 140, 209, 3, 39, 50, 248 };


        public static string Encrypt(string plainText)
        {
            return Base64.Base64Encode(EncryptToBytes(plainText));
        }

        public static byte[] EncryptToBytes(string plainText)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (var ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string Decrypt(string encryptedString)
        {
            return Decode(Base64.Base64Decode(encryptedString));
        }
        public static string Decode(byte[] cipherText)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
