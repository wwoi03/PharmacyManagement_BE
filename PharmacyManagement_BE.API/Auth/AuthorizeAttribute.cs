using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagement_BE.API.Auth
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] policies) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { policies };
        }
    }
}
