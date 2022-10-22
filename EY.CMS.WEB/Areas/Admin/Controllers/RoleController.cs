using AutoMapper;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
           
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesMapping = _mapper.Map<List<AppRoleDto>>(roles);
            return View(rolesMapping);
        }

        public IActionResult Added()
        {
            return View();
        }

        public async Task<JsonResult> RoleAdded(AppRoleDto role)
        {
            IdentityResult result = await _roleManager.CreateAsync(_mapper.Map<AppRoleDto, AppRole>(role));
            if (result.Succeeded)
            {
                return Json(true);
            }
            else
            {
                return Json(result.Errors);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var role = _mapper.Map<AppRoleDto>(await _roleManager.FindByIdAsync(id.ToString()));
            return View(role);
        }

        public async Task<JsonResult> RoleEdit(AppRoleDto model)
        {
            AppRole role = await _roleManager.FindByIdAsync(TempData["id"].ToString());

            if (role != null)
            {
                role.Name = model.Name;
                IdentityResult result = _roleManager.UpdateAsync(role).Result;

                if (result.Succeeded)
                {
                    return Json(true);
                }
                else
                {
                    return Json(result.Errors);
                }
            }
            else
            {
                return Json("role bulunamadı");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var role = _mapper.Map<AppRoleDto>(await _roleManager.FindByIdAsync(id.ToString()));
            return View(role);
        }

        public async Task<JsonResult> RoleDelete()
        {
            AppRole role = await _roleManager.FindByIdAsync(TempData["id"].ToString());
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return Json(true);
        }



        //user logics

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            var mappingUsers = _mapper.Map<List<AppUserDto>>(users);
            return View(mappingUsers);
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SignUp(AppUserDto model)
        {
            var appUserMapping = _mapper.Map<AppUserDto, AppUser>(model);
            IdentityResult result = await _userManager.CreateAsync(appUserMapping, model.Password);
            await _userManager.AddToRoleAsync(appUserMapping, "admin");

            if (result.Succeeded)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }

        }


        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;
            AppUser user = _userManager.FindByIdAsync(id).Result;

            ViewBag.userName = user.UserName;

            IQueryable<AppRole> roles = _roleManager.Roles;

            List<string> userroles = _userManager.GetRolesAsync(user).Result as List<string>;

            List<RoleAssignDto> roleAssignViewModels = new List<RoleAssignDto>();

            foreach (var role in roles)
            {
                RoleAssignDto r = new RoleAssignDto();
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userroles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                roleAssignViewModels.Add(r);
            }

            return View(roleAssignViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignDto> roleAssignViewModels)
        {
            AppUser user = await _userManager.FindByIdAsync(TempData["userId"].ToString());

            foreach (var item in roleAssignViewModels)
            {
                if (item.RoleName != null)
                {
                    if (item.Exist)
                    {
                        await _userManager.AddToRoleAsync(user, item.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                    }
                }
            }

            return RedirectToAction("Users");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ChangePassword(ChangePassword input)
        {
            AppUser user =  _userManager.FindByNameAsync(User.Identity.Name).Result;
            bool exist = _userManager.CheckPasswordAsync(user, input.OldPassword).Result;

            if (exist)
            {
                IdentityResult result = _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword).Result;
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();

                    await _signInManager.PasswordSignInAsync(user, input.NewPassword, true, false);

                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(0);
            }

        }
    }
}
