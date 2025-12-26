using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamkare.Common.Interface.Entities;

public interface ICategoryEntity<TCategoryEntity> : IMainEntity
{
    [ForeignKey("ParentId")]
    [Display(Name = nameof(GlobalResources.ParentId), ResourceType = typeof(GlobalResources))]
    public long? ParentId { get; set; }

    [CoreRequired]
    [Display(Name = nameof(GlobalResources.ShowInMenu), ResourceType = typeof(GlobalResources))]
    public bool ShowInMenu { get; set; }

    [InverseProperty("Parent")]
    public ICollection<TCategoryEntity> InverseParent { get; set; }

    [InverseProperty("InverseParent")]
    [Display(Name = nameof(GlobalResources.Parent), ResourceType = typeof(GlobalResources))]
    public TCategoryEntity Parent { get; set; }
}