using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace IgniteUI.Blazor.Controls;

internal class IgbTemplateContent<T> : ComponentBase
    where T : class
{
    private bool _hasPopulatedContext;

    [Parameter]
    public RenderFragment<T>? Template { get; set; }

    [Parameter]
    public T Context { get; set; } = default!;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (parameters.TryGetValue<T>(nameof(Context), out _))
        {
            _hasPopulatedContext = true;
        }

        return base.SetParametersAsync(parameters);
    }

    public void Update()
    {
        StateHasChanged();
    }

    internal void UpdateContent(T? context, RenderFragment<T>? template, bool populateContext = true)
    {
        if (populateContext && context != null)
        {
            Context = context;
            _hasPopulatedContext = true;
        }
        Template = template;
        Update();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");

        if (Template is not null && _hasPopulatedContext)
        {
            builder.AddContent(1, Template(Context));
        }

        builder.CloseElement();
    }
}
