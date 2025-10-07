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
    public class EmployeesController(IEmployeeService employeeService, IWebHostEnvironment environment) : Controller
    {
        public IActionResult Index() { 
            
            var employees = employeeService.GetAllEmployees();
            return View(employees);
        
        }

        [HttpGet]
        public IActionResult Create() {   
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto ) {

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
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto dto) {

            if(id != dto.Id) return BadRequest();

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

                    return View(dto);

                }
            }
            else return View(dto);



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

