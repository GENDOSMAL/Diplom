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
    
    public partial class OurMaterials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OurMaterials()
        {
            this.MaterialsUpdate = new HashSet<MaterialsUpdate>();
            this.OrderMaterial = new HashSet<OrderMaterial>();
        }
    
        public System.Guid idMaterials { get; set; }
        public string NameOfMaterial { get; set; }
        public string UnitOfMeasue { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string Description { get; set; }
        public string TypeOfMaterial { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialsUpdate> MaterialsUpdate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMaterial> OrderMaterial { get; set; }
    }
}