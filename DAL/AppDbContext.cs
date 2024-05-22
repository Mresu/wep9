using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication9.Models;

namespace WebApplication9.DAL
{
    public class AppDbContext : IdentityDbContext

    {
        internal object Portfolio;

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Portfolio> portfolios { get; set; }
    }
}
