using AutoMapper;
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
    internal class SearchDiseaseQueryHandler : IRequestHandler<SearchDiseaseQueryRequest, ResponseAPI<List<DiseaseDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public SearchDiseaseQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<DiseaseDTO>>> Handle(SearchDiseaseQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<List<DiseaseDTO>>(StatusCodes.Status400BadRequest, validation.Message);


                // Tìm kiếm bệnh theo tên gần đúng
                var listDisease = await _entities.DiseaseService.Search(request.KeyWord, cancellationToken);

                ////Kiểm tra danh sách
                //if (listDisease == null || listDisease.Count == 0)
                //    return new ResponseErrorAPI<List<DiseaseDTO>>(StatusCodes.Status404NotFound, "Không tìm thấy loại bệnh");

                //Gán giá trị response
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
