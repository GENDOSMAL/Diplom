//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RepairFlatRestApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactUpdate
    {
        public System.Guid idContactUpdate { get; set; }
        public Nullable<System.Guid> idContact { get; set; }
        public Nullable<System.DateTime> DataOfUpdate { get; set; }
        public Nullable<System.Guid> idUser { get; set; }
    
        public virtual TypeOfContact TypeOfContact { get; set; }
    }
}
