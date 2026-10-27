namespace IgniteUI.Blazor.Controls
{
    /// <summary>Names the web component property a parameter maps to, when it differs from the parameter name.</summary>
    public class WCWidgetMemberNameAttribute
        : Attribute
    {
        /// <summary>Maps to <paramref name="alternateName"/>.</summary>
        public WCWidgetMemberNameAttribute(string alternateName)
        {
            Name = alternateName;
        }

        /// <summary>The web component property name.</summary>
        public string Name
        {
            get; private set;
        }
    }

}
