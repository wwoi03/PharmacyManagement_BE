using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class OrderService : RepositoryService<Order>, IOrderService
    {
        private readonly PharmacyManagementContext _context;

        public OrderService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<Order>> GetAllOrderByStaffId(Guid id)
        {
            return _context.Orders.Where(x => x.StaffId == id).ToList();
        }
    }
}

