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

        private string streetName;
        private int houseNumber;
        private int floor;
        private int zipCode;
        private string city;

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetName
        {
            get { return streetName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("StreetName m� ikke v�re null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("StreetName m� ikke v�re udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("StreetName m� ikke v�re l�ngere end 100 karakterer");
                }
                else
                {
                    streetName = value;
                }
            }
        }

        public int HouseNumber {
            get { return houseNumber; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("HouseNumber m� ikke v�re mindre end 1");
                }
                else
                {
                    houseNumber = value;
                }
            }
        }

        public int Floor
        {
            get { return floor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Floor m� ikke v�re mindre end 0");
                }
                else
                {
                    floor = value;
                }
            }
        }

        public int ZipCode
        {
            get { return zipCode; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("ZipCode m� ikke v�re mindre end 1");
                }
                else
                {
                    zipCode = value;
                }
            }
        }

        [Required]
        [StringLength(100)]
        public string City
        {
            get { return city; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("City m� ikke v�re null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("City m� ikke v�re udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("City m� ikke v�re l�ngere end 100 karakterer");
                }
                else
                {
                    city = value;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Union> Unions { get; set; }
    }
}
