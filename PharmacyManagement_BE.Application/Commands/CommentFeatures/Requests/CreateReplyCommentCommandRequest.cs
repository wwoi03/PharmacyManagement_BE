using MediatR;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.Common.ValidationNotifies;
using PharmacyManagement_BE.Infrastructure.Customs.SupportFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PharmacyManagement_BE.Application.Commands.CommentFeatures.Requests
{
    public class CreateReplyCommentCommandRequest : IRequest<ResponseAPI<CommentDTO>>
    {
        public Guid ReplyCommentId { get; set; }
        public string CommentText { get; set; }

        public ValidationNotify<string> IsValid()
        {
            if (string.IsNullOrWhiteSpace(CommentText))
                return new ValidationNotifyError<string>("Vui lòng nhập bình luận.");

            return new ValidationNotifySuccess<string>();
        }
    }
}
