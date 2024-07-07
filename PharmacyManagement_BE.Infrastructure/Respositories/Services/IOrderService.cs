using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.OrderDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IOrderService : IRepositoryService<Order>
    {
        Task<List<Order>> GetAllOrderByStaffId(Guid id);
        Task<List<StatisticDTO>> StatisticOrder(TimeType type);
        Task<List<StatisticDTO>> StatisticRevenue(TimeType type);
        Task<List<OrderDTO>> GetRequestCancellations();
    }
}
