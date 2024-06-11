﻿using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.DTOs.SymptomDTOs;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface ISymptomService : IRepositoryService<Symptom>
    {
        //Kiểm tra tồn tại triệu chứng chưa: giống cả tên và mô tả
        Task<bool> CheckExit(string Name, string description);
        Task<List<Symptom>> Search(string KeyWord, CancellationToken cancellationToken);
    }
}
