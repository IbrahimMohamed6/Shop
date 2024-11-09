using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.BLL.Common.Attachements;
using Shop.BLL.Mapping;
using Shop.BLL.Services.DepartmentServices;
using Shop.BLL.Services.EmployeeServices;
using Shop.DAL.Entites.Identity;
using Shop.DAL.Preasistince.Data.Contexts;
using Shop.DAL.Preasistince.Repository.DepartmentRepositories;
using Shop.DAL.Preasistince.Repository.EmployeeRepository;
using Shop.DAL.Preasistince.UnitOfWork;

namespace Shop.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepisitory>();
            builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();

            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IAttachement, Attachement>();
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfils()));
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>((Options =>
			{
				Options.Password.RequiredUniqueChars = 2;
				Options.Password.RequireDigit = true;
				Options.Password.RequireLowercase = true;
				Options.Password.RequireUppercase = true;
				Options.Password.RequireNonAlphanumeric = true;
				Options.Password.RequiredLength = 6;
				Options.User.RequireUniqueEmail = true;
				Options.Lockout.AllowedForNewUsers = true;
				Options.Lockout.MaxFailedAccessAttempts = 5;
				Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			})).AddEntityFrameworkStores<ShopDbContext>();



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
                pattern: "{controller=Account}/{action=SignIn}/{id?}");

            app.Run();
        }
    }
}
