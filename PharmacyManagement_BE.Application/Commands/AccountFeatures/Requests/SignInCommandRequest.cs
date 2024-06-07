using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.AccountFeatures.Requests
{
    public class SignInCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
