using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Implementations
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        public PharmacyManagementContext Context { get; set; }

        public RepositoryService(PharmacyManagementContext context)
        {
            this.Context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid? id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public bool Create(T entity)
        {
            try
            {
                Context.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                Context.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool Update(T entity)
        {
            try
            {
                Context.Update(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
