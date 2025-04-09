using Company.Project.BLL.Interfaces;
using Company.Project.BLL.Repositories;
using Company.Project.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Company.Project.DAL.Models;
using Microsoft.Extensions.Options;
using Company.Project.PL.Mapping;
using Company.Project.BLL;
using Microsoft.AspNetCore.Identity;

namespace Company.Project.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();         // Register Built-in MVC Services 
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow Dependency Injection in DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow Dependency Injection in EmployeeRepository 

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            }); // Allow Dependency Injection in CompanyDbContext


            // Dependency Injection LifeTime:
            //1. builder.Services.AddScooped        : Create Object Life Time Per Request      - After Request ends become unreachable
            //2. builder.Services.AddTransiant      : Create Object Life Time Per Operation   
            //3. builder.Services.AddSingletoon     : Create Object Life Time Per Applitcation   

            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<AppUser , IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders(); 

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

    }
}