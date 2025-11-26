import { Enum, ValueType, markEnum, Type } from "./type";

export enum TypeDescriptionPlatform {
	WPF = 0,
	JQuery = 1,
	React = 2,
	Angular = 3,
	WebComponents = 4,
	XamarinForms = 5,
	XamarinAndroid = 6,
	XamariniOS = 7,
	WindowsForms = 8,
	UWP = 9,
	WinUI = 10,
	Unknown = 11,
	Blazor = 12,
	Kotlin = 13,
	Swift = 14
}

/**
 * @hidden 
 */
export let TypeDescriptionPlatform_$type = markEnum('TypeDescriptionPlatform', 'WPF,0|JQuery,1|React,2|Angular,3|WebComponents,4|XamarinForms,5|XamarinAndroid,6|XamariniOS,7|WindowsForms,8|UWP,9|WinUI,10|Unknown,11|Blazor,12|Kotlin,13|Swift,14');


