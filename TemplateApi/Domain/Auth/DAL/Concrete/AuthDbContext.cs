using Microsoft.EntityFrameworkCore;
using TemplateApi.Domain.Auth.Entities;

namespace TemplateApi.Domain.Auth.DAL.Concrete
{
    public class AuthDbContext : DbContext
    {
        private const string DefaultSchema = "Auth";

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<UserCredential> UserCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
