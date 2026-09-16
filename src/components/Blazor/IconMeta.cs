
namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Identifies a registered icon by its name and the collection it belongs to.
    /// </summary>
    [BlazorPlainObject]
    public partial class IgbIconMeta : BaseJsonSerializable
    {
        /// <inheritdoc />
        public override string Type { get { return "WebIconMeta"; } }

        private string _collection = string.Empty;

        /// <summary>
        /// The name of the collection the icon is registered in.
        /// </summary>
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

    }
}
