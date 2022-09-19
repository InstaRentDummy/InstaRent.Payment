using InstaRent.Catalog.Bags;
using InstaRent.Catalog.Grpc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace InstaRent.Payment.Transactions
{
    public class TransactionBagService : ITransactionBagService, ITransientDependency
    {
        //private readonly ILogger<TransactionBagService> _logger;
        private readonly IObjectMapper _mapper;
        private readonly BagPublic.BagPublicClient _bagPublicGrpcClient;

        public TransactionBagService(
            //ILogger<TransactionBagService> logger,
            IObjectMapper mapper,
            BagPublic.BagPublicClient bagPublicGrpcClient)
        {
            //_logger = logger;
            _mapper = mapper;
            _bagPublicGrpcClient = bagPublicGrpcClient;
        }

        public async Task<BagDto> GetAsync(Guid bagId)
        {
            var response = await _bagPublicGrpcClient.GetByIdAsync(new BagRequest() { Id = bagId.ToString() });

            //_logger.LogInformation("=== GRPC response {@response}", response);

            return _mapper.Map<BagResponse, BagDto>(response) ??
                   throw new UserFriendlyException("BagNotFound");
        }
    }
}
