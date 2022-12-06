using Microsoft.EntityFrameworkCore;
using Auth.Domain.DAL.Configurations;
using Auth.Domain.Entities;

namespace Auth.Domain.DAL.Concrete
{
    public class AuthDbContext : DbContext
    {
        private const string _defaultSchema = "Auth";

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<UserCredential> UserCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_defaultSchema);

            modelBuilder.ApplyConfiguration(new PermissionConfiguration()); //todo create empty mark interface or use default and load with reflection
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
