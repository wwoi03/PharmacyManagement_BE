using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.Queries.RoleFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.RoleFeatures.Handlers
{
    internal class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQueryRequest, ResponseAPI<List<RoleResponse>>>
    {
        private readonly IPMEntities _entities;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;

        public GetAllRoleQueryHandler(IPMEntities entities, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper)
        {
            this._entities = entities;
            this._roleManager = roleManager;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<RoleResponse>>> Handle(GetAllRoleQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = _roleManager.Roles.ToList();
                var response = _mapper.Map<List<RoleResponse>>(roles);

                return new ResponseSuccessAPI<List<RoleResponse>>(StatusCodes.Status200OK, string.Empty, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<RoleResponse>> ("Lỗi hệ thống.");
            }
        }
    }
}
