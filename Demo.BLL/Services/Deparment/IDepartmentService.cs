using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.Department;

namespace Demo.BLL.Services.Deparment
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentToReturnDto>> GetAllDeparmentsAsync();
        Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int Id);
        Task<int> CreateDepartmentAsync(DepartmentToCreateDto department);
        Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto department);
        Task<bool> DeleteDepartmentAsync(int Id);
    }
}
