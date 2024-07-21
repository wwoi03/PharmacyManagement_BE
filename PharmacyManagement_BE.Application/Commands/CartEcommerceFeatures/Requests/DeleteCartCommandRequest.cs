using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CartEcommerceFeatures.Requests
{
    public class DeleteCartCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid CartId { get; set; }
    }
}
