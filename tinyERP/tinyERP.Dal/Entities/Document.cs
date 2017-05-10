using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tinyERP.Dal.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tag { get; set; }

        [Required]
        public string RelativePath { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual OrderConfirmation OrderConfirmation { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
