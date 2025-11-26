import { Base, Type } from "./type";

/**
 * @hidden 
 */
export interface IExecutionContext { 
	execute(callback: () => void): void;
enqueueAction(callback: () => void): void;
enqueueAnimationAction(callback: () => void): void;
executeDelayed(callback: () => void, delayMilliseconds: number): void;
getCurrentRelativeTime(): number;
}

/**
 * @hidden 
 */
export let IExecutionContext_$type = new Type(null, 'IExecutionContext');


