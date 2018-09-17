namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        public int ID { get; set; }

        public int AdministratorID { get; set; }

        public int ReservationID { get; set; }

        public TimeSpan StartTime { get; set; }

        public virtual Administrator Administrator { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
