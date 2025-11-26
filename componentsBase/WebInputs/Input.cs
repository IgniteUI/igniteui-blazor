using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IgbInputBase : BaseRendererControl
    {
        [Inject]
        internal ILogger<IgbInputBase> Logger { get; set; } = default;

        private void EnsureInputOcurredHandled()
        {
            if (EventCallback<IgbComponentValueChangedEventArgs>.Empty.Equals(this.InputOcurred))
            {
                this.InputOcurred = new EventCallback<IgbComponentValueChangedEventArgs>(null, (Action<IgbComponentValueChangedEventArgs>)((e) => { })); this._inputOcurred = null;
            }
        }

        partial void OnHandlingInputOcurred(IgbComponentValueChangedEventArgs args)
        {
            if (!EventCallback<string>.Empty.Equals(ValueChanging))
            {
                ValueChanging.InvokeAsync(args.Detail);
            }
        }

        private EventCallback<string>? _valueChanging = null;
        [Parameter]
        public EventCallback<string> ValueChanging
        {
            get
            {
                return this._valueChanging != null ? this._valueChanging.Value : EventCallback<string>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<string>.Empty))
                {
                    if (!value.Equals(_valueChanging))
                    {
                        this.EnsureInputOcurredHandled();

                        _valueChanging = value;
                    }
                }
                else
                {
                    _valueChanging = null;
                }
            }
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            // Params are case-insensitive & can't keep old name as deprecated,
            // so coerce value to avoid old code setting incorrect type errors:
            parameters.TryGetValue("Readonly", out object result);
            if (result != null && result is string value)
            {
                Logger.LogWarning("Readonly has been renamed, use ReadOnly instead");
                var updatedParams = parameters.ToDictionary().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                bool.TryParse(value, out var coerced);
                updatedParams["Readonly"] = coerced;
                parameters = ParameterView.FromDictionary(updatedParams);
            }
            return base.SetParametersAsync(parameters);
        }
    }

    public partial class IgbInput
    {
        public override Task SetParametersAsync(ParameterView parameters)
        {
            // Params are case-insensitive & can't keep old name as deprecated,
            // so coerce value to avoid old code setting incorrect type errors:
            parameters = TryCoerceRenamedNumericProp(parameters, "Minlength", "MinLength");
            parameters = TryCoerceRenamedNumericProp(parameters, "Maxlength", "MaxLength");

            return base.SetParametersAsync(parameters);
        }

        private ParameterView TryCoerceRenamedNumericProp(ParameterView parameters, string oldName, string newName)
        {
            parameters.TryGetValue(oldName, out object result);
            if (result != null && result is string value)
            {
                Logger.LogWarning($"{oldName} has been renamed, use {newName} instead");
                var updatedParams = parameters.ToDictionary().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                if (double.TryParse(value, out var coerced))
                {
                    updatedParams[oldName] = coerced;
                }
                else
                {
                    updatedParams.Remove(oldName);
                }
                parameters = ParameterView.FromDictionary(updatedParams);
            }

            return parameters;
        }
    }
}
