namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecurringBooking")]
    public partial class RecurringBooking
    {
        public int ID { get; set; }

        public int AdministratorID { get; set; }

        public int RecurringReservationID { get; set; }

        public virtual Administrators Administrators { get; set; }

        public virtual RecurringReservations RecurringReservations { get; set; }
    }
}
