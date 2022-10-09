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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, bag_id, renter_id, lessee_id, date_transactedMin, date_transactedMax, bag_rented_date, isdeleted);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TransactionConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Transaction>>()
                .PageBy<Transaction, IMongoQueryable<Transaction>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
            Guid? bag_id = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            DateTime? bag_rented_date = null,
           bool? isdeleted = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, bag_id, renter_id, lessee_id, date_transactedMin, date_transactedMax, bag_rented_date, isdeleted);
            return await query.As<IMongoQueryable<Transaction>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Transaction> ApplyFilter(
            IQueryable<Transaction> query,
            string filterText = null,
            Guid? bag_id = null,
            string renter_id = null,
            string lessee_id = null,
            DateTime? date_transactedMin = null,
            DateTime? date_transactedMax = null,
            DateTime? bag_rented_date = null,
            bool? isdeleted = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Lessee_Id.Contains(filterText) || e.Cart_Items.Any(i => i.BagName.Contains(filterText)) || e.Cart_Items.Any(t => t.RenterId.Contains(filterText)))
                    .WhereIf(!string.IsNullOrWhiteSpace(lessee_id), e => e.Lessee_Id == lessee_id)
                    .WhereIf(!string.IsNullOrWhiteSpace(renter_id), e => e.Cart_Items.Any(x => x.RenterId == renter_id))
                    .WhereIf(bag_id != null && bag_id != Guid.Empty, e => e.Cart_Items.Any(x => x.BagId == bag_id))
                    .WhereIf(date_transactedMin.HasValue, e => e.Date_Transacted >= date_transactedMin.Value)
                    .WhereIf(date_transactedMax.HasValue, e => e.Date_Transacted <= date_transactedMax.Value)
                    .WhereIf(bag_rented_date.HasValue, e => e.Cart_Items.Any(x => x.BagId == bag_id && x.StartDate <= bag_rented_date.Value && x.EndDate >= bag_rented_date.Value))
                    .WhereIf(isdeleted.HasValue, e => e.Isdeleted.Equals(isdeleted.Value));
        }
    }
}
