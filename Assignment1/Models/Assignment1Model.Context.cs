﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<HouseSet> HouseSet { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<ImageSet> ImageSet { get; set; }
        public virtual DbSet<RateSet> RateSet { get; set; }
        public virtual DbSet<TransactionSet> TransactionSet { get; set; }
        public virtual DbSet<AgentSet> AgentSet { get; set; }
        public virtual DbSet<Events> Events { get; set; }
    }
}