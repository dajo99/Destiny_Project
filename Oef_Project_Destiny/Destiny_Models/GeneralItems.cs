﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destiny_DAL;

namespace Destiny_Models
{
    public static class GeneralItems
    {
        public static List<string> ZeldzaamheidLijst { get; set; } = new List<string>() { "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
        public static List<string> ZeldzaamheidLijstVoorZoeken { get; set; } = new List<string>() { "All","Common", "Uncommon", "Rare", "Legendary", "Exotic" };

        public static int? ConversieToInt(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && int.TryParse(text, out int number))
            {
                return number;
            }
            return null;
        }
    }
}
