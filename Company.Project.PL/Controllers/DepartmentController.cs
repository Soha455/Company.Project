using Company.Project.BLL.Interfaces;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        // Ask CLR to create DepartmentRepository object 
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] //Get: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();

            return View(departments);
        }

        [HttpGet] //Get: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //Get: /Department/Create
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department() 
                {
                   Code = model.Code,
                   Name = model.Name,
                   CreateAt = model.CreateAt
                };

                var count = _departmentRepository.Add(department);
                if (count > 0)
                { 
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
