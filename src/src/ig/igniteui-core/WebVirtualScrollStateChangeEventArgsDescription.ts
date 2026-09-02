import { Description } from "./Description";
import { WebVirtualScrollStateChangeEventArgsDetailDescription } from "./WebVirtualScrollStateChangeEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden
 */
export class WebVirtualScrollStateChangeEventArgsDescription extends Description {
	static $t: Type = markType(WebVirtualScrollStateChangeEventArgsDescription, 'WebVirtualScrollStateChangeEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebVirtualScrollStateChangeEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "VirtualScrollStateChangeEventArgs";
	constructor() {
		super();
	}
	private _detail: WebVirtualScrollStateChangeEventArgsDetailDescription = null;
	get detail(): WebVirtualScrollStateChangeEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebVirtualScrollStateChangeEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


