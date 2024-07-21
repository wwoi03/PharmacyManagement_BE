using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CartEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CartEcommerceFeatures.Requests
{
    public class GetCartQueryRequest : IRequest<ResponseAPI<List<ItemCartDTO>>>
    {
    }
}
