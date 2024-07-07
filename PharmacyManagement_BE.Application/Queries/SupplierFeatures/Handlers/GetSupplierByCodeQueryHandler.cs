using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.SupplierFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupplierFeatures.Handlers
{
    internal class GetSupplierByCodeQueryHandler : IRequestHandler<GetSupplierByCodeQueryRequest, ResponseAPI<Supplier>>
    {
        private readonly IPMEntities _entities;

        public GetSupplierByCodeQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<Supplier>> Handle(GetSupplierByCodeQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.SupplierService.GetSupplierByCode(request.CodeSupplier);

                if (response == null)
                {
                    return new ResponseSuccessAPI<Supplier>(StatusCodes.Status409Conflict);
                }

                return new ResponseSuccessAPI<Supplier>(StatusCodes.Status200OK,  response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<Supplier>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống");
            }
        }
    }
}
