import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden
 */
export class WebVirtualScrollStateChangeEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebVirtualScrollStateChangeEventArgsDetailDescription, 'WebVirtualScrollStateChangeEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebVirtualScrollStateChangeEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "VirtualScrollStateChangeEventArgsDetail";
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
	private _endIndex: number = 0;
	get endIndex(): number {
		return this._endIndex;
	}
	set endIndex(value: number) {
		this._endIndex = value;
		this.markDirty("EndIndex");
	}
	private _viewportSize: number = 0;
	get viewportSize(): number {
		return this._viewportSize;
	}
	set viewportSize(value: number) {
		this._viewportSize = value;
		this.markDirty("ViewportSize");
	}
	private _totalSize: number = 0;
	get totalSize(): number {
		return this._totalSize;
	}
	set totalSize(value: number) {
		this._totalSize = value;
		this.markDirty("TotalSize");
	}
}


