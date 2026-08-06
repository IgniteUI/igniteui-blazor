using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Event arguments for the <see cref="IgbTabs.Change"/> event, carrying the <see cref="IgbTab"/>
    /// instance the event applies to.
    /// </summary>
    public partial class IgbTabComponentEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebTabComponentEventArgs"; } }

        private static bool _marshalByValue = true;

        private IgbTab _detail;

        partial void OnDetailChanging(ref IgbTab newValue);

        /// <summary>
        /// The tab that became selected.
        /// </summary>
        [Parameter]
        public IgbTab Detail
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

        partial void FindByNameTabComponentEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameTabComponentEventArgs(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbTabComponentEventArgs(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbTabComponentEventArgs(ser);

            if (IsPropDirty("Detail"))
            { ser.AddSerializableProp("detail", this._detail); }

        }

        protected internal override void ToEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.ToEventJson(control, args);

            if (IsPropDirty("Detail"))
            { args["detail"] = ObjectToParam(this._detail); }

        }

        protected internal override void FromEventJson(BaseRendererControl control, Dictionary<string, object> args)
        {
            base.FromEventJson(control, args);
            this.SuppressParentNotify = true;

            if (args.ContainsKey("detail"))
            { this.Detail = (IgbTab)ConvertReturnValue(args["detail"], "Tab", true); }

            this.SuppressParentNotify = false;
        }

    }
}
