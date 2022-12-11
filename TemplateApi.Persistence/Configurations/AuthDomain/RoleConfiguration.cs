using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Schemas.Entities;
using TemplateApi.Persistence.Constants;
using TemplateApi.Persistence.Markers;

namespace TemplateApi.Persistence.Configurations.AuthDomain
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>, IAuthConfiguration
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableNames.Roles);

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasMany(x => x.Credentials)
                .WithMany(x => x.Roles);

            builder.HasData(Role.GetValues());
        }
    }
}
