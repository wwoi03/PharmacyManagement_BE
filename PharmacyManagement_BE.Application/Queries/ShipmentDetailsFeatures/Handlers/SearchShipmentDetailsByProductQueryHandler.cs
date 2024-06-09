using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.ShipmentDetailsFeatures.Handlers
{
    internal class SearchShipmentDetailsByProductQueryHandler : IRequestHandler<SearchShipmentDetailsByProductQueryRequest, ResponseAPI<List<ListShipmentDetailsDTO>>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public SearchShipmentDetailsByProductQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<ListShipmentDetailsDTO>>> Handle(SearchShipmentDetailsByProductQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _entities.ShipmentDetailsService.SearchShipmentDetailsByProduct(request.ShipmentId, request.NameOrCodeMedicine);

                return new ResponseSuccessAPI<List<ListShipmentDetailsDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ListShipmentDetailsDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
