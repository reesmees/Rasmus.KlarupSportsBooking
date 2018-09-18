namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UnionLogin
    {
        public int ID { get; set; }

        public int UnionID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username
        {
            get { return Username; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Username må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username må ikke være udelukkende whitespace");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException("Username må ikke være længere end 50 karakterer");
                }
                else
                {
                    Username = value;
                }
            }
        }

        [Required]
        [StringLength(50)]
        public string Password
        {
            get { return Password; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Password må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password må ikke være udelukkende whitespace");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException("Password må ikke være længere end 50 karakterer");
                }
                else
                {
                    Password = value;
                }
            }
        }

        public virtual Union Union { get; set; }
    }
}
