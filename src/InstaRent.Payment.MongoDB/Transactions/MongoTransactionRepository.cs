using InstaRent.Payment.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace InstaRent.Payment.Transactions
{
    public class MongoTransactionRepository : MongoDbRepository<PaymentMongoDbContext, Transaction, Guid>, ITransactionRepository
    {
        public MongoTransactionRepository(IMongoDbContextProvider<PaymentMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Transaction>> GetListAsync(
            string filterText = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            bool? isdeleted = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, renter_id, lessee_id, date_transactedMin, date_transactedMax, isdeleted);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Transaction>>()
                .PageBy<Transaction, IMongoQueryable<Transaction>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
           bool? isdeleted = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, renter_id, lessee_id, date_transactedMin, date_transactedMax, isdeleted);
            return await query.As<IMongoQueryable<Transaction>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Transaction> ApplyFilter(
            IQueryable<Transaction> query,
            string filterText = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            bool? isdeleted = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Lessee_Id.Contains(filterText) || e.Cart_Items.Any(i => i.BagName.Contains(filterText)) || e.Cart_Items.Any(t => t.RenterId.Contains(filterText)))
                    .WhereIf(!string.IsNullOrWhiteSpace(lessee_id), e => e.Lessee_Id.Contains(lessee_id))
                    .WhereIf(!string.IsNullOrWhiteSpace(renter_id), e => e.Cart_Items.Any(x => x.RenterId.Contains(renter_id)))
                    .WhereIf(date_transactedMin.HasValue, e => e.Date_Transacted >= date_transactedMin.Value)
                    .WhereIf(date_transactedMax.HasValue, e => e.Date_Transacted <= date_transactedMax.Value)
                    .WhereIf(isdeleted.HasValue, e => e.Isdeleted.Equals(isdeleted.Value));
        }
    }
}
