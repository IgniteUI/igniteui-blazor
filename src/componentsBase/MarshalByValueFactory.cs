using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace IgniteUI.Blazor.Controls
{
    public class MarshalByValueFactory
    {
        internal static bool MustMarshalByValue(string typeName)
        {
            switch (typeName)
            {
                //@@MustMarshalByValue
                case "CalendarDate":
                    return true;
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

        internal static object CreateInstance(string typeName)
        {
            switch (typeName)
            {
                //@@MarshalByValue
                case "CalendarDate":
                    return new IgbCalendarDate();
                    break;
                case "CalendarFormatOptions":
                    return new IgbCalendarFormatOptions();
                    break;
                case "FocusOptions":
                    return new IgbFocusOptions();
                    break;
                case "FormatSpecifier":
                    return new IgbFormatSpecifier();
                    break;
                case "NumberFormatSpecifier":
                    return new IgbNumberFormatSpecifier();
                    break;
                case "ActiveStepChangedEventArgs":
                case "WebActiveStepChangedEventArgs":
                    return new IgbActiveStepChangedEventArgs();
                    break;
                case "ActiveStepChangedEventArgsDetail":
                case "WebActiveStepChangedEventArgsDetail":
                    return new IgbActiveStepChangedEventArgsDetail();
                    break;
                case "ActiveStepChangingEventArgs":
                case "WebActiveStepChangingEventArgs":
                    return new IgbActiveStepChangingEventArgs();
                    break;
                case "ActiveStepChangingEventArgsDetail":
                case "WebActiveStepChangingEventArgsDetail":
                    return new IgbActiveStepChangingEventArgsDetail();
                    break;
                case "ChatDraftMessage":
                case "WebChatDraftMessage":
                    return new IgbChatDraftMessage();
                    break;
                case "ChatMessage":
                case "WebChatMessage":
                    return new IgbChatMessage();
                    break;
                case "ChatMessageAttachment":
                case "WebChatMessageAttachment":
                    return new IgbChatMessageAttachment();
                    break;
                case "ChatMessageAttachmentEventArgs":
                case "WebChatMessageAttachmentEventArgs":
                    return new IgbChatMessageAttachmentEventArgs();
                    break;
                case "ChatMessageEventArgs":
                case "WebChatMessageEventArgs":
                    return new IgbChatMessageEventArgs();
                    break;
                case "ChatMessageReaction":
                case "WebChatMessageReaction":
                    return new IgbChatMessageReaction();
                    break;
                case "ChatMessageReactionEventArgs":
                case "WebChatMessageReactionEventArgs":
                    return new IgbChatMessageReactionEventArgs();
                    break;
                case "CheckboxChangeEventArgs":
                case "WebCheckboxChangeEventArgs":
                    return new IgbCheckboxChangeEventArgs();
                    break;
                case "CheckboxChangeEventArgsDetail":
                case "WebCheckboxChangeEventArgsDetail":
                    return new IgbCheckboxChangeEventArgsDetail();
                    break;
                case "ComboChangeEventArgs":
                case "WebComboChangeEventArgs":
                    return new IgbComboChangeEventArgs();
                    break;
                case "ComboChangeEventArgsDetail":
                case "WebComboChangeEventArgsDetail":
                    return new IgbComboChangeEventArgsDetail();
                    break;
                case "ComponentBoolValueChangedEventArgs":
                case "WebComponentBoolValueChangedEventArgs":
                    return new IgbComponentBoolValueChangedEventArgs();
                    break;
                case "ComponentDateValueChangedEventArgs":
                case "WebComponentDateValueChangedEventArgs":
                    return new IgbComponentDateValueChangedEventArgs();
                    break;
                case "ComponentValueChangedEventArgs":
                case "WebComponentValueChangedEventArgs":
                    return new IgbComponentValueChangedEventArgs();
                    break;
                case "DateRangeValueDetail":
                case "WebDateRangeValueDetail":
                    return new IgbDateRangeValueDetail();
                    break;
                case "DateRangeValueEventArgs":
                case "WebDateRangeValueEventArgs":
                    return new IgbDateRangeValueEventArgs();
                    break;
                case "DropdownItemComponentEventArgs":
                case "WebDropdownItemComponentEventArgs":
                    return new IgbDropdownItemComponentEventArgs();
                    break;
                case "ExpansionPanelComponentEventArgs":
                case "WebExpansionPanelComponentEventArgs":
                    return new IgbExpansionPanelComponentEventArgs();
                    break;
                case "HighlightNavigation":
                case "WebHighlightNavigation":
                    return new IgbHighlightNavigation();
                    break;
                case "IconMeta":
                case "WebIconMeta":
                    return new IgbIconMeta();
                    break;
                case "NumberEventArgs":
                case "WebNumberEventArgs":
                    return new IgbNumberEventArgs();
                    break;
                case "RadioChangeEventArgs":
                case "WebRadioChangeEventArgs":
                    return new IgbRadioChangeEventArgs();
                    break;
                case "RadioChangeEventArgsDetail":
                case "WebRadioChangeEventArgsDetail":
                    return new IgbRadioChangeEventArgsDetail();
                    break;
                case "RangeSliderValue":
                case "WebRangeSliderValue":
                    return new IgbRangeSliderValue();
                    break;
                case "SelectItemComponentEventArgs":
                case "WebSelectItemComponentEventArgs":
                    return new IgbSelectItemComponentEventArgs();
                    break;
                case "SplitterResizeEventArgs":
                case "WebSplitterResizeEventArgs":
                    return new IgbSplitterResizeEventArgs();
                    break;
                case "SplitterResizeEventArgsDetail":
                case "WebSplitterResizeEventArgsDetail":
                    return new IgbSplitterResizeEventArgsDetail();
                    break;
                case "TabComponentEventArgs":
                case "WebTabComponentEventArgs":
                    return new IgbTabComponentEventArgs();
                    break;
                case "TileChangeStateEventArgs":
                case "WebTileChangeStateEventArgs":
                    return new IgbTileChangeStateEventArgs();
                    break;
                case "TileChangeStateEventArgsDetail":
                case "WebTileChangeStateEventArgsDetail":
                    return new IgbTileChangeStateEventArgsDetail();
                    break;
                case "TileComponentEventArgs":
                case "WebTileComponentEventArgs":
                    return new IgbTileComponentEventArgs();
                    break;
                case "TreeItemComponentEventArgs":
                case "WebTreeItemComponentEventArgs":
                    return new IgbTreeItemComponentEventArgs();
                    break;
                case "TreeSelectionEventArgs":
                case "WebTreeSelectionEventArgs":
                    return new IgbTreeSelectionEventArgs();
                    break;
                case "TreeSelectionEventArgsDetail":
                case "WebTreeSelectionEventArgsDetail":
                    return new IgbTreeSelectionEventArgsDetail();
                    break;

                    //@@MarshalByValueEnd
            }
            return null;
        }

    }
}
