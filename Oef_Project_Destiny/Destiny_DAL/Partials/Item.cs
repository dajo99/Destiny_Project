using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destiny_Models;

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
                    return "Gelieve de naam in te vullen!" + Environment.NewLine;
                }
                if (columnName == "Zeldzaamheid" && string.IsNullOrWhiteSpace(Zeldzaamheid))
                {
                    return "Gelieve de zeldzaamheid in te geven!" + Environment.NewLine;
                }
                /* werkt niet!! Foutmelding als je dit probeert te inititialiseren bij partial klasse SpecialItem
                if (columnName == "Boost" && SpecialItem.Boost != null && SpecialItem.Boost < 0)
                {
                    return "Boost mag geen negatief getal zijn!" + Environment.NewLine;
                }
                if (columnName == "Durability" && SpecialItem.Durability != null && SpecialItem.Durability < 0)
                {
                    return "Durability mag geen negatief getal zijn!" + Environment.NewLine;
                }*/
                return "";
            }
        }
        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   Naam == item.Naam &&
                   Zeldzaamheid == "Exotic";
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
