using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbSelectModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebSelectModule");

            IgbBaseComboBoxLikeModule.MarkIsLoadRequested(runtime);
IgbIconModule.MarkIsLoadRequested(runtime);
IgbInputModule.MarkIsLoadRequested(runtime);
IgbSelectGroupModule.MarkIsLoadRequested(runtime);
IgbSelectHeaderModule.MarkIsLoadRequested(runtime);
IgbSelectItemModule.MarkIsLoadRequested(runtime);

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebSelectModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebSelectModule");
        }
    }
}
