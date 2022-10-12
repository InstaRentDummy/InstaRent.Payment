using InstaRent.Payment.CartItems;
using InstaRent.Payment.Transactions;
using InstaRent.Payment.UserPreferences;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace InstaRent.Payment
{
    public class PaymentOrderAppServiceTests : PaymentApplicationTestBase
    {
        private readonly IPaymentOrderAppService _paymentOrderAppService;
        private readonly IRepository<Transaction, Guid> _transactionRepository;
        private readonly IRepository<UserPreference, Guid> _userPrefernceRepository;

        public PaymentOrderAppServiceTests()
        {
            _paymentOrderAppService = GetRequiredService<IPaymentOrderAppService>();
            _transactionRepository = GetRequiredService<IRepository<Transaction, Guid>>();
            _userPrefernceRepository = GetRequiredService<IRepository<UserPreference, Guid>>();
        }

        [Fact]
        public async Task PaymentAsync()
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
            var serviceResult = await _paymentOrderAppService.PaymentAsync(input);

            // Assert
            var result = await _transactionRepository.FindAsync(c => c.Id == serviceResult.Id);
            var userresult = await _userPrefernceRepository.FindAsync(c => c.UserId == "newlessee@gmail.com");

            result.ShouldNotBe(null);
            result.Lessee_Id.ShouldBe("newlessee@gmail.com");
            result.Cart_Items[0].BagName.ShouldBe("Bag1");
            result.Cart_Items[0].RenterId.ShouldBe("renter_1@gmail.com");
            result.Cart_Items[0].Price.ShouldBe(100.00);
            result.Cart_Items[0].StartDate.ShouldBe(new DateTime(2022, 10, 28).ToUniversalTime());
            result.Cart_Items[0].EndDate.ShouldBe(new DateTime(2022, 10, 30).ToUniversalTime());
            result.Cart_Items[0].ImageUrls[0].ShouldBe("http://imagerepo.url.com/image7933");
            result.Cart_Items[0].Tags[0].ShouldBe("Test Tag1");

            userresult.ShouldNotBeNull();
            userresult.Tags.Count.ShouldBe(1);


        }
    }
}
