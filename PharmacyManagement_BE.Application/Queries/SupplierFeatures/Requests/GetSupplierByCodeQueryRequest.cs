using MediatR;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupplierFeatures.Requests
{
    public class GetSupplierByCodeQueryRequest : IRequest<ResponseAPI<Supplier>>
    {
        public string CodeSupplier { get; set; }
    }
}
