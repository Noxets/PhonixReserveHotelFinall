namespace Hamkare.Common.Dto.News;

public class NewsDto : BaseDto
{
    public string Description { get; set; }

    public string Name { get; set; }

    public bool CanComment { get; set; }

    public long GroupId { get; set; }
}