using InstaRent.Catalog.Bags;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;

namespace InstaRent.Payment.Transactions
{
    public interface ITransactionBagService
    {
        [ItemNotNull]
        Task<BagDto> GetAsync(Guid bagId);
    }
}
