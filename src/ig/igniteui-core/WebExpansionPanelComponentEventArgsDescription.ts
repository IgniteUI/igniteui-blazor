import { Description } from "./Description";
import { WebExpansionPanelDescription } from "./WebExpansionPanelDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebExpansionPanelComponentEventArgsDescription extends Description {
	static $t: Type = markType(WebExpansionPanelComponentEventArgsDescription, 'WebExpansionPanelComponentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebExpansionPanelComponentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ExpansionPanelComponentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebExpansionPanelDescription = null;
	get detail(): WebExpansionPanelDescription {
		return this._detail;
	}
	set detail(value: WebExpansionPanelDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


