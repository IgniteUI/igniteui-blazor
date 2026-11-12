namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Data the client receives as JSON that the .NET side never parses: <see cref="LocalJson"/> or <see cref="RemoteJson"/>.
    /// </summary>
    public interface IJsonData
    {
        internal string ToRef();
    }

    /// <summary>
    /// An escape hatch for data that is already JSON text, set through <see cref="IJsonDataBinding.DataJson"/> in place of a component's typed data.
    /// The text reaches the client as it is, with no deserialization on the .NET side, so the items never exist as .NET objects:
    /// those the component hands back, such as in its events or selection, arrive as <see cref="System.Text.Json.JsonElement"/> values.
    /// Change notifications do not apply; assign a new instance to update.
    /// </summary>
    public class LocalJson : IJsonData
    {
        /// <summary>Wraps <paramref name="json"/>.</summary>
        public LocalJson(string json)
        {
            _json = json;
        }

        /// <summary>Creates a value from <paramref name="json"/>.</summary>
        public static LocalJson From(string json)
        {
            return new LocalJson(json);
        }

        private string _json;
        /// <summary>The JSON text.</summary>
        public string Json { get { return _json; } }

        internal string ToRef()
        {
            return "localJson:::" + Json.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        string IJsonData.ToRef()
        {
            return ToRef();
        }
    }

    /// <summary>
    /// An escape hatch for data the client fetches itself as JSON from a URL, set through <see cref="IJsonDataBinding.DataJson"/> in place of a component's typed data.
    /// The data never passes through the .NET side, so the URL must be reachable from the browser, and the items never exist as .NET objects:
    /// those the component hands back, such as in its events or selection, arrive as <see cref="System.Text.Json.JsonElement"/> values.
    /// Change notifications do not apply; assign a new instance to reload.
    /// </summary>
    public class RemoteJson : IJsonData
    {
        /// <summary>Points at <paramref name="uri"/>.</summary>
        public RemoteJson(string uri)
        {
            _uri = uri;
        }

        /// <summary>Creates a value from <paramref name="uri"/>.</summary>
        public static RemoteJson From(string uri)
        {
            return new RemoteJson(uri);
        }

        private string? _uri;
        /// <summary>The URL the client fetches.</summary>
        public string? Uri { get { return _uri; } }

        internal string ToRef()
        {
            return "json:::" + Uri;
        }

        string IJsonData.ToRef()
        {
            return ToRef();
        }
    }
}
