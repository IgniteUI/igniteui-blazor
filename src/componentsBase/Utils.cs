namespace IgniteUI.Blazor.Controls
{
    internal static class Utils
    {
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
