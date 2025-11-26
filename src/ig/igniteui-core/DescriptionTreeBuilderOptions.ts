import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DescriptionTreeBuilderOptions extends Base {
	static $t: Type = markType(DescriptionTreeBuilderOptions, 'DescriptionTreeBuilderOptions');
	private _preserveKeyOrder: boolean = false;
	get preserveKeyOrder(): boolean {
		return this._preserveKeyOrder;
	}
	set preserveKeyOrder(value: boolean) {
		this._preserveKeyOrder = value;
	}
	private _checkModifiedFlag: boolean = false;
	get checkModifiedFlag(): boolean {
		return this._checkModifiedFlag;
	}
	set checkModifiedFlag(value: boolean) {
		this._checkModifiedFlag = value;
	}
	private _clearModifiedFlag: boolean = false;
	get clearModifiedFlag(): boolean {
		return this._clearModifiedFlag;
	}
	set clearModifiedFlag(value: boolean) {
		this._clearModifiedFlag = value;
	}
	private _isModified: boolean = false;
	get isModified(): boolean {
		return this._isModified;
	}
	set isModified(value: boolean) {
		this._isModified = value;
	}
}


