import { Enum, ValueType, markEnum, Type } from "./type";

/**
 * @hidden 
 */
export const enum DescriptionTreeActionType {
	UpdateProperty = 0,
	ResetProperty = 1,
	InsertItem = 2,
	ReplaceItem = 3,
	RemoveItem = 4,
	ClearItems = 5
}

/**
 * @hidden 
 */
export let DescriptionTreeActionType_$type = markEnum('DescriptionTreeActionType', 'UpdateProperty,0|ResetProperty,1|InsertItem,2|ReplaceItem,3|RemoveItem,4|ClearItems,5');


