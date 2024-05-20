using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CartFeatures.Requests
{
    public class UpdateCartCommandRequest : IRequest<ResponseAPI<string>>
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid UnitId { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (Quantity < 1)
            {
                Quantity = 1;
                return new ValidationNotifyError<string>("Số lượng không thể bé hơn 1");
            }
            return new ValidationNotifySuccess<string>();
        }

    }
}
