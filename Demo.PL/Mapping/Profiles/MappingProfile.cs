using AutoMapper;
using Demo.BLL.DTOs.Department;
using Demo.BLL.DTOs.Employee;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Department;
using Demo.PL.ViewModels.Employee;
using Demo.PL.ViewModels.Users;

namespace Demo.PL.Mapping.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region Employee Module

            #endregion

            #region Department Module

            CreateMap<DepartmentViewModel, DepartmentToCreateDto>().ReverseMap(); 
                //if Destination Class has different name than the Source we should use this 
                /*.ForMember(dest => dest.Name, config=> config.MapFrom(src => src.DepartmentName))*/;

            CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, DepartmentToUpdateDto>();
            CreateMap<EmployeeEditViewModel, EmployeeToCreateDto>().ReverseMap();
            #endregion

            #region User Module
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();

            #endregion


        }

    }
}
