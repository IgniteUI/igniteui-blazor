
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbChatMessageReactionEventArgs : BaseRendererElement
    {
        public override string Type { get { return "WebChatMessageReactionEventArgs"; } }

        private static bool _marshalByValue = true;

        public IgbChatMessageReactionEventArgs() : base()
        {
            OnCreatedIgbChatMessageReactionEventArgs();

        }

        partial void OnCreatedIgbChatMessageReactionEventArgs();

        private IgbChatMessageReaction _detail;

        partial void OnDetailChanging(ref IgbChatMessageReaction newValue);
        [Parameter]
        public IgbChatMessageReaction Detail
        {
            get { return this._detail; }
            set
            {
                OnDetailChanging(ref value);
                MarkPropDirty("Detail");
                if (this._detail != null)
                {
                    this.DetachChild(this._detail);
                }
                if (value != null)
                {
                    this.AttachChild(value);
                }
                this._detail = value;
            }

        }

        partial void FindByNameChatMessageReactionEventArgs(string name, ref object item);
        public override object FindByName(string name)
        {

            var baseResult = base.FindByName(name);
            if (baseResult != null)
            {
                return baseResult;
            }

            object item = null;
            FindByNameChatMessageReactionEventArgs(name, ref item);
            if (item != null)
            {
                return item;
            }

            return null;
        }

        partial void SerializeCoreIgbChatMessageReactionEventArgs(RendererSerializer ser);

        internal override void SerializeCore(RendererSerializer ser)
        {
            base.SerializeCore(ser);

            SerializeCoreIgbChatMessageReactionEventArgs(ser);

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
            { this.Detail = (IgbChatMessageReaction)ConvertReturnValue(args["detail"], "ChatMessageReaction", true); }

            this.SuppressParentNotify = false;
        }

    }
}
