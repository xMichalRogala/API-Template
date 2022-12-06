using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateApi.Domain.Core.Entities;

namespace TemplateApi.Domain.Core.DAL
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
