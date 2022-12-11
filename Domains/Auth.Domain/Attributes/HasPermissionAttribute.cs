using Auth.Domain.Schemas.Entities;
using Microsoft.AspNetCore.Authorization;

namespace  Auth.Domain.Attributes
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString()) 
        {
        
        }
    }
}
