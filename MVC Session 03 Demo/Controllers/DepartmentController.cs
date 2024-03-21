using Microsoft.AspNetCore.Mvc;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;

namespace MVC_Session_03_Demo.Controllers
{
	public class DepartmentController : Controller
	{

		private readonly IDepartmentRepository _departmentRepo;

		public DepartmentController(IDepartmentRepository departmentRepository)
		{
			_departmentRepo = departmentRepository;
		
		}

		public IActionResult Index()
		{
			var departments = _departmentRepo.GetAll();	
			return View(departments);
		}
	}
}
