namespace KtelProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KtelModels : DbContext
    {
        public KtelModels()
            : base("name=KtelModelsEntities")
        {
        }

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Capacity> Capacities { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Capacities)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Drivers)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bus>()
                .HasMany(e => e.Trips)
                .WithRequired(e => e.Bus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Capacity>()
                .Property(e => e.SpecificRouteID)
                .IsUnicode(false);

           // modelBuilder.Entity<Capacity>()
           //     .Property(e => e.RouteDay)
           //     .IsUnicode(false);

            modelBuilder.Entity<Capacity>()
                .Property(e => e.RouteTime)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Passenger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Passenger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.DayOfTrip)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.Departure)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.Destination)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.Trips)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .Property(e => e.TripTime)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<KtelProject.Models.SignUp> SignUps { get; set; }
    }
}
