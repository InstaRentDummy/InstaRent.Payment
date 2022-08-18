using InstaRent.Payment.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            string lessee_id = null,
            string renter_id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, date_transactedMin, date_transactedMax, lessee_id, renter_id);
            //query = query.OrderBy(sorting);
            return await query.As<IMongoQueryable<Transaction>>()
                .PageBy<Transaction, IMongoQueryable<Transaction>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            string lessee_id = null,
            string renter_id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, date_transactedMin, date_transactedMax, lessee_id, renter_id);
            return await query.As<IMongoQueryable<Transaction>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Transaction> ApplyFilter(
            IQueryable<Transaction> query,
            string filterText = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            string lessee_id = null,
            string renter_id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Lessee_Id.Contains(filterText) || e.Renter_Id.Contains(filterText))
                    .WhereIf(date_transactedMin.HasValue, e => e.Date_Transacted >= date_transactedMin.Value)
                    .WhereIf(date_transactedMax.HasValue, e => e.Date_Transacted <= date_transactedMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(lessee_id), e => e.Lessee_Id.Contains(lessee_id))
                    .WhereIf(!string.IsNullOrWhiteSpace(renter_id), e => e.Renter_Id.Contains(renter_id));
        }
    }
}
