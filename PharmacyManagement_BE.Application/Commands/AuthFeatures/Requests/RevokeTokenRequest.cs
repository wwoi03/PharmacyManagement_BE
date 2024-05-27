using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.AuthFeatures.Requests
{
    public class RevokeTokenRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
    }
}
