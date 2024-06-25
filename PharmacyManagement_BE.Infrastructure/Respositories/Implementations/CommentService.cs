﻿using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Domain.Types;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.CommentDTOs;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.StatisticDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.DBContext.Dapper;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class CommentService : RepositoryService<Comment>, ICommentService
    {
        private readonly PMDapperContext _dapperContext;

        public CommentService(PharmacyManagementContext context, PMDapperContext dapperContext) : base(context)
        {
            this._dapperContext = dapperContext;
        }

        #region Dapper
        //Lấy danh sách cmt hỏi đáp
        public async Task<List<CommentDTO>> GetCustomerCommentQAs()
        {
            var parameters = new DynamicParameters();
            var listComment = new List<CommentDTO>();

            parameters.Add("@CommentType", CommentType.CommentQA);

            string sql = @"
                   SELECT *
                    FROM Comments AS cm1
                    WHERE cm1.CustomerId IS NOT NULL 
                    AND CommentType = @CommentType
                      AND cm1.CustomerId NOT IN (
                        SELECT cm2.ReplayCommentId
                        FROM Comments AS cm2)";

            // Thực hiện truy vấn và lấy kết quả
            
            listComment = (await _dapperContext.GetConnection.QueryAsync<CommentDTO>(sql, parameters)).AsList<CommentDTO>();

            return listComment;
           
        }

        public async Task<List<CommentDTO>> GetCustomerCommentEvaluates()
        {
            var parameters = new DynamicParameters();
            var listComment = new List<CommentDTO>();

            parameters.Add("@CommentType", CommentType.CommentEvaluate);

            string sql = @"
                   SELECT *
                    FROM Comments AS cm1
                    WHERE cm1.CustomerId IS NOT NULL 
                    AND CommentType = @CommentType
                      AND cm1.CustomerId NOT IN (
                        SELECT cm2.ReplayCommentId
                        FROM Comments AS cm2)";

            // Thực hiện truy vấn và lấy kết quả

            listComment = (await _dapperContext.GetConnection.QueryAsync<CommentDTO>(sql, parameters)).ToList();

            return listComment;

        }

        public async Task<Comment> SetUpCommentReply(Comment replyComment, Comment convertComment, Guid userId)
        {
            //set các giá trị còn thiếu cho cmt
            convertComment.CommentDate = DateTime.Now;
            convertComment.CommentType = replyComment.CommentType;
            convertComment.ProductId = replyComment.ProductId;
            convertComment.Product = replyComment.Product;

            if (await Context.Staffs.AnyAsync(r => r.Id == userId))
                convertComment.StaffId = userId;
            else if(await Context.Customers.AnyAsync(r => r.Id == userId))
            {
                convertComment.CustomerId = userId;
                convertComment.Customer = await Context.Customers.FirstOrDefaultAsync(r => r.Id == userId);
            }

            return convertComment;
        }
        #endregion Dapper
    }
}
