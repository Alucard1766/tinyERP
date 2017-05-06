using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tinyERP.Dal.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public bool IsRevenue { get; set; } = true;

        [Required]
        public int PrivatePart { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Today;

        public string Comment { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        public int BudgetId { get; set; }

        [ForeignKey("BudgetId")]
        public virtual Budget Budget { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int? DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document{ get; set; }
    }
}
