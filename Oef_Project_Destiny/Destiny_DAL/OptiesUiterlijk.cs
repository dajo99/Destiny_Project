using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
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

        }
        
        public static List<string> Gender
        {
            get;
            set;
        }
         static OptiesUiterlijk()
        {
            GezichtOpties = new List<string>();
            HaarOpties = new List<string>();
            Gender = new List<string>();
            TattooOpties = new List<string>();
            OptiesUiterlijk.TattooOpties.Add("streep");
            OptiesUiterlijk.TattooOpties.Add("geen marking");
            OptiesUiterlijk.TattooOpties.Add("gezicht tattoo");

            OptiesUiterlijk.HaarOpties.Add("krullen");
            OptiesUiterlijk.HaarOpties.Add("lang");
            OptiesUiterlijk.HaarOpties.Add("kort");

            OptiesUiterlijk.GezichtOpties.Add("jong");
            OptiesUiterlijk.GezichtOpties.Add("oud");
            OptiesUiterlijk.GezichtOpties.Add("krijger");

            OptiesUiterlijk.Gender.Add("Man");
            OptiesUiterlijk.Gender.Add("Vrouw");

        }
         
        
        
       
    }
}
