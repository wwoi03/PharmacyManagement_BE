using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagement_BE.Infrastructure.Common.Securitys
{
    public class AuthorizeOrAttribute : TypeFilterAttribute
    {
        public AuthorizeOrAttribute(string roles) : base(typeof(AuthorizeOrFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}