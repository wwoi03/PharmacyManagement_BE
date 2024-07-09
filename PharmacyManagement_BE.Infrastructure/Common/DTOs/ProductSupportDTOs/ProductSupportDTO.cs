using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Respositories.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductSupportDTOs
{
    public class ProductSupportDTO
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Guid SupportId { get; set; }
        public Support Support { get; set; } = null!;
    }
}
