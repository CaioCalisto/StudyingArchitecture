using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace UsingSVG.Pages;

public partial class Map : ComponentBase
{
    [Inject]
    public IJSRuntime _jsInterop { get; set; }
    private string? mapSource;
    
    public Map()
    {
        this.mapSource = "/images/worldmap.svg";
    }

    private async Task TestJavascriptInvoke()
    {
        await _jsInterop.InvokeAsync<bool>("alertMessage", "My custom message");
    }

    private async Task AddMark(MouseEventArgs eventArgs)
    {
        await _jsInterop.InvokeVoidAsync("addDoneMark", "/images/done.svg", eventArgs.ScreenX, eventArgs.ScreenY);
    }
}