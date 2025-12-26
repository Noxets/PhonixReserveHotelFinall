using Hamkare.Common.Entities.Generics;
using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Common.Entities.News;

[Display(Name = nameof(EntitiesResources.NewsGroup), ResourceType = typeof(EntitiesResources))]
public class NewsCategory : CategoryEntity<NewsCategory>
{
    public virtual ICollection<News> News { get; set; }
}