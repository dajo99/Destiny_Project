using Destiny_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_Models
{
    public static class GeneralWapens
    {
        public static List<Wapen> Wapens { get; set; }
        public static List<string> ZeldzaamheidLijst { get; set; }

        public static List<string> Categorielijst { get; set; }

        public static List<string> Damagetypelijst { get; set; }

        public static int ConversieToInt(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && int.TryParse(text, out int number))
            {
                return number;
            }
            return 0;
        }
    }
}
