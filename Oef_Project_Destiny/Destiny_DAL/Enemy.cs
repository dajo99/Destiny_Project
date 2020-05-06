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
    
    public partial class Enemy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enemy()
        {
            this.MapEnemies = new HashSet<MapEnemy>();
            this.RewardTables = new HashSet<RewardTable>();
        }
    
        public int id { get; set; }
        public string SoortEnemy { get; set; }
        public string Toughness { get; set; }
        public string Archtypes { get; set; }
        public Nullable<int> SchildDamageTypeId { get; set; }
        public int RasId { get; set; }
        public int WapenId { get; set; }
    
        public virtual Damagetype Damagetype { get; set; }
        public virtual Ras Ras { get; set; }
        public virtual Wapen Wapen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MapEnemy> MapEnemies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RewardTable> RewardTables { get; set; }
    }
}
