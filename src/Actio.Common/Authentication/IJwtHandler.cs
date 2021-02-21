using System;

namespace Actio.Common.Authentication
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}
