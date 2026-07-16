import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebRatingDescription extends Description {
	static $t: Type = markType(WebRatingDescription, 'WebRatingDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRating";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _max: number = 0;
	get max(): number {
		return this._max;
	}
	set max(value: number) {
		this._max = value;
		this.markDirty("Max");
	}
	private _step: number = 0;
	get step(): number {
		return this._step;
	}
	set step(value: number) {
		this._step = value;
		this.markDirty("Step");
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _valueFormat: string = null;
	get valueFormat(): string {
		return this._valueFormat;
	}
	set valueFormat(value: string) {
		this._valueFormat = value;
		this.markDirty("ValueFormat");
	}
	private _value: number = 0;
	get value(): number {
		return this._value;
	}
	set value(value: number) {
		this._value = value;
		this.markDirty("Value");
	}
	private _hoverPreview: boolean = false;
	get hoverPreview(): boolean {
		return this._hoverPreview;
	}
	set hoverPreview(value: boolean) {
		this._hoverPreview = value;
		this.markDirty("HoverPreview");
	}
	private _readOnly: boolean = false;
	get readOnly(): boolean {
		return this._readOnly;
	}
	set readOnly(value: boolean) {
		this._readOnly = value;
		this.markDirty("ReadOnly");
	}
	private _single: boolean = false;
	get single(): boolean {
		return this._single;
	}
	set single(value: boolean) {
		this._single = value;
		this.markDirty("Single");
	}
	private _allowReset: boolean = false;
	get allowReset(): boolean {
		return this._allowReset;
	}
	set allowReset(value: boolean) {
		this._allowReset = value;
		this.markDirty("AllowReset");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
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
	private _hover: string = null;
	get hoverRef(): string {
		return this._hover;
	}
	set hoverRef(value: string) {
		this._hover = value;
		this.markDirty("HoverRef");
	}
}


