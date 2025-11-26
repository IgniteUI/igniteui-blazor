import { WebBaseComboBoxLikeDescription } from "./WebBaseComboBoxLikeDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSelectDescription extends WebBaseComboBoxLikeDescription {
	static $t: Type = markType(WebSelectDescription, 'WebSelectDescription', (<any>WebBaseComboBoxLikeDescription).$type);
	protected get_type(): string {
		return "WebSelect";
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
	private _outlined: boolean = false;
	get outlined(): boolean {
		return this._outlined;
	}
	set outlined(value: boolean) {
		this._outlined = value;
		this.markDirty("Outlined");
	}
	private _autofocus: boolean = false;
	get autofocus(): boolean {
		return this._autofocus;
	}
	set autofocus(value: boolean) {
		this._autofocus = value;
		this.markDirty("Autofocus");
	}
	private _distance: number = 0;
	get distance(): number {
		return this._distance;
	}
	set distance(value: number) {
		this._distance = value;
		this.markDirty("Distance");
	}
	private _label: string = null;
	get label(): string {
		return this._label;
	}
	set label(value: string) {
		this._label = value;
		this.markDirty("Label");
	}
	private _placeholder: string = null;
	get placeholder(): string {
		return this._placeholder;
	}
	set placeholder(value: string) {
		this._placeholder = value;
		this.markDirty("Placeholder");
	}
	private _placement: string = null;
	get placement(): string {
		return this._placement;
	}
	set placement(value: string) {
		this._placement = value;
		this.markDirty("Placement");
	}
	private _scrollStrategy: string = null;
	get scrollStrategy(): string {
		return this._scrollStrategy;
	}
	set scrollStrategy(value: string) {
		this._scrollStrategy = value;
		this.markDirty("ScrollStrategy");
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
	private _focus: string = null;
	get focusRef(): string {
		return this._focus;
	}
	set focusRef(value: string) {
		this._focus = value;
		this.markDirty("FocusRef");
	}
	private _blur: string = null;
	get blurRef(): string {
		return this._blur;
	}
	set blurRef(value: string) {
		this._blur = value;
		this.markDirty("BlurRef");
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


