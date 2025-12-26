using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Entities.Identity;
using Hamkare.Common.Interface.Entities;
using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;

namespace Hamkare.Common.Entities.Generics;

public class RootEntity : IRootEntity
{
    [ForeignKey("CreatorId")]
    [NotMapped] public virtual User Creator { get; set; }

    [ForeignKey("EditorId")]
    [NotMapped] public virtual User Editor { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = nameof(GlobalResources.Id), ResourceType = typeof(GlobalResources))]
    public long Id { get; set; }

    [CoreRequired] public bool Active { get; set; } = true;

    public bool Deleted { get; set; }

    public bool System { get; set; }

    
    [Display(Name = nameof(GlobalResources.CreatorId), ResourceType = typeof(GlobalResources))]
    public long CreatorId { get; set; }

    
    [Display(Name = nameof(GlobalResources.EditorId), ResourceType = typeof(GlobalResources))]
    public long? EditorId { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(GlobalResources.CreateDate), ResourceType = typeof(GlobalResources))]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(GlobalResources.EditDate), ResourceType = typeof(GlobalResources))]
    public DateTime? EditDate { get; set; }

    public virtual bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        return errors.Any();
    }
}