using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace InstaRent.Payment.UserPreferences
{
    public class UserPreferenceManager : DomainService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        public UserPreferenceManager(IUserPreferenceRepository userPreferenceRepository)
        {
            _userPreferenceRepository = userPreferenceRepository;
        }

        public async Task<UserPreference> UpdateTagsAsync(
            string userId, List<string> tags, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _userPreferenceRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.UserId == userId);

            var userPreference = await AsyncExecuter.FirstOrDefaultAsync(query);

            foreach (var tag in tags)
            {
                if (userPreference.Tags.Where(x => x.tagname == tag).Any())
                {
                    var _tag = userPreference.Tags.Where(x => x.tagname == tag).First();
                    _tag.weightage++;
                }
                else
                    userPreference.Tags.Add(new Tag() { tagname = tag, weightage = 1 });
            }

            userPreference.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _userPreferenceRepository.UpdateAsync(userPreference);
        }


        public async Task<UserPreference> UpdateRatingAsync(
           string userId, double? avgRating, double? totalNumOfRating, [CanBeNull] string concurrencyStamp = null
       )
        {
            var queryable = await _userPreferenceRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.UserId == userId);

            var userPreference = await AsyncExecuter.FirstOrDefaultAsync(query);

            userPreference.AvgRating = avgRating;
            userPreference.TotalNumofRating = totalNumOfRating;

            userPreference.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _userPreferenceRepository.UpdateAsync(userPreference);
        }
    }
}
