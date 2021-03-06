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
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.Inventories = new HashSet<Inventory>();
            this.NpcSellItems = new HashSet<NpcSellItem>();
            this.RewardTables = new HashSet<RewardTable>();
        }
    
        public int id { get; set; }
        public string Naam { get; set; }
        public Nullable<int> Prijs { get; set; }
        public string Beschrijving { get; set; }
        public string Zeldzaamheid { get; set; }
        public Nullable<bool> Can_Be_Sold { get; set; }
    
        public virtual Armor Armor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NpcSellItem> NpcSellItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RewardTable> RewardTables { get; set; }
        public virtual SpecialItem SpecialItem { get; set; }
        public virtual Wapen Wapen { get; set; }
    }
}
