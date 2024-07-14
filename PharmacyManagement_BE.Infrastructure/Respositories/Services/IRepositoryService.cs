using PharmacyManagement_BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagement_BE.Infrastructure.Respositories.Services
{
    public interface IRepositoryService<T> where T : class
    {
        Task<T> GetById(Guid? id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdIncluding(Guid? id, params Expression<Func<T, object>>[] includeProperties);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
