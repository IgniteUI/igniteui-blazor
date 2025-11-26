
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace IgniteUI.Blazor.Controls
{

    public partial class IgbRating
    {
        [Inject]
        private ILogger<IgbRating> Logger { get; set; } = default;

        public override Task SetParametersAsync(ParameterView parameters)
        {
            // Params are case-insensitive & can't keep old name as deprecated,
            // so coerce value to avoid old code setting incorrect type errors:
            parameters.TryGetValue("Readonly", out object result);
            if (result != null && result is string value)
            {
                Logger.LogWarning("Readonly has been renamed, use ReadOnly instead");
                var updatedParams = parameters.ToDictionary().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                bool.TryParse(value, out var coerced);
                updatedParams["Readonly"] = coerced;
                parameters = ParameterView.FromDictionary(updatedParams);
            }
            return base.SetParametersAsync(parameters);
        }
    }
}
