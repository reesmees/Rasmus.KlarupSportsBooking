namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Union
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Union()
        {
            RecurringReservations = new HashSet<RecurringReservation>();
            Reservations = new HashSet<Reservation>();
            UnionLeaders = new HashSet<UnionLeader>();
            UnionLogins = new HashSet<UnionLogin>();
        }

        private string unionName;

        public int ID { get; set; }

        [Column("E-mailID")]
        public int E_mailID { get; set; }

        public int AddressID { get; set; }

        [Required]
        [StringLength(255)]
        public string UnionName
        {
            get { return unionName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("UnionName må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("UnionName må ikke være udelukkende whitespace");
                }
                else if (value.Length > 255)
                {
                    throw new ArgumentException("UnionName må ikke være længere end 255 karakterer");
                }
                else
                {
                    unionName = value;
                }
            }
        }

        public virtual Address Address { get; set; }

        public virtual E_mails E_mails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecurringReservation> RecurringReservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnionLeader> UnionLeaders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnionLogin> UnionLogins { get; set; }
    }
}
