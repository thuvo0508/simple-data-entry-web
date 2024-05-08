using Microsoft.EntityFrameworkCore;
using SerMis.Models;
using System.Collections.Generic;

namespace SerMis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
