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
    
    public partial class OrderInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderInformation()
        {
            this.OrderMaterial = new HashSet<OrderMaterial>();
            this.OrderMeasurements = new HashSet<OrderMeasurements>();
            this.OrderPayment = new HashSet<OrderPayment>();
            this.OrderServises = new HashSet<OrderServises>();
            this.OrderTasks = new HashSet<OrderTasks>();
        }
    
        public System.Guid? IdOrder { get; set; }
        public Nullable<System.Guid> IdAdress { get; set; }
        public Nullable<System.Guid> IdWorkerMake { get; set; }
        public Nullable<System.Guid> idClient { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> AllSumma { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> IdColoboration { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<System.Guid> MainContactID { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
    
        public virtual AdressDescription AdressDescription { get; set; }
        public virtual ClientDetails ClientDetails { get; set; }
        public virtual ColoborationOfBrigade ColoborationOfBrigade { get; set; }
        public virtual WorkerDetails WorkerDetails { get; set; }
        public virtual WorkerOrderInformation WorkerOrderInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMaterial> OrderMaterial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMeasurements> OrderMeasurements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayment> OrderPayment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderServises> OrderServises { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTasks> OrderTasks { get; set; }
        public virtual UserContact UserContact { get; set; }
    }
}
