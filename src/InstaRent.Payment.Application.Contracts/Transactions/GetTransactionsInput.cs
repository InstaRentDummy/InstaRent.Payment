using System;
using Volo.Abp.Application.Dtos;

namespace InstaRent.Payment.Transactions
{
    public class GetTransactionsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string renter_id { get; set; }
        public string lessee_id { get; set; }
        public DateTime? date_transactedMin { get; set; }
        public DateTime? date_transactedMax { get; set; }
        public bool? isdeleted { get; set; }

        public GetTransactionsInput()
        {

        }
    }
}
