﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G03.BLL.Interfaces;
using Route.C41.G03.DAL.Models;
using System;

namespace MVC_Session_03_Demo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IEmployeeRepository employeeRepository,IWebHostEnvironment env /*IDepartmentRepository departmentRepo*/)
        {
            _employeeRepo = employeeRepository;
            _env = env;
            //_departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            TempData.Keep();

            //ViewData["Message"] = "Hello, ViewData";
            //ViewBag.Message = "Hello, ViewBag";

            var employees = _employeeRepo.GetAll();  
            return View(employees);
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
        public IActionResult Create(Employee employee)
        {
        if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                
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
        return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details") 
        {
        if (!id.HasValue)
                return BadRequest();    
        
        var employee = _employeeRepo.Get(id.Value);

            if (employee is null)
                return NotFound();

            return View(viewName, employee);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _employeeRepo.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Employee");

                return View(employee);  
            }

        }


        public IActionResult Delete (int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);

                else
                    ModelState.AddModelError(string.Empty, "An Error occured during updating the Employee");

                return View(employee);
            }
        }
    }
}
