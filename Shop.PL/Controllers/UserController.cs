using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Shop.DAL.Entites.Identity;
using Shop.PL.Models.Users;

namespace Shop.PL.Controllers
{
        [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        #region Index
        public async Task<IActionResult> Index(string SearchInput)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(users =>
                users.NormalizedEmail.Trim()
                .Contains(SearchInput.Trim().ToUpper())).ToListAsync();
            }
            return View(users);
        }
        #endregion

        #region Details

        public async Task<IActionResult> Details(string id,string value="Details")
        {
            if (id is null)
                return BadRequest();
            var User=await _userManager.FindByIdAsync(id);
            if (User is null)
                return NotFound();
            if (value == "Edit")
            {
                var UserViewModel = new UpdateUsersViewModel
                {
                    Id = User.Id,
                    FName = User.FirstName,
                    LName = User.LastName,
                   
                };
                return View(value, UserViewModel);
            }
            return View(value,User);
        }
        #endregion

        #region Edit

        public async Task<IActionResult> Edit(string Id)
        {
            if (Id is null)
                return NotFound();
            return await Details(Id,"Edit");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id,UpdateUsersViewModel model)
        {
            try
            {
                if (Id is null)
                    return NotFound();
                if (Id != model.Id)
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    var User = await _userManager.FindByIdAsync(Id);
                    User.FirstName = model.FName;
                    User.LastName = model.LName;
                    await _userManager.UpdateAsync(User);
                        return RedirectToAction(nameof(Index));
                  
                }
                
            }
            catch (Exception ex)
            {
               ModelState.AddModelError("", ex.Message);

            }
                  ModelState.AddModelError("", "Invalid Update");
                  return View(model);
        }
        #endregion


        #region Delete


        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id is null)
                    return NotFound();
                var User = await _userManager.FindByIdAsync(id);
                if (User is null)
                    return BadRequest();
                var DeleteUser = await _userManager.DeleteAsync(User);
                if (DeleteUser.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var item in DeleteUser.Errors)
                    ModelState.AddModelError("", item.Description);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
