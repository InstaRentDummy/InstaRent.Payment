using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Payment.Transactions
{
    public class TransactionUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public List<object> cart_items { get; set; }
        public DateTime date_transacted { get; set; }
        [Required]
        public string lessee_id { get; set; }
        [Required]
        public string renter_id { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
