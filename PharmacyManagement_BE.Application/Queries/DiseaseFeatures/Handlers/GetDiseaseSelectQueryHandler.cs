using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Handlers
{
    internal class GetDiseaseSelectQueryHandler : IRequestHandler<GetDiseaseSelectQueryRequest, ResponseAPI<List<SelectDiseaseDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetDiseaseSelectQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<SelectDiseaseDTO>>> Handle(GetDiseaseSelectQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.DiseaseService.GetDiseaseSelect();

                return new ResponseSuccessAPI<List<SelectDiseaseDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<SelectDiseaseDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
