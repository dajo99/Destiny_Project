//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Destiny_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GranaatAbilityDamage
    {
        public int id { get; set; }
        public int DamagetypeId { get; set; }
        public int Damage { get; set; }
        public Nullable<int> DamageTickRate { get; set; }
        public int GranaatSubklasseId { get; set; }
    
        public virtual Damagetype Damagetype { get; set; }
        public virtual GranaatSubklasse GranaatSubklasse { get; set; }
    }
}
