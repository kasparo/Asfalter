//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asfalter.Engine.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class unit
    {
        public unit()
        {
            this.unit_record = new HashSet<unit_record>();
        }
    
        public int idUnit { get; set; }
        public string systemId { get; set; }
        public System.DateTime created { get; set; }
        public Nullable<System.DateTime> lastActivity { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<unit_record> unit_record { get; set; }
    }
}
