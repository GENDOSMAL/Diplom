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
    
    public partial class EstabilismentPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstabilismentPost()
        {
            this.WorkersOperats = new HashSet<WorkersOperats>();
        }
    
        public System.Guid idEstabilisment { get; set; }
        public Nullable<System.Guid> idWorker { get; set; }
        public Nullable<System.Guid> idPost { get; set; }
        public Nullable<decimal> Salary { get; set; }
    
        public virtual WorkerDetails WorkerDetails { get; set; }
        public virtual WorkerPosts WorkerPosts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkersOperats> WorkersOperats { get; set; }
    }
}
