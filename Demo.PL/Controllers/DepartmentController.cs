using Demo.BLL.DTOs;
using Demo.BLL.DTOs.Department;
using Demo.BLL.Services.Interfaces;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly ILogger<HomeController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController (IDepartmentService departmentService , ILogger<HomeController> logger , IWebHostEnvironment environment)
        {
            this.departmentService = departmentService;
            this.logger = logger;
            this.environment = environment;
        }


        [HttpGet]
        public IActionResult index() {
            var departments = departmentService.GetAllDepartments();
            return View(departments);
        }

        [HttpGet]
        public IActionResult create() { 
        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(DepartmentViewModel departmentModel) {

            if (ModelState.IsValid) {

                try {
                    var departmentDto = new CreatedDepartmentDto()
                    {

                        Code = departmentModel.Code,
                        Name = departmentModel.Name,
                        Description = departmentModel.Description,
                        DateOfCreation = departmentModel.DateOfCreation,

                    };
                    int res = departmentService.AddDepartment(departmentDto);
                    string message;

                    if (res > 0) message = $"Depatment {departmentModel.Name} created successfully"; 
                    else
                        message = $"Depatment {departmentModel.Name} not created";

                    TempData["message"] = message ;

                    return RedirectToAction(nameof(index));

                } catch (Exception ex) {

                    if (environment.IsDevelopment()) {
                        ModelState.AddModelError(string.Empty,ex.Message);
                        return View(departmentModel);
                    }
                    else { 
                        return View(departmentModel);
                    }
                    
                
                }
            
            
            
            } else return View(departmentModel);
            
            
        
        
        }


        [HttpGet]
        public IActionResult Details(int? id) {

            if (!id.HasValue) return BadRequest();
            var department = departmentService.GetById(id.Value);

            if (department is null) return NotFound();

            return View(department);
            
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = departmentService.GetById(id.Value);

            if (department is null) return NotFound();

            var departmentView = new DepartmentViewModel()
            {

                Name = department.Name,
                Code = department.Code,
                DateOfCreation = department.DateOfCreation,
                Description = department.Description,
            };

            return View(departmentView);

        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var updateDto = new UpdateDepartmentDto() {

                        Id = id,
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation,
                    };
                    var res = departmentService.UpdateDepartment(updateDto);
                    if (res > 0) return RedirectToAction(nameof(index));
                    else return View(viewModel);
                }
                catch (Exception ex)
                { 

                        return View(viewModel);

                }



            }
            else return View(viewModel);

        }


        [HttpGet]
        public IActionResult Delete(int? id) {

            if (!id.HasValue) return BadRequest();
            var department = departmentService.GetById(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            if (id == 0) return BadRequest();
            try { 
            
                bool isDeleted = departmentService.DeleteDepartment(id);

                if (isDeleted) return RedirectToAction(nameof(index));

                else ModelState.AddModelError(string.Empty , "Department Can not be deleted");

                return RedirectToAction(nameof(Delete), new { id });
            
            } catch (Exception ex) {

                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                    return RedirectToAction(nameof(index));
                }
                else
                {
                    return View("ErrorView" , ex);
                }


            }
        }



    }
}
