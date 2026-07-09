using IgniteUI.Blazor.Controls;

namespace Blazor.TestBed.WebApp.Components.Common
{
    public static class CustomTypes
    {

        public static readonly Dictionary<string, object> PredefinedTypes
    = new Dictionary<string, object> {
        {
            "IgbDateRangeDescriptor", new IgbDateRangeDescriptor(){
                DateRange = new DateTime[1] { DateTime.Now.ToUniversalTime() },
                RangeType = DateRangeType.Specific
            }
        },
        {
         "IgbChatOptions", new IgbChatOptions()
            {
                CurrentUserId = "user1",
                DisableAutoScroll = false,
                DisableInputAttachments = false,
                IsTyping = false,
                HeaderText = "Chat Header",
                InputPlaceholder = "Type a message...",
                Suggestions = new string[] { "Suggestion 1", "Suggestion 2" },
                SuggestionsPosition = ChatSuggestionsPosition.BelowMessages,
                StopTypingDelay = 2000,
                AdoptRootStyles = true,
                Renderers = new IgbChatRenderers()
            
            }
        },
        };

        public static readonly Dictionary<string, object[]> PredefinedMethodArgs = new Dictionary<string, object[]>
        {
             { "RegisterIconAsync", ["customIcon", "filter-solid.svg", "collection"] },
             { "RegisterIconFromTextAsync", ["customIcon2", "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\"><!--!Font Awesome Free 6.7.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d=\"M3.9 54.9C10.5 40.9 24.5 32 40 32l432 0c15.5 0 29.5 8.9 36.1 22.9s4.6 30.5-5.2 42.5L320 320.9 320 448c0 12.1-6.8 23.2-17.7 28.6s-23.8 4.3-33.5-3l-64-48c-8.1-6-12.8-15.5-12.8-25.6l0-79.1L9 97.3C-.7 85.4-2.8 68.8 3.9 54.9z\"/></svg>", "collection"]},
             {"AddRowAsync", [ new NwindDataItem(){ }, null]},
             { "FocusPaneAsync", ["content1"] },
             { "LoadLayoutAsync", ["[]"] },
        };

        public static readonly Dictionary<string, object> PredefinedPropertyValues = new Dictionary<string, object>
        {
             { "Step", 0.5 },
             { "Locale", "en" },
            { "VisibleMonths", 2.0 },
            // prompt needs to be a single symbol
            { "Prompt", "/" },
            { "StartMinSize", "100px" },
            { "StartMaxSize", "500px" },
            { "EndMinSize", "100px" },
            { "EndMaxSize", "500px" },
            { "StartSize", "50%" },
            { "EndSize", "50%" },
        };
    }
}
