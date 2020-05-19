using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class SpecialItemCategorie
    {
        public override bool Equals(object obj)
        {
            return obj is SpecialItemCategorie categorie &&
                   id == categorie.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}
