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
        public async Task<IActionResult> Index(string SearchInput)
        {
            IEnumerable<Department> departments;         // reference of class can refer to any object 
            if (string.IsNullOrEmpty(SearchInput))
            {
                departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            }
            else
            {
                departments = await _unitOfWork.DepartmentRepository.GetByNameAsync(SearchInput);
            }
            return View(departments);
        }

        [HttpGet] //Get: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create(CreateDepartmentDto model)
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
                 _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Department is added successfully";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet] 
        public async Task<IActionResult> Details(int? id , string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if(department is null) return NotFound(new { statusCode = 404 , message = $"Department with Id :{id} is not found" });

            return View(viewName ,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not found" });
            var dto = _mapper.Map<CreateDepartmentDto>(department);

            return View(dto);
            //return Details( id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id , CreateDepartmentDto model, string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);

                if (id != department.Id) return BadRequest();   //400

                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(viewName, model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(new { statusCode = 404, message = $"Department with Id :{id} is not found" });

            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();   //400
                 _unitOfWork.DepartmentRepository.Delete(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }



    }
}
