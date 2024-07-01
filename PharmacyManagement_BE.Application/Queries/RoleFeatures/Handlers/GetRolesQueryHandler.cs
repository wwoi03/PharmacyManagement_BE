using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Queries.RoleFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.RoleDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.RoleFeatures.Handlers
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, ResponseAPI<List<ListRoleDTO>>>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public GetRolesQueryHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<List<ListRoleDTO>>> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Lấy danh sách role
                var roles = _roleManager.Roles.ToList();

                List<ListRoleDTO> response = roles.Select(i => new ListRoleDTO
                {
                    RoleName = i.Name,
                    RoleNormalizedName = i.NormalizedName,
                }).ToList();

                return new ResponseSuccessAPI<List<ListRoleDTO>>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<List<ListRoleDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
