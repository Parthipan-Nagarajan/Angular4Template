using ECommerce.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Api.TokenManagement
{
    public class TokenManager
    {
        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="userInformation"></param>
        /// <returns></returns>
        public static string CreateToken(Guid custID)
        {
            string customerID = custID.ToString();
            var claims = new List<Claim>();
            claims.Add(CreateClaim("CustomerID", customerID));

            var tokenHandler = new JwtSecurityTokenHandler();

            const string SecretKey = "mysupersecret_secretkey!123";
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new ClaimsIdentity(claims)),
                Issuer = "ECommerce",
                Audience = "Public",
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public static ClaimsPrincipal ValidateToken(string tokenString)
        {
            const string SecretKey = "mysupersecret_secretkey!123";
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "ECommerce",
                ValidAudience = "Public",
                ValidateLifetime = true,
                ValidateIssuer = true,
                RequireExpirationTime = true,
                IssuerSigningKey = _signingKey

            };

            SecurityToken token = new JwtSecurityToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(tokenString, validationParameters, out token);

                return principal;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }

            return null;

        }

        /// <summary>
        /// Create Claim
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value);
        }

        /// <summary>
        /// Get Bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;

        }

        /// <summary>
        /// Get UserID
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public static Guid GetUserID(string tokenString)
        {
            ClaimsPrincipal principal = TokenManager.ValidateToken(tokenString);
            var claim = principal.Claims.Where(p => p.Type == "CustomerID").SingleOrDefault();
            if (claim == null)
            {
                return Guid.Empty;
            }

            Guid customerID = Guid.Parse(claim.Value);

            return customerID;

        }

    }
}
 