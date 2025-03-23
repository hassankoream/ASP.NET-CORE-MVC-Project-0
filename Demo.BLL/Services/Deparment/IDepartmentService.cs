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
        IEnumerable<DepartmentToReturnDto> GetAllDeparments();
        DepartmentDetailsToReturnDto? GetDepartmentById(int Id);

        int CreateDepartment(DepartmentToCreateDto department);
        int UpdateDepartment(DepartmentToUpdateDto department);
        bool DeleteDepartment(int Id);
    }
}
