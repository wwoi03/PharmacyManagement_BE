using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ShipmentDetailsDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IShipmentDetailsService : IRepositoryService<ShipmentDetails>
    {
        Task<bool> RemoveRangeShipmentDetails(List<ShipmentDetails> shipmentDetails);
        Task<List<ListShipmentDetailsDTO>> GetShipmentDetailsByShipment(Guid shipmentId);
        Task<DetailsShipmentDetailsDTO> GetDetailsShipmentDetails(Guid shipmentDetailsId);
        Task<List<ListShipmentDetailsDTO>> SearchShipmentDetailsByProduct(Guid shipmentId,string NameOrCodeMedicine);
    }
}
