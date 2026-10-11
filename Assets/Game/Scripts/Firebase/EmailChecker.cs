public class EmailChecker
{
    public bool IsCorrectDomain(string email)
    {
        const string CorrectDomain = "@gmail.com";

        return email.EndsWith(CorrectDomain);
    }

    public bool IsCorrectSymbols(string email)
    {
        const char AtSign = '@';
        const string CorrectDomain = "@gmail.com";

        return email.IndexOf(AtSign) == email.Length - CorrectDomain.Length;
    }
}