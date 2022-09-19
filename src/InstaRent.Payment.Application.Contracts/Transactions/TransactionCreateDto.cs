using InstaRent.Payment.CartItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstaRent.Payment.Transactions
{
    public class TransactionCreateDto
    {
        [Required]
        public string Lessee_Id { get; set; }

        //[Required]
        //public string Renter_Id { get; set; }

        public DateTime Date_Transacted { get; set; }

        [Required]
        public List<CartItemDto> Cart_Items { get; set; }

    }
}
