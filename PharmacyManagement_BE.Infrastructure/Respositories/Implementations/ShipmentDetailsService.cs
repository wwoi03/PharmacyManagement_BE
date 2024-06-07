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
    public class ShipmentDetailsService : RepositoryService<ShipmentDetails>, IShipmentDetailsService
    {
        private readonly PharmacyManagementContext _context;

        public ShipmentDetailsService(PharmacyManagementContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> RemoveRangeShipmentDetails(List<ShipmentDetails> shipmentDetails)
        {
            try
            {
                _context.RemoveRange(shipmentDetails);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
