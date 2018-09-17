namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservations()
        {
            Booking = new HashSet<Booking>();
        }

        public int ID { get; set; }

        public int UnionID { get; set; }

        public int ActivityID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ReservationLength { get; set; }

        public bool IsHandled { get; set; }

        public virtual Activities Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Booking { get; set; }

        public virtual Unions Unions { get; set; }
    }
}
