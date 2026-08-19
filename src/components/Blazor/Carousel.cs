using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Presents a set of <see cref="IgbCarouselSlide"/> components by sequentially displaying
    /// a subset of one or more slides.
    /// </summary>
    public partial class IgbCarousel : BaseRendererControl
    {
        public override string Type { get { return "WebCarousel"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbCarouselModule.IsLoadRequested(IgBlazor))
            {
                IgbCarouselModule.Register(IgBlazor);
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
                return "igc-carousel";
            }
        }

        protected override ControlEventBehavior DefaultEventBehavior
        {
            get { return ControlEventBehavior.Immediate; }
        }

        private bool _disableLoop = false;

        /// <summary>
        /// Whether the carousel should skip rotating to the first slide after it reaches the last.
        /// </summary>
        [Parameter]
        public bool DisableLoop
        {
            get { return this._disableLoop; }
            set
            {
                if (this._disableLoop != value || !IsPropDirty("DisableLoop"))
                {
                    MarkPropDirty("DisableLoop");
                }
                this._disableLoop = value;

            }
        }
        private bool _disablePauseOnInteraction = false;

        /// <summary>
        /// Whether the carousel should ignore user interactions and not pause on them.
        /// </summary>
        [Parameter]
        public bool DisablePauseOnInteraction
        {
            get { return this._disablePauseOnInteraction; }
            set
            {
                if (this._disablePauseOnInteraction != value || !IsPropDirty("DisablePauseOnInteraction"))
                {
                    MarkPropDirty("DisablePauseOnInteraction");
                }
                this._disablePauseOnInteraction = value;

            }
        }
        private bool _hideNavigation = false;

        /// <summary>
        /// Whether the carousel should skip rendering of the default navigation buttons.
        /// </summary>
        [Parameter]
        public bool HideNavigation
        {
            get { return this._hideNavigation; }
            set
            {
                if (this._hideNavigation != value || !IsPropDirty("HideNavigation"))
                {
                    MarkPropDirty("HideNavigation");
                }
                this._hideNavigation = value;

            }
        }
        private bool _hideIndicators = false;

        /// <summary>
        /// Whether the carousel should skip rendering of the indicator controls (dots).
        /// </summary>
        [Parameter]
        public bool HideIndicators
        {
            get { return this._hideIndicators; }
            set
            {
                if (this._hideIndicators != value || !IsPropDirty("HideIndicators"))
                {
                    MarkPropDirty("HideIndicators");
                }
                this._hideIndicators = value;

            }
        }
        private bool _vertical = false;

        /// <summary>
        /// Whether the carousel has vertical alignment.
        /// </summary>
        [Parameter]
        public bool Vertical
        {
            get { return this._vertical; }
            set
            {
                if (this._vertical != value || !IsPropDirty("Vertical"))
                {
                    MarkPropDirty("Vertical");
                }
                this._vertical = value;

            }
        }
        private CarouselIndicatorsOrientation _indicatorsOrientation = CarouselIndicatorsOrientation.End;

        /// <summary>
        /// Sets the orientation of the indicator controls (dots).
        /// </summary>
        [Parameter]
        public CarouselIndicatorsOrientation IndicatorsOrientation
        {
            get { return this._indicatorsOrientation; }
            set
            {
                if (this._indicatorsOrientation != value || !IsPropDirty("IndicatorsOrientation"))
                {
                    MarkPropDirty("IndicatorsOrientation");
                }
                this._indicatorsOrientation = value;

            }
        }
        private string _indicatorsLabelFormat;

        /// <summary>
        /// The format used to set the aria-label on the carousel indicators.
        /// Instances of <c>{0}</c> will be replaced with the index of the corresponding slide.
        /// </summary>
        [Parameter]
        public string IndicatorsLabelFormat
        {
            get { return this._indicatorsLabelFormat; }
            set
            {
                if (this._indicatorsLabelFormat != value || !IsPropDirty("IndicatorsLabelFormat"))
                {
                    MarkPropDirty("IndicatorsLabelFormat");
                }
                this._indicatorsLabelFormat = value;

            }
        }
        private string _slidesLabelFormat;

        /// <summary>
        /// The format used to set the aria-label on the carousel slides and the text displayed
        /// when the number of indicators is greater than the maximum indicator count.
        /// Instances of <c>{0}</c> will be replaced with the index of the corresponding slide.
        /// Instances of <c>{1}</c> will be replaced with the total amount of slides.
        /// </summary>
        [Parameter]
        public string SlidesLabelFormat
        {
            get { return this._slidesLabelFormat; }
            set
            {
                if (this._slidesLabelFormat != value || !IsPropDirty("SlidesLabelFormat"))
                {
                    MarkPropDirty("SlidesLabelFormat");
                }
                this._slidesLabelFormat = value;

            }
        }
        private double _interval = 0;

        /// <summary>
        /// The duration in milliseconds between changing the active slide.
        /// </summary>
        [Parameter]
        public double Interval
        {
            get { return this._interval; }
            set
            {
                if (this._interval != value || !IsPropDirty("Interval"))
                {
                    MarkPropDirty("Interval");
                }
                this._interval = value;

            }
        }
        private double _maximumIndicatorsCount = 10;

        /// <summary>
        /// The maximum number of indicator controls (dots) that can be shown. Defaults to <c>10</c>.
        /// </summary>
        [Parameter]
        public double MaximumIndicatorsCount
        {
            get { return this._maximumIndicatorsCount; }
            set
            {
                if (this._maximumIndicatorsCount != value || !IsPropDirty("MaximumIndicatorsCount"))
                {
                    MarkPropDirty("MaximumIndicatorsCount");
                }
                this._maximumIndicatorsCount = value;

            }
        }
        private HorizontalTransitionAnimation _animationType = HorizontalTransitionAnimation.Slide;

        /// <summary>
        /// The animation type.
        /// </summary>
        [Parameter]
        public HorizontalTransitionAnimation AnimationType
        {
            get { return this._animationType; }
            set
            {
                if (this._animationType != value || !IsPropDirty("AnimationType"))
                {
                    MarkPropDirty("AnimationType");
                }
                this._animationType = value;

            }
        }

        /// <summary>
        /// Gets the total number of slides.
        /// </summary>
        public async Task<double> GetTotalAsync()
        {
            var iv = await InvokeMethod("p:Total", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Gets the total number of slides.
        /// </summary>
        public double GetTotal()
        {
            var iv = InvokeMethodSync("p:Total", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Gets the index of the current active slide.
        /// </summary>
        public async Task<double> GetCurrentAsync()
        {
            var iv = await InvokeMethod("p:Current", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Gets the index of the current active slide.
        /// </summary>
        public double GetCurrent()
        {
            var iv = InvokeMethodSync("p:Current", new object[] { }, new string[] { });
            return ReturnToDouble(iv);
        }

        /// <summary>
        /// Gets whether the carousel is in playing state.
        /// </summary>
        public async Task<bool> GetIsPlayingAsync()
        {
            var iv = await InvokeMethod("p:IsPlaying", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Gets whether the carousel is in playing state.
        /// </summary>
        public bool GetIsPlaying()
        {
            var iv = InvokeMethodSync("p:IsPlaying", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Gets whether the carousel is in paused state.
        /// </summary>
        public async Task<bool> GetIsPausedAsync()
        {
            var iv = await InvokeMethod("p:IsPaused", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Gets whether the carousel is in paused state.
        /// </summary>
        public bool GetIsPaused()
        {
            var iv = InvokeMethodSync("p:IsPaused", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
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
        /// Resumes playing of the carousel slides.
        /// </summary>
        public async Task PlayAsync()
        {
            await InvokeMethod("play", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Resumes playing of the carousel slides.
        /// </summary>
        public void Play()
        {
            InvokeMethodSync("play", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Pauses the rotation of the carousel slides.
        /// </summary>
        public async Task PauseAsync()
        {
            await InvokeMethod("pause", new object[] { }, new string[] { });
        }

        /// <summary>
        /// Pauses the rotation of the carousel slides.
        /// </summary>
        public void Pause()
        {
            InvokeMethodSync("pause", new object[] { }, new string[] { });
        }
        /// <summary>
        /// Switches to the next slide, running any animations.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> NextAsync()
        {
            var iv = await InvokeMethod("next", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Switches to the next slide, running any animations.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.
        /// </returns>
        public bool Next()
        {
            var iv = InvokeMethodSync("next", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }
        /// <summary>
        /// Switches to the previous slide, running any animations.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> PrevAsync()
        {
            var iv = await InvokeMethod("prev", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Switches to the previous slide, running any animations.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.
        /// </returns>
        public bool Prev()
        {
            var iv = InvokeMethodSync("prev", new object[] { }, new string[] { });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Switches to the slide at the specified index, running any animations.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> SelectAsync(double index, CarouselAnimationDirection? animationDirection = null)
        {
            var iv = await InvokeMethod("select", new object[] { index, ObjectToParam(animationDirection, typeof(CarouselAnimationDirection)) }, new string[] { "Number", "Json" });
            return ReturnToBoolean(iv);
        }

        /// <summary>
        /// Switches to the slide at the specified index, running any animations.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the operation was successful; otherwise <see langword="false"/>.
        /// </returns>
        public bool Select(double index, CarouselAnimationDirection? animationDirection = null)
        {
            var iv = InvokeMethodSync("select", new object[] { index, ObjectToParam(animationDirection, typeof(CarouselAnimationDirection)) }, new string[] { "Number", "Json" });
            return ReturnToBoolean(iv);
        }

        private string _slideChangedRef = null;
        private string _slideChangedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="SlideChanged"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string SlideChangedScript
        {

            set
            {
                if (value != this._slideChangedScript)
                {
                    this._slideChangedScript = value;
                    this.OnRefChanged("SlideChanged", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._slideChangedRef = refName;
                        this.MarkPropDirty("SlideChangedRef");
                    });
                }
            }
            get
            {
                return this._slideChangedScript;
            }
        }

        private EventCallback<IgbNumberEventArgs>? _slideChanged = null;

        /// <summary>
        /// Emitted when the current active slide is changed either by user interaction or automatic transitioning.
        /// </summary>
        [Parameter]
        public EventCallback<IgbNumberEventArgs> SlideChanged
        {
            get
            {
                return this._slideChanged != null ? this._slideChanged.Value : EventCallback<IgbNumberEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!CompareEventCallbacks(value, _slideChanged, ref eventCallbacksCache))
                    {
                        _slideChanged = value;
                        this.SetHandler<IgbNumberEventArgs>(this.Name, "SlideChanged", value);
                        this.OnRefChanged("SlideChanged", null, "event:::SlideChanged", true, false, (refName, oldValue, newValue) =>
                        {
                            this._slideChangedRef = refName;
                            this.MarkPropDirty("SlideChangedRef");
                        });
                    }
                }
                else
                {
                    _slideChanged = null;
                    this.SetHandler<IgbNumberEventArgs>(this.Name, "SlideChanged", null);
                    this.OnRefChanged("SlideChanged", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._slideChangedRef = null;
                        this.MarkPropDirty("SlideChangedRef");
                    });
                }
            }
        }

        private string _playingRef = null;
        private string _playingScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Playing"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string PlayingScript
        {

            set
            {
                if (value != this._playingScript)
                {
                    this._playingScript = value;
                    this.OnRefChanged("Playing", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._playingRef = refName;
                        this.MarkPropDirty("PlayingRef");
                    });
                }
            }
            get
            {
                return this._playingScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _playing = null;

        /// <summary>
        /// Emitted when the carousel enters playing state by a user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Playing
        {
            get
            {
                return this._playing != null ? this._playing.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!CompareEventCallbacks(value, _playing, ref eventCallbacksCache))
                    {
                        _playing = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Playing", value);
                        this.OnRefChanged("Playing", null, "event:::Playing", true, false, (refName, oldValue, newValue) =>
                        {
                            this._playingRef = refName;
                            this.MarkPropDirty("PlayingRef");
                        });
                    }
                }
                else
                {
                    _playing = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Playing", null);
                    this.OnRefChanged("Playing", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._playingRef = null;
                        this.MarkPropDirty("PlayingRef");
                    });
                }
            }
        }

        private string _pausedRef = null;
        private string _pausedScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Paused"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string PausedScript
        {

            set
            {
                if (value != this._pausedScript)
                {
                    this._pausedScript = value;
                    this.OnRefChanged("Paused", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._pausedRef = refName;
                        this.MarkPropDirty("PausedRef");
                    });
                }
            }
            get
            {
                return this._pausedScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _paused = null;

        /// <summary>
        /// Emitted when the carousel enters paused state by a user interaction.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Paused
        {
            get
            {
                return this._paused != null ? this._paused.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!CompareEventCallbacks(value, _paused, ref eventCallbacksCache))
                    {
                        _paused = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Paused", value);
                        this.OnRefChanged("Paused", null, "event:::Paused", true, false, (refName, oldValue, newValue) =>
                        {
                            this._pausedRef = refName;
                            this.MarkPropDirty("PausedRef");
                        });
                    }
                }
                else
                {
                    _paused = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Paused", null);
                    this.OnRefChanged("Paused", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._pausedRef = null;
                        this.MarkPropDirty("PausedRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("DisableLoop"))
            { ser.AddBooleanProp("disableLoop", this._disableLoop); }
            if (IsPropDirty("DisablePauseOnInteraction"))
            { ser.AddBooleanProp("disablePauseOnInteraction", this._disablePauseOnInteraction); }
            if (IsPropDirty("HideNavigation"))
            { ser.AddBooleanProp("hideNavigation", this._hideNavigation); }
            if (IsPropDirty("HideIndicators"))
            { ser.AddBooleanProp("hideIndicators", this._hideIndicators); }
            if (IsPropDirty("Vertical"))
            { ser.AddBooleanProp("vertical", this._vertical); }
            if (IsPropDirty("IndicatorsOrientation"))
            { ser.AddEnumProp("indicatorsOrientation", this._indicatorsOrientation); }
            if (IsPropDirty("IndicatorsLabelFormat"))
            { ser.AddStringProp("indicatorsLabelFormat", this._indicatorsLabelFormat); }
            if (IsPropDirty("SlidesLabelFormat"))
            { ser.AddStringProp("slidesLabelFormat", this._slidesLabelFormat); }
            if (IsPropDirty("Interval"))
            { ser.AddNumberProp("interval", this._interval); }
            if (IsPropDirty("MaximumIndicatorsCount"))
            { ser.AddNumberProp("maximumIndicatorsCount", this._maximumIndicatorsCount); }
            if (IsPropDirty("AnimationType"))
            { ser.AddEnumProp("animationType", this._animationType); }
            if (IsPropDirty("SlideChangedRef"))
            { ser.AddStringProp("slideChangedRef", this._slideChangedRef); }
            if (IsPropDirty("PlayingRef"))
            { ser.AddStringProp("playingRef", this._playingRef); }
            if (IsPropDirty("PausedRef"))
            { ser.AddStringProp("pausedRef", this._pausedRef); }

        }

    }
}
