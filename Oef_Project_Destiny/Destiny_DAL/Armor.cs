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
    
    public partial class Armor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Armor()
        {
            this.PerksArmor = new HashSet<PerkArmor>();
        }
    
        public int id { get; set; }
        public Nullable<int> Mobility { get; set; }
        public Nullable<int> Resilience { get; set; }
        public Nullable<int> Recovery { get; set; }
        public Nullable<int> Discipline { get; set; }
        public Nullable<int> Intellect { get; set; }
        public Nullable<int> Strength { get; set; }
        public Nullable<int> MaxPerks { get; set; }
        public string ArmorSlot { get; set; }
        public int LightAmount { get; set; }
    
        public virtual Item Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerkArmor> PerksArmor { get; set; }
    }
}
