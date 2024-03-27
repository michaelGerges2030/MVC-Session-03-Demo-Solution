using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Session_03_Demo.ViewModels;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Session_03_Demo.Controllers
{
	public class DepartmentController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper,IDepartmentRepository departmentRepository, IWebHostEnvironment env)
		{
            _mapper = mapper;
            _departmentRepo = departmentRepository;
            _env = env;
        }

		public IActionResult Index()
		{
			var departments = _departmentRepo.GetAll();
			var mappedDepts = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
			return View(mappedDepts);
		}

		[HttpGet]
		public IActionResult Create() 
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
           if (ModelState.IsValid) 
			{

				var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
				
				var count = _departmentRepo.Add(mappedDept);	
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
		   return View(departmentVM);	
        }

		//[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details") 
		{
			if (!id.HasValue)
				return BadRequest();

			var department = _departmentRepo.Get(id.Value);

			var mappedDept = _mapper.Map<Department, DepartmentViewModel>(department);

			if (department is null)
				return NotFound();	

			return View(viewName,mappedDept);		
		}

        [HttpGet]
        public IActionResult Edit(int? id)
        {
			return Details(id, "Edit");
        }

		
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, DepartmentViewModel departmentVM)
		{

			if(id != departmentVM.Id)
				return BadRequest();

			if (!ModelState.IsValid) 
				return View(departmentVM);	

			try
			{
				var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
				_departmentRepo.Update(mappedDept);
				return RedirectToAction(nameof(Index));
			}
			catch(Exception ex) 
			{
				if(_env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);

				else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Department");

				return View(departmentVM);
            }
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(DepartmentViewModel departmentVM) 
		{
			try
			{
				var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
            _departmentRepo.Delete(mappedDept);
            return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Department");

                return View(departmentVM);
            }
        }

	}
}
