using Hamkare.Common.Entities.Generics;
using Hamkare.Common.Entities.Identity;
using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamkare.Common.Entities.News;

[Display(Name = nameof(EntitiesResources.NewsComment), ResourceType = typeof(EntitiesResources))]
public class NewsComment : CommentEntity
{
    public long NewsId { get; set; }

    public virtual User Activator { get; set; }

    [ForeignKey("NewsId")]
    public virtual News News { get; set; }
    
    [ForeignKey("ParentId")]
    public virtual NewsComment Parent { get; set; }
}