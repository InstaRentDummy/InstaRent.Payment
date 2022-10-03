using InstaRent.Payment.Transactions;
using InstaRent.Payment.UserPreferences;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace InstaRent.Payment.MongoDB;

[DependsOn(
    typeof(PaymentDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class PaymentMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<PaymentMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<Transaction, Transactions.MongoTransactionRepository>();
            options.AddRepository<UserPreference, UserPreferences.MongoUserPreferenceRepository>();
        });
    }
}
