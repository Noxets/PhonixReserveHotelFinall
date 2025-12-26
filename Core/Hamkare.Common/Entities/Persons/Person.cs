using Hamkare.Common.Entities.Identity;
using Hamkare.Resource;
using Hamkare.Resource.Persons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Entities.Generics;
using Hamkare.Utility.Attributes.Core;
using Hamkare.Utility.Attributes.DateTime;
using Hamkare.Utility.Attributes.Number;

namespace Hamkare.Common.Entities.Persons;

[Display(Name = nameof(EntitiesResources.Person), ResourceType = typeof(EntitiesResources))]
public class Person : RootEntity
{
    [CoreRequired]
    [Display(Name = nameof(PersonResources.FirstName), ResourceType = typeof(PersonResources))]
    public string FirstName { get; set; }

    [CoreRequired]
    [Display(Name = nameof(PersonResources.LastName), ResourceType = typeof(PersonResources))]
    public string LastName { get; set; }

    [Display(Name = nameof(PersonResources.FatherName), ResourceType = typeof(PersonResources))]
    public string FatherName { get; set; }

    [CoreRequired]
    [Display(Name = nameof(PersonResources.BirthDate), ResourceType = typeof(PersonResources))]
    [DateNotGreaterThen]
    public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Number]
    [Display(Name = nameof(PersonResources.NationalCode), ResourceType = typeof(PersonResources))]
    public string NationalCode { get; set; }

    [Display(Name = nameof(PersonResources.Gender), ResourceType = typeof(PersonResources))]
    public bool Gender { get; set; }

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [NotMapped]
    public string FullName => FirstName + " " + LastName;

    public override bool Validate(out List<string> errors)
    {
        base.Validate(out errors);
        return errors.Any();
    }
}