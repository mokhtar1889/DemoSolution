using Demo.BLL.DTOs.Department;
using Demo.BLL.DTOs.Employee;
using Demo.BLL.Services.Classes;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Models.EmployeeModel;
using Demo.DAL.Models.Shared;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Demo.PL.Controllers
{
    public class EmployeesController(IEmployeeService employeeService, IWebHostEnvironment environment , IDepartmentService departmentService) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName) { 
            
            var employees = employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        
        }

        [HttpGet]
        public IActionResult Create() {
            ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeModel) {

            var employeeDto = new CreatedEmployeeDto()
            {

                Name = employeeModel.Name,
                Age = employeeModel.Age,
                Address = employeeModel.Address,
                DepartmentId = employeeModel.DepartmentId,
                Gender = employeeModel.Gender,
                HiringDate = employeeModel.HiringDate,
                IsActive = employeeModel.IsActive,
                PhoneNumber = employeeModel.PhoneNumber,
                Salary = employeeModel.Salary,
                Email = employeeModel.Email,
                EmployeeType = employeeModel.EmployeeType,
            };

            if (ModelState.IsValid)
            {

                try
                {

                    int res = employeeService.AddEmployee(employeeDto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else
                    {

                        ModelState.AddModelError(string.Empty, "Department can not be added");
                        return View(employeeDto);

                    }

                }
                catch (Exception ex)
                {

                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(employeeDto);
                    }
                    else
                    {
                        return View(employeeDto);
                    }


                }



            }
            else return View(employeeDto);

        }


        [HttpGet]
        public IActionResult Details(int? id) {

            if (!id.HasValue) return BadRequest();
            var employee = employeeService.GetById(id.Value);

            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id) {

            if (!id.HasValue) return BadRequest();
            var employee = employeeService.GetById(id.Value);

            if (employee is null) return NotFound();

            var empDto = new UpdatedEmployeeDto()
            {

                Id = employee.Id,
                Name = employee.Name,
                HiringDate = employee.HiringDate,
                Address = employee.Address,
                Age = employee.Age,
                Email= employee.Email,
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                Gender  = Enum.Parse<Gender>(employee.Gender),
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                IsActive = employee.IsActive,


            };

            return View(empDto);

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeModel) {

            var dto = new UpdatedEmployeeDto()
            {
                Id = id ,
                Name = employeeModel.Name,
                Age = employeeModel.Age,
                Address = employeeModel.Address,
                DepartmentId = employeeModel.DepartmentId,
                Gender = employeeModel.Gender,
                HiringDate = employeeModel.HiringDate,
                IsActive = employeeModel.IsActive,
                PhoneNumber = employeeModel.PhoneNumber,
                Salary = employeeModel.Salary,
                Email = employeeModel.Email,
                EmployeeType = employeeModel.EmployeeType,
            };
            if (ModelState.IsValid)
            {

                try
                {
                    var res = employeeService.UpdateEmployee(dto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else return View(dto);
                }
                catch (Exception ex)
                {

                    return View(employeeModel);

                }
            }
            else return View(employeeModel);



        }


        [HttpPost]
        public IActionResult Delete(int id)
        {

            if (id == 0) return BadRequest();
            try
            {

                bool isDeleted = employeeService.DeleteEmployee(id);

                if (isDeleted) return RedirectToAction(nameof(Index));

                else ModelState.AddModelError(string.Empty, "Employee Can not be deleted");

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("ErrorView", ex);
                }


            }
        }



    }
}

