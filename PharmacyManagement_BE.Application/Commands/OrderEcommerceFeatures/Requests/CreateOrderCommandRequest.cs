using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CartEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.OrderEcommerceFeatures.Requests
{
    public class CreateOrderCommandRequest : IRequest<ResponseAPI<string>>
    {
        public string OrdererName { get; set; }
        public string ReceiverName { get; set; }
        public string RecipientPhone { get; set; }
        public string? Email { get; set; }
        public string ProvinceOrCity { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetails { get; set; }
        public decimal TransportFee { get; set; }
        public string? Note { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? BranchId { get; set; }
        public string? Voucher { get; set; }
        public List<ItemCartDTO> Products { get; set; }
        public HttpContext? Context { get; set; }

        public ValidationNotify<string> Valid()
        {
            if (Products.Count < 1)
                return new ValidationNotifyError<string>("Vui lòng thêm sản phẩm cần thanh toán.", "products");
            if (string.IsNullOrEmpty(OrdererName))
                return new ValidationNotifyError<string>("Vui lòng nhập tên người đặt hàng.", "ordererName");
            if (string.IsNullOrEmpty(ReceiverName))
                return new ValidationNotifyError<string>("Vui lòng nhập tên người nhận hàng.", "receiverName");
            if (string.IsNullOrEmpty(RecipientPhone))
                return new ValidationNotifyError<string>("Vui lòng nhập số điện thoại người nhận.", "recipientPhone");
            if (string.IsNullOrEmpty(ProvinceOrCity))
                return new ValidationNotifyError<string>("Vui lòng nhập tỉnh/thành phố.", "provinceOrCity");
            if (string.IsNullOrEmpty(District))
                return new ValidationNotifyError<string>("Vui lòng nhập quận/huyện.", "district");
            if (string.IsNullOrEmpty(Ward))
                return new ValidationNotifyError<string>("Vui lòng nhập phường/xã.", "ward");
            if (string.IsNullOrEmpty(AddressDetails))
                return new ValidationNotifyError<string>("Vui lòng nhập địa chỉ chi tiết.", "addressDetails");
            if (TransportFee < 0)
                return new ValidationNotifyError<string>("Phí vận chuyển không hợp lệ.", "transportFee");
            if (PaymentMethodId == Guid.Empty)
                return new ValidationNotifyError<string>("Phương thức thanh toán không hợp lệ.", "paymentMethodId");

            return new ValidationNotifySuccess<string>();
        }
    }
}
