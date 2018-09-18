namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ID { get; set; }

        public int UnionID { get; set; }

        public int ActivityID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date {
            get { return Date; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Date må ikke være null");
                }
                else if (value < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException("Du kan ikke reservere i fortiden");
                }
                else
                {
                    Date = value;
                }
            }
        }

        public int ReservationLength {
            get { return ReservationLength; }
            set
            {
                if (value < 30)
                {
                    throw new ArgumentOutOfRangeException("Du kan ikke reservere en tid på under 30 minutter");
                }
                else if (value > 300)
                {
                    throw new ArgumentOutOfRangeException("Du kan ikke reservere en tid på over 5 timer");
                }
                else
                {
                    ReservationLength = value;
                }
            }
        }

        public bool IsHandled { get; set; }

        public virtual Activity Activity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Union Union { get; set; }
    }
}
