using InstaRent.Payment.CartItems;
using InstaRent.Payment.Transactions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment
{
    public class PaymentOrderAppService : ApplicationService, IPaymentOrderAppService
    {
        private readonly TransactionManager _transactionManager;
        private readonly ITransactionBagService _bagService;

        public PaymentOrderAppService(TransactionManager transactionManager
            //, ITransactionBagService bagService
            )
        {
            _transactionManager = transactionManager;
            //_bagService = bagService;
        }

        public virtual async Task<TransactionDto> PaymentAsync(TransactionCreateDto input)
        {
            foreach (var item in input.Cart_Items)
            {
                //var product = await _bagService.GetAsync(item.BagId);

                //if (!product.Equals("available"))
                //{
                //    throw new UserFriendlyException($"The bag, {item.BagName} is not available in stock!");
                //}
            }

            Task.Delay(1000).Wait();

            var transaction = await _transactionManager.CreateAsync(
            input.Lessee_Id, input.Date_Transacted, ObjectMapper.Map<List<CartItemDto>, List<CartItem>>(input.Cart_Items));

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }
    }
}
