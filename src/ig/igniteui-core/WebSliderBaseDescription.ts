import { Description } from "./Description";
import { NumberFormatSpecifierDescription } from "./NumberFormatSpecifierDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSliderBaseDescription extends Description {
	static $t: Type = markType(WebSliderBaseDescription, 'WebSliderBaseDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSliderBase";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _min: number = 0;
	get min(): number {
		return this._min;
	}
	set min(value: number) {
		this._min = value;
		this.markDirty("Min");
	}
	private _max: number = 0;
	get max(): number {
		return this._max;
	}
	set max(value: number) {
		this._max = value;
		this.markDirty("Max");
	}
	private _lowerBound: number = 0;
	get lowerBound(): number {
		return this._lowerBound;
	}
	set lowerBound(value: number) {
		this._lowerBound = value;
		this.markDirty("LowerBound");
	}
	private _upperBound: number = 0;
	get upperBound(): number {
		return this._upperBound;
	}
	set upperBound(value: number) {
		this._upperBound = value;
		this.markDirty("UpperBound");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _discreteTrack: boolean = false;
	get discreteTrack(): boolean {
		return this._discreteTrack;
	}
	set discreteTrack(value: boolean) {
		this._discreteTrack = value;
		this.markDirty("DiscreteTrack");
	}
	private _hideTooltip: boolean = false;
	get hideTooltip(): boolean {
		return this._hideTooltip;
	}
	set hideTooltip(value: boolean) {
		this._hideTooltip = value;
		this.markDirty("HideTooltip");
	}
	private _step: number = 0;
	get step(): number {
		return this._step;
	}
	set step(value: number) {
		this._step = value;
		this.markDirty("Step");
	}
	private _primaryTicks: number = 0;
	get primaryTicks(): number {
		return this._primaryTicks;
	}
	set primaryTicks(value: number) {
		this._primaryTicks = value;
		this.markDirty("PrimaryTicks");
	}
	private _secondaryTicks: number = 0;
	get secondaryTicks(): number {
		return this._secondaryTicks;
	}
	set secondaryTicks(value: number) {
		this._secondaryTicks = value;
		this.markDirty("SecondaryTicks");
	}
	private _tickOrientation: string = null;
	get tickOrientation(): string {
		return this._tickOrientation;
	}
	set tickOrientation(value: string) {
		this._tickOrientation = value;
		this.markDirty("TickOrientation");
	}
	private _hidePrimaryLabels: boolean = false;
	get hidePrimaryLabels(): boolean {
		return this._hidePrimaryLabels;
	}
	set hidePrimaryLabels(value: boolean) {
		this._hidePrimaryLabels = value;
		this.markDirty("HidePrimaryLabels");
	}
	private _hideSecondaryLabels: boolean = false;
	get hideSecondaryLabels(): boolean {
		return this._hideSecondaryLabels;
	}
	set hideSecondaryLabels(value: boolean) {
		this._hideSecondaryLabels = value;
		this.markDirty("HideSecondaryLabels");
	}
	private _locale: string = null;
	get locale(): string {
		return this._locale;
	}
	set locale(value: string) {
		this._locale = value;
		this.markDirty("Locale");
	}
	private _valueFormat: string = null;
	get valueFormat(): string {
		return this._valueFormat;
	}
	set valueFormat(value: string) {
		this._valueFormat = value;
		this.markDirty("ValueFormat");
	}
	private _tickLabelRotation: string = null;
	get tickLabelRotation(): string {
		return this._tickLabelRotation;
	}
	set tickLabelRotation(value: string) {
		this._tickLabelRotation = value;
		this.markDirty("TickLabelRotation");
	}
	private _valueFormatOptions: NumberFormatSpecifierDescription = null;
	get valueFormatOptions(): NumberFormatSpecifierDescription {
		return this._valueFormatOptions;
	}
	set valueFormatOptions(value: NumberFormatSpecifierDescription) {
		this._valueFormatOptions = value;
		this.markDirty("ValueFormatOptions");
	}
}


