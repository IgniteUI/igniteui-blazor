using System.Text.Json.Serialization;

namespace IgniteUI.Blazor.Lite.TestBed.Components.Common
{
    /// <summary>
    /// What a single component run exercised, so a passing run reports the members it
    /// covered and a run that never got as far as the component says why.
    /// </summary>
    public class ComponentRunSummary
    {
        public string Component { get; set; } = string.Empty;

        /// <summary>The client-side selector the checks were run against.</summary>
        public string Selector { get; set; } = string.Empty;

        /// <summary>Failure message for why the run never reached/ran the component, if any.</summary>
        public string? Failure { get; set; }

        /// <summary>Initial values compared against the client component's, before anything was set.</summary>
        public int Defaults { get; set; }

        public int Properties { get; set; }

        public int Events { get; set; }

        public int ServerTemplates { get; set; }

        public int ClientTemplates { get; set; }

        public int Methods { get; set; }

        public int Errors { get; set; }

        [JsonIgnore]
        public int Checks => Defaults + Properties + Events + ServerTemplates + ClientTemplates + Methods;

        public static ComponentRunSummary Failed(string component, string failure)
            => new() { Component = component, Failure = failure };

        public override string ToString()
        {
            if (Failure != null)
            {
                return $"{Component}: {Failure}";
            }

            if (Checks == 0)
            {
                return $"{Component} ({Selector}): rendered, no members to check";
            }

            var text = $"{Component} ({Selector}): {Checks} checks - "
                + $"{Defaults} defaults, {Properties} props, {Events} events, {Methods} methods, "
                + $"{ServerTemplates} server / {ClientTemplates} client templates";

            return Errors > 0 ? $"{text}, {Errors} errors" : text;
        }
    }
}
