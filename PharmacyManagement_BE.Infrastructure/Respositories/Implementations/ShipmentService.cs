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
    public class ShipmentService : RepositoryService<Shipment>, IShipmentService
    {
        private readonly PharmacyManagementContext _context;

        public ShipmentService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<Shipment>> GetAllShipmentByStaffId(Guid id)
        {
            return _context.Shipments.Where(x => x.StaffId == id).ToList();
        }
    }
}
