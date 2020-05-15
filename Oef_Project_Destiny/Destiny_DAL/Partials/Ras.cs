using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class Ras
    {
        public override bool Equals(object obj)
        {
            return obj is Ras ras &&
                   Naam == ras.Naam;
        }

        public override int GetHashCode()
        {
            return -1386946022 + EqualityComparer<string>.Default.GetHashCode(Naam);
        }

        public override string ToString()
        {
            return Naam;
        }

    }
}
