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
    
    public partial class GranaatSubklasse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GranaatSubklasse()
        {
            this.GranaatAbilityDamages = new HashSet<GranaatAbilityDamage>();
        }
    
        public int id { get; set; }
        public int CharacterSubklasseId { get; set; }
        public string Naam { get; set; }
    
        public virtual CharacterSubklasse CharacterSubklasse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GranaatAbilityDamage> GranaatAbilityDamages { get; set; }
    }
}
