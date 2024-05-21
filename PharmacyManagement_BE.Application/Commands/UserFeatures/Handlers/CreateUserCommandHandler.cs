using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PharmacyManagement_BE.Application.Commands.UserFeatures.Requests;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Roles;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.UserFeatures.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ResponseAPI<SignUpCommandResponse>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;

        public CreateUserCommandHandler(
            IPMEntities entities, 
            IMapper mapper, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration)
        {
            this._entities = entities;
            this._mapper = mapper;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._configuration = configuration;
        }

        public async Task<ResponseAPI<SignUpCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // B1: Kiểm tra thông tin yêu cầu
                var validation = request.IsValid();

                if (validation.IsSuccessed == false)
                    return new ResponseErrorAPI<SignUpCommandResponse>(validation.Message);

                // B2: Kiểm tra người dùng tồn tại
                var userExists = await _entities.CustomerService.GetCustomerByUsername(request.UserName);

                if (userExists != null)
                    return new ResponseErrorAPI<SignUpCommandResponse>("Người dùng đã tồn tại.");

                // B3: Thêm người dùng mới
                var user = _mapper.Map<Customer>(request);
                user.SecurityStamp = Guid.NewGuid().ToString();

                if (!(await _userManager.CreateAsync(user, request.Password)).Succeeded)
                    return new ResponseErrorAPI<SignUpCommandResponse>("Mật khẩu phải dài từ 8-16 ký tự bao gồm 1 chữ viết hoa và 1 chữ viết thường.");

                // B4: Thêm Role cho người dùng
                await _userManager.AddToRoleAsync(user, ProductRole.PM_PRODUCT_LIST);

                // B5: Lưu lại trạng thái Database
                _entities.SaveChange();

                // B6: Response
                var response = _mapper.Map<SignUpCommandResponse>(user);
                return new ResponseSuccessAPI<SignUpCommandResponse>(200, "Đăng ký tài khoản thành công.", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseErrorAPI<SignUpCommandResponse>("Lỗi hệ thống, vui lòng thử lại sau.");
            }
        }
    }
}
