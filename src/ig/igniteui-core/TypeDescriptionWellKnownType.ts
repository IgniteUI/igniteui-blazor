import { Enum, ValueType, markEnum, Type } from "./type";

export enum TypeDescriptionWellKnownType {
	Void = 0,
	Number = 1,
	string1 = 2,
	Date = 3,
	Brush = 4,
	Color = 5,
	BrushCollection = 6,
	boolean1 = 7,
	ExportedType = 8,
	Collection = 9,
	Array = 10,
	Point = 11,
	Size = 12,
	IList = 13,
	Rect = 14,
	DataTemplate = 15,
	ColorCollection = 16,
	Unknown = 17,
	MethodRef = 18,
	EventRef = 19,
	DataRef = 20,
	TimeSpan = 21,
	TemplateRef = 22,
	DoubleCollection = 23,
	Pixel = 24,
	PixelPoint = 25,
	PixelRect = 26,
	PixelSize = 27
}

/**
 * @hidden 
 */
export let TypeDescriptionWellKnownType_$type = markEnum('TypeDescriptionWellKnownType', 'Void,0|Number,1|String:string1,2|Date,3|Brush,4|Color,5|BrushCollection,6|Boolean:boolean1,7|ExportedType,8|Collection,9|Array,10|Point,11|Size,12|IList,13|Rect,14|DataTemplate,15|ColorCollection,16|Unknown,17|MethodRef,18|EventRef,19|DataRef,20|TimeSpan,21|TemplateRef,22|DoubleCollection,23|Pixel,24|PixelPoint,25|PixelRect,26|PixelSize,27');


