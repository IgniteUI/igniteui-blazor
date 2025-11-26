using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbListItemModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebListItemModule");

            
        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebListItemModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebListItemModule");
        }
    }
}
