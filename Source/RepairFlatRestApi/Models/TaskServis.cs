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
    
    public partial class TaskServis
    {
        public System.Guid idOfCompare { get; set; }
        public Nullable<System.Guid> idTask { get; set; }
        public Nullable<System.Guid> idOrderServis { get; set; }
    
        public virtual OrderServises OrderServises { get; set; }
        public virtual OrderTasks OrderTasks { get; set; }
    }
}