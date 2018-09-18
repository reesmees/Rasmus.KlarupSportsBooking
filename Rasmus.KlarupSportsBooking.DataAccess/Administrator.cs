namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Administrator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administrator()
        {
            Bookings = new HashSet<Booking>();
            RecurringBookings = new HashSet<RecurringBooking>();
        }

        public int ID { get; set; }

        [Column("E-mailID")]
        public int E_mailID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name
        {
            get { return Name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name må ikke være udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("Name må ikke være længere end 100 karakterer");
                }
                else
                {
                    Name = value;
                }
            }
        }

        [Required]
        [StringLength(100)]
        public string Password
        {
            get { return Password; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Password må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password må ikke være udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("Password må ikke være længere end 100 karakterer");
                }
                else
                {
                    Password = value;
                }
            }
        }

        public virtual E_mails E_mails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecurringBooking> RecurringBookings { get; set; }
    }
}
