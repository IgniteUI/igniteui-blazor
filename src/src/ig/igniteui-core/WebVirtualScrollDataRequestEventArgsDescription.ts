import { Description } from "./Description";
import { WebVirtualScrollDataRequestEventArgsDetailDescription } from "./WebVirtualScrollDataRequestEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden
 */
export class WebVirtualScrollDataRequestEventArgsDescription extends Description {
	static $t: Type = markType(WebVirtualScrollDataRequestEventArgsDescription, 'WebVirtualScrollDataRequestEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebVirtualScrollDataRequestEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "VirtualScrollDataRequestEventArgs";
	constructor() {
		super();
	}
	private _detail: WebVirtualScrollDataRequestEventArgsDetailDescription = null;
	get detail(): WebVirtualScrollDataRequestEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebVirtualScrollDataRequestEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


