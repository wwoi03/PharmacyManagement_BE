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
    internal class GetSupportsQueryHandler : IRequestHandler<GetSupportsQueryRequest, ResponseAPI<List<SupportDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetSupportsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<SupportDTO>>> Handle(GetSupportsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách hỗ trợ của thuốc
                var listSupport = await _entities.SupportService.GetAll();


                //Gán danh sách hỗ trợ của thuốc thành response
                var response = _mapper.Map<List<SupportDTO>>(listSupport);

                //Kiểm tra danh sách
                if (response == null || response.Count == 0)
                    return new ResponseErrorAPI<List<SupportDTO>>(StatusCodes.Status404NotFound, "Không tìm thấy danh sách hỗ trợ của thuốc");

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
