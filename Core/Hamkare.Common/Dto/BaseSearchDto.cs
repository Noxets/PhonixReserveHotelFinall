using Hamkare.Common.Interface.Dto;

namespace Hamkare.Common.Dto;

public class BaseSearchDto : IBaseSearchDto
{
    public bool? Action { get; set; }

    public long? Creator { get; set; }

    public long? Editor { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    public int Top { get; set; }

    public int Skip { get; set; }

    public string OrderBy { get; set; }

    public bool IsDescending { get; set; }
}