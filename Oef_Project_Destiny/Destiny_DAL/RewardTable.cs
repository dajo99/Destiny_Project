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
    
    public partial class RewardTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RewardTable()
        {
            this.CurrencyDropTable = new HashSet<CurrencyDropTable>();
            this.RewardTable1 = new HashSet<RewardTable>();
        }
    
        public int id { get; set; }
        public Nullable<int> MissionId { get; set; }
        public Nullable<int> EnemyId { get; set; }
        public int ItemId { get; set; }
        public int HoeveelheidItems { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrencyDropTable> CurrencyDropTable { get; set; }
        public virtual Item Item { get; set; }
        public virtual Mission Mission { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RewardTable> RewardTable1 { get; set; }
        public virtual RewardTable RewardTable2 { get; set; }
    }
}
