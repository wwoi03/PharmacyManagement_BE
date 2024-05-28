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
    internal class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQueryRequest, ResponseAPI<StaffResponse>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public GetStaffByIdQueryHandler(IPMEntities entities, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<StaffResponse>> Handle(GetStaffByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.Id.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<StaffResponse>(StatusCodes.Status404NotFound, "Nhân viên không tồn tại.");

                // Lấy danh sách role của nhân viên
                var userRoles = await _userManager.GetRolesAsync(userExists);

                // Chuyển đổi thông tin trả về view
                var response = _mapper.Map<StaffResponse>(userExists);
                response.Roles = (List<string>)userRoles;

                return new ResponseSuccessAPI<StaffResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<StaffResponse>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
