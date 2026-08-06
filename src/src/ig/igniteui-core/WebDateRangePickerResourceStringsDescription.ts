import { WebCalendarResourceStringsDescription } from "./WebCalendarResourceStringsDescription";
import { Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDateRangePickerResourceStringsDescription extends WebCalendarResourceStringsDescription {
	static $t: Type = markType(WebDateRangePickerResourceStringsDescription, 'WebDateRangePickerResourceStringsDescription', (<any>WebCalendarResourceStringsDescription).$type);
	protected get_type(): string {
		return "WebDateRangePickerResourceStrings";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _separator: string = null!;
	get separator(): string {
		return this._separator;
	}
	set separator(value: string) {
		this._separator = value;
		this.markDirty("Separator");
	}
	private _doneButton: string = null!;
	get doneButton(): string {
		return this._doneButton;
	}
	set doneButton(value: string) {
		this._doneButton = value;
		this.markDirty("DoneButton");
	}
	private _cancelButton: string = null!;
	get cancelButton(): string {
		return this._cancelButton;
	}
	set cancelButton(value: string) {
		this._cancelButton = value;
		this.markDirty("CancelButton");
	}
}


