import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebFocusOptionsDescription extends Description {
	static $t: Type = markType(WebFocusOptionsDescription, 'WebFocusOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebFocusOptions";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "FocusOptions";
	constructor() {
		super();
	}
	private _preventScroll: boolean = false;
	get preventScroll(): boolean {
		return this._preventScroll;
	}
	set preventScroll(value: boolean) {
		this._preventScroll = value;
		this.markDirty("PreventScroll");
	}
}


