using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entities;
using Auth.Domain.DAL.Constants;

namespace Auth.Domain.DAL.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableNames.Roles);

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasMany(x => x.Credentials)
                .WithMany();

            builder.HasData(Role.GetValues());
        }
    }
}
