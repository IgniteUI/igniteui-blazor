import { WebBaseComboBoxDescription } from "./WebBaseComboBoxDescription";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebColorPickerDescription extends WebBaseComboBoxDescription {
	static $t: Type = markType(WebColorPickerDescription, 'WebColorPickerDescription', (<any>WebBaseComboBoxDescription).$type);
	protected get_type(): string {
		return "WebColorPicker";
	}
	constructor() {
		super();
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _format: string = null;
	get format(): string {
		return this._format;
	}
	set format(value: string) {
		this._format = value;
		this.markDirty("Format");
	}
	private _hideFormats: boolean = false;
	get hideFormats(): boolean {
		return this._hideFormats;
	}
	set hideFormats(value: boolean) {
		this._hideFormats = value;
		this.markDirty("HideFormats");
	}
	private _showAlpha: boolean = false;
	get showAlpha(): boolean {
		return this._showAlpha;
	}
	set showAlpha(value: boolean) {
		this._showAlpha = value;
		this.markDirty("ShowAlpha");
	}
	private _mode: string = null;
	get mode(): string {
		return this._mode;
	}
	set mode(value: string) {
		this._mode = value;
		this.markDirty("Mode");
	}
	private _swatches: string[] = null;
	get swatches(): string[] {
		return this._swatches;
	}
	set swatches(value: string[]) {
		this._swatches = value;
		this.markDirty("Swatches");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _required: boolean = false;
	get required(): boolean {
		return this._required;
	}
	set required(value: boolean) {
		this._required = value;
		this.markDirty("Required");
	}
	private _defaultValue: any = null;
	get defaultValue(): any {
		return this._defaultValue;
	}
	set defaultValue(value: any) {
		this._defaultValue = value;
		this.markDirty("DefaultValue");
	}
	private _invalid: boolean = false;
	get invalid(): boolean {
		return this._invalid;
	}
	set invalid(value: boolean) {
		this._invalid = value;
		this.markDirty("Invalid");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
	private _input: string = null;
	get inputRef(): string {
		return this._input;
	}
	set inputRef(value: string) {
		this._input = value;
		this.markDirty("InputRef");
	}
	private _opening: string = null;
	get openingRef(): string {
		return this._opening;
	}
	set openingRef(value: string) {
		this._opening = value;
		this.markDirty("OpeningRef");
	}
	private _opened: string = null;
	get openedRef(): string {
		return this._opened;
	}
	set openedRef(value: string) {
		this._opened = value;
		this.markDirty("OpenedRef");
	}
	private _closing: string = null;
	get closingRef(): string {
		return this._closing;
	}
	set closingRef(value: string) {
		this._closing = value;
		this.markDirty("ClosingRef");
	}
	private _closed: string = null;
	get closedRef(): string {
		return this._closed;
	}
	set closedRef(value: string) {
		this._closed = value;
		this.markDirty("ClosedRef");
	}
}


