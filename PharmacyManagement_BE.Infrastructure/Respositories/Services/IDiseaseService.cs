﻿using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IDiseaseService : IRepositoryService<Disease>
    {
        public Task<bool> CheckName(string Name);
        public Task<List<Disease>> GetAllDisease(CancellationToken cancellationToken);
        public Task<List<Disease>> SearchDisease(string KeyWord, CancellationToken cancellationToken);
    }
}
