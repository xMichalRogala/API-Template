using Core.Domain.Schemas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TemplateApi.Persistence.Configurations.CoreDomain
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        private const string _userSchema = "Core";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users12", _userSchema);

            builder.HasIndex(x => x.Login)
                .IsUnique();
        }
    }
}
