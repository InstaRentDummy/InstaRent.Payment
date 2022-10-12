using InstaRent.Payment.UserPreferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace InstaRent.Payment.Transactions
{
    public class TransactionDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TransactionDataSeedContributor(ITransactionRepository transactionRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _transactionRepository = transactionRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _transactionRepository.InsertAsync(new Transaction
            (
                id: Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"),
                lessee_Id: "lessee_1@gmail.com",
                date_Transacted: DateTime.Now,
                cart_Items: new List<CartItem>() { new CartItem(
                     bagId: Guid.Parse("249ab4d0-5cf5-4e66-8b74-a3a20b099eba"),
                     renterId: "renter_1@gmail.com",
                     bagName: "Bag1",
                     price: 100.00,
                     startDate: new DateTime(2022, 10, 11),
                     endDate: new DateTime(2022, 10, 16),
                     imageUrls: new List<string>() { "http://imagerepo.url.com/image7933" },
                     tags: new List<string>() { "Test Tag1" })
                }
            ));



            await _transactionRepository.InsertAsync(new Transaction
            (
              id: Guid.Parse("edba497f-ec22-4773-bd69-9188fe5e7933"),
              lessee_Id: "lessee_2@gmail.com",
              date_Transacted: DateTime.Now,
              cart_Items: new List<CartItem>() { new CartItem(
                     bagId: Guid.Parse("41544f01-56f2-4746-be9d-f926acd27dfc"),
                     renterId: "renter_2@gmail.com",
                     bagName: "Bag2",
                     price: 150.00,
                     startDate: new DateTime(2022, 10, 20),
                     endDate: new DateTime(2022, 10, 22),
                     imageUrls: new List<string>() { "http://imagerepo.url.com/image7935" },
                     tags: new List<string>() { "Test Tag 2" })
              }
            ));
            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }

}
