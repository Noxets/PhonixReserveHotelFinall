using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Hamkare.Component.HamkareComponents;

public partial class HamkareListEdit<T> where T : new()
{
    #region Paging

    // protected override async Task OnAfterRenderAsync(bool firstRender)
    // {
    //     if (firstRender)
    //         await RowsPerPageInit();
    //
    //     await base.OnAfterRenderAsync(firstRender);
    // }
    //
    // private async Task RowsPerPageInit()
    // {
    //     var result = await ProtectedLocalStorage.GetAsync<int>("HamkareRowsPerPage");
    //
    //     if (result.Success)
    //         Table.RowsPerPage = result.Value;
    // }
    //
    // private async Task RowsPerPageChanged(int rowsPerPage)
    // {
    //     if (!OnRender)
    //     {
    //         if (HidePaging)
    //         {
    //             await ProtectedLocalStorage.SetAsync("HamkareRowsPerPage", int.MaxValue);
    //             Table.RowsPerPage = int.MaxValue;
    //         }
    //         else
    //         {
    //             await ProtectedLocalStorage.SetAsync("HamkareRowsPerPage", rowsPerPage);
    //             Table.RowsPerPage = rowsPerPage;
    //         }
    //     }
    // }

    #endregion

    #region Parameter

    private HamkareTable<T> Table { get; set; }
    
    [Parameter] public string Title { get; set; }
    [Parameter] public string ReturnLink { get; set; }
    [Parameter] public string AddLink { get; set; }
    [Parameter] public IEnumerable<T> Elements { get; set; } = new List<T>();
    [Parameter] public Func<T, bool> QuickFilter { get; set; }
    [Parameter] public bool OnRender { get; set; } = true;
    private Task Before { get; set; }
    [Parameter] public string SearchString { get; set; }
    [Parameter] public EventCallback<string> SearchStringChange { get; set; }
    
    [Parameter] public EventCallback<T> OnEdit { get; set; }

    [Parameter] public bool HidePaging { get; set; }

    private int RowsPerPage { get; set; } = 10;

    [Parameter] public Func<T, Task<bool>> OnSubmit { get; set; }

    [Parameter]
    [Category(CategoryTypes.Table.Header)]
    public RenderFragment HeaderContent { get; set; }

    [Parameter]
    [Category(CategoryTypes.Table.Rows)]
    public RenderFragment<T> RowTemplate { get; set; }

    [Parameter]
    [Category(CategoryTypes.Table.Editing)]
    public RenderFragment<T> RowEditingTemplate { get; set; }

    [Parameter]
    public bool HorizontalScrollbar { get; set; }

    [Parameter] public RenderFragment ButtonPlace { get; set; }

    [Parameter]
    [Category(CategoryTypes.Table.Footer)]
    public RenderFragment FooterContent { get; set; }

    #endregion

    private void Callback(string search)
    {
        SearchString = search;
        SearchStringChange.InvokeAsync(search);
    }

    private void RowEditPreview(object element) => Before = OnEdit.InvokeAsync((T)element);

    private async void SubmitEdit(object element)
    {
        var result = (T)element;
        var old = Elements.First(x => x.Equals(Before));

        if (await OnSubmit(result))
            old = result;
    }

    private void CancelEdit(object element)
    {
        element ??= new T();

        element = Before;
    }
}