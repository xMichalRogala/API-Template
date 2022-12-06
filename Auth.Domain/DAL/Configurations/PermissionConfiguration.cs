using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.DAL.Constants;
using Auth.Domain.Entities;

namespace Auth.Domain.DAL.Configurations
{
    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableNames.Permissions);

            builder.HasKey(p => p.Id);

            IEnumerable<Permission> permissions = Enum
                .GetValues<Enums.Permission>()
                .Select(p => new Permission
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            builder.HasData(permissions);
        }
    }
}
