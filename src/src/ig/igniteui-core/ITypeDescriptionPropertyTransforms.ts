import { Base, Type } from "./type";
import { TypeDescriptionPlatform } from "./TypeDescriptionPlatform";
import { DescriptionTreeAction } from "./DescriptionTreeAction";

/**
 * @hidden 
 */
export interface ITypeDescriptionPropertyTransforms { 
	transform(platform: TypeDescriptionPlatform, propertyValue: any, action: DescriptionTreeAction): any;
}

/**
 * @hidden 
 */
export let ITypeDescriptionPropertyTransforms_$type = new Type(null, 'ITypeDescriptionPropertyTransforms');


