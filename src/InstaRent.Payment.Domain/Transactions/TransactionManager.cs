using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace InstaRent.Payment.Transactions
{
    public class TransactionManager : DomainService
    {
        private readonly ITransactionRepository _repository;

        public TransactionManager(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> CreateAsync(
        string lessee_id, DateTime transaction_date, List<CartItem> cartItems)
        {
            var bag = new Transaction(
             GuidGenerator.Create(),
             lessee_id, transaction_date, cartItems, false
             );

            return await _repository.InsertAsync(bag);
        }

        public async Task<Transaction> UpdateAsync(
            Guid id, string lessee_id, DateTime transaction_date, List<CartItem> cartItems, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var transaction = await AsyncExecuter.FirstOrDefaultAsync(query);

            //transaction.Renter_Id = renter_id;
            transaction.Lessee_Id = lessee_id;
            transaction.Cart_Items = cartItems;
            transaction.Date_Transacted = transaction_date;
            transaction.LastModificationTime = DateTime.Now;

            transaction.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _repository.UpdateAsync(transaction);
        }

        public async Task DeleteAsync(Guid id, [CanBeNull] string concurrencyStamp = null)
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var transaction = await AsyncExecuter.FirstOrDefaultAsync(query);

            transaction.Isdeleted = true;
            transaction.LastModificationTime = DateTime.Now;

            transaction.SetConcurrencyStampIfNotNull(concurrencyStamp);
            await _repository.UpdateAsync(transaction);
        }
    }
}
