using InstaRent.Catalog.Grpc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        ConfigureGrpc(context);
    }

    private void ConfigureGrpc(ServiceConfigurationContext context)
    {
        context.Services.AddGrpcClient<BagPublic.BagPublicClient>((services, options) =>
        {
            var remoteServiceOptions = services.GetRequiredService<IOptionsMonitor<AbpRemoteServiceOptions>>().CurrentValue;
            var catalogServiceConfiguration = remoteServiceOptions.RemoteServices.GetConfigurationOrDefault("Catalog");
            var catalogGrpcUrl = catalogServiceConfiguration.GetOrDefault("GrpcUrl");

            options.Address = new Uri(catalogGrpcUrl);
        });
    }

}
