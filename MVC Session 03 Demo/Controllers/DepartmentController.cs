using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Models;
using System;

namespace MVC_Session_03_Demo.Controllers
{
	public class DepartmentController : Controller
	{

		private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment env)
		{
			_departmentRepo = departmentRepository;
            _env = env;
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

		[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details") 
		{
			if (!id.HasValue)
				return BadRequest();

			var department = _departmentRepo.Get(id.Value);

			if (department is null)
				return NotFound();	

			return View(viewName,department);		
		}

        [HttpGet]
        public IActionResult Edit(int? id)
        {
			return Details(id, "Edit");
        }

		
		[HttpPost]
        public IActionResult Edit([FromRoute]int id, Department department)
		{

			if(id != department.Id)
				return BadRequest();

			if (!ModelState.IsValid) 
				return View(department);	

			try
			{
				_departmentRepo.Update(department);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex) 
			{
				if(_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);

				else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Department");

				return View(department);
            }
		}
    }
}
