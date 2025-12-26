using Hamkare.Component.ViewModels.Base;

namespace Hamkare.Component.HamkareComponents;

public class HamkarePrintButtonViewModel : BlazorModel<HamkarePrintButton>
{
    public PdfSetting PdfSetting { get; set; }
}

public class PdfSetting 
{
    public string Element { get; set; }
    
    public string FileName { get; set; }
}