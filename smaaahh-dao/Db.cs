using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_dao
{
    public class Db : DbContext
    {

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<RideRequest> RideRequests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PromotionCode> PromotionCodes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public Db() : base("smaaahh")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity <Ride> ()
            //.HasForeignKey(f => f.DriverRating)
            //.WillCascadeOnDelete(false);
        }
    }
}
