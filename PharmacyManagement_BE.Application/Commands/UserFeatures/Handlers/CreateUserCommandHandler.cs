using AutoMapper;
using MediatR;
using PharmacyManagement_BE.Application.Commands.UserFeatures.Requests;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.UserFeatures.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, ResponseAPI<RegisterCommandResponse>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<RegisterCommandResponse>> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // B1: Kiểm tra thông tin yêu cầu
                var validation = request.IsValid();

                if (validation.IsSuccessed == false)
                    return new ResponseErrorAPI<RegisterCommandResponse>(validation.Message);

                // B2: Kiểm tra người dùng tồn tại
                var userExists = _entities.CustomerService.GetCustomerByUsername(request.UserName);

                if (userExists == null)
                    return new ResponseErrorAPI<RegisterCommandResponse>("Người dùng đã tồn tại.");

                // B3: Thêm người dùng mới
                var customer = _mapper.Map<Customer>(request);
                if (_entities.CustomerService.Create(customer) == false)
                    return new ResponseErrorAPI<RegisterCommandResponse>("Lỗi hệ thống, vui lòng thử lại sau.");

                // B4: Lưu lại trạng thái Database
                _entities.SaveChange();

                // B5: Response
                var response = _mapper.Map<RegisterCommandResponse>(customer);
                return new ResponseSuccessAPI<RegisterCommandResponse>(200, "Đăng ký tài khoản thành công.", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseErrorAPI<RegisterCommandResponse>("Lỗi hệ thống, vui lòng thử lại sau.");
            }
        }
    }
}
