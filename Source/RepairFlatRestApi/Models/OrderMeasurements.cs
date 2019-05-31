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
    
    public partial class OrderMeasurements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderMeasurements()
        {
            this.OrderElementOfMeasurments = new HashSet<OrderElementOfMeasurments>();
        }
    
        public System.Guid idMeasurements { get; set; }
        public Nullable<System.Guid> IdOrder { get; set; }
        public Nullable<System.Guid> idPremisesType { get; set; }
        public string Description { get; set; }
        public Nullable<double> Lenght { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Pwalls { get; set; }
        public Nullable<double> PCelling { get; set; }
        public Nullable<double> Swalls { get; set; }
        public Nullable<double> Sfloor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderElementOfMeasurments> OrderElementOfMeasurments { get; set; }
        public virtual OrderInformation OrderInformation { get; set; }
        public virtual PremisesType PremisesType { get; set; }
    }
}
