using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CommentFeatures.Requests
{
    public class GetDetailsCommentQueryRequest : IRequest<ResponseAPI<CommentDTO>>
    {
        public Guid Id { get; set; }
    }
}
