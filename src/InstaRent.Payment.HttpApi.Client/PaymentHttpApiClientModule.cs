using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace InstaRent.Payment;

[DependsOn(
    typeof(PaymentApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class PaymentHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(PaymentApplicationContractsModule).Assembly,
            PaymentRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PaymentHttpApiClientModule>();
        });

    }
}
