using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Pins the timezone contract of the date wire format: a calendar date never drifts, a wall clock
/// value returns verbatim, and an instant survives as the same instant.
/// </summary>
/// <remarks>
/// These are the three cases the shape has to keep apart, and the value alone cannot tell them
/// apart — midnight is a calendar date in one model and an instant in another — so the type the
/// consumer used is what decides. The decode side is exercised through a component instance
/// because that is where the kind a component last sent is applied.
/// </remarks>
public class DateWireFormatTests
{
    // Decoding needs no rendering: the payload is already a string by the time it is parsed.
    private static readonly IgbDatePicker Decoder = new();

    [Fact]
    public void DateOnly_CrossesAsBareDate()
    {
        Assert.Equal("2026-03-15", DateTimeWireFormat.ToWireString(new DateOnly(2026, 3, 15)));
    }

    [Fact]
    public void WallClockDateTime_CrossesWithoutDesignator()
    {
        var wire = DateTimeWireFormat.ToWireString(
            new DateTime(2026, 3, 15, 9, 30, 0, DateTimeKind.Unspecified));

        Assert.Equal("2026-03-15T09:30:00.0000000", wire);
        Assert.DoesNotContain("Z", wire);
        Assert.DoesNotContain("+", wire);
    }

    [Fact]
    public void UtcDateTime_CrossesWithDesignator()
    {
        var wire = DateTimeWireFormat.ToWireString(
            new DateTime(2026, 3, 15, 9, 30, 0, DateTimeKind.Utc));

        Assert.Equal("2026-03-15T09:30:00.0000000Z", wire);
    }

    [Fact]
    public void DateTimeOffset_CrossesWithItsOffset()
    {
        var wire = DateTimeWireFormat.ToWireString(
            new DateTimeOffset(2026, 3, 15, 9, 30, 0, TimeSpan.FromHours(3)));

        Assert.Equal("2026-03-15T09:30:00.0000000+03:00", wire);
    }

    [Theory]
    // The browser's zone is deliberately far from UTC in both directions, because a date-only
    // value parsed as an instant lands on the neighbouring day for one of the two.
    [InlineData("2026-03-15")]
    public void DateOnly_DoesNotDrift_WhateverTheClientOffset(string payload)
    {
        var decoded = Decoder.ReturnToDateOnly(payload, tryConvertValue: false);

        Assert.Equal(new DateOnly(2026, 3, 15), decoded);
    }

    [Theory]
    [InlineData("2026-03-15T09:30:00.000+03:00")]
    [InlineData("2026-03-15T09:30:00.000-08:00")]
    [InlineData("2026-03-15T09:30:00.000")]
    public void WallClockValue_ReturnsVerbatim_WhateverTheClientOffset(string payload)
    {
        var decoded = Decoder.ReturnToDate(payload, DateTimeKind.Unspecified, tryConvertValue: false);

        Assert.Equal(new DateTime(2026, 3, 15, 9, 30, 0), decoded);
        Assert.Equal(DateTimeKind.Unspecified, decoded.Kind);
    }

    [Fact]
    public void UtcValue_ReturnsTheSameInstant()
    {
        // 09:30 at +03:00 is 06:30 UTC; the instant has to survive, not the reading.
        var decoded = Decoder.ReturnToDate(
            "2026-03-15T09:30:00.000+03:00", DateTimeKind.Utc, tryConvertValue: false);

        Assert.Equal(new DateTime(2026, 3, 15, 6, 30, 0, DateTimeKind.Utc), decoded);
        Assert.Equal(DateTimeKind.Utc, decoded.Kind);
    }

    [Fact]
    public void DateTimeOffsetValue_KeepsTheClientOffset()
    {
        var decoded = Decoder.ReturnToDateTimeOffset(
            "2026-03-15T09:30:00.000+03:00", tryConvertValue: false);

        Assert.Equal(TimeSpan.FromHours(3), decoded.Offset);
        Assert.Equal(new DateTimeOffset(2026, 3, 15, 6, 30, 0, TimeSpan.Zero), decoded.ToUniversalTime());
    }

    [Fact]
    public void DateOnlyPayload_IsRecognised()
    {
        Assert.True(DateTimeWireFormat.IsDateOnlyPayload("2026-03-15"));
        Assert.False(DateTimeWireFormat.IsDateOnlyPayload("2026-03-15T00:00:00.000"));
        Assert.False(DateTimeWireFormat.IsDateOnlyPayload(null));
    }
}
