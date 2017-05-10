using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tinyERP.Dal.Entities
{
    public class OrderConfirmation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OrderConfNumber { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
