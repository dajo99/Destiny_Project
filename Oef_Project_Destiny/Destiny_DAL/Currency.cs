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
    
    public partial class Currency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Currency()
        {
            this.CurrencyDropTables = new HashSet<CurrencyDropTable>();
            this.CurrencyInventories = new HashSet<CurrencyInventory>();
            this.Npcs = new HashSet<Npc>();
        }
    
        public int id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrencyDropTable> CurrencyDropTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrencyInventory> CurrencyInventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Npc> Npcs { get; set; }
    }
}
