import { FormatSpecifierDescription } from "./FormatSpecifierDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class NumberFormatSpecifierDescription extends FormatSpecifierDescription {
	static $t: Type = markType(NumberFormatSpecifierDescription, 'NumberFormatSpecifierDescription', (<any>FormatSpecifierDescription).$type);
	protected get_type(): string {
		return "NumberFormatSpecifier";
	}
	private static __marshalByValue1: boolean = true;
	private static __marshalByValueAlias1: string = "NumberFormatSpecifier";
	constructor() {
		super();
	}
	private _locale: string = null;
	get locale(): string {
		return this._locale;
	}
	set locale(value: string) {
		this._locale = value;
		this.markDirty("Locale");
	}
	private _compactDisplay: string = null;
	get compactDisplay(): string {
		return this._compactDisplay;
	}
	set compactDisplay(value: string) {
		this._compactDisplay = value;
		this.markDirty("CompactDisplay");
	}
	private _currency: string = null;
	get currency(): string {
		return this._currency;
	}
	set currency(value: string) {
		this._currency = value;
		this.markDirty("Currency");
	}
	private _currencyDisplay: string = null;
	get currencyDisplay(): string {
		return this._currencyDisplay;
	}
	set currencyDisplay(value: string) {
		this._currencyDisplay = value;
		this.markDirty("CurrencyDisplay");
	}
	private _currencySign: string = null;
	get currencySign(): string {
		return this._currencySign;
	}
	set currencySign(value: string) {
		this._currencySign = value;
		this.markDirty("CurrencySign");
	}
	private _currencyCode: string = null;
	get currencyCode(): string {
		return this._currencyCode;
	}
	set currencyCode(value: string) {
		this._currencyCode = value;
		this.markDirty("CurrencyCode");
	}
	private _localeMatcher: string = null;
	get localeMatcher(): string {
		return this._localeMatcher;
	}
	set localeMatcher(value: string) {
		this._localeMatcher = value;
		this.markDirty("LocaleMatcher");
	}
	private _notation: string = null;
	get notation(): string {
		return this._notation;
	}
	set notation(value: string) {
		this._notation = value;
		this.markDirty("Notation");
	}
	private _numberingSystem: string = null;
	get numberingSystem(): string {
		return this._numberingSystem;
	}
	set numberingSystem(value: string) {
		this._numberingSystem = value;
		this.markDirty("NumberingSystem");
	}
	private _signDisplay: string = null;
	get signDisplay(): string {
		return this._signDisplay;
	}
	set signDisplay(value: string) {
		this._signDisplay = value;
		this.markDirty("SignDisplay");
	}
	private _style: string = null;
	get style(): string {
		return this._style;
	}
	set style(value: string) {
		this._style = value;
		this.markDirty("Style");
	}
	private _unit: string = null;
	get unit(): string {
		return this._unit;
	}
	set unit(value: string) {
		this._unit = value;
		this.markDirty("Unit");
	}
	private _unitDisplay: string = null;
	get unitDisplay(): string {
		return this._unitDisplay;
	}
	set unitDisplay(value: string) {
		this._unitDisplay = value;
		this.markDirty("UnitDisplay");
	}
	private _useGrouping: boolean = false;
	get useGrouping(): boolean {
		return this._useGrouping;
	}
	set useGrouping(value: boolean) {
		this._useGrouping = value;
		this.markDirty("UseGrouping");
	}
	private _minimumIntegerDigits: number = 0;
	get minimumIntegerDigits(): number {
		return this._minimumIntegerDigits;
	}
	set minimumIntegerDigits(value: number) {
		this._minimumIntegerDigits = value;
		this.markDirty("MinimumIntegerDigits");
	}
	private _minimumFractionDigits: number = 0;
	get minimumFractionDigits(): number {
		return this._minimumFractionDigits;
	}
	set minimumFractionDigits(value: number) {
		this._minimumFractionDigits = value;
		this.markDirty("MinimumFractionDigits");
	}
	private _maximumFractionDigits: number = 0;
	get maximumFractionDigits(): number {
		return this._maximumFractionDigits;
	}
	set maximumFractionDigits(value: number) {
		this._maximumFractionDigits = value;
		this.markDirty("MaximumFractionDigits");
	}
	private _minimumSignificantDigits: number = 0;
	get minimumSignificantDigits(): number {
		return this._minimumSignificantDigits;
	}
	set minimumSignificantDigits(value: number) {
		this._minimumSignificantDigits = value;
		this.markDirty("MinimumSignificantDigits");
	}
	private _maximumSignificantDigits: number = 0;
	get maximumSignificantDigits(): number {
		return this._maximumSignificantDigits;
	}
	set maximumSignificantDigits(value: number) {
		this._maximumSignificantDigits = value;
		this.markDirty("MaximumSignificantDigits");
	}
}


