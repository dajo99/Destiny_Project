using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class Item : Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Naam" && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Gelieve de naam in te vullen!";
                }
                if (columnName == "Zeldzaamheid" && string.IsNullOrWhiteSpace(Zeldzaamheid))
                {
                    return "Gelieve de zeldzaamheid in te geven!";
                }
                return "";
            }
        }
        public override bool Equals(object obj)
        {
            //Een exotic item met dezelfde naam kan maar 1x in wapens en/of armor en/of special items zitten. 
            //Bijvoorbeeld een exotic wapen met naam x, en een exotic armorstuk met dezelfde naam (x) kan dus niet!
            return obj is Item item &&
                   Naam == item.Naam &&
                   Zeldzaamheid == item.Zeldzaamheid &&
                   item.Zeldzaamheid.Contains("Exotic");
        }
        public override int GetHashCode()
        {
            int hashCode = 1305993403;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Naam);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Zeldzaamheid);
            return hashCode;
        }   
    }
}
