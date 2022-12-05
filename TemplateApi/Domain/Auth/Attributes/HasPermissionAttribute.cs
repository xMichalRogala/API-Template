using Microsoft.AspNetCore.Authorization;
using TemplateApi.Domain.Auth.Enums;

namespace TemplateApi.Domain.Auth.Attributes
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString()) 
        {
        
        }
    }
}
