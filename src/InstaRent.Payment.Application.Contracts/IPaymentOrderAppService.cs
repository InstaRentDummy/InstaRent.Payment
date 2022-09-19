using InstaRent.Payment.Transactions;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment
{
    public interface IPaymentOrderAppService : IApplicationService
    {
        Task<TransactionDto> PaymentAsync(TransactionCreateDto input);
    }
}
