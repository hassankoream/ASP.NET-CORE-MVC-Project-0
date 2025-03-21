using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.Department
{
    public class DepartmentToCreateDto //Department 
    {

        
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "Name is Required, Please enter Name!")] //Custome error message
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Created On")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreationDate { get; set; }
    }
}
