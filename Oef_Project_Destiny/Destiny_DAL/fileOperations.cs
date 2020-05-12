using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Destiny_DAL
{
   public static class fileOperations
    {
        public static void Foutloggen(Exception fout)
        {
            using (StreamWriter schrijver = new StreamWriter("foutenbestand.txt", true))
            {
                schrijver.WriteLine(DateTime.Now.ToString("HH:mm:ss:tt"));
                schrijver.WriteLine(fout.GetType().Name);
                schrijver.WriteLine(fout.Message);
                schrijver.WriteLine(fout.StackTrace);
                schrijver.WriteLine(new string('*', 100));
                schrijver.WriteLine();
            }
        }
    }
}
