namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// A value that is already JSON text, for an untyped data parameter.
    /// The text reaches the client as it is, with no deserialization on the .NET side.
    /// Change notifications do not apply; assign a new instance to update.
    /// For a strongly-typed data parameter such as <see cref="IgbCombo{TValue, TItem}.Data"/>, use
    /// <see cref="LocalJson{TItem}"/> instead, which is directly assignable to it.
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
    /// A <see cref="LocalJson"/> that is directly assignable to a strongly-typed data parameter such as <see cref="IgbCombo{TValue, TItem}.Data"/>.
    /// Enumerating it yields nothing; the interop layer substitutes the wrapped JSON before any enumeration of the value takes place.
    /// </summary>
    public sealed class LocalJson<TItem> : LocalJson, IEnumerable<TItem>
    {
        /// <summary>Wraps <paramref name="json"/>.</summary>
        public LocalJson(string json) : base(json)
        {
        }

        /// <summary>Creates a value from <paramref name="json"/>.</summary>
        public static new LocalJson<TItem> From(string json)
        {
            return new LocalJson<TItem>(json);
        }

        /// <inheritdoc/>
        public IEnumerator<TItem> GetEnumerator()
        {
            return Enumerable.Empty<TItem>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// A value the client fetches itself as JSON from a URL, for an untyped data parameter.
    /// The data never passes through the .NET side, so the URL must be reachable from the browser.
    /// Change notifications do not apply; assign a new instance to reload.
    /// For a strongly-typed data parameter such as <see cref="IgbCombo{TValue, TItem}.Data"/>, use
    /// <see cref="RemoteJson{TItem}"/> instead, which is directly assignable to it.
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

    /// <summary>
    /// A <see cref="RemoteJson"/> that is directly assignable to a strongly-typed data parameter such as <see cref="IgbCombo{TValue, TItem}.Data"/>.
    /// Enumerating it yields nothing; the interop layer substitutes the wrapped URL reference before any enumeration of the value takes place.
    /// </summary>
    public sealed class RemoteJson<TItem> : RemoteJson, IEnumerable<TItem>
    {
        /// <summary>Points at <paramref name="uri"/>.</summary>
        public RemoteJson(string uri) : base(uri)
        {
        }

        /// <summary>Creates a value from <paramref name="uri"/>.</summary>
        public static new RemoteJson<TItem> From(string uri)
        {
            return new RemoteJson<TItem>(uri);
        }

        /// <inheritdoc/>
        public IEnumerator<TItem> GetEnumerator()
        {
            return Enumerable.Empty<TItem>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
