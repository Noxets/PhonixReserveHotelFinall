using Hamkare.Common.Interface.Dto;

namespace Hamkare.Common.Dto;

public class BaseDto : IBaseDto
{
    public long Id { get; set; }

    public bool Active { get; set; }
}