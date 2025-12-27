using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Entities.Generics;
using Hamkare.Common.Entities.Identity;

namespace Hamkare.Common.Entities;

public class HotelComment : CommentEntity
{
    public long HotelId { get; set; }

    public virtual User Activator { get; set; }

    [ForeignKey("HotelId")]
    public virtual Hotel Hotel { get; set; }
    
    [ForeignKey("ParentId")]
    public virtual HotelComment Parent { get; set; }
}