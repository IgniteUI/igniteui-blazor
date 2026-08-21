using System.Diagnostics.CodeAnalysis;

namespace IgniteUI.Blazor.Controls
{
    internal static class Utils
    {
        // NOTE: enumType is deliberately unannotated. Annotating it propagates the requirement into
        // ObjectToParam on the component base classes, where DAM-annotated method parameters trigger
        // IL2111 in every consuming app (OpenComponent<T> roots all component members "via reflection").
        [UnconditionalSuppressMessage("Trimming", "IL2070", Justification = "The trimmer preserves all fields of enum types that are kept, and the enum types reaching here are statically referenced by their callers.")]
        internal static bool TryGetWCEnumName(Type enumType, string enumMemberName, out string name)
        {
            name = null;

            foreach (var field in enumType.GetFields())
            {
                if (!field.IsPublic || field.IsSpecialName || field.Name != enumMemberName)
                {
                    continue;
                }

                foreach (var attr in field.GetCustomAttributes(true))
                {
                    if (attr is WCEnumNameAttribute wc)
                    {
                        name = wc.Name;
                        return true;
                    }
                }

                break;
            }

            return false;
        }
    }
}
