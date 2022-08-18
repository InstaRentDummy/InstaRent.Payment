namespace InstaRent.Payment
{
    public static class TransactionConsts
    {
        private const string DefaultSorting = "{0}bag_name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Bag." : string.Empty);
        }
    }
}
