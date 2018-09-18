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
                    throw new ArgumentNullException("Name m� ikke v�re null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name m� ikke v�re udelukkende whitespace");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("Name m� ikke v�re l�ngere end 100 karakterer");
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
                    throw new ArgumentNullException("PhoneNumber m� ikke v�re null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("PhoneNumber m� ikke v�re udelukkende whitespace");
                }
                else if (value.Length > 20)
                {
                    throw new ArgumentException("PhoneNumber m� ikke v�re l�ngere end 20 karakterer");
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
