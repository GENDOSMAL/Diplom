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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.LoginingInformation1 = new HashSet<LoginingInformation>();
        }
    
        public int idUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Otchestv { get; set; }
        public Nullable<System.DateTime> Birstday { get; set; }
        public string TypeOfUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoginingInformation> LoginingInformation1 { get; set; }
    }
}
