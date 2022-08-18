using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace InstaRent.Payment.Transactions
{
    public class Transaction : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [NotNull]
        public virtual List<object> Cart_Items { get; set; }

        [NotNull]
        public virtual DateTime Date_Transacted { get; set; }

        [NotNull]
        public virtual string Lessee_Id { get; set; }

        [NotNull]
        public virtual string Renter_Id { get; set; }

        public string ConcurrencyStamp { get; set; }

        public Transaction()
        {

        }

        public Transaction(Guid id, List<object> cart_items, DateTime date_transacted, string lessee_id, string renter_id)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(lessee_id, nameof(lessee_id));
            Check.NotNull(renter_id, nameof(renter_id));
            Cart_Items = cart_items;
            Date_Transacted = date_transacted;
            Lessee_Id = lessee_id;
            Renter_Id = renter_id;
        }
    }
}
