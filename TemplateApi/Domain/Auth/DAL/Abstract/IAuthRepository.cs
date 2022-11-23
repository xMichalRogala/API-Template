using TemplateApi.Commons.Data.Repository;
using TemplateApi.Domain.Auth.Entities;

namespace TemplateApi.Domain.Auth.DAL.Abstract
{
    public interface IAuthRepository : IGenericRepository<UserCredential, Guid>
    {
    }
}
