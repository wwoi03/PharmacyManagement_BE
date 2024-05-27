using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PharmacyManagement_BE.Application.Commands.RoleFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.RoleFeatures.Handlers
{
    internal class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, ResponseAPI<string>>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper)
        {
            this._roleManager = roleManager;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kiểm tra role đã tồn tại
                var roleExists = _roleManager.Roles.FirstOrDefault(r => r.Name == request.Name);

                if (roleExists != null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status422UnprocessableEntity, "Quyền đã tồn tại.", String.Empty);

                // Thêm mới Role
                var role = _mapper.Map<IdentityRole<Guid>>(request);
                var r = new IdentityRole<Guid>(request.Name.ToUpper().Trim());
                var result = await _roleManager.CreateAsync(r);

                if (result.Succeeded)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status200OK, "Thêm mới quyền thành công.", String.Empty);
                else
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Không thể thêm mới quyền.", String.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.", String.Empty);
            }
        }
    }
}
