import { IFormatProvider, IFormatProvider_$type, Type } from "./type";

/**
 * @hidden 
 */
export interface IFormattable { 
	toString(format: string, formatProvider: IFormatProvider): string;
}

/**
 * @hidden 
 */
export let IFormattable_$type = new Type(null, 'IFormattable');


