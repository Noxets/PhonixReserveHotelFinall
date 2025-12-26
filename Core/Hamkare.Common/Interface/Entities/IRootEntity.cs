using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;

namespace Hamkare.Common.Interface.Entities;

public interface IRootEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = nameof(GlobalResources.Id), ResourceType = typeof(GlobalResources))]
    public long Id { get; set; }

    [CoreRequired]
    [Display(Name = nameof(GlobalResources.Active), ResourceType = typeof(GlobalResources))]
    public bool Active { get; set; }

    [Display(Name = nameof(GlobalResources.Deleted), ResourceType = typeof(GlobalResources))]
    public bool Deleted { get; set; }

    [Display(Name = nameof(GlobalResources.System), ResourceType = typeof(GlobalResources))]
    public bool System { get; set; }

    [ForeignKey("CreatorId")]
    [Display(Name = nameof(GlobalResources.CreatorId), ResourceType = typeof(GlobalResources))]
    public long CreatorId { get; set; }

    [ForeignKey("EditorId")]
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