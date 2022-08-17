using InstaRent.Payment.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InstaRent.Payment;

public abstract class PaymentController : AbpControllerBase
{
    protected PaymentController()
    {
        LocalizationResource = typeof(PaymentResource);
    }
}
