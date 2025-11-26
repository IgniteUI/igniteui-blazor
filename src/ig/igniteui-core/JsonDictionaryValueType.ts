import { Enum, ValueType, markEnum, Type } from "./type";

/**
 * These are currently for Infragistics internal use.
 */
export enum JsonDictionaryValueType {
	NumberValue = 0,
	BooleanValue = 1,
	StringValue = 2,
	NullValue = 3
}

/**
 * @hidden 
 */
export let JsonDictionaryValueType_$type = markEnum('JsonDictionaryValueType', 'NumberValue,0|BooleanValue,1|StringValue,2|NullValue,3');


