namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KlarupSportsBookingContext : DbContext
    {
        public KlarupSportsBookingContext()
            : base("name=KlarupSportsBookingContext")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<E_mails> E_mails { get; set; }
        public virtual DbSet<RecurringBooking> RecurringBookings { get; set; }
        public virtual DbSet<RecurringReservation> RecurringReservations { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<UnionLeader> UnionLeaders { get; set; }
        public virtual DbSet<UnionLogin> UnionLogins { get; set; }
        public virtual DbSet<Union> Unions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .Property(e => e.ActivityName)
                .IsUnicode(false);

            modelBuilder.Entity<Activity>()
                .Property(e => e.HallUsage)
                .IsUnicode(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.RecurringReservations)
                .WithRequired(e => e.Activity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Activity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.StreetName)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Unions)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Administrator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.RecurringBookings)
                .WithRequired(e => e.Administrator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<E_mails>()
                .Property(e => e.E_mailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<E_mails>()
                .HasMany(e => e.Administrators)
                .WithRequired(e => e.E_mails)
                .HasForeignKey(e => e.E_mailID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<E_mails>()
                .HasMany(e => e.Unions)
                .WithRequired(e => e.E_mails)
                .HasForeignKey(e => e.E_mailID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<RecurringReservation>()
            //    .Property(e => e.Weekday)
            //    .IsUnicode(false);

            modelBuilder.Entity<RecurringReservation>()
                .HasMany(e => e.RecurringBookings)
                .WithRequired(e => e.RecurringReservation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Reservation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UnionLeader>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UnionLeader>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<UnionLogin>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<UnionLogin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Union>()
                .Property(e => e.UnionName)
                .IsUnicode(false);

            modelBuilder.Entity<Union>()
                .HasMany(e => e.RecurringReservations)
                .WithRequired(e => e.Union)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Union>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Union)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Union>()
                .HasMany(e => e.UnionLeaders)
                .WithRequired(e => e.Union)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Union>()
                .HasMany(e => e.UnionLogins)
                .WithRequired(e => e.Union)
                .WillCascadeOnDelete(false);
        }
    }
}
