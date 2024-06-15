using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Handlers
{
    internal class GetDiseasesQueryHandler : IRequestHandler<GetDiseasesQueryRequest, ResponseAPI<List<DiseaseDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDiseasesQueryHandler( IPMEntities entities,IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<DiseaseDTO>>> Handle(GetDiseasesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách bệnh
                var listDisease = await _entities.DiseaseService.GetAll();

                //Kiểm tra danh sách
                //if (listDisease == null || listDisease.Count == 0)
                //    return new ResponseErrorAPI<List<DiseaseDTO>>(StatusCodes.Status404NotFound, "Không tìm thấy danh sách bệnh");

                //Gán danh sách bệnh thành response
                var response = _mapper.Map<List<DiseaseDTO>>(listDisease);

                //Trả về danh sách
                return new ResponseSuccessAPI<List<DiseaseDTO>>(StatusCodes.Status200OK, "Danh sách bệnh", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<DiseaseDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
