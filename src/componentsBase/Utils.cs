using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace IgniteUI.Blazor.Controls
{
    internal static class Utils
    {
        // Per enum type: member name -> [WCEnumName] and [WCEnumName] -> member name, for the
        // members that carry the attribute. Built once per type; enum fields never change.
        private sealed record WCEnumNames(Dictionary<string, string> ByMember, Dictionary<string, string> ByWCName);

        private static readonly ConcurrentDictionary<Type, WCEnumNames> _wcEnumNames = new();

        // enumType intentionally unannotated: the requirement would propagate into ObjectToParam on the
        // component base classes, where DAM method parameters surface IL2111 in every consuming app.
        [UnconditionalSuppressMessage("Trimming", "IL2070", Justification = "The trimmer preserves all fields of enum types that are kept, and the enum types reaching here are statically referenced by their callers.")]
        private static WCEnumNames BuildWCEnumNames(Type enumType)
        {
            var byMember = new Dictionary<string, string>(StringComparer.Ordinal);
            var byWCName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var field in enumType.GetFields())
            {
                if (!field.IsPublic || field.IsSpecialName)
                {
                    continue;
                }

                foreach (var attr in field.GetCustomAttributes(true))
                {
                    if (attr is WCEnumNameAttribute wc && wc.Name != null)
                    {
                        byMember[field.Name] = wc.Name;
                        byWCName.TryAdd(wc.Name, field.Name);
                        break;
                    }
                }
            }

            return new WCEnumNames(byMember, byWCName);
        }

        private static WCEnumNames GetWCEnumNames(Type enumType) => _wcEnumNames.GetOrAdd(enumType, BuildWCEnumNames);

        /// <summary>The [WCEnumName] of an enum member, when it has one.</summary>
        internal static bool TryGetWCEnumName(Type enumType, string? enumMemberName, out string? name)
        {
            name = null;
            return enumMemberName != null && GetWCEnumNames(enumType).ByMember.TryGetValue(enumMemberName, out name);
        }

        /// <summary>The enum member whose [WCEnumName] is <paramref name="wcName"/>, when there is one.</summary>
        internal static bool TryGetEnumMemberName(Type enumType, string? wcName, out string? memberName)
        {
            memberName = null;
            return wcName != null && GetWCEnumNames(enumType).ByWCName.TryGetValue(wcName, out memberName);
        }
    }
}
