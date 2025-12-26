using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.UserPanel.ViewModels;

public class MyFavoriteViewModel
{
    public long Id { get; set; }

    [Display(Name = nameof(GlobalResources.Name), ResourceType = typeof(GlobalResources))]
    public string Name { get; set; }

    public string Type { get; set; }
}