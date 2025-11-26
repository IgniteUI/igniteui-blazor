import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComboChangeEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebComboChangeEventArgsDetailDescription, 'WebComboChangeEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComboChangeEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComboChangeEventArgsDetail";
	constructor() {
		super();
	}
	private _newValueRef: string = null;
	get newValueRef(): string {
		return this._newValueRef;
	}
	set newValueRef(value: string) {
		this._newValueRef = value;
		this.markDirty("NewValueRef");
	}
	private _itemsRef: string = null;
	get itemsRef(): string {
		return this._itemsRef;
	}
	set itemsRef(value: string) {
		this._itemsRef = value;
		this.markDirty("ItemsRef");
	}
	private _changeType: string = null;
	get changeType(): string {
		return this._changeType;
	}
	set changeType(value: string) {
		this._changeType = value;
		this.markDirty("ChangeType");
	}
}


