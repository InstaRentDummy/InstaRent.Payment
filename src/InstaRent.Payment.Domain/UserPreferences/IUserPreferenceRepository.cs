using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Payment.UserPreferences
{
    public interface IUserPreferenceRepository : IRepository<UserPreference, Guid>
    {
        Task<List<UserPreference>> GetListAsync(
            string filterText = null,
            string userId = null,
            string tags = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalNumofRatingMin = null,
            double? totalNumofRatingMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
           string filterText = null,
           string userId = null,
           string tags = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalNumofRatingMin = null,
            double? totalNumofRatingMax = null,
            CancellationToken cancellationToken = default);
    }
}
