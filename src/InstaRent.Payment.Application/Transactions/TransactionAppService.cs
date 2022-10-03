using InstaRent.Payment.CartItems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment.Transactions
{
    public class TransactionAppService : ApplicationService, ITransactionAppService
    {
        private readonly ITransactionRepository _repository;
        private readonly TransactionManager _manager;

        public TransactionAppService(ITransactionRepository repository, TransactionManager manager)
        {
            _repository = repository;
            _manager = manager;
        }

        public virtual async Task<PagedResultDto<TransactionDto>> GetListAsync(GetTransactionsInput input)
        {
            var totalCount = await _repository.GetCountAsync(input.FilterText, input.bag_id, input.renter_id, input.lessee_id, input.date_transactedMin, input.date_transactedMax, null, input.isdeleted);
            var items = await _repository.GetListAsync(input.FilterText, input.bag_id, input.renter_id, input.lessee_id, input.date_transactedMin, input.date_transactedMax, null, input.isdeleted, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        public virtual async Task<bool> CheckTransactionAsync(string bagId, DateTime startDate, DateTime EndDate)
        {
            for (var day = startDate.Date; day.Date <= EndDate.Date; day = day.AddDays(1))
            {
                var items = await _repository.GetListAsync(string.Empty, bagId, string.Empty, string.Empty, null, null, day, false, string.Empty, 1, 0);

                if (items != null)
                    return true;
            }

            return false;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _manager.DeleteAsync(id);
        }

        public virtual async Task<TransactionDto> CreateAsync(TransactionCreateDto input)
        {

            var transaction = await _manager.CreateAsync(
            input.Lessee_Id, input.Date_Transacted, ObjectMapper.Map<List<CartItemDto>, List<CartItem>>(input.Cart_Items));

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }

        public virtual async Task<TransactionDto> UpdateAsync(Guid id, TransactionUpdateDto input)
        {

            var transaction = await _manager.UpdateAsync(
            id, input.Lessee_Id, input.Date_Transacted, ObjectMapper.Map<List<CartItemDto>, List<CartItem>>(input.Cart_Items), input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }
    }
}
