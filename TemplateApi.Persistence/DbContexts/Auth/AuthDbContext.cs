using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TemplateApi.Persistence.Configurations.AuthDomain;
using TemplateApi.Persistence.Markers;

namespace TemplateApi.Persistence.DbContexts.Auth
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

            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
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
