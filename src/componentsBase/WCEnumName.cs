namespace IgniteUI.Blazor.Controls
{
    /// <summary>Names the web component value an enum member maps to.</summary>
    public class WCEnumNameAttribute
        : Attribute
    {
        /// <summary>Maps to <paramref name="alternateName"/>.</summary>
        public WCEnumNameAttribute(string alternateName)
        {
            Name = alternateName;
        }

        /// <summary>The web component value.</summary>
        public string Name
        {
            get; private set;
        }
    }

}
