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
    
    public partial class BrigateContent
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> idWorker { get; set; }
        public string WorkerRole { get; set; }
    
        public virtual BrigateSeparation BrigateSeparation { get; set; }
        public virtual WorkerDetails WorkerDetails { get; set; }
    }
}