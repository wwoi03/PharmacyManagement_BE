using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.RoleFeatures.Requests
{
    public class CreateRoleCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
