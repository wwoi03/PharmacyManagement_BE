using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Domain.Types
{
    public enum PaymentStatus
    {
        PaymentUnpaid, // Chưa thanh toán
        PaymentPaid, // Đã thanh toán
    }
}
