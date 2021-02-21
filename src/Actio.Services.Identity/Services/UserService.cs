using Actio.Common.Authentication;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly IEncryption _encryption;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository user, IEncryption encryption, IJwtHandler jwtHandler)
        {
            _user = user;
            _encryption = encryption;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _user.GetAsync(email);

            if (user.IsNotNull())
                throw new ActioException("email_in_use", $"Email: {email} is already in use.");

            user = new User(email, name);

            user.SetPassword(password, _encryption);

            await _user.AddAsync(user);
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _user.GetAsync(email);

            if (user.IsNull())
                throw new ActioException("invalid_credentials", "Invalid Credentials");

            if (!user.ValidatePassword(password, _encryption))
                throw new ActioException("invalid_credentials", "Invalid Credentials");

            return _jwtHandler.Create(user.Id);
        }
    }
}