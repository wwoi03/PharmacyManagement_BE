﻿using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductEcommerceDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IProductService : IRepositoryService<Product>
    {
        Task<List<ListProductDTO>> SearchProducts(string ContentStr, string CategoryName);
        Task<List<ListProductDTO>> GetProducts();
        Task<Product?> GetProductByCodeMedicineOrName(string codeMedicine, string name);
        Task<List<ItemProductDTO>> GetSellingProductByMonthYear(int month, int year);
        Task<List<ItemProductDTO>> GetNewProducts();
        Task<List<ItemProductDTO>> GetSaleProducts();
        Task<List<SelectProductDTO>> GetProductsSelect();
        Task<List<ItemProductDTO>> SearchProductEcommerce(string content, List<Guid> categories, List<Guid> diseases, List<Guid> symptoms, List<Guid> supports);
    }
}
