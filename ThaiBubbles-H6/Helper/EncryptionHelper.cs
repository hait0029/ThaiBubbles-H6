using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ThaiBubbles_H6.Helper
{
    public class EncryptionHelper
    {
        // Ensure the key and IV are Base64-encoded strings
        private static readonly byte[] Key = Convert.FromBase64String("MTIzNDU2Nzg5MDEyMzQ1Njc4OTAxMjM0NTY3ODkwMTI=");
        private static readonly byte[] IV = Convert.FromBase64String("MTIzNDU2Nzg5MDEyMzQ1Ng==");

        public static string Encrypt(string plainText)
        {
            try
            {
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
                            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        }

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
                // Log the cipherText before decryption
                Console.WriteLine($"Decrypting cipherText: {cipherText}");

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
                                var decryptedText = srDecrypt.ReadToEnd();
                                Console.WriteLine($"Decrypted text: {decryptedText}");
                                return decryptedText;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log detailed error info
                Console.WriteLine($"Decryption failed with error: {ex.Message}");
                throw new Exception("Decryption failed.", ex);
            }
        }

    }
}
