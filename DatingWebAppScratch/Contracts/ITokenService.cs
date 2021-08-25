using DatingWebAppScratch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebAppScratch.Contracts
{
    /// <summary>
    /// This is an ITokenService 
    /// </summary>
    public interface ITokenService
    {
         string CreateTokenService(AppUser user);
    }
}
