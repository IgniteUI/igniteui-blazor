import { Base, Type, markType } from "./type";
import { List$1 } from "./List$1";
import { TypeDescriptionPropretyTransformsMultipleSetsInfo } from "./TypeDescriptionPropretyTransformsMultipleSetsInfo";

/**
 * @hidden 
 */
export class TypeDescriptionPropretyTransformsMultipleSets extends Base {
	static $t: Type = markType(TypeDescriptionPropretyTransformsMultipleSets, 'TypeDescriptionPropretyTransformsMultipleSets');
	constructor() {
		super();
		this.actions = new List$1<TypeDescriptionPropretyTransformsMultipleSetsInfo>((<any>TypeDescriptionPropretyTransformsMultipleSetsInfo).$type, 0);
	}
	private _actions: List$1<TypeDescriptionPropretyTransformsMultipleSetsInfo> = null;
	get actions(): List$1<TypeDescriptionPropretyTransformsMultipleSetsInfo> {
		return this._actions;
	}
	set actions(value: List$1<TypeDescriptionPropretyTransformsMultipleSetsInfo>) {
		this._actions = value;
	}
}


