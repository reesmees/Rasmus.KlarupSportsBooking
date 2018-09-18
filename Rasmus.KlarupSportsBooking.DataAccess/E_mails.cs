namespace Rasmus.KlarupSportsBooking.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("E-mails")]
    public partial class E_mails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public E_mails()
        {
            Administrators = new HashSet<Administrator>();
            Unions = new HashSet<Union>();
        }

        private string e_mailAddress;

        public int ID { get; set; }

        [Column("E-mailAddress")]
        [Required]
        [StringLength(50)]
        public string E_mailAddress
        {
            get { return e_mailAddress; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("E_mailAddress må ikke være null");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("E_mailAddress må ikke være udelukkende whitespace");
                }
                else if (value.Length > 50)
                {
                    throw new ArgumentException("E_mailAddress må ikke være længere end 50 karakterer");
                }
                else
                {
                    e_mailAddress = value;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administrator> Administrators { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Union> Unions { get; set; }
    }
}
