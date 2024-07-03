using MediatR;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CategoryDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CategoryFeatures.Requests
{
    public class GetCategoryByCodeQueryRequest : IRequest<ResponseAPI<Category?>>
    {
        public string CodeCategory { get; set; }
    }
}
