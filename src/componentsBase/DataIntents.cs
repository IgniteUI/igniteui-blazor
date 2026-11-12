namespace IgniteUI.Blazor.Controls
{
    // Currently not used in Lite, possibly in the future (if lite components self-configure)
    internal interface IDataIntentAttribute
    {
        string Intent { get; }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    internal class DataIntentAttribute
        : Attribute, IDataIntentAttribute
    {
        public DataIntentAttribute(string intent)
        {
            Intent = intent;
        }

        public string Intent { get; private set; }
    }
}
