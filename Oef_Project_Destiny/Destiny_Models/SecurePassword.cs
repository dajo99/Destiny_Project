using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_Models
{
    public static class SecurePassword
    {
        /* Oude manier
            public static string DecryptString(string encrString)
            {
                byte[] b;
                string decrypted;

                try
                {
                    b = Convert.FromBase64String(encrString);
                    decrypted = Encoding.ASCII.GetString(b);
                }
                catch (FormatException fe)
                {
                    decrypted = "";
                }
                return decrypted;
            }

            public static string EncryptString(string decryptString)
            {
                byte[] b = Encoding.ASCII.GetBytes(decryptString);
                string encrypted = Convert.ToBase64String(b);
                return encrypted;
            }*/
        public static string EncryptString(string encr)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(encr);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("892C8E496E1E33355E858B327B@C238A939B7601E96159F9E9CAD05@19BA5055CD"));
                using (TripleDESCryptoServiceProvider tripdes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripdes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public static string DecryptString(string decr)
        {
            try
            {
                byte[] data = Convert.FromBase64String(decr);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("892C8E496E1E33355E858B327B@C238A939B7601E96159F9E9CAD05@19BA5055CD"));
                    using (TripleDESCryptoServiceProvider tripdes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripdes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return UTF8Encoding.UTF8.GetString(results); ;
                    }
                }
            }
            catch (FormatException fe)
            {
                return null;
            }
            
        }
    }
}

