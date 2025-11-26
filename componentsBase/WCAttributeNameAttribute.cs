using System;

namespace IgniteUI.Blazor.Controls
{
    public class WCAttributeNameAttribute: Attribute
    {
        public WCAttributeNameAttribute(string alternateName)
        {
            Name = alternateName;
        }
        
        public string Name
        {
            get; private set;
        }
    }

}