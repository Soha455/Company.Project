using Company.Project.BLL.Interfaces;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        // Ask CLR to create EmployeeRepository object 
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] //Get: /Employee/Index
        public IActionResult Index()
        {
            var Employees = _employeeRepository.GetAll();

            return View(Employees);
        }

        [HttpGet] //Get: /Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    CreateAt = model.CreateAt

                };

                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { statusCode = 404, message = $"Employee with Id :{id} is not found" });

            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();   //400
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();   //400
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }

    }
}
