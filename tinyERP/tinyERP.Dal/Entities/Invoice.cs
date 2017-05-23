using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tinyERP.Dal.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        [Required]
        public bool IsPayed { get; set; }

        [Required]
        public double Amount { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        public bool IsNotPayed => !IsPayed;
    }
}
