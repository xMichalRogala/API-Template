using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entities;
using Permission = Auth.Domain.Enums.Permission;

namespace Auth.Domain.DAL.Configurations
{
    internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable(nameof(RolePermission));
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.HasData(
                Create(Role.Registered, Permission.Access),
                Create(Role.Registered, Permission.Read));
        }

        private static RolePermission Create(
            Role role, Permission permission)
        {
            return new RolePermission
            {
                RoleId = role.Id,
                PermissionId = (int)permission
            };
        }
    }
}
