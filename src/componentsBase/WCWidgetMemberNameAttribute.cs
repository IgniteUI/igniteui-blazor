using System;

namespace IgniteUI.Blazor.Controls
{
    public class WCWidgetMemberNameAttribute
        : Attribute
    {
        public WCWidgetMemberNameAttribute(string alternateName)
        {
            Name = alternateName;
        }
        
        public string Name
        {
            get; private set;
        }
    }

}