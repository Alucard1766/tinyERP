using System;
using System.ComponentModel.DataAnnotations;

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

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
