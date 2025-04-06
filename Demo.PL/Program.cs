using Demo.BLL.Common.Service.AttachmentService;
using Demo.BLL.Services.Deparment;
using Demo.BLL.Services.Employee;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Presistance.Repositories.Generic;
using Demo.DAL.Presistance.UniteOfWork;
using Demo.PL.Mapping.Profiles;
using Microsoft.EntityFrameworkCore;
namespace Demo.PL
{
    public class Program
    {
        #region 1 - Department Controller - Index

        /*
         
         
         
         
         */
        #endregion

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                 .AddDataAnnotationsLocalization();
            builder.Services.AddDbContext<ApplicationDbContext>((options =>



            //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefalutConnection"])
            options.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("DefalutConnection"))
            ));


            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
