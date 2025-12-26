using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamkare.UserPanel.ViewModels;

public class MyCommentViewModel
{
    public long Id { get; set; }

    [Display(Name = nameof(GlobalResources.Name), ResourceType = typeof(GlobalResources))]
    public string Name { get; set; }

    public string Type { get; set; }

    public bool Active { get; set; }

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
}