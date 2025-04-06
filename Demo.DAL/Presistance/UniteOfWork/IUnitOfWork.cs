using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;

namespace Demo.DAL.Presistance.UniteOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Signatures

        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository departmentRepository { get; }

        int Complete();
        //int Commit();
    }
}
