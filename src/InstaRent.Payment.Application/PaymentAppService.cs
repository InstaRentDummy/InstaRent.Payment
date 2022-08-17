using InstaRent.Payment.Localization;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment;

public abstract class PaymentAppService : ApplicationService
{
    protected PaymentAppService()
    {
        LocalizationResource = typeof(PaymentResource);
        ObjectMapperContext = typeof(PaymentApplicationModule);
    }
}
