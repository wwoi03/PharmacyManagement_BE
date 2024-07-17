using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ProductSupportFeatures.Requests
{
    public class DeleteProductSupportCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid ProductId { get; set; }
        public Guid SupportId { get; set; }
    }
}
