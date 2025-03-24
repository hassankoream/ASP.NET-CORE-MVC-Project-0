using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.Employee;

namespace Demo.BLL.Services.Employee
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeToReturnDto> GetAllEmployees();
        EmployeeDetailsToReturnDto? GetEmployeeById(int Id);

        int CreateEmployee(EmployeeToCreateDto employee);
        int UpdateEmployee(EmployeeToUpdateDto employee);
        bool DeleteEmployee(int Id);
    }
}
