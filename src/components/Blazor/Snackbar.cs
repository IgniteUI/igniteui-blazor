using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A snackbar component is used to provide feedback about an operation
    /// by showing a brief message at the bottom of the screen.
    /// The component integrates with the
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Invoker_Commands_API">Invoker Commands API</see>:
    /// an Ignite UI button or a native <c>&lt;button&gt;</c> with <c>command="--show"</c> / <c>"--hide"</c> /
    /// <c>"--toggle"</c> and <c>commandfor</c> pointing to this component will call the
    /// corresponding method declaratively.
    /// </summary>
    public partial class IgbSnackbar : IgbBaseAlertLike
    {
        public override string Type { get { return "WebSnackbar"; } }

        protected override void EnsureModulesLoaded()
        {
            if (!IgbSnackbarModule.IsLoadRequested(IgBlazor))
            {
                IgbSnackbarModule.Register(IgBlazor);
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
                return "igc-snackbar";
            }
        }

        private string _actionText;

        /// <summary>
        /// The text of the action button.
        /// </summary>
        [Parameter]
        public string ActionText
        {
            get { return this._actionText; }
            set
            {
                if (this._actionText != value || !IsPropDirty("ActionText"))
                {
                    MarkPropDirty("ActionText");
                }
                this._actionText = value;

            }
        }

        private string _actionRef = null;
        private string _actionScript = null;

        /// <summary>
        /// Name of a client-side function that handles the <see cref="Action"/> event in the browser instead.
        /// </summary>
        /// <remarks>
        /// Register the function on the client like
        /// <c>igRegisterScript("MyHandler", function (args) { }, false)</c>.
        /// </remarks>
        [Parameter]
        public string ActionScript
        {

            set
            {
                if (value != this._actionScript)
                {
                    this._actionScript = value;
                    this.OnRefChanged("Action", null, value, true, false, (string refName, object oldValue, object newValue) =>
                    {
                        this._actionRef = refName;
                        this.MarkPropDirty("ActionRef");
                    });
                }
            }
            get
            {
                return this._actionScript;
            }
        }

        private EventCallback<IgbVoidEventArgs>? _action = null;

        /// <summary>
        /// Emitted when the snackbar action button is clicked.
        /// </summary>
        [Parameter]
        public EventCallback<IgbVoidEventArgs> Action
        {
            get
            {
                return this._action != null ? this._action.Value : EventCallback<IgbVoidEventArgs>.Empty;
            }
            set
            {
                if (value.HasHandler())
                {
                    if (!value.EqualsCompat(_action))
                    {
                        _action = value;
                        this.SetHandler<IgbVoidEventArgs>(this.Name, "Action", value);
                        this.OnRefChanged("Action", null, "event:::Action", true, false, (refName, oldValue, newValue) =>
                        {
                            this._actionRef = refName;
                            this.MarkPropDirty("ActionRef");
                        });
                    }
                }
                else
                {
                    _action = null;
                    this.SetHandler<IgbVoidEventArgs>(this.Name, "Action", null);
                    this.OnRefChanged("Action", null, null, true, false, (refName, oldValue, newValue) =>
                    {
                        this._actionRef = null;
                        this.MarkPropDirty("ActionRef");
                    });
                }
            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("ActionText"))
            { ser.AddStringProp("actionText", this._actionText); }
            if (IsPropDirty("ActionRef"))
            { ser.AddStringProp("actionRef", this._actionRef); }

        }

    }
}
