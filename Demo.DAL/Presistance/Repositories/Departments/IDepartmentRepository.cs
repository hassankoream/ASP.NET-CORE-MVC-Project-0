using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repositories.Generic;

namespace Demo.DAL.Presistance.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //IEnumerable<Department> GetAll(bool AsNoTracking = true);
        //IQueryable<Department> GetAllQueryable();
        //Department? GetById(int id);

        //int AddDepartment(Department department);
        //int UpdateDepartment(Department department);
        //int DeleteDepartment(Department department);


        /*
        We are gonna replace what was above was the Generic Type
        */


    }
}
