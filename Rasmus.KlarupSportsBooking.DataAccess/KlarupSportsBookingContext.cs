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

        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Administrators> Administrators { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<E_mails> E_mails { get; set; }
        public virtual DbSet<RecurringBooking> RecurringBooking { get; set; }
        public virtual DbSet<RecurringReservations> RecurringReservations { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<UnionLeaders> UnionLeaders { get; set; }
        public virtual DbSet<Unions> Unions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activities>()
                .Property(e => e.ActivityName)
                .IsUnicode(false);

            modelBuilder.Entity<Activities>()
                .Property(e => e.HallUsage)
                .IsUnicode(false);

            modelBuilder.Entity<Activities>()
                .HasMany(e => e.RecurringReservations)
                .WithRequired(e => e.Activities)
                .HasForeignKey(e => e.ActivityID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activities>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Activities)
                .HasForeignKey(e => e.ActivityID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.StreetName)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .HasMany(e => e.Unions)
                .WithRequired(e => e.Addresses)
                .HasForeignKey(e => e.AddressID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrators>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Administrators>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Administrators>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Administrators)
                .HasForeignKey(e => e.AdministratorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrators>()
                .HasMany(e => e.RecurringBooking)
                .WithRequired(e => e.Administrators)
                .HasForeignKey(e => e.AdministratorID)
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

            modelBuilder.Entity<RecurringReservations>()
                .Property(e => e.Weekday)
                .IsUnicode(false);

            modelBuilder.Entity<RecurringReservations>()
                .HasMany(e => e.RecurringBooking)
                .WithRequired(e => e.RecurringReservations)
                .HasForeignKey(e => e.RecurringReservationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservations>()
                .HasMany(e => e.Booking)
                .WithRequired(e => e.Reservations)
                .HasForeignKey(e => e.ReservationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UnionLeaders>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UnionLeaders>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Unions>()
                .Property(e => e.UnionName)
                .IsUnicode(false);

            modelBuilder.Entity<Unions>()
                .HasMany(e => e.RecurringReservations)
                .WithRequired(e => e.Unions)
                .HasForeignKey(e => e.UnionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unions>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Unions)
                .HasForeignKey(e => e.UnionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unions>()
                .HasMany(e => e.UnionLeaders)
                .WithRequired(e => e.Unions)
                .HasForeignKey(e => e.UnionID)
                .WillCascadeOnDelete(false);
        }
    }
}
