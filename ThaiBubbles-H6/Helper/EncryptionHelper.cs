namespace ThaiBubbles_H6.Helper
{
    public class EncryptionHelper
    {
        // Ensure the key and IV are Base64-encoded strings
        // IV is an protection layer for cipher text
        // base64 is a binary-to-text encoding scheme that represents binary data
        private static readonly byte[] Key = Convert.FromBase64String("MTIzNDU2Nzg5MDEyMzQ1Njc4OTAxMjM0NTY3ODkwMTI=");
        private static readonly byte[] IV = Convert.FromBase64String("MTIzNDU2Nzg5MDEyMzQ1Ng==");

        public static string Encrypt(string plainText)
        {
            try
            {
                // convert the plain text to bytes array using UTF8 encoding
                // UTF-8 represents text in bytes so computer can understand
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // Create an Aes object/system
                using (Aes aesAlg = Aes.Create())
                {
                    // Key is like a password to encrypt and decrypt the data
                    aesAlg.Key = Key;
                    // IV(Initialzation vector) is an additional protection layer for cipher text
                    aesAlg.IV = IV;
                    // PaddingMode is used to fill the block size of the cipher text
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // Create an encryptor object that combines Key and IV to make bytes and scramble them
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create a memory stream to store the encrypted data
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        // Create a CryptoStream object to write the encrypted data to the MemoryStream
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        }
                        // Convert the encrypted data from MemoryStream to Base64 string
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
                // shows the encrypted text in base64 format
                Console.WriteLine($"Decrypting cipherText: {cipherText}");

                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    //decryptor object that combines Key and IV to make bytes and unscramble them to their original form
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    //decrypt the data 
                    using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                    {
                        // Create a CryptoStream object to read the decrypted data from the MemoryStream and turn them to its original form
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
