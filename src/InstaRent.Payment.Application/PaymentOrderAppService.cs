using InstaRent.Payment.CartItems;
using InstaRent.Payment.Transactions;
using InstaRent.Payment.UserPreferences;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace InstaRent.Payment
{
    public class PaymentOrderAppService : ApplicationService, IPaymentOrderAppService
    {
        private readonly TransactionManager _transactionManager;
        private readonly UserPreferenceManager _userPreferenceManager;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        //private readonly ITransactionBagService _service;

        public PaymentOrderAppService(TransactionManager transactionManager, UserPreferenceManager userPreferenceManager
            , ITransactionRepository transactionRepository, IUserPreferenceRepository userPreferenceRepository
            //, ITransactionBagService bagService
            )
        {
            _transactionManager = transactionManager;
            _userPreferenceManager = userPreferenceManager;
            _transactionRepository = transactionRepository;
            _userPreferenceRepository = userPreferenceRepository;
            //_bagService = bagService;
        }

        public virtual async Task<TransactionDto> PaymentAsync(TransactionCreateDto input)
        {
            foreach (var item in input.Cart_Items)
            {
                for (var day = item.StartDate.Date; day.Date <= item.EndDate.Date; day = day.AddDays(1))
                {
                    var items = await _transactionRepository.GetListAsync(null, item.BagId, null, null, null, null, day, false, null, 1, 0);

                    if (items != null && items.Count > 0)
                        throw new UserFriendlyException($"The bag, {item.BagName} is not available in stock!");
                }
                //if (!product.Equals("available"))
                //{
                //    throw new UserFriendlyException($"The bag, {item.BagName} is not available in stock!");
                //}
            }

            var transaction = await _transactionManager.CreateAsync(
            input.Lessee_Id, input.Date_Transacted, ObjectMapper.Map<List<CartItemDto>, List<CartItem>>(input.Cart_Items));

            List<string> _tags = new List<string>();

            input.Cart_Items.ForEach(x =>
            {
                _tags.AddRange(x.Tags);
            });

            var result = await _userPreferenceRepository.GetCountAsync(null, input.Lessee_Id, null);

            if (result > 0)
                await _userPreferenceManager.UpdateTagsAsync(input.Lessee_Id, _tags);
            else
                await _userPreferenceManager.CreateAsync(input.Lessee_Id, _tags);

            return ObjectMapper.Map<Transaction, TransactionDto>(transaction);
        }

    }
}
