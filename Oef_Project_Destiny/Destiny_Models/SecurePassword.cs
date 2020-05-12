using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_Models
{
    public static class SecurePassword
    {
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
        }
    }
}

