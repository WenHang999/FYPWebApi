﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication6.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class fypmobileEntities : DbContext
    {
        public fypmobileEntities()
            : base("name=fypmobileEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<ballotResult> ballotResults { get; set; }
        public virtual DbSet<booking> bookings { get; set; }
        public virtual DbSet<eventTicket> eventTickets { get; set; }
        public virtual DbSet<notification> notifications { get; set; }
        public virtual DbSet<rating> ratings { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
    }
}
