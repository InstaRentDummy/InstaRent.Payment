using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace InstaRent.Payment.UserPreferences
{
    public class UserPreference : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [CanBeNull]
        public virtual string UserId { get; set; }

        [CanBeNull]
        public virtual List<Tag> Tags { get; set; }
        
        public string ConcurrencyStamp { get; set; }

        public UserPreference()
        {

        }

        public UserPreference(Guid id, string userId, List<Tag> tags)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Check.Length(userId, nameof(userId), UserPreferenceConsts.UserIdMaxLength, 0);
            Id = id;
            UserId = userId;
            Tags = tags;
            
        }

    }

    public class Tag : ITag
    {
        [CanBeNull]
        public virtual string tagname { get; set; }

        [CanBeNull]
        public virtual int weightage { get; set; }

        public Tag()
        {

        }

        public Tag(string tagname, int weightage)
        {
            this.tagname = tagname;
            this.weightage = weightage;
        }

    }
}
