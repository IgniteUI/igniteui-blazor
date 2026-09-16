namespace IgniteUI.Blazor.Controls
{
    public class MarshalByValueFactory
    {
        internal static bool MustMarshalByValue(string? typeName)
        {
            switch (typeName)
            {
                //@@MustMarshalByValue
                case "CalendarFormatOptions":
                    return true;
                case "FocusOptions":
                    return true;
                case "FormatSpecifier":
                    return true;
                case "NumberFormatSpecifier":
                    return true;
                case "ActiveStepChangedEventArgs":
                case "WebActiveStepChangedEventArgs":
                    return true;
                case "ActiveStepChangedEventArgsDetail":
                case "WebActiveStepChangedEventArgsDetail":
                    return true;
                case "ActiveStepChangingEventArgs":
                case "WebActiveStepChangingEventArgs":
                    return true;
                case "ActiveStepChangingEventArgsDetail":
                case "WebActiveStepChangingEventArgsDetail":
                    return true;
                case "ChatDraftMessage":
                case "WebChatDraftMessage":
                    return true;
                case "ChatMessage":
                case "WebChatMessage":
                    return true;
                case "ChatMessageAttachment":
                case "WebChatMessageAttachment":
                    return true;
                case "ChatMessageAttachmentEventArgs":
                case "WebChatMessageAttachmentEventArgs":
                    return true;
                case "ChatMessageEventArgs":
                case "WebChatMessageEventArgs":
                    return true;
                case "ChatMessageReaction":
                case "WebChatMessageReaction":
                    return true;
                case "ChatMessageReactionEventArgs":
                case "WebChatMessageReactionEventArgs":
                    return true;
                case "CheckboxChangeEventArgs":
                case "WebCheckboxChangeEventArgs":
                    return true;
                case "CheckboxChangeEventArgsDetail":
                case "WebCheckboxChangeEventArgsDetail":
                    return true;
                case "ComboChangeEventArgs":
                case "WebComboChangeEventArgs":
                    return true;
                case "ComboChangeEventArgsDetail":
                case "WebComboChangeEventArgsDetail":
                    return true;
                case "ComponentBoolValueChangedEventArgs":
                case "WebComponentBoolValueChangedEventArgs":
                    return true;
                case "ComponentDateValueChangedEventArgs":
                case "WebComponentDateValueChangedEventArgs":
                    return true;
                case "ComponentValueChangedEventArgs":
                case "WebComponentValueChangedEventArgs":
                    return true;
                case "DateRangeValueDetail":
                case "WebDateRangeValueDetail":
                    return true;
                case "DateRangeValueEventArgs":
                case "WebDateRangeValueEventArgs":
                    return true;
                case "DropdownItemComponentEventArgs":
                case "WebDropdownItemComponentEventArgs":
                    return true;
                case "ExpansionPanelComponentEventArgs":
                case "WebExpansionPanelComponentEventArgs":
                    return true;
                case "HighlightNavigation":
                case "WebHighlightNavigation":
                    return true;
                case "IconMeta":
                case "WebIconMeta":
                    return true;
                case "NumberEventArgs":
                case "WebNumberEventArgs":
                    return true;
                case "RadioChangeEventArgs":
                case "WebRadioChangeEventArgs":
                    return true;
                case "RadioChangeEventArgsDetail":
                case "WebRadioChangeEventArgsDetail":
                    return true;
                case "RangeSliderValue":
                case "WebRangeSliderValue":
                    return true;
                case "SelectItemComponentEventArgs":
                case "WebSelectItemComponentEventArgs":
                    return true;
                case "SplitterResizeEventArgs":
                case "WebSplitterResizeEventArgs":
                    return true;
                case "SplitterResizeEventArgsDetail":
                case "WebSplitterResizeEventArgsDetail":
                    return true;
                case "TabComponentEventArgs":
                case "WebTabComponentEventArgs":
                    return true;
                case "TileChangeStateEventArgs":
                case "WebTileChangeStateEventArgs":
                    return true;
                case "TileChangeStateEventArgsDetail":
                case "WebTileChangeStateEventArgsDetail":
                    return true;
                case "TileComponentEventArgs":
                case "WebTileComponentEventArgs":
                    return true;
                case "TreeItemComponentEventArgs":
                case "WebTreeItemComponentEventArgs":
                    return true;
                case "TreeSelectionEventArgs":
                case "WebTreeSelectionEventArgs":
                    return true;
                case "TreeSelectionEventArgsDetail":
                case "WebTreeSelectionEventArgsDetail":
                    return true;

                    //@@MustMarshalByValueEnd
            }
            return false;
        }

        internal static object? CreateInstance(string typeName)
        {
            switch (typeName)
            {
                //@@MarshalByValue
                case "CalendarFormatOptions":
                    return new IgbCalendarFormatOptions();
                case "FocusOptions":
                    return new IgbFocusOptions();
                case "FormatSpecifier":
                    return new IgbFormatSpecifier();
                case "NumberFormatSpecifier":
                    return new IgbNumberFormatSpecifier();
                case "ActiveStepChangedEventArgs":
                case "WebActiveStepChangedEventArgs":
                    return new IgbActiveStepChangedEventArgs();
                case "ActiveStepChangedEventArgsDetail":
                case "WebActiveStepChangedEventArgsDetail":
                    return new IgbActiveStepChangedEventArgsDetail();
                case "ActiveStepChangingEventArgs":
                case "WebActiveStepChangingEventArgs":
                    return new IgbActiveStepChangingEventArgs();
                case "ActiveStepChangingEventArgsDetail":
                case "WebActiveStepChangingEventArgsDetail":
                    return new IgbActiveStepChangingEventArgsDetail();
                case "ChatDraftMessage":
                case "WebChatDraftMessage":
                    return new IgbChatDraftMessage();
                case "ChatMessage":
                case "WebChatMessage":
                    return new IgbChatMessage();
                case "ChatMessageAttachment":
                case "WebChatMessageAttachment":
                    return new IgbChatMessageAttachment();
                case "ChatMessageAttachmentEventArgs":
                case "WebChatMessageAttachmentEventArgs":
                    return new IgbChatMessageAttachmentEventArgs();
                case "ChatMessageEventArgs":
                case "WebChatMessageEventArgs":
                    return new IgbChatMessageEventArgs();
                case "ChatMessageReaction":
                case "WebChatMessageReaction":
                    return new IgbChatMessageReaction();
                case "ChatMessageReactionEventArgs":
                case "WebChatMessageReactionEventArgs":
                    return new IgbChatMessageReactionEventArgs();
                case "CheckboxChangeEventArgs":
                case "WebCheckboxChangeEventArgs":
                    return new IgbCheckboxChangeEventArgs();
                case "CheckboxChangeEventArgsDetail":
                case "WebCheckboxChangeEventArgsDetail":
                    return new IgbCheckboxChangeEventArgsDetail();
                case "ComboChangeEventArgs":
                case "WebComboChangeEventArgs":
                    return new IgbComboChangeEventArgs();
                case "ComboChangeEventArgsDetail":
                case "WebComboChangeEventArgsDetail":
                    return new IgbComboChangeEventArgsDetail();
                case "ComponentBoolValueChangedEventArgs":
                case "WebComponentBoolValueChangedEventArgs":
                    return new IgbComponentBoolValueChangedEventArgs();
                case "ComponentDateValueChangedEventArgs":
                case "WebComponentDateValueChangedEventArgs":
                    return new IgbComponentDateValueChangedEventArgs();
                case "ComponentValueChangedEventArgs":
                case "WebComponentValueChangedEventArgs":
                    return new IgbComponentValueChangedEventArgs();
                case "DateRangeValueDetail":
                case "WebDateRangeValueDetail":
                    return new IgbDateRangeValueDetail();
                case "DateRangeValueEventArgs":
                case "WebDateRangeValueEventArgs":
                    return new IgbDateRangeValueEventArgs();
                case "DropdownItemComponentEventArgs":
                case "WebDropdownItemComponentEventArgs":
                    return new IgbDropdownItemComponentEventArgs();
                case "ExpansionPanelComponentEventArgs":
                case "WebExpansionPanelComponentEventArgs":
                    return new IgbExpansionPanelComponentEventArgs();
                case "HighlightNavigation":
                case "WebHighlightNavigation":
                    return new IgbHighlightNavigation();
                case "IconMeta":
                case "WebIconMeta":
                    return new IgbIconMeta();
                case "NumberEventArgs":
                case "WebNumberEventArgs":
                    return new IgbNumberEventArgs();
                case "RadioChangeEventArgs":
                case "WebRadioChangeEventArgs":
                    return new IgbRadioChangeEventArgs();
                case "RadioChangeEventArgsDetail":
                case "WebRadioChangeEventArgsDetail":
                    return new IgbRadioChangeEventArgsDetail();
                case "RangeSliderValue":
                case "WebRangeSliderValue":
                    return new IgbRangeSliderValue();
                case "SelectItemComponentEventArgs":
                case "WebSelectItemComponentEventArgs":
                    return new IgbSelectItemComponentEventArgs();
                case "SplitterResizeEventArgs":
                case "WebSplitterResizeEventArgs":
                    return new IgbSplitterResizeEventArgs();
                case "SplitterResizeEventArgsDetail":
                case "WebSplitterResizeEventArgsDetail":
                    return new IgbSplitterResizeEventArgsDetail();
                case "TabComponentEventArgs":
                case "WebTabComponentEventArgs":
                    return new IgbTabComponentEventArgs();
                case "TileChangeStateEventArgs":
                case "WebTileChangeStateEventArgs":
                    return new IgbTileChangeStateEventArgs();
                case "TileChangeStateEventArgsDetail":
                case "WebTileChangeStateEventArgsDetail":
                    return new IgbTileChangeStateEventArgsDetail();
                case "TileComponentEventArgs":
                case "WebTileComponentEventArgs":
                    return new IgbTileComponentEventArgs();
                case "TreeItemComponentEventArgs":
                case "WebTreeItemComponentEventArgs":
                    return new IgbTreeItemComponentEventArgs();
                case "TreeSelectionEventArgs":
                case "WebTreeSelectionEventArgs":
                    return new IgbTreeSelectionEventArgs();
                case "TreeSelectionEventArgsDetail":
                case "WebTreeSelectionEventArgsDetail":
                    return new IgbTreeSelectionEventArgsDetail();

                    //@@MarshalByValueEnd
            }
            return null;
        }

    }
}
