using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDatePickerModule {
        public static void Register(IIgniteUIBlazor runtime) {
            ModuleLoader.Load(runtime, "WebDatePickerModule");

            IgbBaseComboBoxLikeModule.MarkIsLoadRequested(runtime);
IgbCalendarModule.MarkIsLoadRequested(runtime);
IgbDateTimeInputModule.MarkIsLoadRequested(runtime);
IgbDialogModule.MarkIsLoadRequested(runtime);
IgbIconModule.MarkIsLoadRequested(runtime);

        }

        public static void MarkIsLoadRequested(IIgniteUIBlazor runtime) {
            ModuleLoader.MarkIsLoadRequested(runtime, "WebDatePickerModule");
        }

        public static bool IsLoadRequested(IIgniteUIBlazor runtime) {
            return ModuleLoader.IsLoadRequested(runtime, "WebDatePickerModule");
        }
    }
}
