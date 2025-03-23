using Demo.BLL.Services.Deparment;
using Demo.BLL.Services.Employee;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Presistance.Repositories.Generic;
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
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>((options =>



            //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefalutConnection"])
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefalutConnection"))
            ));


            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


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
