using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Interface.Entities;
using Hamkare.Resource;

namespace Hamkare.Common.Entities.Generics;

public class MainEntity : RootEntity, IMainEntity
{
    [Display(Name = nameof(GlobalResources.Order), ResourceType = typeof(GlobalResources))]
    public int Order { get; set; } = 1;

    [NotMapped]
    [Display(Name = nameof(GlobalResources.Name), ResourceType = typeof(GlobalResources))]
    public virtual string Name { get; set; }
    
    [NotMapped]
    [Display(Name = nameof(GlobalResources.Description), ResourceType = typeof(GlobalResources))]
    public virtual string Description { get; set; }
}