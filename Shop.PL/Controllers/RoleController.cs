using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Entites.Identity;
using Shop.PL.Models.Roles;

namespace Shop.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        #region Index
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var Role = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Role);
                return View(MappedRole);
            }
            else
            {
                var SearchRole = await _roleManager.FindByNameAsync(search);
                var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(SearchRole);
                return View(MappedRole);
            }

        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var MappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                    var CreateRole = await _roleManager.CreateAsync(MappedRole);
                    if (CreateRole.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var Role in CreateRole.Errors)
                        ModelState.AddModelError("", Role.Description);
                }

                ModelState.AddModelError("", "Invalid Create");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
                return View(model);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string Id)
        {
            if (Id is null)
                return NotFound();
            var Role= await _roleManager.FindByIdAsync(Id);
            return View(Role);

        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {

            if (Id is null)
                return NotFound();
            var Role=await _roleManager.FindByIdAsync(Id);
            if(Role is null)
                return NotFound();

            return View(new RoleViewModel()
            {
                Id = Role.Id,
                RoleName = Role.Name
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id,RoleViewModel model)
        {
            try
            {
                if (Id is null)
                    return NotFound();
                if (!ModelState.IsValid)
                    ModelState.AddModelError("", "Invalid Update");
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                var UpdateRole = await _roleManager.UpdateAsync(mappedRole);
                if (UpdateRole.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                
            }
                return View(model);
           
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id is null)
                return NotFound();
            var Role=await _roleManager.FindByIdAsync(Id);
            if(Role is null)
                return BadRequest();
            var DeleteRole=await _roleManager.DeleteAsync(Role);
            if (DeleteRole.Succeeded)
                return RedirectToAction(nameof(Index));
            foreach (var error in DeleteRole.Errors)
                ModelState.AddModelError("", error.Description);
            return RedirectToAction(nameof(Index));
            
        }
        #endregion

        #region Add Or Remove Users
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            ViewData["RoleId"] = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            var users = await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserInRoleViewModel>();

            foreach (var user in users)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                usersInRole.Add(new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = isInRole
                });
            }

            ViewData["RoleId"] = roleId;
            return View(usersInRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId, List<UserInRoleViewModel> RoleinUser)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                return NotFound();
            }

            foreach (var user in RoleinUser)
            {
                var appUser = await _userManager.FindByIdAsync(user.UserId);
                if (appUser != null)
                {
                    if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                    {
                        // Add user to the role if they are selected and not already in the role
                        await _userManager.AddToRoleAsync(appUser, role.Name);
                    }
                    else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                    {
                        // Remove user from the role if they are not selected and are currently in the role
                        await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region AccessDenid
        public IActionResult AccessDenied()
        {
            return View();
        } 
        #endregion

    }
}
