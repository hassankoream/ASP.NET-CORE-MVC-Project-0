using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities;
using Demo.DAL.Entities.Employees;

namespace Demo.DAL.Presistance.Repositories.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAllQueryable();

        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }
}
