using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstaRent.Payment.Transactions
{
    public class TransactionCreateDto
    {
        [Required]
        public List<object> cart_items { get; set; }
        public DateTime date_transacted { get; set; }
        [Required]
        public string lessee_id { get; set; }
        [Required]
        public string renter_id { get; set; }
    }
}
