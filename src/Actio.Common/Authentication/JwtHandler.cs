using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Actio.Common.Authentication
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly JwtOptions _options;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;


        public JwtHandler(IOptions<JwtOptions> options)
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _options = options.Value;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpiryInMinutes);
            var now = nowUtc.ToEpochTimeSpan().TotalSeconds.ToInt64();
            var exp = expires.ToEpochTimeSpan().TotalSeconds.ToInt64();

            var payload = new JwtPayload
            {
                {"sub", userId},
                {"iss", _options.Issuer},
                {"exp", exp},
                {"iat", now},
                {"unqiue_name", userId}
            };

            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}