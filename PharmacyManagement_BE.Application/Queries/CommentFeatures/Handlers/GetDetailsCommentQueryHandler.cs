using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Queries.CommentFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
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
    internal class GetDetailsCommentQueryHandler : IRequestHandler<GetDetailsCommentQueryRequest, ResponseAPI<CommentDTO>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public GetDetailsCommentQueryHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<CommentDTO>> Handle(GetDetailsCommentQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra tồn tại
                var validation = await _entities.CommentService.GetById(request.Id);

                if (validation == null)
                    return new ResponseErrorAPI<CommentDTO>(StatusCodes.Status404NotFound, "Bình luận không tồn tại.");

                var comment = _mapper.Map<CommentDTO>(validation);

                return new ResponseSuccessAPI<CommentDTO>(StatusCodes.Status200OK, "Thông tin bình luận", comment);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<CommentDTO>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
