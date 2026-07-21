import { Enum, ValueType, markEnum, Type } from "./type";

/**
 * Describes available modes for color interpolation.
 */
export enum InterpolationMode {
	/**
	 * Interpolation in RGB space.
	 */
	RGB = 0,
	/**
	 * Interpolation in HSV space.
	 */
	HSV = 1
}

/**
 * @hidden 
 */
export let InterpolationMode_$type = markEnum('InterpolationMode', 'RGB,0|HSV,1');


