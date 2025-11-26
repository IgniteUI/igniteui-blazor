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
case "TabComponentEventArgs":
case "WebTabComponentEventArgs":
                return true;
case "TabHeaderElement":
case "WebTabHeaderElement":
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
case "TabComponentEventArgs":
            case "WebTabComponentEventArgs":
                return new IgbTabComponentEventArgs();
            break;
case "TabHeaderElement":
            case "WebTabHeaderElement":
                return new IgbTabHeaderElement();
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