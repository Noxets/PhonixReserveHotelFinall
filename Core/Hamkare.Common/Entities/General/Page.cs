using Hamkare.Resource;
using Hamkare.Resource.General;
using System.ComponentModel.DataAnnotations;
using Hamkare.Common.Entities.Generics;
using Hamkare.Utility.Attributes.Core;

namespace Hamkare.Common.Entities.General;

[Display(Name = nameof(EntitiesResources.Display), ResourceType = typeof(EntitiesResources))]
public class Page : RootEntity
{
    [CoreRequired]
    [Display(Name = nameof(GeneralResources.PageName), ResourceType = typeof(GeneralResources))]
    public string PageName { get; set; }
}