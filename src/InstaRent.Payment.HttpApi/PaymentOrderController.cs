using InstaRent.Payment.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace InstaRent.Payment
{
    [RemoteService(Name = "Payment")]
    [Area("payment")]
    [ControllerName("PaymentOrder")]
    [Route("api/payment/order")]
    public class PaymentOrderController : AbpController, IPaymentOrderAppService
    {
        private readonly IPaymentOrderAppService _appService;

        public PaymentOrderController(IPaymentOrderAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        public virtual Task<TransactionDto> PaymentAsync(TransactionCreateDto input)
        {
            return _appService.PaymentAsync(input);
        }

    }
}
