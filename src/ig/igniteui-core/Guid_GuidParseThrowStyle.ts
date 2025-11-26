import { Enum, ValueType, markEnum, Type } from "./type";

/**
 * @hidden 
 */
export const enum Guid_GuidParseThrowStyle {
	None = 0,
	All = 1,
	AllButOverflow = 2
}

/**
 * @hidden 
 */
export let Guid_GuidParseThrowStyle_$type = markEnum('Guid_GuidParseThrowStyle', 'None,0|All,1|AllButOverflow,2');


