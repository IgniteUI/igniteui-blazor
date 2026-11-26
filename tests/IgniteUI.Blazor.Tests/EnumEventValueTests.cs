using System.Reflection;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Enum values that cross in event details and are written back to them: <c>StringToEnum</c>
/// reads the web component value, <c>EnumToString</c> writes it. Both must honor [WCEnumName].
/// </summary>
public class EnumEventValueTests
{
    private static readonly IgbCombo<object, object> _component = new();

    public static TheoryData<Type, string, string> WCEnumNameMembers()
    {
        var data = new TheoryData<Type, string, string>();
        foreach (var type in typeof(WCEnumNameAttribute).Assembly.GetTypes().Where(t => t.IsEnum && t.IsPublic))
        {
            foreach (var x in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => new { field, wc = field.GetCustomAttribute<WCEnumNameAttribute>() })
                .Where(x => x.wc is not null))
            {
                data.Add(type, x.field.Name, x.wc!.Name);
            }
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(WCEnumNameMembers))]
    public void WCEnumNameMembers_RoundTrip(Type enumType, string member, string wcName)
    {
        var value = Enum.Parse(enumType, member);

        var written = typeof(IgbComponentBase).GetMethod(nameof(IgbComponentBase.EnumToString), BindingFlags.Instance | BindingFlags.NonPublic)!
            .MakeGenericMethod(enumType).Invoke(_component, [value]);
        var read = typeof(IgbComponentBase).GetMethod(nameof(IgbComponentBase.StringToEnum), BindingFlags.Instance | BindingFlags.NonPublic)!
            .MakeGenericMethod(enumType).Invoke(_component, [wcName]);

        Assert.Equal(wcName, written);
        Assert.Equal(value, read);
    }

    [Fact]
    public void WCEnumNameMembers_AreFound()
    {
        // Guards the sweep above against finding nothing.
        Assert.Contains(WCEnumNameMembers(), row => (Type)row[0] == typeof(ComboCloseTrigger) && (string)row[2] == "outside-click");
    }

    [Theory]
    [InlineData("enter", ComboCloseTrigger.Enter)]
    [InlineData("arrow-up", ComboCloseTrigger.ArrowUp)]
    [InlineData("outside-click", ComboCloseTrigger.OutsideClick)]
    [InlineData("apply", ComboCloseTrigger.Apply)]
    public void ComboCloseTrigger_Reads(string wire, ComboCloseTrigger expected)
    {
        Assert.Equal(expected, _component.StringToEnum<ComboCloseTrigger>(wire));
    }

    [Theory]
    [InlineData(ComboFilteringMode.Local, "local")]
    [InlineData(ComboFilteringMode.Remote, "remote")]
    public void ComboFilteringMode_Serialization(ComboFilteringMode mode, string expected)
    {
        Assert.Equal(expected, _component.EnumToString(mode));
        Assert.Equal(mode, _component.StringToEnum<ComboFilteringMode>(expected));
    }

    [Theory]
    [InlineData(ComboSelectionCommit.Immediate, "immediate")]
    [InlineData(ComboSelectionCommit.Confirm, "confirm")]
    public void ComboSelectionCommit_Serialization(ComboSelectionCommit commit, string expected)
    {
        Assert.Equal(expected, _component.EnumToString(commit));
        Assert.Equal(commit, _component.StringToEnum<ComboSelectionCommit>(expected));
    }

    [Theory]
    [InlineData(ComboChangeType.Commit, "commit")]
    public void ComboChangeType_Serialization(ComboChangeType type, string expected)
    {
        Assert.Equal(expected, _component.EnumToString(type));
        Assert.Equal(type, _component.StringToEnum<ComboChangeType>(expected));
    }
}
