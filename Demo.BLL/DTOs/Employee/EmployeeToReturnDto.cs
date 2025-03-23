using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;

namespace Demo.BLL.DTOs.Employee
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }


      
        public string Gender { get; set; }
        public string EmployeeType { get; set; }
    }
}
