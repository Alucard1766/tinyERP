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
        public int PrivatePart { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Comment { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int BudgetId { get; set; }

        [ForeignKey("BudgetId")]
        public Budget Budget { get; set; }
    }
}