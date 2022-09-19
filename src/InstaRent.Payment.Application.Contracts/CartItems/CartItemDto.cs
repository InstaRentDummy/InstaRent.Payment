using InstaRent.Payment.Transactions;
using System;
using System.Collections.Generic;

namespace InstaRent.Payment.CartItems
{
    public class CartItemDto : ICartItem
    {
        public Guid BagId { get; set; }
        public string RenterId { get; set; } = string.Empty;
        public string BagName { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public List<string> Tags { get; set; } = new();

    }
}
