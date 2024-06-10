using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Handlers
{
    internal class GetDetailsDiseaseQueryHandler : IRequestHandler<GetDetailsDiseaseQueryRequest, ResponseAPI<DiseaseDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDetailsDiseaseQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<DiseaseDTO>> Handle(GetDetailsDiseaseQueryRequest request, CancellationToken cancellationToken)
        {
           
            try
            {
                //Kiểm tra tồn tại
                var validation = await _entities.DiseaseService.GetById(request.Id);

                if(validation == null)
                    return new ResponseErrorAPI<DiseaseDTO>(StatusCodes.Status404NotFound, "Bệnh không tồn tại.");

                var disease = _mapper.Map<DiseaseDTO>(validation);

                return new ResponseSuccessAPI<DiseaseDTO>(StatusCodes.Status200OK, "Thông tin bệnh", disease);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<DiseaseDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
