using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace smaaahh_api
{
    public class AuthService
    {
        private readonly MemberShipProvider _membershipProvider;
        private readonly RSAKeyProvider _rsaProvider;

        public AuthService(MemberShipProvider membershipProvider, RSAKeyProvider rsaProvider)
        {
            _membershipProvider = membershipProvider;
            _rsaProvider = rsaProvider;
        }
        public AuthService()
        {

        }

        public async Task<string> GenerateJwtTokenAsync(string email, string password, string type)
        {
            if (!_membershipProvider.VerifyPassword(email, password, type))
                return "Wrong access";

            List<Claim> claims = _membershipProvider.GetUserClaims(email);

            string publicAndPrivateKey = await _rsaProvider.GetPrivateAndPublicKeyAsync();
            if (publicAndPrivateKey == null)
                return "RSA key error";

            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            publicAndPrivate.FromXmlString(publicAndPrivateKey);

            RsaSecurityKey s = new RsaSecurityKey(publicAndPrivate);
            JwtSecurityToken jwtToken = new JwtSecurityToken
            (
                issuer: "http://localhost",
                audience: "http://localhost",
                claims: claims,
                signingCredentials: new SigningCredentials(s, SecurityAlgorithms.RsaSha256Signature),
                expires: DateTime.Now.AddDays(30)
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwtToken);

            return tokenString;
        }

        public async Task<Boolean> ValidateTokenAsync(string TokenString)
        {
            Boolean result = false;

            try
            {
                SecurityToken securityToken = new JwtSecurityToken(TokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();

                string publicAndPrivateKey = await _rsaProvider.GetPrivateAndPublicKeyAsync();
                if (publicAndPrivateKey == null)
                    return result;

                publicAndPrivate.FromXmlString(publicAndPrivateKey);

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost",
                    ValidAudience = "http://localhost",
                    IssuerSigningKey = new RsaSecurityKey(publicAndPrivate)
                };

                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public async Task<ClaimsPrincipal> GetClaimTokenAsync(string TokenString)
        {
            SecurityToken securityToken = new JwtSecurityToken(TokenString);
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();

            string publicAndPrivateKey = await _rsaProvider.GetPrivateAndPublicKeyAsync();

            publicAndPrivate.FromXmlString(publicAndPrivateKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "http://localhost",
                ValidAudience = "http://localhost",
                IssuerSigningKey = new RsaSecurityKey(publicAndPrivate)
            };

            ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);

            return claimsPrincipal;
        }
    }
}