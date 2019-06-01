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
    
    public partial class OrderTasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderTasks()
        {
            this.OrderTasksState = new HashSet<OrderTasksState>();
            this.TaskMaterials = new HashSet<TaskMaterials>();
            this.TaskServis = new HashSet<TaskServis>();
            this.OrderWorker = new HashSet<OrderWorker>();
        }
    
        public System.Guid IdTask { get; set; }
        public Nullable<System.Guid> IdOrder { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DeadEnd { get; set; }
        public Nullable<decimal> SummaAboutTask { get; set; }
        public Nullable<System.Guid> idBrigade { get; set; }
    
        public virtual OrderInformation OrderInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTasksState> OrderTasksState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskMaterials> TaskMaterials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskServis> TaskServis { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderWorker> OrderWorker { get; set; }
    }
}
