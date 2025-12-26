using Hamkare.Common.Entities.Identity;
using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamkare.Common.Interface.Entities;

public interface ICommentEntity : IRootEntity
{
    [Display(Name = nameof(CommentResources.Text), ResourceType = typeof(CommentResources))]
    public string Text { get; set; }

    [Display(Name = nameof(CommentResources.IsRead), ResourceType = typeof(CommentResources))]
    public bool IsRead { get; set; }

    [CoreRequired]
    [Display(Name = nameof(CommentResources.Anonymous), ResourceType = typeof(CommentResources))]
    public bool Anonymous { get; set; }

    [Display(Name = nameof(CommentResources.Like), ResourceType = typeof(CommentResources))]
    public long Like { get; set; }

    [Display(Name = nameof(CommentResources.Dislike), ResourceType = typeof(CommentResources))]
    public long Dislike { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    [CoreRange(0, 5)]
    [Display(Name = nameof(CommentResources.Rating), ResourceType = typeof(CommentResources))]
    public decimal Rating { get; set; }

    [ForeignKey("ActivatorId")]
    public long? ActivatorId { get; set; }

    [NotMapped]
    public User Creator { get; set; }

    [NotMapped]
    public User Editor { get; set; }
}