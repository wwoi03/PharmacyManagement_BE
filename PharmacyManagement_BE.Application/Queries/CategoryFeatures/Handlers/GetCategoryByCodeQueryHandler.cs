using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.CategoryFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CategoryFeatures.Handlers
{
    internal class GetCategoryByCodeQueryHandler : IRequestHandler<GetCategoryByCodeQueryRequest, ResponseAPI<Category?>>
    {
        private readonly IPMEntities _entities;

        public GetCategoryByCodeQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<Category?>> Handle(GetCategoryByCodeQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.CategoryService.GetCategoryByCode(request.CodeCategory);

                if (response == null)
                {
                    return new ResponseSuccessAPI<Category?>(StatusCodes.Status409Conflict, response);
                }
                else
                {
                    return new ResponseSuccessAPI<Category?>(StatusCodes.Status200OK, response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<Category?>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
