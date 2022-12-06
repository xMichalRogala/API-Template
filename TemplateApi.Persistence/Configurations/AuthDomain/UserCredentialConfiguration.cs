using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auth.Domain.Entities;
using TemplateApi.Persistence.Constants;

namespace TemplateApi.Persistence.Configurations.AuthDomain
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.ToTable(TableNames.UserCredentials);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Login).IsUnique();
        }
    }
}
