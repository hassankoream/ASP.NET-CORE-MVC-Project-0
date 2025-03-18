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

        int CreateDepartment(DepartmentToCreateDto deparment);
        int UpdateDepartment(DepartmentToUpdateDto deparment);
        bool DeleteDepartment(int Id);
    }
}
