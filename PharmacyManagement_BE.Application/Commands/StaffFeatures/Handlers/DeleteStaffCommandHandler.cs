using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.StaffFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.StaffFeatures.Handlers
{
    internal class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public DeleteStaffCommandHandler(IPMEntities entities, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<ResponseAPI<string>> Handle(DeleteStaffCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra nhân viên tồn tại
                var userExists = await _userManager.FindByIdAsync(request.Id.ToString());

                if (userExists == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Nhân viên không tồn tại.");

                // Kiểm tra nhân viên đã có dữ liệu trong nhập kho
                var shipments = await _entities.ShipmentService.GetAllShipmentByStaffId(request.Id);
                if (shipments.Count > 0)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Hiện tại không thể xóa nhân viên.");

                // Kiểm tra nhân viên đã có dữ liệu trong đặt hàng
                var orders = await _entities.OrderService.GetAllOrderByStaffId(request.Id);
                if (orders.Count > 0)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Hiện tại không thể xóa nhân viên.");

                // Xóa các quyền của nhân viên
                var staff = await _userManager.FindByIdAsync(request.Id.ToString());
                var userRoles = await _userManager.GetRolesAsync(staff);

                await _userManager.RemoveFromRolesAsync(staff, userRoles);

                // Xóa nhân viên
                var result = await _userManager.DeleteAsync(staff);
                
                if (!result.Succeeded)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Hiện tại không thể xóa nhân viên.");

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Xóa nhân viên thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
