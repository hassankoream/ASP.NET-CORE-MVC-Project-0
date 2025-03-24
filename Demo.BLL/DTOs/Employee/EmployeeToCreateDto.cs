using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Common.Enums;

namespace Demo.BLL.DTOs.Employee
{
    public class EmployeeToCreateDto
    {
        [Required]
        [MaxLength(50, ErrorMessage ="Max Lenght should be 50 Characters")]
        [MinLength(5, ErrorMessage ="Min Lenght should be 5 Characters")]
        public string Name { get; set; } = null!;



        [Range(22,30)]
        [Required(ErrorMessage = "Range is 22 to 30")]
        public int? Age { get; set; }



        [RegularExpression(@"^\d{1,5}-[A-Za-z]{1,20}-[A-Za-z]{1,20}-[A-Za-z]{1,20}$",
       ErrorMessage = "Address must be in the format: 1-5 digits, followed by street (1-20 chars), city (1-20 chars), and country (1-20 chars).")]
        public string? Address { get; set; }




        [DataType(DataType.Currency)]
        [Required]
        public decimal Salary { get; set; }




        [Display(Name="Is Active")]
        public bool IsActive { get; set; }



        [EmailAddress]
        public string? Email { get; set; }



        [Display(Name= "Phone Number")]
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }



        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }



        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }





    }
}
