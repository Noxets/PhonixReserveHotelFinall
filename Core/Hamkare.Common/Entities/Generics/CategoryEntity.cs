using Hamkare.Common.Interface.Entities;
using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Utility.Attributes.Core;

namespace Hamkare.Common.Entities.Generics;

public class CategoryEntity<TGroupEntity> : MainEntity, ICategoryEntity<TGroupEntity>
{
    [Display(Name = nameof(GlobalResources.ParentId), ResourceType = typeof(GlobalResources))]
    public long? ParentId { get; set; }

    [CoreRequired]
    [Display(Name = nameof(GlobalResources.ShowInMenu), ResourceType = typeof(GlobalResources))]
    public bool ShowInMenu { get; set; } = true;

    public virtual ICollection<TGroupEntity> InverseParent { get; set; } = new List<TGroupEntity>();

    [Display(Name = nameof(GlobalResources.Parent), ResourceType = typeof(GlobalResources))]
  
    [ForeignKey("ParentId")]
    public virtual TGroupEntity Parent { get; set; }
}