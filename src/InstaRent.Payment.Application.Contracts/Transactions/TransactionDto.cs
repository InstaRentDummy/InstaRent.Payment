using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Payment.Transactions
{
    public class TransactionDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public List<object> cart_items { get; set; }
        public DateTime date_transacted { get; set; }
        public string lessee_id { get; set; }
        public string renter_id { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
