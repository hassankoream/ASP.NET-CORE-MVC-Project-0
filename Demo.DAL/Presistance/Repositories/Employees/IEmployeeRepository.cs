using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Repositories.Generic;

namespace Demo.DAL.Presistance.Repositories.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll(bool AsNoTracking = true);
        //IQueryable<Employee> GetAllQueryable();
        //Employee? GetById(int id);

        //int AddEmployee(Employee Employee);
        //int UpdateEmployee(Employee Employee);
        //int DeleteEmployee(Employee Employee);


        /*
         We are gonna replace what was above was the Generic Type
         */
    }
}
