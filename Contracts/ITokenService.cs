using DatingWebAppScratch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebAppScratch.Contracts
{
    public interface ITokenService
    {
         string CreateTokenService(AppUser user);
    }
}
