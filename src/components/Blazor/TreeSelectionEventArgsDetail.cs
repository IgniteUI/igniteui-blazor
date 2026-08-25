using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// The payload carried by <see cref="IgbTree.SelectionChanged"/>, holding the selection
    /// the tree is about to apply.
    /// </summary>
    public partial class IgbTreeSelectionEventArgsDetail : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTreeSelectionEventArgsDetail"; } }

        private static bool _marshalByValue = true;

        private IgbTreeItem[]? _newSelection;

        /// <summary>
        /// The tree items that will make up the new selection.
        /// </summary>
        [Parameter]
        public IgbTreeItem[]? NewSelection
        {
            get { return this._newSelection; }
            set
            {
                if (this._newSelection != value || !IsPropDirty("NewSelection"))
                {
                    MarkPropDirty("NewSelection");
                }
                this._newSelection = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("NewSelection"))
            { ser.AddSerializableArrayProp("newSelection", this._newSelection); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("NewSelection"))
            { args["newSelection"] = ObjectArrayToParam(this._newSelection); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?>? args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args != null && args.ContainsKey("newSelection"))
            { this.NewSelection = ReturnToObjectArray<IgbTreeItem>(args["newSelection"]); }

            this.SuppressParentNotify = false;
        }

    }
}
