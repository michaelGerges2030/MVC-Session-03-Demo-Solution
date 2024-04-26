using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVC_Session_03_Demo.Extensions;
using Route.C41.G03.DAL.Data;
using MVC_Session_03_Demo.Helpers;
using Route.C41.G03.DAL.Models;
using System;
using Route.C41.G03.DAL.Data.Configurations;


namespace MVC_Session_03_Demo
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services

			webApplicationBuilder.Services.AddControllersWithViews();

			//services.AddTransient<ApplicationDbContext>();
			//services.AddSingleton<ApplicationDbContext>();
			webApplicationBuilder.Services.AddScoped<ApplicationDbContext>();

			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			}, ServiceLifetime.Scoped)/*.AddApplicationServices()*/;


			//ApplicationServicesExtensions.AddApplicationServices(services);
			webApplicationBuilder.Services.AddApplicationServices();

			webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));


			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredLength = 7;

				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

				options.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			//services.AddAuthentication();

			//services.AddScoped<UserManager<ApplicationUser>>();
			//services.AddScoped<SignInManager<ApplicationUser>>();
			//services.AddScoped<RoleManager<ApplicationUser>>();


			webApplicationBuilder.Services.ConfigureApplicationCookie(options =>
			{
				//options.AccessDeniedPath = "/Account/AccessDenied";
				options.AccessDeniedPath = "/Home/Error";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.LoginPath = "/Account/SignIn";
			});


			webApplicationBuilder.Services.AddAuthentication(options =>
			{
				//options.DefaultAuthenticateScheme = "Identity.Application";
				//options.DefaultChallengeScheme = "Identity.Application";

			}); 
			#endregion
		
		var app = webApplicationBuilder.Build();

			#region Configure Kestrel Middlewares

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
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

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});

			#endregion

			app.Run();

		}
	}
}
