namespace IgniteUI.Blazor.Controls
{
    /// <summary>Names the web component attribute a parameter maps to, when it differs from the parameter name.</summary>
    public class WCAttributeNameAttribute : Attribute
    {
        /// <summary>Maps to <paramref name="alternateName"/>.</summary>
        public WCAttributeNameAttribute(string alternateName)
        {
            Name = alternateName;
        }

        /// <summary>The web component attribute name.</summary>
        public string Name
        {
            get; private set;
        }
    }

}
