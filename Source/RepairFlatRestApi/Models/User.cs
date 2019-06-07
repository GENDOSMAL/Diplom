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
            this.DeletedSubStr = new HashSet<DeletedSubStr>();
            this.TaskWorker = new HashSet<TaskWorker>();
            this.UpdateSubInformation = new HashSet<UpdateSubInformation>();
            this.UserContact = new HashSet<UserContact>();
        }
    
        public System.Guid idUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Pasport { get; set; }
        public Nullable<int> Female { get; set; }
        public Nullable<System.DateTime> BirstDay { get; set; }
        public string TypeOfUser { get; set; }
    
        public virtual ClientDetails ClientDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeletedSubStr> DeletedSubStr { get; set; }
        public virtual LoginInformation LoginInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskWorker> TaskWorker { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UpdateSubInformation> UpdateSubInformation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserContact> UserContact { get; set; }
        public virtual WorkerDetails WorkerDetails { get; set; }
    }
}
