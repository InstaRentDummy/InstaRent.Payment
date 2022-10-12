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

namespace InstaRent.Payment.UserPreferences
{
    public class MongoUserPreferenceRepository : MongoDbRepository<PaymentMongoDbContext, UserPreference, Guid>, IUserPreferenceRepository
    {
        public MongoUserPreferenceRepository(IMongoDbContextProvider<PaymentMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<UserPreference>> GetListAsync(
            string filterText = null,
            string userId = null,
            string tags = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, tags);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UserPreferenceConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<UserPreference>>()
                .PageBy<UserPreference, IMongoQueryable<UserPreference>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           string userId = null,
           string tags = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, tags);
            return await query.As<IMongoQueryable<UserPreference>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<UserPreference> ApplyFilter(
            IQueryable<UserPreference> query,
            string filterText,
            string userId = null,
            string tags = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.UserId.Contains(filterText) || e.Tags.Any(t => t.tagname.Contains(filterText)))
                    .WhereIf(!string.IsNullOrWhiteSpace(userId), e => e.UserId.Contains(userId))
                    .WhereIf(!string.IsNullOrWhiteSpace(tags), e => e.Tags.Any(t => t.tagname.Contains(tags)));
                   
        }


        protected virtual IQueryable<UserPreference> ApplyRecommendationFilter(
            IQueryable<UserPreference> query,
            string userId)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(userId), e => e.UserId == userId);
        }
    }
}
