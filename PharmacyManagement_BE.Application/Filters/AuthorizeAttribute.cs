using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Filters
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(PMAuthorizeActionFilter))
        {

        }

        public AuthorizeAttribute(string roles) : base(typeof(PMAuthorizeActionFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
