namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncryption
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
