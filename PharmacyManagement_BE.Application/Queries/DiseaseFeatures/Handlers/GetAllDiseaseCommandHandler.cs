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
    public class GetAllDiseaseCommandHandler : IRequestHandler<GetAllDiseaseCommandRequest, ResponseAPI<List<DetailsDiseaseResponse>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetAllDiseaseCommandHandler( IPMEntities entities,IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<DetailsDiseaseResponse>>> Handle(GetAllDiseaseCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách bệnh
                var listDisease = await _entities.DiseaseService.GetAllDisease(cancellationToken);


                //Gán danh sách bệnh thành response
                var response = _mapper.Map<List<DetailsDiseaseResponse>>(listDisease);

                //Kiểm tra danh sách
                if (response == null || response.Count == 0)
                    return new ResponseErrorAPI<List<DetailsDiseaseResponse>>(StatusCodes.Status404NotFound, "Không tìm thấy danh sách bệnh");

                //Trả về danh sách
                return new ResponseSuccessAPI<List<DetailsDiseaseResponse>>(StatusCodes.Status200OK, "Danh sách bệnh", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<DetailsDiseaseResponse>>("Lỗi hệ thống.");
            }
        }
    }
}
