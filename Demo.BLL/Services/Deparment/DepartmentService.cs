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
using Demo.DAL.Presistance.UniteOfWork;

namespace Demo.BLL.Services.Deparment
{
    public class DepartmentService : IDepartmentService
    {

        //private readonly IDepartmentRepository _departmentRepository;

        //public DepartmentService(IDepartmentRepository departmentRepository)
        //{
        //    _departmentRepository = departmentRepository;
        //}
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DepartmentToReturnDto>> GetAllDeparmentsAsync()
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
            var departments = await _unitOfWork.departmentRepository.GetAllQueryable().Where(D => !D.IsDeleted).Select(department => new DepartmentToReturnDto()
            {
                Id = department.Id,
                Code = department.Code,

                Name = department.Name,

                Description = department.Description,

                CreationDate = department.CreationDate,
            }).AsNoTracking().ToListAsync();

            return departments;




        }
        public async Task<DepartmentDetailsToReturnDto?> GetDepartmentByIdAsync(int Id)
        {
            var department = await _unitOfWork.departmentRepository.GetByIdAsync(Id);
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
        public async Task<int> CreateDepartmentAsync(DepartmentToCreateDto department)
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
            _unitOfWork.departmentRepository.AddEntity(departmentCreated);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<int> UpdateDepartmentAsync(DepartmentToUpdateDto department)
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
            //int rowAffected = _unitOfWork.departmentRepository.UpdateEntity(departmentUpdated);
            //return rowAffected;
            _unitOfWork.departmentRepository.UpdateEntity(departmentUpdated);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int Id)
        {
            var departmentRepo = _unitOfWork.departmentRepository;
            var department = await departmentRepo.GetByIdAsync(Id);
            if (department is not null)
            {
                //int rowsAffected = _unitOfWork.departmentRepository.DeleteEntity(department);
                //return rowsAffected > 0;
                departmentRepo.DeleteEntity(department);
                return await _unitOfWork.CompleteAsync() > 0;

            }
            return false;
        }



    }
}
