using System.Globalization;
using System.Text.Json;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbCombo<TValue, TItem>
    {
        // The combo exposes RenderFragment templates (ItemTemplate / GroupHeaderTemplate)
        // that are hosted client-side through an <igc-template-content> element. That
        // hosting routes back into the Blazor renderer via AdjustDynamicContent, which
        // requires the DynamicContentHolder to be present. Like the grid and charts, the
        // combo therefore needs dynamic content enabled so the holder is created.
        private protected override bool NeedsDynamicContent
        {
            get
            {
                return true;
            }
        }

        // The web component calls both templates with a plain { item } context, which arrives
        // here as a property bag whose item entry is still unresolved.
        private protected override object? ConvertTemplateContext(string? templateId, object? context)
        {
            if (context is not Dictionary<string, object?> args || !args.TryGetValue("item", out var item))
            {
                return base.ConvertTemplateContext(templateId, context);
            }

            if (templateId != null && templateId == _itemTemplateTemplateId)
            {
                // A data item crosses as a uuid ref; resolve it against the combo's data source.
                // Primitive items cross by value instead, so coerce those to TItem. Anything that is
                // still no TItem becomes null, which the template content never renders.
                return ToItem(ConvertReturnValue(item)) is TItem typed ? typed : null;
            }

            if (templateId != null && templateId == _groupHeaderTemplateTemplateId)
            {
                // The header record is synthetic ({ [valueKey]: key, [displayKey]: key, [groupKey]: key }),
                // not a data item, so only the group key is taken from it.
                object? key = null;
                if (GroupKey != null &&
                    ConvertReturnValue(item) is Dictionary<string, object?> record &&
                    record.TryGetValue(GroupKey, out var keyObj))
                {
                    key = ConvertReturnValue(keyObj);
                    if (key is JsonElement { ValueKind: JsonValueKind.True or JsonValueKind.False } boolKey)
                    {
                        key = boolKey.GetBoolean();
                    }
                    else if (key is JsonElement { ValueKind: JsonValueKind.Null })
                    {
                        key = null;
                    }
                }
                return new IgbComboGroupHeaderContext { Key = key };
            }

            return base.ConvertTemplateContext(templateId, context);
        }

        private static object? ToItem(object? value)
        {
            if (value == null || value is TItem)
            {
                return value;
            }
            if (value is JsonElement { ValueKind: JsonValueKind.True or JsonValueKind.False } boolValue)
            {
                value = boolValue.GetBoolean();
            }
            var target = Nullable.GetUnderlyingType(typeof(TItem)) ?? typeof(TItem);
            if (value is IConvertible && typeof(IConvertible).IsAssignableFrom(target) && !target.IsEnum)
            {
                try
                {
                    return Convert.ChangeType(value, target, CultureInfo.InvariantCulture);
                }
                catch (Exception e) when (e is InvalidCastException or FormatException or OverflowException)
                {
                    return null;
                }
            }
            return value;
        }
    }
}
