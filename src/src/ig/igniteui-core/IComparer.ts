import { Base, Type } from "./type";

/**
 * @hidden 
 */
export interface IComparer { 
	compareObject(x: any, y: any): number;
}

/**
 * @hidden 
 */
export let IComparer_$type = new Type(null, 'IComparer');


