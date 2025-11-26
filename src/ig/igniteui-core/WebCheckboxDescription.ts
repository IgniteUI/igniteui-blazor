import { WebCheckboxBaseDescription } from "./WebCheckboxBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCheckboxDescription extends WebCheckboxBaseDescription {
	static $t: Type = markType(WebCheckboxDescription, 'WebCheckboxDescription', (<any>WebCheckboxBaseDescription).$type);
	protected get_type(): string {
		return "WebCheckbox";
	}
	constructor() {
		super();
	}
	private _indeterminate: boolean = false;
	get indeterminate(): boolean {
		return this._indeterminate;
	}
	set indeterminate(value: boolean) {
		this._indeterminate = value;
		this.markDirty("Indeterminate");
	}
}


