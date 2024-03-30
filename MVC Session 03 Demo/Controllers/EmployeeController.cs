using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using MVC_Session_03_Demo.ViewModels;
using Route.C41.G03.BLL.Interfaces;
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
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper,IHostEnvironment env /*IDepartmentRepository departmentRepo*/)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            //_departmentRepo = departmentRepo;
        }

        public IActionResult Index(string searchInp)
        {
            //TempData.Keep();

            //ViewData["Message"] = "Hello, ViewData";
            //ViewBag.Message = "Hello, ViewBag";

            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(searchInp))
            {
                 employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.SearchByName(searchInp.ToLower());
            }

            var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
        if (ModelState.IsValid)
            {

                //var mapperEmp = new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Address = employeeVM.Address,
                //    Age = employeeVM.Age,   
                //    Salary = employeeVM.Salary,
                //    Email = employeeVM.Email,
                //    PhoneNumber = employeeVM.PhoneNumber,
                //    IsActive = employeeVM.IsActive,
                //    HiringDate = employeeVM.HiringDate,
                //    CreationDate = employeeVM.CreationDate,
                //    IsDeleted = employeeVM.IsDeleted
                //};

                var mapperEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);    

                 _unitOfWork.EmployeeRepository.Add(mapperEmp);

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
        
        var employee = _unitOfWork.EmployeeRepository.Get(id.Value);


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
                _unitOfWork.EmployeeRepository.Update(mappedEmp);
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
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
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
