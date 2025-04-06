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
        IEnumerable<T> GetAll(bool AsNoTracking = true);
        IQueryable<T> GetAllQueryable();
        T? GetById(int id);

        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }
}
