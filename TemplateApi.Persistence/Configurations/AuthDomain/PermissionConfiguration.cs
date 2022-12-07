using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entities;
using TemplateApi.Persistence.Constants;
using TemplateApi.Persistence.Markers;

namespace TemplateApi.Persistence.Configurations.AuthDomain
{
    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>, IAuthConfiguration
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableNames.Permissions);

            builder.HasKey(p => p.Id);

            IEnumerable<Permission> permissions = Enum
                .GetValues<Auth.Domain.Enums.Permission>()
                .Select(p => new Permission
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            builder.HasData(permissions);
        }
    }
}
