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
        Task<IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue);
        Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int Id);
        
        Task<int> CreateEmployeeAsync(EmployeeToCreateDto employee);
        Task<int> UpdateEmployeeAsync(EmployeeToUpdateDto employee);
        Task<bool> DeleteEmployeeAsync(int Id);
    }
}
