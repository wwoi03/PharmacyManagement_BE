using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ProductFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ProductFeatures.Handlers
{
    internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQueryRequest, ResponseAPI<DetailsProductDTO>>
    {
        private readonly PMEntities _entities;

        public GetProductDetailsQueryHandler(PMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<DetailsProductDTO>> Handle(GetProductDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {

                return new ResponseSuccessAPI<DetailsProductDTO>(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<DetailsProductDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
