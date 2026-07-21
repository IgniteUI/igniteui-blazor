using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace IgniteUI.Blazor.Controls
{
    public class LocalJson
    {
        public LocalJson(string json)
        {
            _json = json;
        }

        public static LocalJson From(string json)
        {
            return new LocalJson(json);
        }

        private string _json;
        public string Json { get { return _json; }}

        internal string ToRef()
        {
            return "localJson:::" + Json.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }

    public class RemoteJson
    {
        public RemoteJson(string uri)
        {
            _uri = uri;
        }

        public static RemoteJson WithUri(string uri)
        {
            return new RemoteJson(uri);
        }

        public static RemoteJson From(string uri)
        {
            return new RemoteJson(uri);
        }

        private string _uri;
        public string Uri { get { return _uri; }}

        internal string ToRef()
        {
            return "json:::" + Uri;
        }
    }
}