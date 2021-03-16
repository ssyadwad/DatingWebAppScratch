using DatingWebAppScratch.Contracts;
using DatingWebAppScratch.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingWebAppScratch.Services
{
    public class CreateTokenService : ITokenService
    {
        private IConfiguration _config;
        private SymmetricSecurityKey _key;
        public CreateTokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]));
        }
        /// <summary>
        /// 1. Create a claim
        /// 2. Create Signing credentials
        /// 3. Create a token discreptors
        /// and then write it to handler
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string ITokenService.CreateTokenService(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = credentials

            };

            var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token= JwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
