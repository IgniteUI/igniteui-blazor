namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A value that is already JSON text, for a data parameter such as <see cref="IgbCombo{T}.Data"/>.
    /// The text reaches the client as it is, with no deserialization on the .NET side.
    /// Change notifications do not apply; assign a new instance to update.
    /// </summary>
    public class LocalJson
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
    }

    /// <summary>
    /// A value the client fetches itself as JSON from a URL, for a data parameter such as <see cref="IgbCombo{T}.Data"/>.
    /// The data never passes through the .NET side, so the URL must be reachable from the browser.
    /// Change notifications do not apply; assign a new instance to reload.
    /// </summary>
    public class RemoteJson
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
    }
}
