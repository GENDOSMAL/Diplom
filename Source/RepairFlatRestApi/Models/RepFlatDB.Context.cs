﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RepairFlatDB : DbContext
    {
        public RepairFlatDB()
            : base("name=RepairFlatDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<AdressDescription> AdressDescription { get; set; }
        public virtual DbSet<ClientDetails> ClientDetails { get; set; }
        public virtual DbSet<ContactUpdate> ContactUpdate { get; set; }
        public virtual DbSet<DeletedSubStr> DeletedSubStr { get; set; }
        public virtual DbSet<EstabilismentPost> EstabilismentPost { get; set; }
        public virtual DbSet<LoginInformation> LoginInformation { get; set; }
        public virtual DbSet<MaterialsUpdate> MaterialsUpdate { get; set; }
        public virtual DbSet<OrderElementOfMeasurments> OrderElementOfMeasurments { get; set; }
        public virtual DbSet<OrderInformation> OrderInformation { get; set; }
        public virtual DbSet<OrderMaterial> OrderMaterial { get; set; }
        public virtual DbSet<OrderMeasurements> OrderMeasurements { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<OrderServises> OrderServises { get; set; }
        public virtual DbSet<OrderTasks> OrderTasks { get; set; }
        public virtual DbSet<OrderTasksState> OrderTasksState { get; set; }
        public virtual DbSet<OurMaterials> OurMaterials { get; set; }
        public virtual DbSet<OurServices> OurServices { get; set; }
        public virtual DbSet<PremisesType> PremisesType { get; set; }
        public virtual DbSet<PremisesUpdate> PremisesUpdate { get; set; }
        public virtual DbSet<ServicesUpdate> ServicesUpdate { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeOfContact> TypeOfContact { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserContact> UserContact { get; set; }
        public virtual DbSet<WorkerDetails> WorkerDetails { get; set; }
        public virtual DbSet<WorkerPosts> WorkerPosts { get; set; }
        public virtual DbSet<WorkersOperats> WorkersOperats { get; set; }
        public virtual DbSet<WorkersPayGive> WorkersPayGive { get; set; }
    }
}
