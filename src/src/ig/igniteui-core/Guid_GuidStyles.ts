import { Enum, ValueType, markEnum, Type } from "./type";

/**
 * @hidden 
 */
export const enum Guid_GuidStyles {
	AllowBraces = 2,
	AllowDashes = 4,
	AllowHexPrefix = 8,
	AllowParenthesis = 1,
	Any = 15,
	BraceFormat = 96,
	DigitFormat = 64,
	HexFormat = 160,
	None = 0,
	NumberFormat = 0,
	ParenthesisFormat = 80,
	RequireBraces = 32,
	RequireDashes = 64,
	RequireHexPrefix = 128,
	RequireParenthesis = 16
}

/**
 * @hidden 
 */
export let Guid_GuidStyles_$type = markEnum('Guid_GuidStyles', 'AllowBraces,2|AllowDashes,4|AllowHexPrefix,8|AllowParenthesis,1|Any,15|BraceFormat,96|DigitFormat,64|HexFormat,160|None,0|NumberFormat,0|ParenthesisFormat,80|RequireBraces,32|RequireDashes,64|RequireHexPrefix,128|RequireParenthesis,16');


