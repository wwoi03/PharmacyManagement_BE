using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.AccountFeatures.Requests
{
    public class LockAccountCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid UserId { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}
