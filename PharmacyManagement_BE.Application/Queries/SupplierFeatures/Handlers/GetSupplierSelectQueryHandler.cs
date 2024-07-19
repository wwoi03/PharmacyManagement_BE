using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.SupplierFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupplierDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupplierFeatures.Handlers
{
    internal class GetSupplierSelectQueryHandler : IRequestHandler<GetSupplierSelectQueryRequest, ResponseAPI<List<SelectSupplierDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetSupplierSelectQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<SelectSupplierDTO>>> Handle(GetSupplierSelectQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.SupplierService.GetSuppliersSelect();

                return new ResponseSuccessAPI<List<SelectSupplierDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<SelectSupplierDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
