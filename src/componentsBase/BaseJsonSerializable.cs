
namespace IgniteUI.Blazor.Controls
{
    public class BaseJsonSerializable : JsonSerializable
    {
        public virtual string Type
        {
            get
            {
                var typeName = (this.GetType().Name.Replace("View", "View"));
                if (typeName.StartsWith("Igb"))
                {
                    typeName = typeName.Substring(3);
                }
                return typeName;
            }
        }

        protected string _name = Guid.NewGuid().ToString();
        public string Name
        {
            set
            {
                if (this._name != value || !IsPropDirty("Name"))
                {
                    MarkPropDirty("Name");
                }
                _name = value;
            }
            get
            {
                return _name;
            }
        }

        private HashSet<string> _dirtyProperties = new HashSet<string>();
        public bool IsDirty { get { return _dirtyProperties.Count > 0; } }

        public bool IsPropDirty(string propertyName)
        {
            return _dirtyProperties.Contains(propertyName);
        }

        protected void MarkPropDirty(string propertyName)
        {
            _dirtyProperties.Add(propertyName);
        }

        public virtual void Serialize(SerializationContext context, string? propertyName)
        {
            if (!IsDirty)
            {
                return;
            }

            var ser = new RendererSerializer(context, Type);
            ser.Type = Type;
            ser.Start(propertyName);
            SerializeCore(ser);
            ser.End();
        }

        internal virtual void SerializeCore(RendererSerializer ser)
        {
            if (IsPropDirty("Name"))
            { ser.AddStringProp("name", this._name); }
        }
    }
}
