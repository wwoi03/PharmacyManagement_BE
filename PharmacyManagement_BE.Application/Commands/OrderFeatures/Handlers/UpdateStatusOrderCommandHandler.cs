using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.OrderFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.OrderFeatures.Handlers
{
    internal class UpdateStatusOrderCommandHandler : IRequestHandler<UpdateStatusOrderCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;

        public UpdateStatusOrderCommandHandler(IPMEntities entities)
        {
            this._entities = entities;
        }
        public async Task<ResponseAPI<string>> Handle(UpdateStatusOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ////Kiểm tra tồn tại
                var order = await _entities.OrderService.GetById(request.Id);

                if (order == null)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status404NotFound, "Đơn hàng không tồn tại.");

                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseSuccessAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //Gán giá trị thay đổi => check hủy đơn => không được đổi sang trạng thái khác => làm sau => lười
                order.Status = request.type.ToString();

                // Cập nhật lại đơn hàng
                var status = _entities.OrderService.Update(order);

                //Kiểm tra trạng thái
                if (status == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Cập nhật đơn hàng thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>("Cập nhật đơn hàng thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
