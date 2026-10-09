namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Lets a component take its data as JSON that the .NET side never parses, an escape hatch next to its typed data parameter.
    /// </summary>
    public interface IJsonDataBinding
    {
        /// <summary>
        /// The data as <see cref="LocalJson"/> or <see cref="RemoteJson"/>, in place of the component's typed data; set one or the other, not both.
        /// Prefer the typed data unless it is already JSON, or the client should fetch it itself.
        /// </summary>
        IJsonData? DataJson { get; set; }
    }
}
