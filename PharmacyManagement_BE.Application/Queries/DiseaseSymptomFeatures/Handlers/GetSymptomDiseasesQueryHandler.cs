using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.DiseaseSymptomFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.DiseaseSymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseSymptomFeatures.Handlers
{
    internal class GetSymptomDiseasesQueryHandler : IRequestHandler<GetSymptomDiseasesQueryRequest, ResponseAPI<List<DiseaseSymptomDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetSymptomDiseasesQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<DiseaseSymptomDTO>>> Handle(GetSymptomDiseasesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách bệnh
                var list = await _entities.DiseaseSymptomService.GetAllBySymptom(request.Id);

                //Gán danh sách bệnh thành response
                var response = _mapper.Map<List<DiseaseSymptomDTO>>(list);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<DiseaseSymptomDTO>>(StatusCodes.Status200OK, "Danh sách quan hệ", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<DiseaseSymptomDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
