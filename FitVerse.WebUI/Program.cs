using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.MapperConfigs;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Repositories;
using FitVerse.Data.Service;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Data.UnitOfWork;
using FitVerse.Data.UnitOfWork;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;

namespace FitVerse.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<FitVerseDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(op=>op.AddProfile(typeof(MapperConfig)));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.User.AllowedUserNameCharacters = null;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<FitVerseDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
              options =>
              {
                  options.LoginPath = new PathString("/Account/Login");
                  options.AccessDeniedPath = new PathString("/Account/AccessDenied");
              });
            //Allow UserName Duplication
            builder.Services.AddScoped<IUserValidator<ApplicationUser>, AllowDuplicateUsernameValidator<ApplicationUser>>();


            builder.Services.AddControllers()//make json case sensitive
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            builder.Services.AddScoped<IAnatomyRepository, AnatomyRepository>();
            builder.Services.AddScoped<IMuscleRepository, MuscleRepository>();
            builder.Services.AddScoped<ICoachRepository, CoachRepository>();
            builder.Services.AddScoped<IUnitOFWorkService, UnitOfWorkService>();
            builder.Services.AddScoped<IMuscleService, MuscleService>();





            builder.Services.AddAutoMapper(op=>op.AddProfile(typeof(MapperConfig)));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
