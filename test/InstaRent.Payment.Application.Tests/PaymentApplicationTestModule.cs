using Volo.Abp.Modularity;

namespace InstaRent.Payment;

[DependsOn(
    typeof(PaymentApplicationModule),
    typeof(PaymentDomainTestModule)
    )]
public class PaymentApplicationTestModule : AbpModule
{

}
