using AutoMapper;
using Company.Project.BLL.Interfaces;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Ask CLR to create DepartmentRepository object 
        public DepartmentController(/*IDepartmentRepository departmentRepository*/
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet] //Get: /Department/Index
        public IActionResult Index(string SearchInput)
        {
            IEnumerable<Department> departments;         // reference of class can refer to any object 
            if (string.IsNullOrEmpty(SearchInput))
            {
                departments = _unitOfWork.DepartmentRepository.GetAll();
            }
            else
            {
                departments = _unitOfWork.DepartmentRepository.GetByName(SearchInput);
            }
            return View(departments);
        }

        [HttpGet] //Get: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);
                //var department = new Department() 
                //{
                //   Code = model.Code,
                //   Name = model.Name,
                //   CreateAt = model.CreateAt
                //};
                 _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Comlete();
                if (count > 0)
                {
                    TempData["Message"] = "Department is added successfully";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet] 
        public IActionResult Details(int? id , string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if(department is null) return NotFound(new { statusCode = 404 , message = $"Department with Id :{id} is not found" });

            return View(viewName ,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not found" });

            return Details( id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id ,Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();   //400
                _unitOfWork.DepartmentRepository.Update(department);
                var count = _unitOfWork.Comlete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not found" });

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();   //400
                 _unitOfWork.DepartmentRepository.Delete(department);
                var count = _unitOfWork.Comlete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }



    }
}
