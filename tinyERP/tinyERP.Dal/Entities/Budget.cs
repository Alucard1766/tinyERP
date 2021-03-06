﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tinyERP.Dal.Entities
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double Expenses { get; set; }

        [Required]
        public double Revenue { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
