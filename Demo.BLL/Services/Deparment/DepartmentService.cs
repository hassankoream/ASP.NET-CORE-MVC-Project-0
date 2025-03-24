using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.DTOs.Department;
using Microsoft.EntityFrameworkCore;
using Demo.DAL.Entities;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repositories.Departments;

namespace Demo.BLL.Services.Deparment
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDeparments()
        {
            //var departments = _departmentRepository.GetAll();
            //foreach (var department in departments)
            //{
            //    yield return new DepartmentToReturnDto
            //    {
            //        Id = department.Id,
            //        Code = department.Code,

            //        Name = department.Name,

            //        Description = department.Description,

            //        CreationDate = department.CreationDate,
            //    };
            //}
            //Manual Mapping
            var departments = _departmentRepository.GetAllQueryable().Where(D => !D.IsDeleted).Select(department => new DepartmentToReturnDto()
            {
                Id = department.Id,
                Code = department.Code,

                Name = department.Name,

                Description = department.Description,

                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();

            return departments;




        }
        public DepartmentDetailsToReturnDto? GetDepartmentById(int Id)
        {
            var department = _departmentRepository.GetById(Id);
            if (department is not null) //department!= null or department is {}
            {
                return new DepartmentDetailsToReturnDto()
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate,
                    Description = department.Description,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    IsDeleted = department.IsDeleted,


                };

            }
            return null!;
        }
        public int CreateDepartment(DepartmentToCreateDto department)
        {
            var departmentCreated = new Department()
            {
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
                Description = department.Description,

                //Extra
                CreatedBy = 1,//UserId => Relationship
                LastModifiedBy = 1, //UserId => Relationship
                LastModifiedOn = DateTime.UtcNow,
                
            };
            int rowAffected = _departmentRepository.AddEntity(departmentCreated);
            return rowAffected;
        }
        public int UpdateDepartment(DepartmentToUpdateDto department)
        {
            var departmentUpdated = new Department()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
                Description = department.Description,

                //Extra
                CreatedBy = 1,//UserId => Relationship
                LastModifiedBy = 1, //UserId => Relationship
                LastModifiedOn = DateTime.UtcNow,

            };
            int rowAffected = _departmentRepository.UpdateEntity(departmentUpdated);
            return rowAffected;
        }

        public bool DeleteDepartment(int Id)
        {
            var department = _departmentRepository.GetById(Id);
            if (department is not null)
            {
                int rowsAffected = _departmentRepository.DeleteEntity(department);
                return rowsAffected> 0;
                
            }
            return false;
        }



    }
}
