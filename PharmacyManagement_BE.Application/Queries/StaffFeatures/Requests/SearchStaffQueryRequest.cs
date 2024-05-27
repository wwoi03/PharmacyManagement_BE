using MediatR;
using PharmacyManagement_BE.Application.DTOs.Responses;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StaffFeatures.Requests
{
    public class SearchStaffQueryRequest : IRequest<ResponseAPI<List<StaffResponse>>>
    {
        public string SearchString { get; set; }
    }
}
