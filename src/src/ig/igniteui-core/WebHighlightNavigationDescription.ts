import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebHighlightNavigationDescription extends Description {
	static $t: Type = markType(WebHighlightNavigationDescription, 'WebHighlightNavigationDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebHighlightNavigation";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "HighlightNavigation";
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


