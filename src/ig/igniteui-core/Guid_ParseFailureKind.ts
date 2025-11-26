import { Enum, ValueType, markEnum, Type } from "./type";

/**
 * @hidden 
 */
export const enum Guid_ParseFailureKind {
	None = 0,
	ArgumentNull = 1,
	Format = 2,
	FormatWithParameter = 3,
	NativeException = 4,
	FormatWithInnerException = 5
}

/**
 * @hidden 
 */
export let Guid_ParseFailureKind_$type = markEnum('Guid_ParseFailureKind', 'None,0|ArgumentNull,1|Format,2|FormatWithParameter,3|NativeException,4|FormatWithInnerException,5');


