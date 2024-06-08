﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Application.DTOs.Responses.DiseaseResponses;
using PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Requests;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Application.Queries.DiseaseFeatures.Handlers
{
    public class SearchDiseaseCommandHandler : IRequestHandler<SearchDiseaseCommandRequest, ResponseAPI<List<DetailsDiseaseResponse>>>
    {
        private readonly PharmacyManagementContext _context;
        private readonly IMapper _mapper;

        public SearchDiseaseCommandHandler(PharmacyManagementContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ResponseAPI<List<DetailsDiseaseResponse>>> Handle(SearchDiseaseCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Kiểm tra giá trị đầu vào
                var validation = request.IsValid();

                if (!validation.IsSuccessed)
                    return new ResponseErrorAPI<List<DetailsDiseaseResponse>>(StatusCodes.Status400BadRequest, validation.Message);


                // Tìm kiếm bệnh theo tên gần đúng
                var listDisease = await _context.Diseases
                    .Where
                    (d => EF.Functions.Like(d.Name.ToUpper(), $"%{request.KeyWord.ToUpper().Trim()}%")
                    || EF.Functions.Like(d.Description.ToUpper(), $"%{request.KeyWord.ToUpper().Trim()}%"))
                    .ToListAsync(cancellationToken);

                //Gán giá trị response
                var response = _mapper.Map<List<DetailsDiseaseResponse>>(listDisease);

                //Kiểm tra danh sách
                if (response == null || response.Count == 0)
                    return new ResponseErrorAPI<List<DetailsDiseaseResponse>>(StatusCodes.Status404NotFound, "Không tìm thấy loại bệnh");

                //Trả về danh sách
                return new ResponseSuccessAPI<List<DetailsDiseaseResponse>>(StatusCodes.Status200OK, "Danh sách bệnh", response);
            }
            catch (Exception)
            {
                return new ResponseErrorAPI<List<DetailsDiseaseResponse>>("Lỗi hệ thống.");
            }
        }
    }
}