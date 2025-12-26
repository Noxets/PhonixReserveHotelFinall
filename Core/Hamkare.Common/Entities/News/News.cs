using Hamkare.Resource;
using Hamkare.Resource.News;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Entities.Generics;
using Hamkare.Utility.Attributes;

namespace Hamkare.Common.Entities.News;

[Module]
[Display(Name = nameof(EntitiesResources.News), ResourceType = typeof(EntitiesResources))]
public class News : MainEntity
{
    [Display(Name = nameof(NewsResources.CanComment), ResourceType = typeof(NewsResources))]
    public bool CanComment { get; set; }

    [Display(Name = nameof(NewsResources.Views), ResourceType = typeof(NewsResources))]
    public long Views { get; set; }

    [Display(Name = nameof(CommentResources.Like), ResourceType = typeof(CommentResources))]
    public long Like { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(NewsResources.ReleaseDate), ResourceType = typeof(NewsResources))]
    public DateTime ReleaseDate { get; set; }
   
    public long? NewsGroupId { get; set; }

    [ForeignKey("NewsGroupId")]
    public virtual NewsCategory NewsCategory { get; set; }

    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();

    public decimal? AverageRating => NewsComments.GetAnyActive()
        ? Math.Round(NewsComments.GetAllActive().Average(x => x.Rating), 1)
        : null;  
    
    public override bool Validate(out List<string> errors)
    {
        base.Validate(out errors);

        return errors.Any();
    }
}