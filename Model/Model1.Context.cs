﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IPEntities : DbContext
    {
        public IPEntities()
            : base("name=IPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<IpPrice> IpPrice { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<VersionInfo> VersionInfo { get; set; }
        public virtual DbSet<VPSInfo> VPSInfo { get; set; }
        public virtual DbSet<NodeInfo> NodeInfo { get; set; }
        public virtual DbSet<TaskRule> TaskRule { get; set; }
        public virtual DbSet<CheckRule> CheckRule { get; set; }
    }
}