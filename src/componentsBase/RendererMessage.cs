using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    internal class RendererMessage
    {
        private Dictionary<string, string> _data = new Dictionary<string, string>();
        private String _type = null;
        public string Type 
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        public void SetData(string key, string data) 
        {
            _data[key] = data;
        }

        public string ToJson() 
        {
            List<string> props = new List<string>();

            props.Add("\"type\": \"" + _type + "\"");
            foreach (string key in _data.Keys) {
                props.Add("\"" + key + "\": " + _data[key]);
            }

            return "{" + string.Join(",\n", props) + "}";
        }

        public ElementReference[] NativeElements { get; set; }
    }

}
