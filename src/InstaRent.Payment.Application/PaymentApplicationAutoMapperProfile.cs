using AutoMapper;
using InstaRent.Payment.CartItems;
using InstaRent.Payment.Transactions;

namespace InstaRent.Payment;

public class PaymentApplicationAutoMapperProfile : Profile
{
    public PaymentApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Transaction, TransactionDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<CartItemDto, CartItem>();
    }
}
