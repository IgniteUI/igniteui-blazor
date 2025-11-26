using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbProgressBaseModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebProgressBaseModule");

            
        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebProgressBaseModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebProgressBaseModule");
        }
    }
}
