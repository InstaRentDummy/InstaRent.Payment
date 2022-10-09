using InstaRent.Payment.MongoDB;
using Volo.Abp.Modularity;

namespace InstaRent.Payment;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(PaymentMongoDbTestModule)
    )]
public class PaymentDomainTestModule : AbpModule
{

}
