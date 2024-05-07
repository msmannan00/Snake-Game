
public class HelperMethods : GenericSingletonClass<HelperMethods>
{
    public string ExtractUsernameFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');
        if (atIndex >= 0)
        {
            return email.Substring(0, atIndex);
        }
        return null;
    }
}