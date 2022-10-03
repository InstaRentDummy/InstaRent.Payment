namespace InstaRent.Payment.UserPreferences
{
    public static class UserPreferenceConsts
    {
        private const string DefaultSorting = "{0}UserId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "UserPreference." : string.Empty);
        }

        public const int UserIdMaxLength = 256;
    }
}
