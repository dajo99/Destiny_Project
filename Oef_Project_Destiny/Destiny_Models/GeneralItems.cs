using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destiny_DAL;

namespace Destiny_Models
{
    public static class GeneralItems
    {
        public static List<Item> Items { get; set; }
        public static List<string> ZeldzaamheidLijst { get; set; }


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
