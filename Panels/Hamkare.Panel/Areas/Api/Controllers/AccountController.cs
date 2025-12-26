using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Enums;
using Hamkare.Panel.ViewModels.ControllerViewModel;
using Hamkare.Resource.Identity;
using Hamkare.Service.Services.Identity;
using Hamkare.Utility.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Panel.Areas.Api.Controllers;

[Route("Api/[controller]/[action]")]
public class AccountController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    UserService userService,
    RoleService roleService)
    : ApiController
{
    #region Confirm

    [HttpGet]
    public async Task<IActionResult> PhoneConfirm(string phoneNumber, string token, bool isPersistent, string returnUrl,
        CancellationToken cancellationToken = default)
    {
        var user = await userService.GetRegisterUserByPhoneNumber(phoneNumber, cancellationToken);

        if (user == null)
        {
            ViewBag.ErrorList = new List<ErrorViewModel>
            {
                new()
                {
                    ToastType = ToastType.Danger,
                    Title = IdentityResources.ErrorUserNotFind,
                    Description = IdentityResources.ErrorUserNotFind
                }
            };

            return LocalRedirect("/Error");
        }

        var result = await userManager.VerifyChangePhoneNumberTokenAsync(user, token, phoneNumber);

        if (result)
        {
            user.PhoneNumberConfirmed = true;
            user.LockoutEnabled = false;
            await userService.AddOrUpdateAsync(user, cancellationToken);

            await signInManager.SignInAsync(user, true);

            var roles = await userManager.GetRolesAsync(user);
            var role = await roleService.GetAsync(x => roles.Contains(x.Name), cancellationToken);
            
            return LocalRedirect(string.IsNullOrEmpty(returnUrl)
                ? $"/{role?.ReturnUrl}"
                : returnUrl);
        }

        ViewBag.ErrorList = new List<ErrorViewModel>
        {
            new()
            {
                ToastType = ToastType.Danger,
                Title = IdentityResources.InvalidToken,
                Description = IdentityResources.InvalidToken
            }
        };

        return LocalRedirect("/Error");
    }

    #endregion

    #region Login

    [HttpGet]
    public async Task<IActionResult> LoginWithPassword(string userName, string password, string returnUrl,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            ViewBag.ErrorList = new List<ErrorViewModel>
            {
                new()
                {
                    ToastType = ToastType.Warning,
                    Title = IdentityResources.Login,
                    Description = IdentityResources.ErrorLoginFill
                }
            };

            return LocalRedirect("/Error");
        }

        var user = await userService.GetActiveAsync(x => x.UserName == userName, cancellationToken);

        if (user != null)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password,
                true, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                var role = await roleService.GetAsync(x => roles.Contains(x.Name), cancellationToken);
                
                return LocalRedirect(string.IsNullOrEmpty(returnUrl)
                    ? $"/{role?.ReturnUrl}"
                    : returnUrl);
            }

            foreach (var item in result.GetIdentityErrors())
                ViewBag.ErrorList = result.GetIdentityErrors().Select(_ => new ErrorViewModel
                {
                    ToastType = ToastType.Danger,
                    Title = IdentityResources.Login,
                    Description = item
                }).ToList();

            return LocalRedirect("/Error");
        }

        ViewBag.ErrorList = new List<ErrorViewModel>
        {
            new()
            {
                ToastType = ToastType.Danger,
                Title = IdentityResources.ErrorUserNotFind,
                Description = IdentityResources.ErrorUserNotFind
            }
        };

        return LocalRedirect("/Error");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await signInManager.SignOutAsync();

        return LocalRedirect("/");
    }

    #endregion
}