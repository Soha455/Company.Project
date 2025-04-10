using Company.Project.DAL.Models;
using Company.Project.PL.Dtos;
using Company.Project.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string SearchInput)
        {
            IEnumerable<RoleReturnToDto> roles;         // reference of class can refer to any object 
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleReturnToDto()
                {
                    Id = U.Id,
                    Name = U.Name
                });
            }
            else
            {
                roles = _roleManager.Roles.Select(U => new RoleReturnToDto()
                {
                    Id = U.Id,
                    Name = U.Name
                }).Where(U => U.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            return View(roles);
        }

        [HttpGet] //Get: /Role/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleReturnToDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded) 
                    { return RedirectToAction(nameof(Index)); }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return NotFound(new { statusCode = 404, message = $"User with Id :{id} is not found" });
            var dto = new RoleReturnToDto()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id, string viewName = "Edit")
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleReturnToDto model, string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");

                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invalid Operation !");
                
                var roleResult = await _roleManager.FindByNameAsync(model.Name);
                
                if (roleResult is null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded) 
                        return RedirectToAction(nameof(Index));

                }

                ModelState.AddModelError("" , "Invalid Operation !");
            }
            return View(viewName, model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleReturnToDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation !");

                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("Invalid Operation !");

                var roleResult = await _roleManager.FindByNameAsync(model.Name);

                 role.Name = model.Name;
                 var result = await _roleManager.DeleteAsync(role);
                 if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Invalid Operation !");
            }
            return View(model);
        }

    }
}
