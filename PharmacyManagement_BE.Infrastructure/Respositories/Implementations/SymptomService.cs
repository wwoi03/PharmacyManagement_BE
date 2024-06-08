using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.Common.ResponseAPIs;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class SymptomService : RepositoryService<Symptom>, ISymptomService
    {
        public SymptomService(PharmacyManagementContext context) : base(context)
        {

        }

        public async Task<bool> CheckExit(string name, string description)
        {
            return await Context.Symptoms.AnyAsync
                (r => r.Name.ToUpper().Trim() == name.ToUpper().Trim() &&
                r.Description.ToUpper().Trim() == description.ToUpper().Trim());
        }

    }
}
