using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Application.Queries.StaffFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StaffFeatures.Handlers
{
    internal class GetRolesStaffQueryHandler : IRequestHandler<GetRolesStaffQueryRequest, ResponseAPI<RolesStaffResponse>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public GetRolesStaffQueryHandler(IPMEntities entities, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<RolesStaffResponse>> Handle(GetRolesStaffQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.StaffId.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<RolesStaffResponse>(StatusCodes.Status404NotFound, "Nhân viên không tồn tại.");

                // Lấy danh sách role hiện tại
                var currentRoles = await _userManager.GetRolesAsync(userExists);

                var response = new RolesStaffResponse()
                {
                    StaffId = userExists.Id,
                    FullName = userExists.FullName,
                    Roles = currentRoles.ToList()
                };

                return new ResponseSuccessAPI<RolesStaffResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<RolesStaffResponse>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
