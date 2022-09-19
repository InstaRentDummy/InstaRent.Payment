using InstaRent.Payment.Transactions;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace InstaRent.Payment.MongoDB;

[ConnectionStringName(PaymentDbProperties.ConnectionStringName)]
public class PaymentMongoDbContext : AbpMongoDbContext, IPaymentMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */
    public IMongoCollection<Transaction> Transactions => Collection<Transaction>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigurePayment();

        modelBuilder.Entity<Transaction>(b => { b.CollectionName = PaymentDbProperties.DbTablePrefix + "Transactions"; });
    }
}
