import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebProgressBaseDescription extends Description {
	static $t: Type = markType(WebProgressBaseDescription, 'WebProgressBaseDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebProgressBase";
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
	private _value: number = 0;
	get value(): number {
		return this._value;
	}
	set value(value: number) {
		this._value = value;
		this.markDirty("Value");
	}
	private _variant: string = null;
	get variant(): string {
		return this._variant;
	}
	set variant(value: string) {
		this._variant = value;
		this.markDirty("Variant");
	}
	private _animationDuration: number = 0;
	get animationDuration(): number {
		return this._animationDuration;
	}
	set animationDuration(value: number) {
		this._animationDuration = value;
		this.markDirty("AnimationDuration");
	}
	private _indeterminate: boolean = false;
	get indeterminate(): boolean {
		return this._indeterminate;
	}
	set indeterminate(value: boolean) {
		this._indeterminate = value;
		this.markDirty("Indeterminate");
	}
	private _hideLabel: boolean = false;
	get hideLabel(): boolean {
		return this._hideLabel;
	}
	set hideLabel(value: boolean) {
		this._hideLabel = value;
		this.markDirty("HideLabel");
	}
	private _labelFormat: string = null;
	get labelFormat(): string {
		return this._labelFormat;
	}
	set labelFormat(value: string) {
		this._labelFormat = value;
		this.markDirty("LabelFormat");
	}
}


