using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDropdownModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebDropdownModule");

            IgbBaseComboBoxLikeModule.MarkIsLoadRequested(runtime);
IgbDropdownItemModule.MarkIsLoadRequested(runtime);
IgbDropdownHeaderModule.MarkIsLoadRequested(runtime);
IgbDropdownGroupModule.MarkIsLoadRequested(runtime);

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebDropdownModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebDropdownModule");
        }
    }
}
