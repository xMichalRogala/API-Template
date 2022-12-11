using Auth.Domain.Schemas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateApi.Persistence.Constants;
using TemplateApi.Persistence.Markers;

namespace TemplateApi.Persistence.Configurations.AuthDomain
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>, IAuthConfiguration
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.ToTable(TableNames.UserCredentials);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Login).IsUnique();
        }
    }
}
