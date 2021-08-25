using DatingWebAppScratch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebAppScratch.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }

    }
}
