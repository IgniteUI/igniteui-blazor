using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Base class for <see cref="IgbInput" /> and <see cref="IgbMaskInput" />.
    /// </summary>
    /// <typeparam name="TValue">The value type; <c>string</c> for <see cref="IgbInput" /> and <see cref="IgbMaskInput" />.</typeparam>
    public partial class IgbInputBase<TValue> : IgbComponentBase
    {
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
    }
}
