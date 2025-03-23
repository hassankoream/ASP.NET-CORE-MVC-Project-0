using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities;
using Demo.DAL.Presistance.Data;

namespace Demo.DAL.Presistance.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _Context;

        public GenericRepository(ApplicationDbContext context) //Injection
        {
            _Context = context;

        }
        public IEnumerable<T> GetAll(bool AsNoTracking = true)
        {
            //Get only What is not Deleted
            if (AsNoTracking)
                return _Context.Set<T>().AsNoTracking().Where(D => !D.IsDeleted).ToList(); //detached

            return _Context.Set<T>().Where(D => !D.IsDeleted).ToList(); //unchanged
        }

        //Get
        public T? GetById(int Id)
        {
            //return _Context.Ts.Local.FirstOrDefault(D => D.Id == Id);
          /*  return _Context.Set<T>().Find(Id);*/ //Search Locally , In case Found  => Return , else => search database.

            //If Deleted
            var entity = _Context.Set<T>().Find(Id);

            // Ensure the entity is not deleted before returning it
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }
        public int AddEntity(T entity)
        {
            _Context.Set<T>().Add(entity); //Save Locally
            int RowAffected = _Context.SaveChanges(); //Apply Remotely
            return RowAffected;
        }
        public int UpdateEntity(T entity)
        {
            _Context.Set<T>().Update(entity); //Modified
            int RowAffected = _Context.SaveChanges(); //Unchanged
            return RowAffected;
        }

        public int DeleteEntity(T entity)
        {
            //_Context.Set<T>().Remove(entity); //Deleted
            //return _Context.SaveChanges(); //untrack
            //IsDelete = true. Why? incase you need to recover

            entity.IsDeleted = true;
            _Context.Set<T>().Update(entity); //Modified
            return  _Context.SaveChanges(); //Unchanged
             

        }

        public IQueryable<T> GetAllQueryable()
        {
            return _Context.Set<T>();
        }
    }
}
