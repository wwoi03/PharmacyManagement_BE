using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.StatisticFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.StatisticFeatures.Handlers
{
    internal class GetCustomerCommentsQueryHandler : IRequestHandler<GetCustomerCommentsQueryRequest, ResponseAPI<List<CommentDTO>>>
    {
        private readonly IPMEntities _entities;

        public GetCustomerCommentsQueryHandler(IPMEntities entities)
        {
            this._entities = entities;
        }

        public async Task<ResponseAPI<List<CommentDTO>>> Handle(GetCustomerCommentsQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Lấy danh sách cmt
                var listComment = await _entities.CommentService.GetCustomerComments();

                //Trả về danh sách
                return new ResponseSuccessAPI<List<CommentDTO>>(StatusCodes.Status200OK, "Danh sách bình luận", listComment);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<CommentDTO>>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
