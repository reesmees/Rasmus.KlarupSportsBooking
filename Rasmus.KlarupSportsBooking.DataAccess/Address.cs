namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            Unions = new HashSet<Union>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetName
        {
            get { return StreetName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("StreetName må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("StreetName må ikke være udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("StreetName må ikke være længere end 100 karakterer");
                }
                else
                {
                    StreetName = value;
                }
            }
        }

        public int HouseNumber {
            get { return HouseNumber; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("HouseNumber må ikke være mindre end 1");
                }
                else
                {
                    HouseNumber = value;
                }
            }
        }

        public int Floor
        {
            get { return Floor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Floor må ikke være mindre end 0");
                }
                else
                {
                    Floor = value;
                }
            }
        }

        public int ZipCode
        {
            get { return ZipCode; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("ZipCode må ikke være mindre end 1");
                }
                else
                {
                    ZipCode = value;
                }
            }
        }

        [Required]
        [StringLength(100)]
        public string City
        {
            get { return City; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("City må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("City må ikke være udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("City må ikke være længere end 100 karakterer");
                }
                else
                {
                    City = value;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Union> Unions { get; set; }
    }
}
