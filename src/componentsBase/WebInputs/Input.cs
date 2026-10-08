using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class for <see cref="IgbInput" /> and <see cref="IgbMaskInput" />.
    /// </summary>
    /// <typeparam name="TValue">The value type; <c>string</c> for <see cref="IgbInput" /> and <see cref="IgbMaskInput" />.</typeparam>
    public partial class IgbInputBase<TValue> : IgbComponentBase
    {
        [Inject]
        internal ILogger<IgbInputBase<TValue>>? Logger { get; set; }

        private void EnsureInputOcurredHandled()
        {
            if (EventCallback<IgbComponentValueChangedEventArgs>.Empty.Equals(this.InputOcurred))
            {
                this.InputOcurred = new EventCallback<IgbComponentValueChangedEventArgs>(null, (Action<IgbComponentValueChangedEventArgs>)((e) => { }));
                this._inputOcurred = null;
            }
        }

        private void RaiseValueChanging(IgbComponentValueChangedEventArgs args)
        {
            if (!EventCallback<TValue?>.Empty.Equals(ValueChanging))
            {
                ValueChanging.InvokeAsync(GenericValueFromEventJson<TValue>(args.Detail));
            }
        }

        private EventCallback<TValue?>? _valueChanging = null;

        /// <summary>
        /// Emitted as the user types, carrying the current value of the input.
        /// Raised alongside <see cref="InputOcurred"/>, whose payload it unwraps.
        /// </summary>
        [Parameter]
        public EventCallback<TValue?> ValueChanging
        {
            get
            {
                return this._valueChanging != null ? this._valueChanging.Value : EventCallback<TValue?>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_valueChanging))
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

        /// <inheritdoc />
        public override Task SetParametersAsync(ParameterView parameters)
        {
            // Params are case-insensitive & can't keep old name as deprecated,
            // so coerce value to avoid old code setting incorrect type errors:
            parameters.TryGetValue("Readonly", out object? result);
            if (result != null && result is string value)
            {
                Logger?.LogWarning("Readonly has been renamed, use ReadOnly instead");
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
        /// <inheritdoc />
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
            parameters.TryGetValue(oldName, out object? result);
            if (result != null && result is string value)
            {
                Logger?.LogWarning($"{oldName} has been renamed, use {newName} instead");
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
