using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;
using System;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }

        public User(string email, string name)
        {
            if (email.IsNullOrWhiteSpace())
                throw new ActioException("empty_user_email", "User email can not be empty");

            if (!email.IsValidEmail())
                throw new ActioException("invalid_user_email", "User email was not valid");

            if (name.IsNullOrWhiteSpace())
                throw new ActioException("empty_user_name", "User name can not be empty");

            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IEncryption encryption)
        {
            if (password.IsNullOrWhiteSpace())
                throw new ActioException("empty_user_password", "User password can not be empty");

            Salt = encryption.GetSalt(password);
            Password = encryption.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncryption encryption) =>
            Password.Equals(encryption.GetHash(password, Salt));
    }
}
