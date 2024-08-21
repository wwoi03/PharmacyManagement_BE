using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using RecommendSimilarProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IPredictionService
    {
        public Task<List<ProductInteractive>> GetSimilarProductInteractive();
        public Task<List<ItemProductDTO>> GetSimilarProducts(Guid customerId);
    }
}
