using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    internal class GetDetailsSymptomQueryHandler : IRequestHandler<GetDetailsSymptomQueryRequest, ResponseAPI<SymptomDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDetailsSymptomQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<SymptomDTO>> Handle(GetDetailsSymptomQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var validation = await _entities.SymptomService.GetById(request.Id);

                if (validation == null)
                    return new ResponseErrorAPI<SymptomDTO>(StatusCodes.Status404NotFound, "Triệu chứng không tồn tại.");

                var Symptom = _mapper.Map<SymptomDTO>(validation);

                return new ResponseSuccessAPI<SymptomDTO>(StatusCodes.Status200OK, "Thông tin triệu chứng", Symptom);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<SymptomDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
