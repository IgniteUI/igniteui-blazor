import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebVirtualScrollDataRequestEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebVirtualScrollDataRequestEventArgsDetailDescription, 'WebVirtualScrollDataRequestEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebVirtualScrollDataRequestEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "VirtualScrollDataRequestEventArgsDetail";
	constructor() {
		super();
	}
	private _startIndex: number = 0;
	get startIndex(): number {
		return this._startIndex;
	}
	set startIndex(value: number) {
		this._startIndex = value;
		this.markDirty("StartIndex");
	}
	private _count: number = 0;
	get count(): number {
		return this._count;
	}
	set count(value: number) {
		this._count = value;
		this.markDirty("Count");
	}
}


