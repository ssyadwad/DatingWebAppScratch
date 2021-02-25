using DatingWebAppScratch.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DatingWebAppScratch.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DatingWebAppScratch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Users:ControllerBase
    {
        private AppDbContext _dbContext { get; set; }
        public Users(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet("{id}")]
        public async Task<AppUser> GetUsers(int id)
        {
            return  await _dbContext.Users.FindAsync(id);
        }
       
        [Authorize]
        [HttpGet]
        public List<AppUser> Get()
        {
            return _dbContext.Users.ToList();
        }
    }
}
