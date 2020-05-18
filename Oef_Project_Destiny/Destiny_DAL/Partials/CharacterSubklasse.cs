using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class CharacterSubklasse
    {
        public override bool Equals(object obj)
        {
            return obj is CharacterSubklasse subklasse &&
                   id == subklasse.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return Naam.ToString();
        }

    }
}
