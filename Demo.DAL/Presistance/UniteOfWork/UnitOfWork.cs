using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;

namespace Demo.DAL.Presistance.UniteOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //EmployeeRepository = new EmployeeRepository(_dbContext);
            //departmentRepository = new DepartmentRepository(_dbContext);
        }
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return new EmployeeRepository(_dbContext);
            }
        }

        public IDepartmentRepository departmentRepository => new DepartmentRepository(_dbContext);
     

        public int Complete()
        {
            return _dbContext.SaveChanges();

        }

        public void Dispose()
        {
            _dbContext.Dispose();   
        }
    }
}
