using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbTooltipModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebTooltipModule");

            
        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebTooltipModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebTooltipModule");
        }
    }
}
