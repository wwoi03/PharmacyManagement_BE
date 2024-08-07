﻿using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface ICommentService : IRepositoryService<Comment>
    {
        Task<List<CommentDTO>> GetCustomerCommentQANoReplys();
        Task<List<CommentDTO>> GetCustomerCommentEvaluateNoReplys();
        Task<Comment> SetUpCommentReply(Comment replyComment, Comment convertComment, Guid userId);
    }
}
