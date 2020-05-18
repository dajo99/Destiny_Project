using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destiny_DAL;

namespace Destiny_Models
{
    public static class User
    {
        public static Account Acc { get; set; }
        public static Character Character { get; set; }
        public static CharacterKlasse CharacterKlasse { get; set; }
        public static CharacterSubklasse CharacterSubKlasse { get; set; }
    }
    
}
