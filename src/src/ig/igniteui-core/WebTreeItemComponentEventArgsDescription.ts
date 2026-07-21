import { Description } from "./Description";
import { WebTreeItemDescription } from "./WebTreeItemDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTreeItemComponentEventArgsDescription extends Description {
	static $t: Type = markType(WebTreeItemComponentEventArgsDescription, 'WebTreeItemComponentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTreeItemComponentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TreeItemComponentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebTreeItemDescription = null;
	get detail(): WebTreeItemDescription {
		return this._detail;
	}
	set detail(value: WebTreeItemDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


