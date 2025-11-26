import { Base, String_$type, Boolean_$type, Type, markType } from "./type";
import { List$1 } from "./List$1";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class Description extends Base {
	static $t: Type = markType(Description, 'Description');
	private _isDescriptionModified: boolean = false;
	isDescriptionModified(): boolean {
		return this._isDescriptionModified;
	}
	clearIsDescriptionModified(): void {
		this._isDescriptionModified = false;
	}
	private _isDirty: Dictionary$2<string, boolean> = new Dictionary$2<string, boolean>(String_$type, Boolean_$type, 0);
	private _keyOrder: List$1<string> = new List$1<string>(String_$type, 0);
	getDirtyKeysInOrder(): string[] {
		if (this._keyOrder.count == 0) {
			this._keyOrder.add("Type");
		}
		return this._keyOrder.toArray();
	}
	protected markDirty(propertyName: string): void {
		this._isDescriptionModified = true;
		if (!this._isDirty.containsKey(propertyName)) {
			if (this._keyOrder.count == 0) {
				this._keyOrder.add("Type");
			}
			this._keyOrder.add(propertyName);
		}
		this._isDirty.item(propertyName, true);
	}
	isDirty(propertyName: string): boolean {
		if (this._isDirty.containsKey(propertyName)) {
			return this._isDirty.item(propertyName);
		}
		return false;
	}
	private _name: string = null;
	get name(): string {
		return this._name;
	}
	set name(value: string) {
		this._name = value;
		this.markDirty("Name");
	}
}


