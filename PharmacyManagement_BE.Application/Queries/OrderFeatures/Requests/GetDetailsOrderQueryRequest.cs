using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.OrderFeatures.Requests
{
    public class GetDetailsOrderQueryRequest : IRequest<ResponseAPI<OrderDTO>>
    {
        public Guid Id { get; set; }
    }
}
