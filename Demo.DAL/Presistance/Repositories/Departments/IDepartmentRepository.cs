using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;

namespace Demo.DAL.Presistance
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool AsNoTracking = true);
        IQueryable<Department> GetAllQueryable();
        Department? GetById(int id);

        int AddDeparment(Department department);
        int UpdateDeparment(Department department);
        int DeleteDeparment(Department department);



    }
}
