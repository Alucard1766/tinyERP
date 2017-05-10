using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tinyERP.Dal.Entities
{
    public class OrderConfirmation
    {

        [Required]
        public string OrderConfNumber { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
