using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.Employee;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Demo.DAL.Presistance.UniteOfWork;
using Castle.Components.DictionaryAdapter.Xml;
using Demo.BLL.Common.Service.AttachmentService;

namespace Demo.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        #region Fields and Constructor
        //private readonly IEmployeeRepository _employeeRepository;

        //public EmployeeService(IEmployeeRepository employeeRepository)
        //{
        //    _employeeRepository = employeeRepository;
        //}
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            this._attachmentService = attachmentService;
        }
        #endregion

        #region Create

        public async Task<int> CreateEmployeeAsync(EmployeeToCreateDto EmployeeDto)
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
                DepartmentId = EmployeeDto.DepartmentId,



            };
            if (EmployeeDto is not null)
                employee.ImageName = await _attachmentService.UploadAsync(EmployeeDto.Image, "images");


            _unitOfWork.EmployeeRepository.AddEntity(employee);
            return await _unitOfWork.CompleteAsync();
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteEmployeeAsync(int Id)
        {
            var EmployeeRepo = _unitOfWork.EmployeeRepository;
            var employee = await EmployeeRepo.GetByIdAsync(Id);
            if (employee is not null) //Equivalent to //employee != null | employee is {}
            {
                EmployeeRepo.DeleteEntity(employee);
                return await _unitOfWork.CompleteAsync() > 0;
            }


            return false;

        }
        #endregion

        #region Get All Index

        public async Task<IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string SearchValue)
        {
            return await _unitOfWork.EmployeeRepository.GetAllQueryable()
                                    .Include(E => E.Department)
                                    .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(SearchValue) || E.Name.ToLower().Contains(SearchValue.ToLower())))
                                    .Select(employee => new EmployeeToReturnDto()
                                    {
                                        Id = employee.Id,
                                        Name = employee.Name,
                                        Age = employee.Age,
                                        Email = employee.Email,
                                        Salary = employee.Salary,
                                        Gender = employee.Gender.ToString(),
                                        EmployeeType = employee.EmployeeType.ToString(),
                                        IsActive = employee.IsActive,

                                        Department = employee.Department.Name ?? "NA",
                                        Image = employee.ImageName,

                                    }).ToListAsync();
        }
        #endregion
        #region Details

        public async Task<EmployeeDetailsToReturnDto?> GetEmployeeByIdAsync(int Id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(Id);
            if (employee is not null)
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
                    Department = employee.Department?.Name ?? "NA",
                    Image = employee.ImageName,


                };
            return null!;
        }
        #endregion

        #region Edit

        public async Task<int> UpdateEmployeeAsync(EmployeeToUpdateDto EmployeeDto)
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
                DepartmentId = EmployeeDto.DepartmentId,
            };
            if (EmployeeDto is not null)
                employeeUpdated.ImageName = await _attachmentService.UploadAsync(EmployeeDto.Image, "Images");


            _unitOfWork.EmployeeRepository.UpdateEntity(employeeUpdated);
            return await _unitOfWork.CompleteAsync();
        }
        #endregion
    }
}
