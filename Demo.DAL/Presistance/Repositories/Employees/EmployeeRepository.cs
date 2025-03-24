using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Generic;


namespace Demo.DAL.Presistance.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }


        //private readonly ApplicationDbContext _Context;

        //public EmployeeRepository(ApplicationDbContext context) //Injection
        //{
        //    _Context = context;

        //}
        //public IEnumerable<Employee> GetAll(bool AsNoTracking = true)
        //{
        //    if (AsNoTracking)
        //        return _Context.Employees.AsNoTracking().Where(D => !D.IsDeleted).ToList(); //detached

        //    return _Context.Employees.ToList(); //unchanged
        //}

        ////Get
        //public Employee? GetById(int Id)
        //{
        //    //return _Context.Employees.Local.FirstOrDefault(D => D.Id == Id);
        //    return _Context.Employees.Find(Id); //Search Locally , In case Found  => Return , else => search database.

        //}
        //public int AddEmployee(Employee Employee)
        //{
        //    _Context.Employees.Add(Employee); //Save Locally
        //    int RowAffected = _Context.SaveChanges(); //Apply Remotely
        //    return RowAffected;
        //}
        //public int UpdateEmployee(Employee Employee)
        //{
        //    _Context.Employees.Update(Employee); //Modified
        //    int RowAffected = _Context.SaveChanges(); //Unchanged
        //    return RowAffected;
        //}

        //public int DeleteEmployee(Employee Employee)
        //{
        //    _Context.Employees.Remove(Employee); //Deleted
        //    return _Context.SaveChanges(); //untrack
        //}

        //public IQueryable<Employee> GetAllQueryable()
        //{
        //    return _Context.Employees;
        //}
    }
}
