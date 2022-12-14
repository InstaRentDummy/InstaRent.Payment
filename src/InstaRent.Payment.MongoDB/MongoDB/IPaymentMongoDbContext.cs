using InstaRent.Payment.Transactions;
using InstaRent.Payment.UserPreferences;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace InstaRent.Payment.MongoDB;

[ConnectionStringName(PaymentDbProperties.ConnectionStringName)]
public interface IPaymentMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
    IMongoCollection<Transaction> Transactions { get; }
    IMongoCollection<UserPreference> UserPreferences { get; }
}
