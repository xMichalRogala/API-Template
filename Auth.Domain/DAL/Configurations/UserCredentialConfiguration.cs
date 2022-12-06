using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auth.Domain.DAL.Constants;
using Auth.Domain.Entities;

namespace Auth.Domain.DAL.Configurations
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
