using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destiny_Models;
namespace Destiny_DAL
{
   public partial class CharacterKlasse
    {
        public override bool Equals(object obj)
        {
            return obj is CharacterKlasse klasse &&
                   Naam == klasse.Naam;
        }

        public override int GetHashCode()
        {
            return -1386946022 + EqualityComparer<string>.Default.GetHashCode(Naam);
        }

        public override string ToString()
        {
            return Naam.ToString();
        }
    }
}
