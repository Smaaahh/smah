﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace smaaahh_api
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class masterEntities : DbContext
    {
        public masterEntities()
            : base("name=masterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<PromotionCodes> PromotionCodes { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<RideRequests> RideRequests { get; set; }
        public virtual DbSet<Riders> Riders { get; set; }
        public virtual DbSet<Rides> Rides { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<MSreplication_options> MSreplication_options { get; set; }
        public virtual DbSet<spt_fallback_db> spt_fallback_db { get; set; }
        public virtual DbSet<spt_fallback_dev> spt_fallback_dev { get; set; }
        public virtual DbSet<spt_fallback_usg> spt_fallback_usg { get; set; }
        public virtual DbSet<spt_monitor> spt_monitor { get; set; }
    }
}
