using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Payment.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        Task<List<Transaction>> GetListAsync(
            string filterText = null,
            Guid? bag_id = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            DateTime? bag_rented_date = null,
            bool? isdeleted = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            Guid? bag_id = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            DateTime? bag_rented_date = null,
            bool? isdeleted = null,
            CancellationToken cancellationToken = default);
    }
}
