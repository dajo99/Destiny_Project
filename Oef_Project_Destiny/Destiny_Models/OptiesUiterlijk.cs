using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destiny_DAL;

namespace Destiny_Models
{
    public static class OptiesUiterlijk
    {

        public static List<string> TattooOpties {
            get;
            set;
        }


        public static List<string> HaarOpties {
            get;
            set;
        }
        
        public static List<string> GezichtOpties
        {
            get;
            set;

        } = new List<string>() { "Jong", "Oud", "Krijger" };

        public static List<string> Gender
        {
            get;
            set;
        }
        //bij het opstarten gaat deze constructor automatisch de lijsten initialiseren en opvullen//
         static OptiesUiterlijk()
        {
            
            HaarOpties = new List<string>() { "Krullen", "Lang", "Kort" };
            Gender = new List<string>() { "Man", "Vrouw" };
            TattooOpties = new List<string>() { "Streep", "Geen marking", "Gezicht tattoo" } ;

        }
         
        
        
       
    }
}
