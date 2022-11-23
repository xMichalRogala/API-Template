using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateApi.Domain.Core.Models;

namespace TemplateApi.Domain.Core
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        private const string _userSchema = "core";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", _userSchema);
        }
    }
}
