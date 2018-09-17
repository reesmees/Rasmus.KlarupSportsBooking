namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Addresses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Addresses()
        {
            Unions = new HashSet<Unions>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetName { get; set; }

        public int HouseNumber { get; set; }

        public int Floor { get; set; }

        public int ZipCode { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unions> Unions { get; set; }
    }
}
