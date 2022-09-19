using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment.Transactions
{
    public interface ITransactionAppService : IApplicationService
    {
        Task<PagedResultDto<TransactionDto>> GetListAsync(GetTransactionsInput input);

        Task<TransactionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TransactionDto> CreateAsync(TransactionCreateDto input);

        Task<TransactionDto> UpdateAsync(Guid id, TransactionUpdateDto input);
    }
}
