using MediatR;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.OrderFeatures.Requests
{
    public class UpdateStatusOrderCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid Id { get; set; }
        public int type { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (!Enum.IsDefined(typeof(OrderType), type))
                return new ValidationNotifyError<string>("Vui lòng chọn trạng thái đơn hàng.", "status");

            return new ValidationNotifySuccess<string>();

        }
    }
}
