import { Base, Number_$type, Type, markType } from "./type";
import { HashSet$1 } from "./HashSet$1";
import { List$1 } from "./List$1";
import { DescriptionTreeAction } from "./DescriptionTreeAction";

/**
 * @hidden 
 */
export class DiffApplyInfo extends Base {
	static $t: Type = markType(DiffApplyInfo, 'DiffApplyInfo');
	constructor() {
		super();
		this.removedIds = new HashSet$1<number>(Number_$type, 0);
		this.newPropertyValues = new List$1<DescriptionTreeAction>((<any>DescriptionTreeAction).$type, 0);
		this.newCollectionContent = new List$1<DescriptionTreeAction>((<any>DescriptionTreeAction).$type, 0);
		this.newRootContent = new List$1<DescriptionTreeAction>((<any>DescriptionTreeAction).$type, 0);
	}
	private _removedIds: HashSet$1<number> = null;
	get removedIds(): HashSet$1<number> {
		return this._removedIds;
	}
	set removedIds(value: HashSet$1<number>) {
		this._removedIds = value;
	}
	private _newPropertyValues: List$1<DescriptionTreeAction> = null;
	get newPropertyValues(): List$1<DescriptionTreeAction> {
		return this._newPropertyValues;
	}
	set newPropertyValues(value: List$1<DescriptionTreeAction>) {
		this._newPropertyValues = value;
	}
	private _newCollectionContent: List$1<DescriptionTreeAction> = null;
	get newCollectionContent(): List$1<DescriptionTreeAction> {
		return this._newCollectionContent;
	}
	set newCollectionContent(value: List$1<DescriptionTreeAction>) {
		this._newCollectionContent = value;
	}
	private _newRootContent: List$1<DescriptionTreeAction> = null;
	get newRootContent(): List$1<DescriptionTreeAction> {
		return this._newRootContent;
	}
	set newRootContent(value: List$1<DescriptionTreeAction>) {
		this._newRootContent = value;
	}
}


