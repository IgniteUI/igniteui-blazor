import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSplitterResizeEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebSplitterResizeEventArgsDetailDescription, 'WebSplitterResizeEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSplitterResizeEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "SplitterResizeEventArgsDetail";
	constructor() {
		super();
	}
	private _startPanelSize: number = 0;
	get startPanelSize(): number {
		return this._startPanelSize;
	}
	set startPanelSize(value: number) {
		this._startPanelSize = value;
		this.markDirty("StartPanelSize");
	}
	private _endPanelSize: number = 0;
	get endPanelSize(): number {
		return this._endPanelSize;
	}
	set endPanelSize(value: number) {
		this._endPanelSize = value;
		this.markDirty("EndPanelSize");
	}
	private _delta: number = 0;
	get delta(): number {
		return this._delta;
	}
	set delta(value: number) {
		this._delta = value;
		this.markDirty("Delta");
	}
}


