using System.ComponentModel.DataAnnotations;
using Hamkare.Resource;

namespace Hamkare.Common.Interface.Entities;

public interface IMainEntity : INameEntity
{
    [Display(Name = nameof(GlobalResources.Order), ResourceType = typeof(GlobalResources))]
    public int Order { get; set; }

    public new string Name { get; set; }
    
    public string Description { get; set; }

    new bool Validate(out List<string> errors);
}