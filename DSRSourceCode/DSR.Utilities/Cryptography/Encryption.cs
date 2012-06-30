using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DSR.Utilities.Cryptography
{
    public static class Encryption
    {
        #region TripleDES Encryption

        /// <summary>
        /// Generates encryption key using TripleDES algorithm.
        /// </summary>
        /// <returns>TripleDES encryption key.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GenerateTripleDESEncryptionKey()
        {
            string keyVal = string.Empty;

            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            keyVal = Convert.ToBase64String(provider.Key);

            return keyVal;
        }

        /// <summary>
        /// Generates initialization vector using TripleDES algorithm.
        /// </summary>
        /// <returns>TripleDESEncryption initialization Vector.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GenerateTripleDESEncryptionInitVector()
        {
            string initVector = string.Empty;

            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            initVector = Convert.ToBase64String(provider.IV);

            return initVector;
        }

        /// <summary>
        /// Get the original key used for encrption/decryption.
        /// </summary>
        /// <returns>Encryption key.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        private static string GetTripleDESEncryptionKey()
        {
            return "ZBknC5U2e5RT6u4kiwUi3bLAdN2hRrEP"; //TODO: put config or system registry
        }

        /// <summary>
        /// Get the original Initialization Vector used for encrption/decryption.
        /// </summary>
        /// <returns>Init Vector</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetTripleDESEncryptionInitVector()
        {
            return "Saaa+FEOnWw="; //TODO: put config or system registry
        }


        /// <summary>
        /// Encrypt using TripleDES algorithm.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="initVector">Encryption initialization vector.</param>
        /// <returns>Encrypted string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        private static string EncryptUsingTripleDES(string stringToEncrypt, string key, string initVector)
        {
            string encryptedString = string.Empty;

            byte[] rgbKey = Convert.FromBase64String(key);
            byte[] rgbIV = Convert.FromBase64String(initVector);
            byte[] rgbPlainText = Encoding.ASCII.GetBytes(stringToEncrypt);

            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform encryptor = provider.CreateEncryptor(rgbKey, rgbIV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(rgbPlainText, 0, rgbPlainText.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    encryptedString = Convert.ToBase64String(ms.ToArray());
                }
            }

            return encryptedString;
        }

        /// <summary>
        /// Decrypt using TripleDES algorithm.
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypted.</param>
        /// <param name="key">Decryption key.</param>
        /// <param name="initVector">Decryption initialization vector.</param>
        /// <returns>Decrypted string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        private static string DecryptUsingTripleDES(string stringToDecrypt, string key, string initVector)
        {
            string decryptedString = string.Empty;

            Byte[] rgbKey = Convert.FromBase64String(key);
            Byte[] rgbIV = Convert.FromBase64String(initVector);
            Byte[] rgbEncryptedText = Convert.FromBase64String(stringToDecrypt);

            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform decryptor = provider.CreateDecryptor(rgbKey, rgbIV);

                using (MemoryStream msCipherText = new MemoryStream(rgbEncryptedText))
                {
                    Byte[] rgbPlainText = new Byte[msCipherText.Length];

                    using (CryptoStream cryptoStream = new CryptoStream(msCipherText, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.Read(rgbPlainText, 0, Convert.ToInt32(msCipherText.Length));
                    }

                    decryptedString = Encoding.ASCII.GetString(rgbPlainText);
                    decryptedString = decryptedString.Replace("\0", "");
                }
            }

            return decryptedString;
        }

        /// <summary>
        /// Encrypt using TripleDES algorithm, uses original system key and initialization vector.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <returns>Encrypted string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string Encrypt(string stringToEncrypt)
        {
            string key = string.Empty;
            string initVector = string.Empty;
            string encryptedString = string.Empty;

            key = GetTripleDESEncryptionKey();
            initVector = GetTripleDESEncryptionInitVector();
            encryptedString = EncryptUsingTripleDES(stringToEncrypt, key, initVector);

            return encryptedString;
        }


        /// <summary>
        /// Encrypt using TripleDES algorithm.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="initVector">Encryption initialization vector.</param>
        /// <returns>Encrypted string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string Encrypt(string stringToEncrypt, string key, string initVector)
        {
            string encryptedString = string.Empty;

            encryptedString = EncryptUsingTripleDES(stringToEncrypt, key, initVector);

            return encryptedString;
        }

        /// <summary>
        /// Decrypt using TripleDES algorithm, uses original system key and initialization vector.
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypted.</param>
        /// <returns>Decrypted string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string Decrypt(string stringToDecrypt)
        {
            string key = string.Empty;
            string initVector = string.Empty;
            string decryptedString = string.Empty;

            key = GetTripleDESEncryptionKey();
            initVector = GetTripleDESEncryptionInitVector();
            decryptedString = DecryptUsingTripleDES(stringToDecrypt, key, initVector);

            return decryptedString;
        }

        /// <summary>
        /// Decrypt using TripleDES algorithm.
        /// </summary>
        /// <param name="stringToDecrypt">String to be decrypted.</param>
        /// <param name="key">Decryption key.</param>
        /// <param name="initVector">Decryption initialization vector.</param>
        /// <returns>Decrypted string.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string Decrypt(string stringToDecrypt, string key, string initVector)
        {
            string decryptedString = string.Empty;

            decryptedString = DecryptUsingTripleDES(stringToDecrypt, key, initVector);

            return decryptedString;
        }

        #endregion

        #region MD5 Hashing

        /// <summary>
        /// Generates Md5 hash.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <returns>The <see cref="System.String"/> object containing Md5 hash.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetMd5Hash(string stringToEncrypt)
        {
            byte[] hashedDataBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            StringBuilder sBuilder = new StringBuilder();

            using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
            {
                hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(stringToEncrypt));

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < hashedDataBytes.Length; i++)
                {
                    sBuilder.Append(hashedDataBytes[i].ToString("x2"));
                }
            }

            return sBuilder.ToString();
        }

        #endregion

        #region SHA1 Hashing

        /// <summary>
        /// Generates SHA1 hash.
        /// </summary>
        /// <param name="stringToEncrypt">String to be encrypted.</param>
        /// <returns>The <see cref="System.String"/> object containing SHA1 hash.</returns>
        /// <createdby>Amit Kumar Chandra</createdby>
        /// <createddate>30/06/2012</createddate>
        public static string GetSHA1Hash(string stringToEncrypt)
        {
            byte[] hashedDataBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            StringBuilder sBuilder = new StringBuilder();

            using (SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider())
            {
                hashedDataBytes = sha1Hasher.ComputeHash(encoder.GetBytes(stringToEncrypt));

                for (int i = 0; i < hashedDataBytes.Length; i++)
                {
                    sBuilder.Append(hashedDataBytes[i].ToString("x2"));
                }
            }

            return sBuilder.ToString();
        }

        #endregion
    }
}
