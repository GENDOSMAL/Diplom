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
    
    public partial class UserContact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserContact()
        {
            this.OrderInformation = new HashSet<OrderInformation>();
        }
    
        public System.Guid id { get; set; }
        public System.Guid idUser { get; set; }
        public Nullable<System.Guid> idType { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateAdd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInformation> OrderInformation { get; set; }
        public virtual TypeOfContact TypeOfContact { get; set; }
        public virtual User User { get; set; }
    }
}
