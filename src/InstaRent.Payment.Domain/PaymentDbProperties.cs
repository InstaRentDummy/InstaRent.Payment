namespace InstaRent.Payment;

public static class PaymentDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "InstaRent";
}
