using Microsoft.AspNetCore.Mvc;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Models;

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

		[HttpGet]
		public IActionResult Create() 
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Department department)
        {
           if (ModelState.IsValid) 
			{
				var count = _departmentRepo.Add(department);	
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
		   return View(department);	
        }

    }
}
