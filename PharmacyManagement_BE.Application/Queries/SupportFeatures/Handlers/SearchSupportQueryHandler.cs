using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.SupportFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SupportDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.SupportFeatures.Handlers
{
    internal class SearchSupportQueryHandler : IRequestHandler<SearchSupportQueryRequest, ResponseAPI<List<SupportDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public SearchSupportQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<SupportDTO>>> Handle(SearchSupportQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<List<SupportDTO>>(StatusCodes.Status400BadRequest, validation.Message);

                // Tìm kiếm hỗ trợ của thuốc theo tên gần đúng
                var listSupport = await _entities.SupportService.Search(request.KeyWord, cancellationToken);

                //Kiểm tra danh sách
                if (listSupport == null || listSupport.Count == 0)
                    return new ResponseErrorAPI<List<SupportDTO>>(StatusCodes.Status404NotFound, "Không tìm thấy hỗ trợ của thuốc");

                //Gán giá trị response
                var response = _mapper.Map<List<SupportDTO>>(listSupport);


                //Trả về danh sách
                return new ResponseSuccessAPI<List<SupportDTO>>(StatusCodes.Status200OK, "Danh sách hỗ trợ của thuốc", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<SupportDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
