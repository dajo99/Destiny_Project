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
    
    public partial class CharacterInventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CharacterInventory()
        {
            this.CurrencyInventories = new HashSet<CurrencyInventory>();
        }
    
        public int id { get; set; }
        public int ItemId { get; set; }
    
        public virtual Character Character { get; set; }
        public virtual Item Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrencyInventory> CurrencyInventories { get; set; }
    }
}
