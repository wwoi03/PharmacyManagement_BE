using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.OrderEcommerceFeatures.Request
{
    public class GetOrdersQueryRequest : IRequest<ResponseAPI<List<ItemOrderDTO>>>
    {
    }
}
