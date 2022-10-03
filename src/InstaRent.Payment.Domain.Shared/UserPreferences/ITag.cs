namespace InstaRent.Payment.UserPreferences
{
    public interface ITag
    {
        string tagname { get; set; }
        int weightage { get; set; }
    }
}
