using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class Locatie: Basisklasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Naam" && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Naam is een verplicht veld!";
                }
                if (columnName == "RestrictedArea" && string.IsNullOrEmpty(RestrictedArea.ToString()))
                {
                    return "Restricted Area is een verplicht veld!";
                }
                return "";
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Locatie locatie &&
                   Naam == locatie.Naam;
        }

        public override int GetHashCode()
        {
            return -1386946022 + EqualityComparer<string>.Default.GetHashCode(Naam);
        }
    }
}
