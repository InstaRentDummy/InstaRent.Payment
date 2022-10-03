using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment.Transactions
{
    public class PublicTransactionAppService : ApplicationService, IPublicTransactionAppService
    {
        private readonly ITransactionRepository _repository;

        public PublicTransactionAppService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<ListResultDto<TransactionDto>> GetListAsync(GetTransactionsInput input)
        {
            var totalCount = await _repository.GetCountAsync(input.FilterText, new Guid(input.bag_id), input.renter_id, input.lessee_id, input.date_transactedMin, input.date_transactedMax, null, input.isdeleted);
            var items = await _repository.GetListAsync(input.FilterText, new Guid(input.bag_id), input.renter_id, input.lessee_id, input.date_transactedMin, input.date_transactedMax, null, input.isdeleted, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TransactionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(items)
            };
        }

        public virtual async Task<TransactionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Transaction, TransactionDto>(await _repository.GetAsync(id));
        }


    }
}
