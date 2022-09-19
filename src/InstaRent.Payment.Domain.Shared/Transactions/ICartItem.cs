using System;
using System.Collections.Generic;

namespace InstaRent.Payment.Transactions
{
    public interface ICartItem
    {
        Guid BagId { get; set; }
        string RenterId { get; set; }
        string BagName { get; set; }
        double Price { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        List<string> ImageUrls { get; set; }
        List<string> Tags { get; set; }
    }
}
