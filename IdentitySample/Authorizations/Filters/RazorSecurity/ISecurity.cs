namespace Authorization.Filters.RazorSecurity
{
    public interface ISecurity
    {
        bool IsGranted(string claimValue);
        bool IsGranted(string claimType , string claimValue);
    }
}
