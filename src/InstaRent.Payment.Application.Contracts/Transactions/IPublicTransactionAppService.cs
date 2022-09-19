using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment.Transactions
{
    public interface IPublicTransactionAppService : IApplicationService
    {
        Task<ListResultDto<TransactionDto>> GetListAsync(GetTransactionsInput input);

        Task<TransactionDto> GetAsync(Guid id);
    }
}
