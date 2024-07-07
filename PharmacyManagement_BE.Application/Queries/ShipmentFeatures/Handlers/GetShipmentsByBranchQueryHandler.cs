using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Handlers
{
    internal class GetShipmentsByBranchQueryHandler : IRequestHandler<GetShipmentsByBranchQueryRequest, ResponseAPI<List<ShipmentResponse>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetShipmentsByBranchQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<ShipmentResponse>>> Handle(GetShipmentsByBranchQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // kiểm tra chi nhánh tồn tại
                var branchId = await _entities.AccountService.GetBranchId();
                var branchExists = await _entities.BranchService.GetById(branchId); ;

                if (branchExists == null)
                {
                    var validation = new ValidationNotifyError<string>()
                    {
                        Obj = "default",
                        Message = "Chi nhánh không tồn tại."
                    };
                    return new ResponseSuccessAPI<List<ShipmentResponse>>(StatusCodes.Status404NotFound, validation);
                }

                var shipmentDTOs = await _entities.ShipmentService.GetShipmentsByBranch(branchId);
                var response = _mapper.Map<List<ShipmentResponse>>(shipmentDTOs);

                return new ResponseSuccessAPI<List<ShipmentResponse>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ShipmentResponse>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
