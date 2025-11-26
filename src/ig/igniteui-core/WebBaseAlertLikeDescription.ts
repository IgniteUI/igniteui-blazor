import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebBaseAlertLikeDescription extends Description {
	static $t: Type = markType(WebBaseAlertLikeDescription, 'WebBaseAlertLikeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBaseAlertLike";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
	private _displayTime: number = 0;
	get displayTime(): number {
		return this._displayTime;
	}
	set displayTime(value: number) {
		this._displayTime = value;
		this.markDirty("DisplayTime");
	}
	private _keepOpen: boolean = false;
	get keepOpen(): boolean {
		return this._keepOpen;
	}
	set keepOpen(value: boolean) {
		this._keepOpen = value;
		this.markDirty("KeepOpen");
	}
	private _position: string = null;
	get position(): string {
		return this._position;
	}
	set position(value: string) {
		this._position = value;
		this.markDirty("Position");
	}
}


