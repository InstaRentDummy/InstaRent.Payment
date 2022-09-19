namespace InstaRent.Payment.Transactions
{
    public static class TransactionConsts
    {
        private const string DefaultSorting = "{0}Lessee_Id asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Transaction." : string.Empty);
        }

        public const int bag_nameMaxLength = 256;
        public const int statusMaxLength = 128;
    }
}
