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
    public class ShipmentDetailsUnitService : RepositoryService<ShipmentDetailsUnit>, IShipmentDetailsUnitService
    {
        public ShipmentDetailsUnitService(PharmacyManagementContext context) : base(context)
        {

        }
    }
}
