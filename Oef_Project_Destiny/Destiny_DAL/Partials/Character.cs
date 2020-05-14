using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny_DAL
{
    public partial class Character
    {
        public override string ToString()
        {
            return Ras.ToString() + Level + HeadOption + Face + Marking + CharacterKlasse.ToString();
        }
    }
}
