using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.Employee;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Entities.Employees;

namespace Demo.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int CreateEmployee(EmployeeToCreateDto EmployeeDto)
        {
            Demo.DAL.Entities.Employees.Employee employee = new DAL.Entities.Employees.Employee()
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                PhoneNumber = EmployeeDto.PhoneNumber,
                Salary = EmployeeDto.Salary,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1, //user ID
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,


            };
            return _employeeRepository.AddEntity(employee);
        }

        public bool DeleteEmployee(int Id)
        {
            var employee = _employeeRepository.GetById(Id);
            if (employee is not null) //Equlivent to //employee != null | employee is {}
               return _employeeRepository.DeleteEntity(employee) > 0;
            return false;

        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllQueryable().Where(E => !E.IsDeleted).Select(employee => new EmployeeToReturnDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                IsActive = employee.IsActive,
            });
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int Id)
        {
            var employee = _employeeRepository.GetById(Id);
            if(employee is not null)
                return new EmployeeDetailsToReturnDto()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Email = employee.Email,
                    HiringDate = employee.HiringDate,
                    PhoneNumber = employee.PhoneNumber,
                    Salary = employee.Salary,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    IsActive = employee.IsActive,
                    IsDeleted = employee.IsDeleted,
                    CreatedOn = employee.CreatedOn,
                    CreatedBy = employee.CreatedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    LastModifiedBy = employee.LastModifiedBy,
                    
                };
            return null!;
        }

        public int UpdateEmployee(EmployeeToUpdateDto EmployeeDto)
        {
            var employeeUpdated = new DAL.Entities.Employees.Employee()
            {
                Id = EmployeeDto.Id, //if 
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Address = EmployeeDto.Address,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                Salary = EmployeeDto.Salary,
                HiringDate = EmployeeDto.HiringDate,
                Gender = EmployeeDto.Gender,
                EmployeeType = EmployeeDto.EmployeeType,
                CreatedBy = 1, //user ID
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
           return _employeeRepository.UpdateEntity(employeeUpdated); 
        }
    }
}
