using Microsoft.Extensions.DependencyInjection;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;

namespace MVC_Session_03_Demo.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

    }
}
