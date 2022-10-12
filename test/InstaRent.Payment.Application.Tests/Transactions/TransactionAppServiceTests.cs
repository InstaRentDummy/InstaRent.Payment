using InstaRent.Payment.CartItems;
using InstaRent.Payment.Transactions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace InstaRent.Payment 
{
    public class TransactionAppServiceTests: PaymentApplicationTestBase
    {
        private readonly ITransactionAppService _transactionAppService;
        private readonly IRepository<Transaction, Guid> _transactionRepository;

        public TransactionAppServiceTests()
        {
            _transactionAppService = GetRequiredService<ITransactionAppService>();
            _transactionRepository = GetRequiredService<IRepository<Transaction, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _transactionAppService.GetListAsync(new GetTransactionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("edba497f-ec22-4773-bd69-9188fe5e7933")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _transactionAppService.GetAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TransactionCreateDto
            {
                Lessee_Id = "newlessee@gmail.com",
                Date_Transacted = DateTime.Now,
                Cart_Items = new List<CartItemDto>() { new CartItemDto(){
                     BagId = Guid.Parse("249ab4d0-5cf5-4e66-8b74-a3a20b099eba"),
                     RenterId = "renter_1@gmail.com",
                     BagName= "Bag1",
                     Price= 100.00,
                     StartDate= new DateTime(2022, 10, 28),
                     EndDate= new DateTime(2022, 10, 30),
                     ImageUrls= new List<string>() { "http://imagerepo.url.com/image7933" },
                     Tags= new List<string>() { "Test Tag1" }
                }
                } 
            };

            // Act
            var serviceResult = await _transactionAppService.CreateAsync(input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Lessee_Id.ShouldBe("newlessee@gmail.com");
            result.Cart_Items[0].BagName.ShouldBe("Bag1");
            result.Cart_Items[0].RenterId.ShouldBe("renter_1@gmail.com");
            result.Cart_Items[0].Price.ShouldBe(100.00);
            result.Cart_Items[0].StartDate.ShouldBe(new DateTime(2022, 10, 28).ToUniversalTime());
            result.Cart_Items[0].EndDate.ShouldBe(new DateTime(2022, 10, 30).ToUniversalTime());
            result.Cart_Items[0].ImageUrls[0].ShouldBe("http://imagerepo.url.com/image7933");
            result.Cart_Items[0].Tags[0].ShouldBe("Test Tag1"); 


        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TransactionUpdateDto()
            {
                Lessee_Id= "lessee_1@gmail.com",
                Date_Transacted = DateTime.Now,
                Cart_Items = new List<CartItemDto>() { new CartItemDto(){
                     BagId = Guid.Parse("249ab4d0-5cf5-4e66-8b74-a3a20b099eba"),
                     RenterId = "renter_1@gmail.com",
                     BagName= "Bag1",
                     Price= 100.00,
                     StartDate= new DateTime(2022, 10, 13),
                     EndDate= new DateTime(2022, 10, 16),
                     ImageUrls= new List<string>() { "http://imagerepo.url.com/image7933" },
                     Tags= new List<string>() { "Test Tag1" }
                }
                }
            };

            // Act
            var serviceResult = await _transactionAppService.UpdateAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"), input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Cart_Items[0].StartDate.ShouldBe(new DateTime(2022, 10, 13).ToUniversalTime());
            result.Cart_Items[0].EndDate.ShouldBe(new DateTime(2022, 10, 16).ToUniversalTime());

        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _transactionAppService.DeleteAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            result.Isdeleted.ShouldBeTrue();
        }

        [Fact]
        public async Task CheckTransactionFoundAsync()
        {
            // Act
            var result = await _transactionAppService.CheckTransactionAsync("41544f01-56f2-4746-be9d-f926acd27dfc", new DateTime(2022, 10, 21).ToUniversalTime(), new DateTime(2022, 10, 23).ToUniversalTime());
             
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task CheckTransactionNotFoundAsync()
        {
            // Act
            var result = await _transactionAppService.CheckTransactionAsync("41544f01-56f2-4746-be9d-f926acd27dfc", new DateTime(2022, 10, 23).ToUniversalTime(), new DateTime(2022, 10, 25).ToUniversalTime());

            result.ShouldBeFalse();
        }
    }
}
