using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using MVC_Session_03_Demo.Helpers;
using MVC_Session_03_Demo.ViewModels;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.BLL.Repositories;
using Route.C41.G03.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Session_03_Demo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper,IHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public IActionResult Index(string searchInp)
        {
            var employees = Enumerable.Empty<Employee>();
            var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(searchInp))
            {
                 employees = employeeRepo.GetAll();
            }
            else
            {
                employees = employeeRepo.SearchByName(searchInp.ToLower());
            }

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
        if (ModelState.IsValid)
            {
               employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");

                var mapperEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                 _unitOfWork.Repository<Employee>().Add(mapperEmp);

                var count = _unitOfWork.Complete();

                if (count > 0)
                {    
                    TempData["Message"] = "Department is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "An Error Has Occured, Department Not Created :(";
                }
                return RedirectToAction(nameof(Index));
            }
        return View(employeeVM);
        }

        public IActionResult Details(int? id, string viewName = "Details") 
        {
        if (!id.HasValue)
                return BadRequest();    
        
        var employee = _unitOfWork.Repository<Employee>().Get(id.Value);


        var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();

            return View(viewName, mappedEmp);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repository<Employee>().Update(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Employee");

                return View(employeeVM);  
            }

        }


        public IActionResult Delete (int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.Repository<Employee>().Delete(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Employee");

                return View(employeeVM);
            }
        }
    }
}
