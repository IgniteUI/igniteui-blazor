using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Identifies a registered icon by its name and the collection it belongs to.
    /// </summary>
    public partial class IgbIconMeta : BaseRendererElement
    {
        /// <inheritdoc />
        public override string Type { get { return "WebIconMeta"; } }

        private static bool _marshalByValue = true;

        private string _collection;

        /// <summary>
        /// The name of the collection the icon is registered in.
        /// </summary>
        [Parameter]
        public string Collection
        {
            get { return this._collection; }
            set
            {
                if (this._collection != value || !IsPropDirty("Collection"))
                {
                    MarkPropDirty("Collection");
                }
                this._collection = value;

            }
        }

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            if (IsPropDirty("Collection"))
            { ser.AddStringProp("collection", this._collection); }

        }

        /// <inheritdoc />
        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Name"))
            { args["name"] = this._name; }
            if (IsPropDirty("Collection"))
            { args["collection"] = this._collection; }

        }

        /// <inheritdoc />
        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("name"))
            { this.Name = ReturnToString(args["name"]); }
            if (args.ContainsKey("collection"))
            { this.Collection = ReturnToString(args["collection"]); }

            this.SuppressParentNotify = false;
        }

    }
}
