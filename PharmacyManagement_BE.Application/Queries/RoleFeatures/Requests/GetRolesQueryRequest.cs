using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.RoleDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.RoleFeatures.Requests
{
    public class GetRolesQueryRequest : IRequest<ResponseAPI<List<ListRoleDTO>>>
    {
    }
}
