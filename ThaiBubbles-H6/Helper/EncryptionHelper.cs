using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ThaiBubbles_H6.Helper
{
    public class EncryptionHelper
    {
        // Ensure your key is exactly 32 bytes for AES-256 and your IV is exactly 16 bytes
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes

        public static string Encrypt(string plainText)
        {
            try
            {
                // Convert plainText to bytes
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            // Write the byte array directly to the stream
                            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        }

                        // Return the base64 string of the encrypted data
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption failed with error: " + ex.Message);
                throw new Exception("Encryption failed.", ex);
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                // Convert base64 string to byte array
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted data from the stream
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decryption failed with error: " + ex.Message);
                throw new Exception("Decryption failed.", ex);
            }
        }
    }
}
