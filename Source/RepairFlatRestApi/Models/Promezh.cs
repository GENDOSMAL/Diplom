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
    
    public partial class Promezh
    {
        public System.Guid idColob { get; set; }
        public Nullable<System.Guid> idSubInf { get; set; }
    
        public virtual OurMaterials OurMaterials { get; set; }
        public virtual UpdateSubInformation UpdateSubInformation { get; set; }
        public virtual OurServices OurServices { get; set; }
        public virtual PremisesType PremisesType { get; set; }
        public virtual TypeOfContact TypeOfContact { get; set; }
        public virtual UpdateSubInformation UpdateSubInformation1 { get; set; }
        public virtual WorkerPosts WorkerPosts { get; set; }
    }
}
