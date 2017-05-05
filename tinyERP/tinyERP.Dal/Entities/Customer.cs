using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tinyERP.Dal.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Company { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Zip { get; set; }

        [Required]
        public string City { get; set; }

        public string Email { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<CustomerHistory> CustomerHistories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
