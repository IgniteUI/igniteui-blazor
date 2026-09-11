using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Represents a calendar that lets users
    /// to select a date value in a variety of different ways.
    /// </summary>
    public partial class IgbCalendar : IgbCalendarBase
    {
        /// <inheritdoc />
        public override string Type { get { return "WebCalendar"; } }

        /// <inheritdoc />
        protected override void EnsureModulesLoaded()
        {
            if (!IgbCalendarModule.IsLoadRequested(IgBlazor))
            {
                IgbCalendarModule.Register(IgBlazor);
            }
        }

        /// <inheritdoc />
        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        /// <inheritdoc />
        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        private DateTime _value = DateTime.MinValue;

        /// <summary>
        /// The current value of the calendar.
        /// Used when <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Single"/>.
        /// </summary>
        [Parameter]
        public DateTime Value
        {
            get { return this._value; }
            set
            {
                if (this._value != value || !IsPropDirty("Value"))
                {
                    MarkPropDirty("Value");
                }
                this._value = value;

            }
        }

        /// <summary>
        /// Get the current value of the calendar.
        /// Used when <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Single"/>.
        /// </summary>
        public async Task<DateTime> GetCurrentValueAsync()
        {
            var iv = await InvokeMethod("p:Value", new object[] { }, new string[] { });
            return ReturnToDate(iv);
        }

        /// <summary>
        /// Get the current value of the calendar.
        /// Used when <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Single"/>.
        /// </summary>
        public DateTime GetCurrentValue()
        {
            var iv = InvokeMethodSync("p:Value", new object[] { }, new string[] { });
            return ReturnToDate(iv);
        }
        private DateTime[] _values;

        /// <summary>
        /// The current values of the calendar.
        /// Used when <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Multiple"/>
        /// or <see cref="CalendarSelection.Range"/>.
        /// </summary>
        [Parameter]
        public DateTime[] Values
        {
            get { return this._values; }
            set
            {
                if (this._values != value || !IsPropDirty("Values"))
                {
                    MarkPropDirty("Values");
                }
                this._values = value;

            }
        }

        /// <summary>
        /// Get the current values of the calendar.
        /// Used when <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Multiple"/>
        /// or <see cref="CalendarSelection.Range"/>.
        /// </summary>
        public async Task<DateTime[]> GetCurrentValuesAsync()
        {
            var iv = await InvokeMethod("p:Values", new object[] { }, new string[] { });
            return ReturnToDateArray(iv);
        }

        /// <summary>
        /// Get the current values of the calendar.
        /// Used when <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Multiple"/>
        /// or <see cref="CalendarSelection.Range"/>.
        /// </summary>
        public DateTime[] GetCurrentValues()
        {
            var iv = InvokeMethodSync("p:Values", new object[] { }, new string[] { });
            return ReturnToDateArray(iv);
        }
        private DateTime _activeDate = DateTime.MinValue;

        /// <summary>
        /// Sets the date which is shown in view and is highlighted. By default it is the current date.
        /// </summary>
        [Parameter]
        public DateTime ActiveDate
        {
            get { return this._activeDate; }
            set
            {
                if (this._activeDate != value || !IsPropDirty("ActiveDate"))
                {
                    MarkPropDirty("ActiveDate");
                }
                this._activeDate = value;

            }
        }
        private bool _hideOutsideDays = false;

        /// <summary>
        /// Whether to hide the dates that do not belong to the current active month.
        /// </summary>
        [Parameter]
        public bool HideOutsideDays
        {
            get { return this._hideOutsideDays; }
            set
            {
                if (this._hideOutsideDays != value || !IsPropDirty("HideOutsideDays"))
                {
                    MarkPropDirty("HideOutsideDays");
                }
                this._hideOutsideDays = value;

            }
        }
        private bool _hideHeader = false;

        /// <summary>
        /// Whether to render the calendar header part.
        /// When <see cref="IgbCalendarBase.Selection"/> is set to <see cref="CalendarSelection.Multiple"/>
        /// the header is always hidden.
        /// </summary>
        [Parameter]
        public bool HideHeader
        {
            get { return this._hideHeader; }
            set
            {
                if (this._hideHeader != value || !IsPropDirty("HideHeader"))
                {
                    MarkPropDirty("HideHeader");
                }
                this._hideHeader = value;

            }
        }
        private CalendarHeaderOrientation _headerOrientation = CalendarHeaderOrientation.Horizontal;

        /// <summary>
        /// The orientation of the calendar header.
        /// </summary>
        [Parameter]
        public CalendarHeaderOrientation HeaderOrientation
        {
            get { return this._headerOrientation; }
            set
            {
                if (this._headerOrientation != value || !IsPropDirty("HeaderOrientation"))
                {
                    MarkPropDirty("HeaderOrientation");
                }
                this._headerOrientation = value;

            }
        }
        private ContentOrientation _orientation = ContentOrientation.Horizontal;

        /// <summary>
        /// The orientation of the calendar months when more than one month
        /// is being shown.
        /// </summary>
        [Parameter]
        public ContentOrientation Orientation
        {
            get { return this._orientation; }
            set
            {
                if (this._orientation != value || !IsPropDirty("Orientation"))
                {
                    MarkPropDirty("Orientation");
                }
                this._orientation = value;

            }
        }
        private double _visibleMonths = 1;

        /// <summary>
        /// The number of months displayed in the days view.
        /// </summary>
        [Parameter]
        public double VisibleMonths
        {
            get { return this._visibleMonths; }
            set
            {
                if (this._visibleMonths != value || !IsPropDirty("VisibleMonths"))
                {
                    MarkPropDirty("VisibleMonths");
                }
                this._visibleMonths = value;

            }
        }
        private CalendarActiveView _activeView = CalendarActiveView.Days;

        /// <summary>
        /// The current active view of the component.
        /// </summary>
        [Parameter]
        public CalendarActiveView ActiveView
        {
            get { return this._activeView; }
            set
            {
                if (this._activeView != value || !IsPropDirty("ActiveView"))
                {
                    MarkPropDirty("ActiveView");
                }
                this._activeView = value;

            }
        }
        private IgbCalendarFormatOptions _formatOptions;

        /// <summary>
        /// The options used to format the months and the weekdays in the calendar views.
        /// </summary>
        [Parameter]
        public IgbCalendarFormatOptions FormatOptions
        {
            get { return this._formatOptions; }
            set
            {
                MarkPropDirty("FormatOptions");
                if (this._formatOptions != null)
                {
                    this.DetachChild(this._formatOptions);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._formatOptions = value;
            }

        }

        private EventCallback<DateTime>? _valueChanged = null;

        /// <summary>
        /// Emitted when the Value property changes.
        /// Enables two-way binding through <c>@bind-Value</c>.
        /// </summary>
        [Parameter]
        public EventCallback<DateTime> ValueChanged
        {
            get
            {
                return this._valueChanged != null ? this._valueChanged.Value : EventCallback<DateTime>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_valueChanged))
                    {
                        this.EnsureChangeHandled();

                        _valueChanged = value;
                    }
                }
                else
                {
                    _valueChanged = null;
                }
            }
        }

        private EventCallback<DateTime[]>? _valuesChanged = null;

        /// <summary>
        /// Emitted when the Values property changes.
        /// Enables two-way binding through <c>@bind-Values</c>.
        /// </summary>
        [Parameter]
        public EventCallback<DateTime[]> ValuesChanged
        {
            get
            {
                return this._valuesChanged != null ? this._valuesChanged.Value : EventCallback<DateTime[]>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_valuesChanged))
                    {
                        this.EnsureChangeHandled();

                        _valuesChanged = value;
                    }
                }
                else
                {
                    _valuesChanged = null;
                }
            }
        }

        private string _changeRef = null;
        private string _changeScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Change"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ChangeScript
        {

            set
            {
                if (value != this._changeScript)
                {
                    this._changeScript = value;
                    this.OnRefChanged("Change", null, value, true, false, (string refName, object oldValue, object newValue) =>
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

        private EventCallback<IgbComponentDataValueChangedEventArgs>? _change = null;

        /// <summary>
        /// Emitted when the calendar changes its value.
        /// </summary>
        [Parameter]
        public EventCallback<IgbComponentDataValueChangedEventArgs> Change
        {
            get
            {
                return this._change != null ? this._change.Value : EventCallback<IgbComponentDataValueChangedEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_change))
                    {
                        _change = value;
                        this.SetHandler<IgbComponentDataValueChangedEventArgs>(this.Name, "Change", value, (args) =>
                        {
                            var newValueValue = default(DateTime);

                            if (this.Selection == CalendarSelection.Single)
                            {
                                newValueValue = (DateTime)(args.Detail);
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Value = newValueValue;
                                }
                                else
                                {
                                    this._value = newValueValue;
                                }
                                OnPropertyPropagatedOut(Name, "Value");
                            }

                            var newValueValues = default(DateTime[]);

                            if (this.Selection != CalendarSelection.Single)
                            {
                                newValueValues = (DateTime[])(DowncastArray<DateTime>(args.Detail));
                                if (UseDirectRender)
                                {
                                    //TODO: maybe we should be doing this for everything. Need to make sure we don't infinity bounce though.
                                    this.Values = newValueValues;
                                }
                                else
                                {
                                    this._values = newValueValues;
                                }
                                OnPropertyPropagatedOut(Name, "Values");
                            }

                            if (!EventCallback<DateTime>.Empty.Equals(ValueChanged))
                            {
                                var task = ValueChanged.InvokeAsync(newValueValue);
                                ObserveHandlerTask(task);
                            }

                            if (!EventCallback<DateTime[]>.Empty.Equals(ValuesChanged))
                            {
                                var task = ValuesChanged.InvokeAsync(newValueValues);
                                ObserveHandlerTask(task);
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
                    this.SetHandler<IgbComponentDataValueChangedEventArgs>(this.Name, "Change", null);
                    this.OnRefChanged("Change", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._changeRef = null;
                        this.MarkPropDirty("ChangeRef");
                    });
                }
            }
        }
        internal void EnsureChangeHandled()
        {
            if (EventCallback<IgbComponentDataValueChangedEventArgs>.Empty.Equals(this.Change))
            {
                this.Change = new EventCallback<IgbComponentDataValueChangedEventArgs>(null, (Action<IgbComponentDataValueChangedEventArgs>)((e) => { }));
                this._change = null;
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Value"))
            { ser.AddDateTimeProp("value", this._value); }
            if (IsPropDirty("Values"))
            { ser.AddDateArrayProp("values", this._values); }
            if (IsPropDirty("ActiveDate"))
            { ser.AddDateTimeProp("activeDate", this._activeDate); }
            if (IsPropDirty("HideOutsideDays"))
            { ser.AddBooleanProp("hideOutsideDays", this._hideOutsideDays); }
            if (IsPropDirty("HideHeader"))
            { ser.AddBooleanProp("hideHeader", this._hideHeader); }
            if (IsPropDirty("HeaderOrientation"))
            { ser.AddEnumProp("headerOrientation", this._headerOrientation); }
            if (IsPropDirty("Orientation"))
            { ser.AddEnumProp("orientation", this._orientation); }
            if (IsPropDirty("VisibleMonths"))
            { ser.AddNumberProp("visibleMonths", this._visibleMonths); }
            if (IsPropDirty("ActiveView"))
            { ser.AddEnumProp("activeView", this._activeView); }
            if (IsPropDirty("FormatOptions"))
            { ser.AddSerializableProp("formatOptions", this._formatOptions); }
            if (IsPropDirty("ChangeRef"))
            { ser.AddStringProp("changeRef", this._changeRef); }

        }

    }
}
