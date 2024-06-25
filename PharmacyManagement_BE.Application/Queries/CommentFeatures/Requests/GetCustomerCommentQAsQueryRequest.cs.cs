using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;

namespace PharmacyManagement_BE.Application.Queries.CommentFeatures.Requests
{
    public class GetCustomerCommentQAsQueryRequest : IRequest<ResponseAPI<List<CommentDTO>>>
    {
    }
}
