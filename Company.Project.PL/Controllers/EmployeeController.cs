using AutoMapper;
using Company.Project.BLL.Interfaces;
using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Company.Project.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections;
using System.Net;

namespace Company.Project.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        // Ask CLR to create EmployeeRepository object and DepartmentRepository for the relationship.
        public EmployeeController(
                                    //IEmployeeRepository employeeRepository ,
                                    //IDepartmentRepository departmentRepository ,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet] //Get: /Employee/Index
        public async Task<IActionResult> Index(string SearchInput)
        {
            IEnumerable<Employee> employees;         // reference of class can refer to any object 
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            //Dictionary : Container for each view in RunTime(Memory)
            // 1.ViewData : Transfer Extra information from controller (Action) to view
            //  ViewData["Message"] = "Hello From ViewData";


            // 2.ViewBag  : Transfer Extra information from controller (Action) to view
            //   ViewBag.Message = "Hello From ViewBag";

            // 3.TempData  : transfer Extra information from Request to another.
            //ex- send message from create view to index view if employee added that it is added successfully
            // in create action

            return View(employees);
        }


        [HttpGet] //Get: /Employee/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); 
            ViewData ["departments"] = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                { 
                    model.ImageName = DocumentSettings.UploadFile(model.Image,"images");
                }
                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Employee is added successfully";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);

            if (employee is null) return NotFound(new { statusCode = 404, message = $"Employee with Id :{id} is not found" });

            var Dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(viewName, Dto);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id , string viewName="Edit")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
           
            if (employee is null) return NotFound(new { statusCode = 404, message = $"Employee with Id :{id} is not found" });
            var dto =_mapper.Map<CreateEmployeeDto>(employee);
            //var employeeDto = new CreateEmployeeDto()
            //{
            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    Address = employee.Address,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    CreateAt = employee.CreateAt

            //};
            //return View(employeeDto);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model , string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "Images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
                }

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                 _unitOfWork.EmployeeRepository.Update(employee);
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
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                if (id != employee.Id) return BadRequest();   //400
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
