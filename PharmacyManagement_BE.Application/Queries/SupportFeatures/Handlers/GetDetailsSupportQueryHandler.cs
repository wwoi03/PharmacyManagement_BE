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
    internal class GetDetailsSupportQueryHandler : IRequestHandler<GetDetailsSupportQueryRequest, ResponseAPI<SupportDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDetailsSupportQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<SupportDTO>> Handle(GetDetailsSupportQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var validation = await _entities.SupportService.GetById(request.Id);

                if (validation == null)
                    return new ResponseErrorAPI<SupportDTO>(StatusCodes.Status404NotFound, "Hỗ trợ của thuốc không tồn tại.");

                var Support = _mapper.Map<SupportDTO>(validation);

                return new ResponseSuccessAPI<SupportDTO>(StatusCodes.Status200OK, "Thông tin hỗ trợ của thuốc", Support);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<SupportDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
