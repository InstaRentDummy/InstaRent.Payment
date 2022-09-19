using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Payment.Transactions
{
    public class Transaction : Entity<Guid>, IHasConcurrencyStamp
    {
        [NotNull]
        public virtual string Lessee_Id { get; set; }

        //[NotNull]
        //public virtual string Renter_Id { get; set; }

        [NotNull]
        public virtual DateTime Date_Transacted { get; set; }

        [NotNull]
        public virtual List<CartItem> Cart_Items { get; set; }

        [CanBeNull]
        public virtual DateTime? LastModificationTime { get; set; }

        [NotNull]
        public virtual bool Isdeleted { get; set; }

        public string ConcurrencyStamp { get; set; }

        public Transaction()
        {

        }

        public Transaction(Guid id, string lessee_Id, DateTime date_Transacted, List<CartItem> cart_Items, bool isdeleted = false)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(lessee_Id, nameof(lessee_Id));
            //Check.NotNull(renter_Id, nameof(renter_Id));
            Check.NotNull(date_Transacted, nameof(date_Transacted));
            Check.NotNull(cart_Items, nameof(cart_Items));
            this.Lessee_Id = lessee_Id;
            //this.Renter_Id = renter_Id;
            this.Date_Transacted = date_Transacted;
            this.Cart_Items = cart_Items;
            this.Isdeleted = isdeleted;
            this.LastModificationTime = DateTime.Now;
        }
    }

    public class CartItem : ICartItem
    {
        public virtual Guid BagId { get; set; }
        public virtual string RenterId { get; set; } = string.Empty;
        public virtual string BagName { get; set; } = string.Empty;
        public virtual double Price { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual List<string> ImageUrls { get; set; } = new();
        public virtual List<string> Tags { get; set; } = new();

        public CartItem()
        { }

        public CartItem(Guid bagId, string renterId, string bagName, double price, DateTime startDate, DateTime endDate, List<string> imageUrls, List<string> tags)
        {
            this.BagId = bagId;
            this.RenterId = renterId;
            this.Price = price;
            this.BagName = bagName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ImageUrls = imageUrls;
            this.Tags = tags;
        }
    }

}
