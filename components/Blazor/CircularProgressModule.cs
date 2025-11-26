using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbCircularProgressModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebCircularProgressModule");

            IgbProgressBaseModule.MarkIsLoadRequested(runtime);

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebCircularProgressModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebCircularProgressModule");
        }
    }
}
