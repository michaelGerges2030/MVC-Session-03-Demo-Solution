using AutoMapper;
using MVC_Session_03_Demo.ViewModels;
using Route.C41.G03.DAL.Data.Migrations;
using Route.C41.G03.DAL.Models;

namespace MVC_Session_03_Demo.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }

    }
}
