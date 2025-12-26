namespace Hamkare.Common.Interface.Dto;

public interface IBaseSearchDto
{
    public int Top { get; set; }

    public int Skip { get; set; }

    public string OrderBy { get; set; }

    public bool IsDescending { get; set; }
}