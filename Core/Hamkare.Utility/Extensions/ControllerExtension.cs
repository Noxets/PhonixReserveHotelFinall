using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Hamkare.Resource.Identity;
using SignInResult=Microsoft.AspNetCore.Identity.SignInResult;

namespace Hamkare.Utility.Extensions;

public static class ControllerExtension
{
    public static async Task<string> RenderViewToStringAsync<TModel>(this Controller controller, string viewName, TModel model)
    {
        var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
        var viewData = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        await using var sw = new StringWriter();

        var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

        if (viewResult.View == null)
            throw new ArgumentNullException($"{viewName} does not match any available view");

        var viewContext = new ViewContext(
        controller.ControllerContext,
        viewResult.View,
        viewData,
        controller.TempData,
        sw,
        new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);
        return sw.ToString();
    }

    public static void AddErrors(this Controller controller, IdentityResult result)
    {
        AddErrors(controller, result.GetIdentityErrors());
    }

    public static void AddErrors(this Controller controller, SignInResult result)
    {
        AddErrors(controller, result.GetIdentityErrors());
    }

    public static void AddErrors(this Controller controller, IEnumerable<string> errors)
    {
        foreach (var err in errors)
        {
            controller.ModelState.AddModelError(string.Empty, err);
        }
    }

    public static void AddErrors(this Controller controller, List<string> errors)
    {
        foreach (var err in errors)
        {
            controller.ModelState.AddModelError(string.Empty, err);
        }
    }

    public static void AddErrors(this Controller controller, string key, IEnumerable<string> errors)
    {
        foreach (var err in errors)
        {
            controller.ModelState.AddModelError(key, err);
        }
    }

    public static void AddErrors(this Controller controller, string err)
    {
        controller.ModelState.AddModelError(string.Empty, err);
    }

    public static void AddErrors(this Controller controller, string key, string err)
    {
        controller.ModelState.AddModelError(key, err);
    }

    public static IEnumerable<string> GetIdentityErrors(this IdentityResult result)
    {
        var errors = new List<string>();

        foreach (var error in result.Errors)
        {
            var err = error.Description;

            if (error.Code == "DuplicateUserName")
                errors.Add(IdentityResources.ErrorDuplicateUsername);

            if (err.StartsWith("Passwords must have at least one uppercase") || err.StartsWith("Passwords must have at least one lowercase") || err.StartsWith("Incorrect password") || err.StartsWith("Passwords must be at least 6 characters") || err.StartsWith("Passwords must have at least one digit"))
                errors.Add(IdentityResources.ErrorPasswordIsNotValid);

            if (err.StartsWith("Invalid token"))
                errors.Add(IdentityResources.InvalidToken);
        }

        return errors.ToArray();
    }

    public static IEnumerable<string> GetIdentityErrors(this SignInResult result)
    {
        var errors = new List<string>();

        if (result.IsLockedOut)
            errors.Add(IdentityResources.IsLockOut);

        if (result.IsNotAllowed)
            errors.Add(IdentityResources.IsNotAllowed);

        if (result.RequiresTwoFactor)
            errors.Add(IdentityResources.RequiresTwoFactor);

        return errors.ToArray();
    }
}