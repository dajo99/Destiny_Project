using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Destiny_DAL
{
   public static class FileOperations
    {
        public static void Foutloggen(Exception fout)
        {
            using (StreamWriter writer = new StreamWriter("foutenbestand.txt", true))
            {
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss:tt"));
                writer.WriteLine(fout.GetType().Name);
                writer.WriteLine(fout.Message);
                writer.WriteLine(fout.StackTrace);
                writer.WriteLine(new string('*', 100));
                writer.WriteLine();
            }
        }
    }
}
