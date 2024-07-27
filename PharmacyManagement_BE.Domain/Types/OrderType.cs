using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Types
{
    public enum OrderType
    {
        OrderWaitingConfirmation, // Đang xử lý/chờ xác nhận
        OrderBeingPrepared, // Đang chuẩn bị hàng
        OrderBeingDelivered, // Đang giao
        OrderDelivered, // Đã giao 
        RequestCancelOrder, 
        CancellationOrderApproved,
        StoreCanceledOrder
    }
}
