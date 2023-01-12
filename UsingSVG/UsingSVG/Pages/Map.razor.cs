using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UsingSVG.Pages;

public partial class Map : ComponentBase
{
    [Inject]
    public IJSRuntime _jsInterop { get; set; }
    private IJSObjectReference _jsModule;
    private string? mapSource;
    
    protected override async Task OnInitializedAsync()
    {
        _jsModule = await _jsInterop.InvokeAsync<IJSObjectReference>("import", new object?[]{ "./js/tracker.js" });
    }
    
    public Map()
    {
        this.mapSource = "/images/worldmap.svg";
    }

    private async Task TestJavascriptInvoke()
    {
        await _jsInterop.InvokeAsync<bool>("alertMessage", "My custom message");
        await _jsModule.InvokeVoidAsync("showAlert", "JS function called from .NET");
        await _jsInterop.InvokeVoidAsync("manipulateSVG");
    }
}