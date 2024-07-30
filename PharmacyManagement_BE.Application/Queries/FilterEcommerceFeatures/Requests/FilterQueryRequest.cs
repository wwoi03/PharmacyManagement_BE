using MediatR;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.ProductDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.FilterEcommerceFeatures.Requests
{
    public class FilterQueryRequest : IRequest<ResponseAPI<List<FilterProductDTO>>>
    {
        //truy xuất phân trang
        public int PageNumber { get; set; }
        //số item trong trang
        public int RowsPerPage { get; set; }

        public Guid? Category { get; set; }
        public string Price { get; set; } = PriceType.Under100.ToString(); 
        //Chỉ lấy top 10 hỗ trợ được ưu tiên
        public List<Guid>? Support { get; set; }
        //Chỉ lấy top 10 bệnh được ưu tiên
        public List<Guid>? Disease { get; set; }
    }
}
