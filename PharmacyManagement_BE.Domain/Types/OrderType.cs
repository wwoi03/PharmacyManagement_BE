using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Types
{
    public enum OrderType
    {
        //Dành cho lấy danh sách
        GetAll = -1,
        //Khách hàng có thể tự hủy đơn
        OrderWaitingConfirmation = 0,
        //Khách hàng phải gửi yêu cầu hủy đơn
        OrderBeingPrepared = 1,
        //Khách hàng phải gửi yêu cầu hủy đơn
        OrderBeingDelivered = 2,
        //đã giao
        OrderDelivered = 3,
        // gửi yêu cầu hủy
        RequestCancelOrder = 4,
        // Xác nhận hủy
        CancellationOrderApproved = 5,
        //phía cửa hàng yêu cầu hủy
        StoreCanceledOrder = 6,
    }
}
