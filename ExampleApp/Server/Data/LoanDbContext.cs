using ExampleApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Server.Data
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerApplication> CustomerApplications { get; set; }
    }
}
