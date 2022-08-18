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

        public TransactionManager(ITransactionRepository Repository)
        {
            _repository = Repository;
        }

        public async Task<Transaction> CreateAsync(
        List<object> cart_items, DateTime date_transacted, string lessee_id, string renter_id)
        {
            var transaction = new Transaction(
             GuidGenerator.Create(),
             cart_items, date_transacted, lessee_id, renter_id);

            return await _repository.InsertAsync(transaction);
        }

        public async Task<Transaction> UpdateAsync(
            Guid id,
            List<object> cart_items, DateTime date_transacted, string lessee_id, string renter_id, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var transaction = await AsyncExecuter.FirstOrDefaultAsync(query);

            transaction.Cart_Items = cart_items;
            transaction.Date_Transacted = date_transacted;
            transaction.Lessee_Id = lessee_id;
            transaction.Renter_Id = renter_id;

            transaction.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _repository.UpdateAsync(transaction);
        }
    }
}
