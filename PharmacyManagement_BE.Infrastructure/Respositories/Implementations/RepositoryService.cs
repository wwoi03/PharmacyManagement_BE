using Microsoft.EntityFrameworkCore;
using PharmacyManagement_BE.Domain.Entities;
using PharmacyManagement_BE.Infrastructure.DBContext;
using PharmacyManagement_BE.Infrastructure.Respositories.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<List<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdIncluding(Guid? id, params Expression<Func<T, object>>[] includeProperties)
        {
            // Kiểm tra nếu id không tồn tại thì trả về null
            if (!id.HasValue)
            {
                return null;
            }

            // Lấy các thuộc tính include
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            // Lấy đối tượng theo id sử dụng 
            return await query.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == id);
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

        // Kệ Hào
        //public async Task<bool> CheckExit<T>( List<Object> exit) where T : class
        //{
        //    var query = Context.Set<T>().AsQueryable(); // Bắt đầu với truy vấn cơ bản từ loại dữ liệu T

        //    foreach (var item in exit)
        //    {
        //        string propertyName = nameof(item); // Lấy tên thuộc tính động
        //        string propertyValue = item.ToString().ToUpper().Replace(" ", ""); // Lấy giá trị thuộc tính

        //        // Thêm điều kiện vào truy vấn động
        //        query = query.Where(r => EF.Functions.Like((EF.Property<string>(r, propertyName)), propertyValue));
        //    }
        //    return await query.AnyAsync();
        //}

    }
}
