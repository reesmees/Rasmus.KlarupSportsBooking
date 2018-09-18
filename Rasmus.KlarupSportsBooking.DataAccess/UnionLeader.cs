namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UnionLeader
    {
        public int ID { get; set; }

        public int UnionID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name
        {
            get { return Name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name må ikke være udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("Name må ikke være længere end 100 karakterer");
                }
                else
                {
                    Name = value;
                }
            }
        }

        [Required]
        [StringLength(20)]
        public string PhoneNumber
        {
            get { return PhoneNumber; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("PhoneNumber må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("PhoneNumber må ikke være udelukkende whitespace");
                }
                else if (value.Length > 20)
                {
                    throw new ArgumentException("PhoneNumber må ikke være længere end 20 karakterer");
                }
                else
                {
                    PhoneNumber = value;
                }
            }
        }

        public virtual Union Union { get; set; }
    }
}
