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
            //array van bytes (tussen 0 en 255) opvullen met een code van chars in bytes
            byte[] data = UTF8Encoding.UTF8.GetBytes(encr);
            //provider gebruiken om met het SHA256 algoritme te werken
            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                //Converteert de invoerreeks naar een bytearray  + Berekent de hash-waarde
                byte[] keys = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes("892C8E496E1E33355E858B327B@C238A939B7601E96159F9E9CAD05@19BA5055CD"));
                //Met behulp van cryptografisch interface, symmetrische encryptie mogelijk maken
                    //Property Key = Hiermee wordt de symmetrische sleutel opgehaald of ingesteld die wordt gebruikt voor codering en decodering.
                    //Property Mode = Specificeert de blokcijfermodus die moet worden gebruikt voor codering.
                    //Property Mode = Manier om ervoor te zorgen om volledige aantal bytes op te vullen als deze korter is.
                using (AesCryptoServiceProvider tripdes = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.ISO10126 })
                {
                    //Een symmetrisch encryptor object aanmaken 
                    ICryptoTransform transform = tripdes.CreateEncryptor();
                    //Transformeren van het opgegeven gebied van de opgegeven bytearray.
                        //Eerste parameter = De invoer in array bytes.
                        //Tweede parameter = De offset (afstand beginpunt-bepaald punt) in de bytearray om gegevens te gaan gebruiken.
                        //Derde parameter = Aantal bytes in de byte array om gegevens te gebruiken.
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    //Teruggeven van een 64 bit string
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public static string DecryptString(string decr)
        {
            try
            {
                //Converteren 64 bits naar 8 bits;
                byte[] data = Convert.FromBase64String(decr);
                using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
                {
                    byte[] keys = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes("892C8E496E1E33355E858B327B@C238A939B7601E96159F9E9CAD05@19BA5055CD"));
                    using (AesCryptoServiceProvider tripdes = new AesCryptoServiceProvider { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.ISO10126 })
                    {//Een symmetrisch decryptor object aanmaken 
                        ICryptoTransform transform = tripdes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        //byte array omzetten in string
                        return UTF8Encoding.UTF8.GetString(results); 
                    }
                }
            }
            catch (FormatException)
            {  
                return "";
            }
            
        }
    }
}

