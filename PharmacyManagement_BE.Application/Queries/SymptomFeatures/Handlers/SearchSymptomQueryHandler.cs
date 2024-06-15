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
    internal class SearchSymptomQueryHandler : IRequestHandler<SearchSymptomQueryRequest, ResponseAPI<List<SymptomDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public SearchSymptomQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<SymptomDTO>>> Handle(SearchSymptomQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<List<SymptomDTO>>(StatusCodes.Status400BadRequest, validation.Message);

                // Tìm kiếm triệu chứng theo tên gần đúng
                var listSymptom = await _entities.SymptomService.Search(request.KeyWord, cancellationToken);
                
                ////Kiểm tra danh sách
                //if (listSymptom == null || listSymptom.Count == 0)
                //    return new ResponseErrorAPI<List<SymptomDTO>>(StatusCodes.Status404NotFound, "Không tìm thấy triệu chứng");

                //Gán giá trị response
                var response = _mapper.Map<List<SymptomDTO>>(listSymptom);

                
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
