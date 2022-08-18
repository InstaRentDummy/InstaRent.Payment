using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace InstaRent.Payment.Transactions
{
    [RemoteService(Name = "Transaction")]
    [Area("Payment")]
    [ControllerName("Transaction")]
    [Route("api/Payment/Transactions")]
    public class TransactionController : AbpController, ITransactionAppService
    {
        private readonly ITransactionAppService _appService;

        public TransactionController(ITransactionAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TransactionDto>> GetListAsync(GetTransactionInput input)
        {
            return _appService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TransactionDto> GetAsync(Guid id)
        {
            return _appService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TransactionDto> CreateAsync(TransactionCreateDto input)
        {
            return _appService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TransactionDto> UpdateAsync(Guid id, TransactionUpdateDto input)
        {
            return _appService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _appService.DeleteAsync(id);
        }
    }
}
