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

        private DateTime date;
        private int reservationLength;

        public int ID { get; set; }

        public int UnionID { get; set; }

        public int ActivityID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date {
            get { return date; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Date må ikke være null");
                }
                else
                {
                    date = value;
                }
            }
        }

        public int ReservationLength {
            get { return reservationLength; }
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
                    reservationLength = value;
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
