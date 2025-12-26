using Hamkare.Common.Entities.Generics;
using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Common.Entities.Identity;

[Display(Name = nameof(EntitiesResources.UserGroup), ResourceType = typeof(EntitiesResources))]
public class UserCategory : CategoryEntity<UserCategory>
{
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}