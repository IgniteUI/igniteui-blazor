import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebBaseComboBoxLikeDescription extends Description {
	static $t: Type = markType(WebBaseComboBoxLikeDescription, 'WebBaseComboBoxLikeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBaseComboBoxLike";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _emitEvent: any = null;
	get emitEvent(): any {
		return this._emitEvent;
	}
	set emitEvent(value: any) {
		this._emitEvent = value;
		this.markDirty("EmitEvent");
	}
	private _keepOpenOnSelect: boolean = false;
	get keepOpenOnSelect(): boolean {
		return this._keepOpenOnSelect;
	}
	set keepOpenOnSelect(value: boolean) {
		this._keepOpenOnSelect = value;
		this.markDirty("KeepOpenOnSelect");
	}
	private _keepOpenOnOutsideClick: boolean = false;
	get keepOpenOnOutsideClick(): boolean {
		return this._keepOpenOnOutsideClick;
	}
	set keepOpenOnOutsideClick(value: boolean) {
		this._keepOpenOnOutsideClick = value;
		this.markDirty("KeepOpenOnOutsideClick");
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
}


