using System;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A range slider component used to select two numeric values within a range.
    /// </summary>
    public partial class IgbRangeSlider : IgbSliderBase
    {
        /// <inheritdoc />
        internal override string RendererType { get { return "WebRangeSlider"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbRangeSliderModule.IsLoadRequested(IgBlazor))
            {
                IgbRangeSliderModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        private protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        private protected override string DirectRenderElementName
        {
            get
            {
                return "igc-range-slider";
            }
        }

        private double _lower = 0;

        /// <summary>
        /// The current value of the lower thumb.
        /// </summary>
        [Parameter]
        public double Lower
        {
            get { return this._lower; }
            set
            {
                if (this._lower != value || !IsPropDirty("Lower"))
                {
                    MarkPropDirty("Lower");
                }
                this._lower = value;

            }
        }
        private double? _upper;

        /// <summary>
        /// The current value of the upper thumb.
        /// </summary>
        /// <remarks>
        /// Until it is set, the upper thumb follows <see cref="IgbSliderBase.UpperBound"/>, or <see cref="IgbSliderBase.Max"/>
        /// without a bound, so the default range covers the full scale, and the getter returns that value.
        /// </remarks>
        [Parameter]
        public double Upper
        {
            get { return this._upper ?? DefaultUpper; }
            set
            {
                if (this._upper != value || !IsPropDirty("Upper"))
                {
                    MarkPropDirty("Upper");
                }
                this._upper = value;

            }
        }

        // The web component's upper thumb before it is set: its upperBound, which is UpperBound clamped
        // between LowerBound (or Min) and Max, or Max without a bound.
        private double DefaultUpper
        {
            get
            {
                var lower = Math.Max(LowerBound ?? Min, Min);
                return Math.Min(Math.Max(UpperBound ?? Max, lower), Max);
            }
        }

        private EventCallback<double>? _lowerChanged = null;

        /// <summary>
        /// Emitted when the user commits a new <see cref="Lower"/> value.
        /// Enables two-way binding through <c>@bind-Lower</c>.
        /// </summary>
        [Parameter]
        public EventCallback<double> LowerChanged
        {
            get
            {
                return this._lowerChanged != null ? this._lowerChanged.Value : EventCallback<double>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_lowerChanged))
                    {
                        this.EnsureChangeHandled();
                        _lowerChanged = value;
                    }
                }
                else
                {
                    _lowerChanged = null;
                }
            }
        }

        private EventCallback<double>? _upperChanged = null;

        /// <summary>
        /// Emitted when the user commits a new <see cref="Upper"/> value.
        /// Enables two-way binding through <c>@bind-Upper</c>.
        /// </summary>
        [Parameter]
        public EventCallback<double> UpperChanged
        {
            get
            {
                return this._upperChanged != null ? this._upperChanged.Value : EventCallback<double>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_upperChanged))
                    {
                        this.EnsureChangeHandled();
                        _upperChanged = value;
                    }
                }
                else
                {
                    _upperChanged = null;
                }
            }
        }

        internal void EnsureChangeHandled()
        {
            if (EventCallback<IgbRangeSliderValueEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbRangeSliderValueEventArgs>(null, (Action<IgbRangeSliderValueEventArgs>)((e) => { }));
                this._change = null;
            }
        }
        private string? _thumbLabelLower;

        /// <summary>
        /// The aria label for the lower thumb.
        /// </summary>
        [Parameter]
        public string? ThumbLabelLower
        {
            get { return this._thumbLabelLower; }
            set
            {
                if (this._thumbLabelLower != value || !IsPropDirty("ThumbLabelLower"))
                {
                    MarkPropDirty("ThumbLabelLower");
                }
                this._thumbLabelLower = value;

            }
        }
        private string? _thumbLabelUpper;

        /// <summary>
        /// The aria label for the upper thumb.
        /// </summary>
        [Parameter]
        public string? ThumbLabelUpper
        {
            get { return this._thumbLabelUpper; }
            set
            {
                if (this._thumbLabelUpper != value || !IsPropDirty("ThumbLabelUpper"))
                {
                    MarkPropDirty("ThumbLabelUpper");
                }
                this._thumbLabelUpper = value;

            }
        }

        private string? _inputRef = null;
        private string? _inputScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Input"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like<br/>
        /// <c>import { registerScript } from './_content/IgniteUI.Blazor/api.js';</c><br/>
        /// <c>registerScript("MyHandler", (args) => { })</c>.
        /// </remarks>
        [Parameter]
        public string? InputScript
        {

            set
            {
                if (value != this._inputScript)
                {
                    this._inputScript = value;
                    this.OnRefChanged("Input", null, value, true, false, (string refName, object? oldValue, object? newValue) =>
                    {
                        this._inputRef = refName;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
            get
            {
                return this._inputScript;
            }
        }

        private EventCallback<IgbRangeSliderValueEventArgs>? _input = null;

        /// <summary>
        /// Emitted when a value is changed via thumb drag or keyboard interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbRangeSliderValueEventArgs> Input
        {
            get
            {
                return this._input != null ? this._input.Value : EventCallback<IgbRangeSliderValueEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_input))
                    {
                        _input = value;
                        this.SetHandler<IgbRangeSliderValueEventArgs>(this.RendererName, "Input", value);
                        this.OnRefChanged("Input", null, "event:::Input", true, false, (refName, oldValue, newValue) =>
                        {
                            this._inputRef = refName;
                            this.MarkPropDirty("InputRef");
                        });
                    }
                }
                else
                {
                    _input = null;
                    this.SetHandler<IgbRangeSliderValueEventArgs>(this.RendererName, "Input", null);
                    this.OnRefChanged("Input", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._inputRef = null;
                        this.MarkPropDirty("InputRef");
                    });
                }
            }
        }

        private string? _changeRef = null;
        private string? _changeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Change"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like<br/>
        /// <c>import { registerScript } from './_content/IgniteUI.Blazor/api.js';</c><br/>
        /// <c>registerScript("MyHandler", (args) => { })</c>.
        /// </remarks>
        [Parameter]
        public string? ChangeScript
        {

            set
            {
                if (value != this._changeScript)
                {
                    this._changeScript = value;
                    this.OnRefChanged("Change", null, value, true, false, (string refName, object? oldValue, object? newValue) =>
                    {
                        this._changeRef = refName;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
            get
            {
                return this._changeScript;
            }
        }

        private EventCallback<IgbRangeSliderValueEventArgs>? _change = null;

        /// <summary>
        /// Emitted when a value change is committed on a thumb drag end or keyboard interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbRangeSliderValueEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbRangeSliderValueEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_change))
                    {
                        _change = value;
                        this.SetHandler<IgbRangeSliderValueEventArgs>(this.RendererName, "Change", value, (args) =>
                        {
                            // Copy the committed thumbs back, as IgbSlider does for Value, so Lower and
                            // Upper report where the user left them, not the bound or default values.
                            // Lower and Upper render as attributes, so a parent that passes them one-way
                            // and re-renders with its unchanged values diffs to no update and leaves the
                            // thumbs where the user put them; LowerChanged and UpperChanged let it bind.
                            var detail = args.Detail;
                            if (detail != null)
                            {
                                var lowerChanged = Math.Abs(this.Lower - detail.Lower) > double.Epsilon;
                                var upperChanged = Math.Abs(this.Upper - detail.Upper) > double.Epsilon;
                                this.Lower = detail.Lower;
                                this.Upper = detail.Upper;
                                OnPropertyPropagatedOut(RendererName, "Lower");
                                OnPropertyPropagatedOut(RendererName, "Upper");
                                if (lowerChanged && !EventCallback<double>.Empty.Equals(LowerChanged))
                                {
                                    ObserveHandlerTask(LowerChanged.InvokeAsync(detail.Lower));
                                }
                                if (upperChanged && !EventCallback<double>.Empty.Equals(UpperChanged))
                                {
                                    ObserveHandlerTask(UpperChanged.InvokeAsync(detail.Upper));
                                }
                            }
                        });
                        this.OnRefChanged("Change", null, "event:::Change", true, false, (refName, oldValue, newValue) =>
                        {
                            this._changeRef = refName;
                            this.MarkPropDirty("ChangeRef");
                        });
                    }
                }
                else
                {
                    _change = null;
                    this.SetHandler<IgbRangeSliderValueEventArgs>(this.RendererName, "Change", null);
                    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._changeRef = null;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Lower"))
            { ser.AddNumberProp("lower", this._lower); }
            if (IsPropDirty("Upper"))
            { ser.AddNumberProp("upper", this.Upper); }
            if (IsPropDirty("ThumbLabelLower"))
            { ser.AddStringProp("thumbLabelLower", this._thumbLabelLower); }
            if (IsPropDirty("ThumbLabelUpper"))
            { ser.AddStringProp("thumbLabelUpper", this._thumbLabelUpper); }
            if (IsPropDirty("InputRef"))
            { ser.AddStringProp("inputRef", this._inputRef); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
