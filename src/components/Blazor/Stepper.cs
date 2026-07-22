using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A stepper component that provides a wizard-like workflow by dividing content into logical steps.
    /// </summary>
    public partial class IgbStepper : BaseRendererControl
    {
        public override string Type { get { return "WebStepper"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbStepperModule.IsLoadRequested(IgBlazor))
            {
                IgbStepperModule.Register(IgBlazor);
            }
        }

        protected override string ResolveDisplay()
        {
            return "inline-block";
        }

        protected override bool SupportsVisualChildren
        {
            get
            {
                return true;
            }
        }

        protected override bool UseDirectRender
        {
            get
            {
                return true;
            }
        }

        protected override string DirectRenderElementName
        {
            get
            {
                return "igc-stepper";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        public IgbStepper() : base()
        {
            OnCreatedIgbStepper();

        }

        partial void OnCreatedIgbStepper();

        public async Task<IgbStep[]> GetStepsAsync()
        {
            var iv = await InvokeMethod("p:Steps", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbStep[]);
            }
            var retVal = ReturnToObjectArray<IgbStep>(iv);
            if (retVal == null)
            {
                return default(IgbStep[]);
            }
            return retVal;

        }
        public IgbStep[] GetSteps()
        {
            var iv = InvokeMethodSync("p:Steps", new object[] { }, new string[] { });

            if (iv == null)
            {
                return default(IgbStep[]);
            }
            var retVal = ReturnToObjectArray<IgbStep>(iv);
            if (retVal == null)
            {
                return default(IgbStep[]);
            }
            return retVal;

        }
        private StepperOrientation _orientation = StepperOrientation.Horizontal;

        partial void OnOrientationChanging(ref StepperOrientation newValue);
        /// <summary>
        /// The orientation of the stepper.
        /// </summary>
        [Parameter]
        public StepperOrientation Orientation
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
        private StepperStepType _stepType = StepperStepType.Full;

        partial void OnStepTypeChanging(ref StepperStepType newValue);
        /// <summary>
        /// The visual type of the steps.
        /// </summary>
        [Parameter]
        public StepperStepType StepType
        {
            get { return this._stepType; }
            set
            {
                if (this._stepType != value || !IsPropDirty("StepType"))
                {
                    MarkPropDirty("StepType");
                }
                this._stepType = value;

            }
        }
        private bool _linear = false;

        partial void OnLinearChanging(ref bool newValue);
        /// <summary>
        /// Whether the stepper is linear.
        /// </summary>
        [Parameter]
        public bool Linear
        {
            get { return this._linear; }
            set
            {
                if (this._linear != value || !IsPropDirty("Linear"))
                {
                    MarkPropDirty("Linear");
                }
                this._linear = value;

            }
        }
        private bool _contentTop = false;

        partial void OnContentTopChanging(ref bool newValue);
        /// <summary>
        /// Whether the content is displayed above the steps.
        /// </summary>
        [Parameter]
        public bool ContentTop
        {
            get { return this._contentTop; }
            set
            {
                if (this._contentTop != value || !IsPropDirty("ContentTop"))
                {
                    MarkPropDirty("ContentTop");
                }
                this._contentTop = value;

            }
        }
        private StepperVerticalAnimation _verticalAnimation = StepperVerticalAnimation.Grow;

        partial void OnVerticalAnimationChanging(ref StepperVerticalAnimation newValue);
        /// <summary>
        /// The animation type when in vertical mode.
        /// </summary>
        [Parameter]
        public StepperVerticalAnimation VerticalAnimation
        {
            get { return this._verticalAnimation; }
            set
            {
                if (this._verticalAnimation != value || !IsPropDirty("VerticalAnimation"))
                {
                    MarkPropDirty("VerticalAnimation");
                }
                this._verticalAnimation = value;

            }
        }
        private HorizontalTransitionAnimation _horizontalAnimation = HorizontalTransitionAnimation.Slide;

        partial void OnHorizontalAnimationChanging(ref HorizontalTransitionAnimation newValue);
        /// <summary>
        /// The animation type when in horizontal mode.
        /// </summary>
        [Parameter]
        public HorizontalTransitionAnimation HorizontalAnimation
        {
            get { return this._horizontalAnimation; }
            set
            {
                if (this._horizontalAnimation != value || !IsPropDirty("HorizontalAnimation"))
                {
                    MarkPropDirty("HorizontalAnimation");
                }
                this._horizontalAnimation = value;

            }
        }
        private double _animationDuration = 0;

        partial void OnAnimationDurationChanging(ref double newValue);
        /// <summary>
        /// The animation duration in either vertical or horizontal mode in milliseconds.
        /// </summary>
        [Parameter]
        public double AnimationDuration
        {
            get { return this._animationDuration; }
            set
            {
                if (this._animationDuration != value || !IsPropDirty("AnimationDuration"))
                {
                    MarkPropDirty("AnimationDuration");
                }
                this._animationDuration = value;

            }
        }
        private StepperTitlePosition _titlePosition = StepperTitlePosition.Auto;

        partial void OnTitlePositionChanging(ref StepperTitlePosition newValue);
        /// <summary>
        /// The position of the steps title.
        /// </summary>
        [Parameter]
        public StepperTitlePosition TitlePosition
        {
            get { return this._titlePosition; }
            set
            {
                if (this._titlePosition != value || !IsPropDirty("TitlePosition"))
                {
                    MarkPropDirty("TitlePosition");
                }
                this._titlePosition = value;

            }
        }

        partial void FindByNameStepper(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameStepper(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }
        public async Task SetNativeElementAsync(Object element)
        {
            await InvokeMethod("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        public void SetNativeElement(Object element)
        {
            InvokeMethodSync("setNativeElement", new object[] { ObjectToParam(element) }, new string[] { "Json" });
        }
        /// <summary>
        /// Activates the step at a given index.
        /// </summary>
        public async Task NavigateToAsync(double index)
        {
            await InvokeMethod("navigateTo", new object[] { index }, new string[] { "Number" });
        }
        public void NavigateTo(double index)
        {
            InvokeMethodSync("navigateTo", new object[] { index }, new string[] { "Number" });
        }
        /// <summary>
        /// Activates the next enabled step.
        /// </summary>
        public async Task NextAsync()
        {
            await InvokeMethod("next", new object[] { }, new string[] { });
        }
        public void Next()
        {
            InvokeMethodSync("next", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Activates the previous enabled step.
        /// </summary>
        public async Task PrevAsync()
        {
            await InvokeMethod("prev", new object[] { }, new string[] { });
        }
        public void Prev()
        {
            InvokeMethodSync("prev", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Resets the stepper to its initial state i.e. activates the first step.
        /// </summary>
        public async Task ResetAsync()
        {
            await InvokeMethod("reset", new object[] { }, new string[] { });
        }
        public void Reset()
        {
            InvokeMethodSync("reset", new object[] { }, new string[] { });
        }

        private string _activeStepChangingRef = null;
        private string _activeStepChangingScript = null;
        [Parameter]
        public string ActiveStepChangingScript
        {

            set
            {
                if (value != this._activeStepChangingScript)
                {
                    this._activeStepChangingScript = value;
                    this.OnRefChanged("ActiveStepChanging", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._activeStepChangingRef = refName;
                        this.MarkPropDirty("ActiveStepChangingRef");
                    });
                }
            }
            get
            {
                return this._activeStepChangingScript;
            }
        }

        partial void OnHandlingActiveStepChanging(IgbActiveStepChangingEventArgs args);
        private EventCallback<IgbActiveStepChangingEventArgs>? _activeStepChanging = null;
        [Parameter]
        public EventCallback<IgbActiveStepChangingEventArgs> ActiveStepChanging
        {
            get
            {
                return this._activeStepChanging != null ? this._activeStepChanging.Value : EventCallback<IgbActiveStepChangingEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbActiveStepChangingEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _activeStepChanging, ref eventCallbacksCache))
                    {
                        _activeStepChanging = value;
                        this.SetHandler<IgbActiveStepChangingEventArgs>(this.Name, "ActiveStepChanging", value, (args) =>
                        {
                            OnHandlingActiveStepChanging(args);

                        });
                        this.OnRefChanged("ActiveStepChanging", null, "event:::ActiveStepChanging", true, false, (refName, oldValue, newValue) =>
                        {
                            this._activeStepChangingRef = refName;
                            this.MarkPropDirty("ActiveStepChangingRef");
                        });
                    }
                }
                else
                {
                    _activeStepChanging = null;
                    this.SetHandler<IgbActiveStepChangingEventArgs>(this.Name, "ActiveStepChanging", null);
                    this.OnRefChanged("ActiveStepChanging", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._activeStepChangingRef = null;
                        this.MarkPropDirty("ActiveStepChangingRef");
                    });
                }
            }
        }

        private string _activeStepChangedRef = null;
        private string _activeStepChangedScript = null;
        [Parameter]
        public string ActiveStepChangedScript
        {

            set
            {
                if (value != this._activeStepChangedScript)
                {
                    this._activeStepChangedScript = value;
                    this.OnRefChanged("ActiveStepChanged", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._activeStepChangedRef = refName;
                        this.MarkPropDirty("ActiveStepChangedRef");
                    });
                }
            }
            get
            {
                return this._activeStepChangedScript;
            }
        }

        partial void OnHandlingActiveStepChanged(IgbActiveStepChangedEventArgs args);
        private EventCallback<IgbActiveStepChangedEventArgs>? _activeStepChanged = null;
        [Parameter]
        public EventCallback<IgbActiveStepChangedEventArgs> ActiveStepChanged
        {
            get
            {
                return this._activeStepChanged != null ? this._activeStepChanged.Value : EventCallback<IgbActiveStepChangedEventArgs>.Empty;
            }
            set
            {
                if (!value.Equals(EventCallback<IgbActiveStepChangedEventArgs>.Empty))
                {
                    if (!CompareEventCallbacks(value, _activeStepChanged, ref eventCallbacksCache))
                    {
                        _activeStepChanged = value;
                        this.SetHandler<IgbActiveStepChangedEventArgs>(this.Name, "ActiveStepChanged", value, (args) =>
                        {
                            OnHandlingActiveStepChanged(args);

                        });
                        this.OnRefChanged("ActiveStepChanged", null, "event:::ActiveStepChanged", true, false, (refName, oldValue, newValue) =>
                        {
                            this._activeStepChangedRef = refName;
                            this.MarkPropDirty("ActiveStepChangedRef");
                        });
                    }
                }
                else
                {
                    _activeStepChanged = null;
                    this.SetHandler<IgbActiveStepChangedEventArgs>(this.Name, "ActiveStepChanged", null);
                    this.OnRefChanged("ActiveStepChanged", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._activeStepChangedRef = null;
                        this.MarkPropDirty("ActiveStepChangedRef");
                    });
                }
            }
        }

        partial void SerializeCoreIgbStepper(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbStepper(ser);

            if (IsPropDirty("Orientation"))
            { ser.AddEnumProp("orientation", this._orientation); }
            if (IsPropDirty("StepType"))
            { ser.AddEnumProp("stepType", this._stepType); }
            if (IsPropDirty("Linear"))
            { ser.AddBooleanProp("linear", this._linear); }
            if (IsPropDirty("ContentTop"))
            { ser.AddBooleanProp("contentTop", this._contentTop); }
            if (IsPropDirty("VerticalAnimation"))
            { ser.AddEnumProp("verticalAnimation", this._verticalAnimation); }
            if (IsPropDirty("HorizontalAnimation"))
            { ser.AddEnumProp("horizontalAnimation", this._horizontalAnimation); }
            if (IsPropDirty("AnimationDuration"))
            { ser.AddNumberProp("animationDuration", this._animationDuration); }
            if (IsPropDirty("TitlePosition"))
            { ser.AddEnumProp("titlePosition", this._titlePosition); }
            if (IsPropDirty("ActiveStepChangingRef"))
            { ser.AddStringProp("activeStepChangingRef", this._activeStepChangingRef); }
            if (IsPropDirty("ActiveStepChangedRef"))
            { ser.AddStringProp("activeStepChangedRef", this._activeStepChangedRef); }

        }

    }
}
