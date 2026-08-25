using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbTree"/> events that concern a single item, such as
    /// <see cref="IgbTree.ItemExpanding"/>, <see cref="IgbTree.ItemCollapsed"/> and
    /// <see cref="IgbTree.ActiveItem"/>.
    /// </summary>
    public partial class IgbTreeItemComponentEventArgs : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebTreeItemComponentEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbTreeItem? _detail;

        /// <summary>
        /// The tree item the event applies to.
        /// </summary>
        [Parameter]
        public IgbTreeItem? Detail
        {
            get { return this._detail; }
            set
            {
                if (this._detail != value || !IsPropDirty("Detail"))
                {
                    MarkPropDirty("Detail");
                }
                this._detail = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object?> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbTreeItem?)ConvertReturnValue(args["detail"], "TreeItem", true); }

            this.SuppressParentNotify = false;
        }

    }
}
