using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.CommentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.CommentFeatures.Handlers
{
    internal class GetCustomerCommentQAsQueryHandler : IRequestHandler<GetCustomerCommentQAsQueryRequest, ResponseAPI<List<CommentDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCustomerCommentQAsQueryHandler(IPMEntities entities)
        {
            _entities = entities;
        }

        public async Task<ResponseAPI<List<CommentDTO>>> Handle(GetCustomerCommentQAsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách cmt
                var listComment = await _entities.CommentService.GetCustomerCommentQAs();

                //Trả về danh sách
                return new ResponseSuccessAPI<List<CommentDTO>>(StatusCodes.Status200OK, "Danh sách bình luận hỏi đáp", listComment);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<CommentDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
