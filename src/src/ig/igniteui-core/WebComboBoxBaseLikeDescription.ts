import { WebBaseComboBoxDescription } from "./WebBaseComboBoxDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebComboBoxBaseLikeDescription extends WebBaseComboBoxDescription {
	static $t: Type = markType(WebComboBoxBaseLikeDescription, 'WebComboBoxBaseLikeDescription', (<any>WebBaseComboBoxDescription).$type);
	protected get_type(): string {
		return "WebComboBoxBaseLike";
	}
	constructor() {
		super();
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
}


