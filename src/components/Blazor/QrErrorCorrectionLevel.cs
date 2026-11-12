namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// How much of a QR code can be obscured or damaged and still read; a higher level recovers more but leaves less room for data.
    /// </summary>
    public enum QrErrorCorrectionLevel
    {
        /// <summary>Recovers about 7% of the data.</summary>
        [WCEnumName("L")]
        Low,
        /// <summary>Recovers about 15% of the data.</summary>
        [WCEnumName("M")]
        Medium,
        /// <summary>Recovers about 25% of the data.</summary>
        [WCEnumName("Q")]
        Quartile,
        /// <summary>Recovers about 30% of the data.</summary>
        [WCEnumName("H")]
        High

    }
}
