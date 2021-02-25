using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DatingWebAppScratch.Contracts;
using DatingWebAppScratch.Data;
using DatingWebAppScratch.Models;
using DatingWebAppScratch.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingWebAppScratch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AppDbContext dbContext;
        private ITokenService _tokenService;
        private int iterationCount = 10000;
        private const int _saltSize = 128;
        private const int _hashSize = 128;
        public AccountController(AppDbContext dbContext,ITokenService tokenService)
        {
            this.dbContext = dbContext;
            _tokenService = tokenService;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
        {
            try
            {
                if (await UserExistsAsync(loginDto.UserName))
                {
                    AppUser user = GetUser(loginDto.UserName);
                    {
                        if (user == null)
                        {
                            return Unauthorized();
                        }
                        else
                        {
                            if (VerifyPassword(user, loginDto.Password))
                            {
                              return   new UserDto() {token= _tokenService.CreateTokenService(user) ,UserName=user.UserName};
                            }
                            else
                            {
                                return Unauthorized();
                            }
                        }
                    }
                }
                return BadRequest("Invalid request!");
                
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid request!");
            }
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AppUser>> Register([FromBody]UserViewModel userViewModel)
        {
            AppUser user = null;
            try
            {
                if (await UserExistsAsync(userViewModel.UserName))
                {
                    return BadRequest("User Already exists");
                }
                else
                {
                    if (string.IsNullOrEmpty(userViewModel.UserName) || string.IsNullOrEmpty(userViewModel.Password))
                    {
                        return BadRequest("One of the field is empty!!");
                    }
                    else
                    {
                        GetPasswordHash(userViewModel.Password, out PasswordHashSalt passwordHashSalt);

                        user = new AppUser()
                        {
                            UserName = userViewModel.UserName,
                            Password = passwordHashSalt.password,
                            PasswordSalt = passwordHashSalt.passwordSalt
                        };
                        dbContext.Users.Add(user);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return user;
        }

        private  AppUser GetUser(string UserName)
        {
            try
            {
                return dbContext.Users.SingleOrDefault(x => x.UserName.ToLower() == UserName.ToLower());
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        private async Task<bool> UserExistsAsync(string username)
        {
            return await dbContext.Users.AnyAsync(x=>x.UserName.ToLower()==username.ToLower());
        }
        public class PasswordHashSalt
        {
            public byte[] password;
            public byte[] passwordSalt;
        }
        public void GetPasswordHash(string Password, out PasswordHashSalt passwordhashSalt)
        {
            byte[] Salt = new byte[_saltSize];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(Salt);


            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, Salt, iterationCount);
            PasswordHashSalt salt = new PasswordHashSalt() { passwordSalt = Salt,
                password = rfc2898DeriveBytes.GetBytes(_hashSize) };
            passwordhashSalt = salt;
        }

        public bool VerifyPassword(AppUser user,string Password)
        {
            try
            {
                byte[] salt, password;

                password = user.Password;
                salt = user.PasswordSalt;

                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, salt, iterationCount);
                return SlowEquals(user.Password, rfc2898DeriveBytes.GetBytes(_hashSize));
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

    }
}