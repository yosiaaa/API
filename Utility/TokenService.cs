﻿using API.ViewModels.Others;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utility
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims); //claim == payload
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        ClaimVM ExtractClaimsFromJwt(string token);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimVM ExtractClaimsFromJwt(string token)
        {
            if (token.IsNullOrEmpty()) return new ClaimVM();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // will be true if we create an issuer
                    ValidateAudience = false, // will be true if we create an audience
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token,
                                                                tokenValidationParameters,
                                                                out var securityToken);

                if(securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var claims = new ClaimVM
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value,
                    };

                    var roles = identity.Claims
                                        .Where(c => c.Type == ClaimTypes.Role)
                                        .Select(claim => claim.Value).ToList();

                    claims.Roles = roles;
                    return claims;
                }
            }
            catch
            {
                // if an error occurs while parsing the JWT token, return an empty object.
                return new ClaimVM();
            }

            return new ClaimVM();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding
                                                    .UTF8
                                                    .GetBytes(_configuration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); // JWS

            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                                                    audience: _configuration["JWT:Audience"],
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(10),
                                                    signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
