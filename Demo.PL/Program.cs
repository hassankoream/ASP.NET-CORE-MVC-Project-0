using Demo.BLL.Common.Service.AttachmentService;
using Demo.BLL.Services.Deparment;
using Demo.BLL.Services.EmailSettings;
using Demo.BLL.Services.Employee;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Entities.Identity;
using Demo.DAL.Presistance;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Presistance.Repositories.Generic;
using Demo.DAL.Presistance.UniteOfWork;
using Demo.PL.Mapping.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
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


            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IEmailSettings, EmailSettings>();
            
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));



            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>

            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 5;
            }

            )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); //PasswordSignInAsync depend on  AddDefaultTokenProviders service



            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                
                options=>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/Error";
                    options.LogoutPath = "/Account/logout";
                }
                );

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
