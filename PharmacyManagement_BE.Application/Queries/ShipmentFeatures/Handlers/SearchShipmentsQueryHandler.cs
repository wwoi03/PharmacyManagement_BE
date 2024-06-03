using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentFeatures.Handlers
{
    internal class SearchShipmentsQueryHandler : IRequestHandler<SearchShipmentsQueryRequest, ResponseAPI<List<ShipmentResponse>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public SearchShipmentsQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<ShipmentResponse>>> Handle(SearchShipmentsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // kiểm tra chi nhánh tồn tại
                var branchExists = await _entities.BranchService.GetById(request.BranchId);

                if (branchExists == null)
                    return new ResponseErrorAPI<List<ShipmentResponse>>(StatusCodes.Status404NotFound, "Chi nhánh không tồn tại.");

                var shipmentDTOs = await _entities.ShipmentService.SearchShipments(request.BranchId, request.FromDate, request.ToDate, request.SupplierName);
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
