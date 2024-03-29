﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Schemas.Entities;
using Permission = Auth.Domain.Schemas.Enums.Permission;
using TemplateApi.Persistence.Markers;

namespace TemplateApi.Persistence.Configurations.AuthDomain
{
    internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>, IAuthConfiguration
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable(nameof(RolePermission));
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.HasData(
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
