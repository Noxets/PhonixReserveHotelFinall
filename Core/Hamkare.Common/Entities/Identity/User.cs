using Hamkare.Common.Entities.News;
using Hamkare.Common.Entities.Persons;
using Microsoft.AspNetCore.Identity;
using Hamkare.Resource;
using Hamkare.Resource.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Interface.Entities;
using Hamkare.Utility.Attributes;

namespace Hamkare.Common.Entities.Identity;

[Module]
public class User : IdentityUser<long>, INameEntity
{
    public long? PersonId { get; set; }

    public long? PersonLegalId { get; set; }

    public long? UserGroupId { get; set; }

    public List<long> FavoriteProducts { get; set; } = [];

    public List<long> FavoriteServices { get; set; } = [];

    public List<long> FavoriteDownloads { get; set; } = [];

    public List<long> FavoriteBranches { get; set; } = [];

    public List<long> FavoriteNews { get; set; } = [];

    [ForeignKey("UserGroupId")] public virtual UserCategory UserCategory { get; set; }

    [ForeignKey("PersonId")] 
    public virtual Person Person { get; set; }

    public virtual ICollection<NewsComment> NewsCommentActivators { get; set; } = new List<NewsComment>();

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(IdentityResources.ExpireActivationCode), ResourceType = typeof(IdentityResources))]
    public DateTime? ExpireActivationCode { get; set; }

    [StringLength(6)]
    [Display(Name = nameof(IdentityResources.ActivationCode), ResourceType = typeof(IdentityResources))]
    public string ActivationCode { get; set; }

    [Display(Name = nameof(IdentityResources.Wallet), ResourceType = typeof(IdentityResources))]
    public decimal Wallet { get; set; }

    [NotMapped]
    public string Name => Person != null && !string.IsNullOrEmpty(Person.FullName)
            ? Person.FullName
            : UserName;

    [Display(Name = nameof(GlobalResources.Image), ResourceType = typeof(GlobalResources))]
    public string Image { get; set; }

    [Display(Name = nameof(IdentityResources.BankAccount), ResourceType = typeof(IdentityResources))]
    public string BankAccount { get; set; }

    [Display(Name = nameof(IdentityResources.CardNumber), ResourceType = typeof(IdentityResources))]
    public string CardNumber { get; set; }

    [NotMapped]
    public string ImagePath =>
        string.IsNullOrWhiteSpace(Image) ? "/_content/Hamkare.Panel/Avatar.webp" : $"/Upload/Person/{Image}";

    [Display(Name = nameof(GlobalResources.Active), ResourceType = typeof(GlobalResources))]
    public bool Active { get; set; }

    [Display(Name = nameof(GlobalResources.Deleted), ResourceType = typeof(GlobalResources))]
    public bool Deleted { get; set; }

    [Display(Name = nameof(GlobalResources.System), ResourceType = typeof(GlobalResources))]
    public bool System { get; set; }

    [Display(Name = nameof(GlobalResources.CreatorId), ResourceType = typeof(GlobalResources))]
    public long CreatorId { get; set; }

    [Display(Name = nameof(GlobalResources.EditorId), ResourceType = typeof(GlobalResources))]
    public long? EditorId { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(GlobalResources.CreateDate), ResourceType = typeof(GlobalResources))]
    public DateTime CreateDate { get; set; }

    [Display(Name = nameof(GlobalResources.EditDate), ResourceType = typeof(GlobalResources))]
    [Column(TypeName = "datetime")]
    public DateTime? EditDate { get; set; }

    public virtual bool Validate(out List<string> errors)
    {
        errors = [];
        return errors.Count != 0;
    }
}