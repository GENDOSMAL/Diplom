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
    }
}
