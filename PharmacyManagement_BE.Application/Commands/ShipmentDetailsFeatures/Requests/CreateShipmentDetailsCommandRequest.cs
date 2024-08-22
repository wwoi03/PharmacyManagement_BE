using MediatR;
using PharmacyManagement_BE.Application.DTOs.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsUnitDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.ShipmentDetailsFeatures.Requests
{
    public class CreateShipmentDetailsCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid ShipmentId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? UnitId { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal ImportPrice { get; set; }
        public int Quantity { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? Note { get; set; }
        public string ProductionBatch { get; set; }
        public List<ShipmentDetailsUnitDTO>? ShipmentDetailsUnits { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrEmpty(ProductionBatch))
                return new ValidationNotifyError<string>("Vui lòng nhập mã lô sản xuất.", "productionBatch");
            if (ProductId == Guid.Empty || ProductId == null)
                return new ValidationNotifyError<string>("Vui lòng thêm sản phẩm.", "productId");
            if (ImportPrice <= 0)
                return new ValidationNotifyError<string>("Giá sản phẩm phải lớn hơn 0.", "importPrice");
            if (Quantity <= 0)
                return new ValidationNotifyError<string>("Số lượng sản phẩm phải lớn hơn 0.", "quantity");
            if (UnitId == null || UnitId == Guid.Empty)
                return new ValidationNotifyError<string>("Vui lòng nhập giá bán sản phẩm.", "unitId");
            if (ManufactureDate > ExpirationDate)
                return new ValidationNotifyError<string>("Ngày sản xuất phải bé hơn ngày hết hạn.", "manufactureDate");
            if (ShipmentDetailsUnits == null || ShipmentDetailsUnits.Count == 0)
                return new ValidationNotifyError<string>("Vui lòng nhập đơn vị bán và giá bán cho sản phẩm.", "ShipmentDetailsUnits");

            return new ValidationNotifySuccess<string>();
        }
    }
}
