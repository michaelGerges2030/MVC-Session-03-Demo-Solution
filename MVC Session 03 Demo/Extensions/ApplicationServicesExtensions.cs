using Microsoft.Extensions.DependencyInjection;
using MVC_Session_03_Demo.Services.EmailSender;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;

namespace MVC_Session_03_Demo.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddTransient<IEmailSender, EmailSender>(); 

            services.AddScoped<IUnitOfWork, UnitOfWork>();  
            return services;
        }

    }
}
