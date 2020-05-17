using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public static class User
    {
        public static Account Acc { get; set; }
        public static Character Character { get; set; }
        
        public static List<Item> Items { get; set; }

    }
    
}
