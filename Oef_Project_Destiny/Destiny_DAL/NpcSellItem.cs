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
    
    public partial class NpcSellItem
    {
        public int id { get; set; }
        public int NpcId { get; set; }
        public int ItemId { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Npc Npc { get; set; }
    }
}