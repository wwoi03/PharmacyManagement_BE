using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Application.Queries.SymptomFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SymptomFeatures.Handlers
{
    internal class GetSymptomsQueryHandler : IRequestHandler<GetSymptomsQueryRequest, ResponseAPI<List<SymptomDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetSymptomsQueryHandler (IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<SymptomDTO>>> Handle(GetSymptomsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách triệu chứng
                var listSymptom = await _entities.SymptomService.GetAll();


                //Gán danh sách triệu chứng thành response
                var response = _mapper.Map<List<SymptomDTO>>(listSymptom);

                //Kiểm tra danh sách
                if (response == null || response.Count == 0)
                    return new ResponseErrorAPI<List<SymptomDTO>>(StatusCodes.Status404NotFound, "Không tìm thấy danh sách triệu chứng");

                //Trả về danh sách
                return new ResponseSuccessAPI<List<SymptomDTO>>(StatusCodes.Status200OK, "Danh sách triệu chứng", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<SymptomDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
