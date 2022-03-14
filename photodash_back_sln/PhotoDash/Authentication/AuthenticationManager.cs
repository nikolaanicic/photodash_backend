using Contracts.Authentication;
using Entities.Dtos.UserDtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoDash.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {

        private UserManager<User> _userManager;
        private IConfiguration _configuration;
        private User _toAuthenticate;

        public AuthenticationManager(UserManager<User> manager,IConfiguration configuration)
        {
            _userManager = manager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GetJwtSecurityToken(signingCredentials,claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _toAuthenticate = await _userManager.FindByNameAsync(userForAuth.UserName);
            return (_toAuthenticate != null && await _userManager.CheckPasswordAsync(_toAuthenticate, userForAuth.Password));
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("SECRET");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IList<Claim>> GetClaims()
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _toAuthenticate.UserName) };
            var roles = await _userManager.GetRolesAsync(_toAuthenticate);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }


        private JwtSecurityToken GetJwtSecurityToken(SigningCredentials credentials, IList<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(issuer: jwtSettings.GetSection("validIssuer").Value,
                signingCredentials: credentials,
                claims: claims,
                audience: jwtSettings.GetSection("validAudience").Value,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)));

            return tokenOptions;
        }

    }
}
