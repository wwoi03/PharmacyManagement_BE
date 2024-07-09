using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Requests
{
    public class CreateProductSupportCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid SupportId { get; set; }
        public Guid ProductId { get; set; }
    }
}
