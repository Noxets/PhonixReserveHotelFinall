using MudBlazor;
using Hamkare.Resource;

namespace Hamkare.Utility.Extensions;

public static class MudExtension
{
    public static void IsActive(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(GlobalResources.ActiveMessage, entityName), Severity.Success);
    }

    public static void IsDeactivate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(GlobalResources.DeactivateMessage, entityName), Severity.Success);
    }

    public static void SuccessAddOrUpdate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(GlobalResources.SubmitSuccessMessage, entityName), Severity.Success);
    }

    public static void ErrorAddOrUpdate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(ErrorResources.ErrorSubmit, entityName), Severity.Error);
    }

    public static void Delete(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(ErrorResources.RequiredNotFound, entityName), Severity.Error);
    }

    public static void ErrorRequired(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(ErrorResources.RequiredNotFound, entityName), Severity.Error);
    }

    public static void Success(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Success);
    }

    public static void Error(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Error);
    }

    public static void Error(this ISnackbar snackbar, List<string> messages)
    {
        foreach (var item in messages)
            snackbar.Add(item, Severity.Error);
    }

    public static void Warning(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Warning);
    }

    public static void ShowErrorSnackbar(this Exception exception, ISnackbar snackbar, string entityName)
    {
        if (exception.Source == "Validate" || exception.Source == "Custom")
            snackbar.Error(exception.Message);
        else
            snackbar.ErrorAddOrUpdate(entityName);
    }

    public static async Task<bool?> RemoveDialog(this IDialogService dialogService)
    {
        return await dialogService.ShowMessageBox(
            GlobalResources.Remove,
            GlobalResources.RemoveMessage,
            yesText: ViewResources.Submit, cancelText: GlobalResources.Cancel);
    }

    public static async Task<bool?> OperationDialog(this IDialogService dialogService, string title, string message)
    {
        return await dialogService.ShowMessageBox(
            title,
            message,
            yesText: ViewResources.Submit, cancelText: GlobalResources.Cancel);
    }
}