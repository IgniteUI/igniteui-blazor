using System;

namespace IgniteUI.Blazor.Controls
{
    public class WCEnumNameAttribute
        : Attribute
    {
        public WCEnumNameAttribute(string alternateName)
        {
            Name = alternateName;
        }
        
        public string Name
        {
            get; private set;
        }
    }

}