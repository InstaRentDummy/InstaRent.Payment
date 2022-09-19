using InstaRent.Payment.CartItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Payment.Transactions
{
    public class TransactionDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        [Required]
        public string Lessee_Id { get; set; }

        //[Required]
        //public string Renter_Id { get; set; }

        public DateTime Date_Transacted { get; set; }

        [Required]
        public List<CartItemDto> Cart_Items { get; set; }

        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
        [JsonIgnore]
        public DateTime? LastModificationTime { get; set; }
        public bool? isdeleted { get; set; }
    }
}
