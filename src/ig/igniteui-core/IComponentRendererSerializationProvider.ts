import { Base, Type } from "./type";
import { DescriptionSerializerBuilder } from "./DescriptionSerializerBuilder";

/**
 * @hidden 
 */
export interface IComponentRendererSerializationProvider { 
	canSerializeRef(key: string, v: any): boolean;
serializeRef(ret: DescriptionSerializerBuilder, key: string, v: any): void;
}

/**
 * @hidden 
 */
export let IComponentRendererSerializationProvider_$type = new Type(null, 'IComponentRendererSerializationProvider');


