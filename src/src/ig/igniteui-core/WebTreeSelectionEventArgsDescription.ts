import { Description } from "./Description";
import { WebTreeSelectionEventArgsDetailDescription } from "./WebTreeSelectionEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTreeSelectionEventArgsDescription extends Description {
	static $t: Type = markType(WebTreeSelectionEventArgsDescription, 'WebTreeSelectionEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTreeSelectionEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TreeSelectionEventArgs";
	constructor() {
		super();
	}
	private _detail: WebTreeSelectionEventArgsDetailDescription = null;
	get detail(): WebTreeSelectionEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebTreeSelectionEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


