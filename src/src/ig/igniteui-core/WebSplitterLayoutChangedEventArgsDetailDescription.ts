import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebSplitterLayoutChangedEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebSplitterLayoutChangedEventArgsDetailDescription, 'WebSplitterLayoutChangedEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSplitterLayoutChangedEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "SplitterLayoutChangedEventArgsDetail";
	constructor() {
		super();
	}
	private _startSize: string = null;
	get startSize(): string {
		return this._startSize;
	}
	set startSize(value: string) {
		this._startSize = value;
		this.markDirty("StartSize");
	}
	private _endSize: string = null;
	get endSize(): string {
		return this._endSize;
	}
	set endSize(value: string) {
		this._endSize = value;
		this.markDirty("EndSize");
	}
	private _startCollapsed: boolean = false;
	get startCollapsed(): boolean {
		return this._startCollapsed;
	}
	set startCollapsed(value: boolean) {
		this._startCollapsed = value;
		this.markDirty("StartCollapsed");
	}
	private _endCollapsed: boolean = false;
	get endCollapsed(): boolean {
		return this._endCollapsed;
	}
	set endCollapsed(value: boolean) {
		this._endCollapsed = value;
		this.markDirty("EndCollapsed");
	}
}


