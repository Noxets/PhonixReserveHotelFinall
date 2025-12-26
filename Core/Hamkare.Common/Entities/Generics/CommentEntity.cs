using Hamkare.Common.Interface.Entities;
using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Utility.Attributes.Core;

namespace Hamkare.Common.Entities.Generics;

public class CommentEntity : RootEntity, ICommentEntity
{
    [Display(Name = nameof(CommentResources.Text), ResourceType = typeof(CommentResources))]
    public string Text { get; set; }

    [Display(Name = nameof(CommentResources.IsRead), ResourceType = typeof(CommentResources))]
    public bool IsRead { get; set; }

    [CoreRequired]
    [Display(Name = nameof(CommentResources.Anonymous), ResourceType = typeof(CommentResources))]
    public bool Anonymous { get; set; } = true;

    [Display(Name = nameof(CommentResources.Like), ResourceType = typeof(CommentResources))]
    public long Like { get; set; }

    [Display(Name = nameof(CommentResources.Dislike), ResourceType = typeof(CommentResources))]
    public long Dislike { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    [CoreRange(0, 5)]
    [Display(Name = nameof(CommentResources.Rating), ResourceType = typeof(CommentResources))]
    public decimal Rating { get; set; }
    
    public long? ParentId { get; set; }

    public long? ActivatorId { get; set; }
}