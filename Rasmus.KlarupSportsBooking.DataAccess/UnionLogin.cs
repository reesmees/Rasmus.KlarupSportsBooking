namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UnionLogin
    {
        private string username;
        private string password;

        public int ID { get; set; }

        public int UnionID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username
        {
            get { return username; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Username m� ikke v�re null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username m� ikke v�re udelukkende whitespace");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException("Username m� ikke v�re l�ngere end 50 karakterer");
                }
                else
                {
                    username = value;
                }
            }
        }

        [Required]
        [StringLength(50)]
        public string Password
        {
            get { return password; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Password m� ikke v�re null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password m� ikke v�re udelukkende whitespace");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException("Password m� ikke v�re l�ngere end 50 karakterer");
                }
                else
                {
                    password = value;
                }
            }
        }

        public virtual Union Union { get; set; }
    }
}
