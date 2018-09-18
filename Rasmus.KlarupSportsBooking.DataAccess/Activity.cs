namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            RecurringReservations = new HashSet<RecurringReservation>();
            Reservations = new HashSet<Reservation>();
        }

        private string activityName;
        private string hallUsage;

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ActivityName {
            get { return activityName; }
            set {
                if (value == null)
                {
                    throw new ArgumentNullException("ActivityName må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ActivityName må ikke være udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("ActivityName må ikke være længere end 100 karakterer");
                }
                else
                {
                    activityName = value;
                }
            }
        }

        [Required]
        [StringLength(50)]
        public string HallUsage
        {
            get { return hallUsage; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("HallUsage må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("HallUsage må ikke være udelukkende whitespace");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException("HallUsage må ikke være længere end 100 karakterer");
                }
                else
                {
                    hallUsage = value;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecurringReservation> RecurringReservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
