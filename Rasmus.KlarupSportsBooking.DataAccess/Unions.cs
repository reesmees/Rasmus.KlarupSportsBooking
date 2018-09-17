namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Unions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unions()
        {
            RecurringReservations = new HashSet<RecurringReservations>();
            Reservations = new HashSet<Reservations>();
            UnionLeaders = new HashSet<UnionLeaders>();
        }

        public int ID { get; set; }

        [Column("E-mailID")]
        public int E_mailID { get; set; }

        public int AddressID { get; set; }

        [Required]
        [StringLength(255)]
        public string UnionName { get; set; }

        public virtual Addresses Addresses { get; set; }

        public virtual E_mails E_mails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecurringReservations> RecurringReservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnionLeaders> UnionLeaders { get; set; }
    }
}
