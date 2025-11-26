import { Enum, ValueType, markEnum, Type } from "./type";

export enum NotifyCollectionChangedAction {
	Add = 0,
	Remove = 1,
	Replace = 2,
	Reset = 4
}

/**
 * @hidden 
 */
export let NotifyCollectionChangedAction_$type = markEnum('NotifyCollectionChangedAction', 'Add,0|Remove,1|Replace,2|Reset,4');


