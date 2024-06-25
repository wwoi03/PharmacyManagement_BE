using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PharmacyManagement_BE.Application.Commands.CommentFeatures.Requests;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Commands.CommentFeatures.Handlers
{
    internal class CreateReplyCommentCommandHandler : IRequestHandler<CreateReplyCommentCommandRequest, ResponseAPI<string>>
    {
        private readonly IPMEntities _entities;
        private readonly IMapper _mapper;

        public CreateReplyCommentCommandHandler(IPMEntities entities, IMapper mapper)
        {
            this._entities = entities;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<string>> Handle(CreateReplyCommentCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //B1: kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<string>(StatusCodes.Status400BadRequest, validation.Message);

                //B2: kiểm tra cmt được trả lời có tồn tại không
                var replyComment = await _entities.CommentService.GetById(request.ReplayCommentId);

                if (replyComment == null)
                    return new ResponseErrorAPI<string>(StatusCodes.Status404NotFound, "Không tìm thấy bình luận phản hồi.");

                // Chuyển đổi request sang dữ liệu bảng cmt
                var convertComment = _mapper.Map<Comment>(request);

                //Lấy id user
                var userId = await _entities.AccountService.GetAccountId();

                // Tạo bình luận mới
                var createComment = await _entities.CommentService.SetUpCommentReply(replyComment, convertComment, userId);

                var comment = _entities.CommentService.Create(createComment);

                //Kiểm tra trạng thái
                if (comment == false)
                    return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Thêm bình luận thất bại, vui lòng thử lại sau.");

                //Lưu vào CSDL
                _entities.SaveChange();

                return new ResponseSuccessAPI<string>(StatusCodes.Status200OK,"Thêm bình luận thành công.");
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<string>(StatusCodes.Status500InternalServerError, "Lỗi hệ thống.");
            }
        }
    }
}
