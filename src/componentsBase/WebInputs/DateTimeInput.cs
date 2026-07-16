using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    public partial class IgbDateTimeInput
    {
        public async Task StepUpAsync()
        {
            await InvokeMethod("stepUp", new object[] { }, new string[] { });
        }
        public void StepUp()
        {
            InvokeMethodSync("stepUp", new object[] { }, new string[] { });
        }
        public async Task StepUpAsync(DatePart datePart)
        {
            await InvokeMethod("stepUp", new object[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }
        public void StepUp(DatePart datePart)
        {
            InvokeMethodSync("stepUp", new object[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }

        public async Task StepDownAsync()
        {
            await InvokeMethod("stepDown", new object[] { }, new string[] { });
        }
        public void StepDown()
        {
            InvokeMethodSync("stepDown", new object[] { }, new string[] { });
        }
        public async Task StepDownAsync(DatePart datePart)
        {
            await InvokeMethod("stepDown", new object[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }
        public void StepDown(DatePart datePart)
        {
            InvokeMethodSync("stepDown", new object[] { ObjectToParam(datePart, typeof(DatePart)) }, new string[] { "Json" });
        }
    }
}
