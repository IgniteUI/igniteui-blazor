import { Description } from "./Description";
import { WebFilteringOptionsDescription } from "./WebFilteringOptionsDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComboDescription extends Description {
	static $t: Type = markType(WebComboDescription, 'WebComboDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCombo";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _dataRef: string = null;
	get dataRef(): string {
		return this._dataRef;
	}
	set dataRef(value: string) {
		this._dataRef = value;
		this.markDirty("DataRef");
	}
	private _outlined: boolean = false;
	get outlined(): boolean {
		return this._outlined;
	}
	set outlined(value: boolean) {
		this._outlined = value;
		this.markDirty("Outlined");
	}
	private _singleSelect: boolean = false;
	get singleSelect(): boolean {
		return this._singleSelect;
	}
	set singleSelect(value: boolean) {
		this._singleSelect = value;
		this.markDirty("SingleSelect");
	}
	private _autofocus: boolean = false;
	get autofocus(): boolean {
		return this._autofocus;
	}
	set autofocus(value: boolean) {
		this._autofocus = value;
		this.markDirty("Autofocus");
	}
	private _autofocusList: boolean = false;
	get autofocusList(): boolean {
		return this._autofocusList;
	}
	set autofocusList(value: boolean) {
		this._autofocusList = value;
		this.markDirty("AutofocusList");
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
	private _placeholderSearch: string = null;
	get placeholderSearch(): string {
		return this._placeholderSearch;
	}
	set placeholderSearch(value: string) {
		this._placeholderSearch = value;
		this.markDirty("PlaceholderSearch");
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
	private _valueKey: string = null;
	get valueKey(): string {
		return this._valueKey;
	}
	set valueKey(value: string) {
		this._valueKey = value;
		this.markDirty("ValueKey");
	}
	private _displayKey: string = null;
	get displayKey(): string {
		return this._displayKey;
	}
	set displayKey(value: string) {
		this._displayKey = value;
		this.markDirty("DisplayKey");
	}
	private _groupKey: string = null;
	get groupKey(): string {
		return this._groupKey;
	}
	set groupKey(value: string) {
		this._groupKey = value;
		this.markDirty("GroupKey");
	}
	private _groupSorting: string = null;
	get groupSorting(): string {
		return this._groupSorting;
	}
	set groupSorting(value: string) {
		this._groupSorting = value;
		this.markDirty("GroupSorting");
	}
	private _filteringOptions: WebFilteringOptionsDescription = null;
	get filteringOptions(): WebFilteringOptionsDescription {
		return this._filteringOptions;
	}
	set filteringOptions(value: WebFilteringOptionsDescription) {
		this._filteringOptions = value;
		this.markDirty("FilteringOptions");
	}
	private _caseSensitiveIcon: boolean = false;
	get caseSensitiveIcon(): boolean {
		return this._caseSensitiveIcon;
	}
	set caseSensitiveIcon(value: boolean) {
		this._caseSensitiveIcon = value;
		this.markDirty("CaseSensitiveIcon");
	}
	private _disableFiltering: boolean = false;
	get disableFiltering(): boolean {
		return this._disableFiltering;
	}
	set disableFiltering(value: boolean) {
		this._disableFiltering = value;
		this.markDirty("DisableFiltering");
	}
	private _value: any[] = null;
	get value(): any[] {
		return this._value;
	}
	set value(value: any[]) {
		this._value = value;
		this.markDirty("Value");
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
	private _itemTemplateRef: string = null;
	get itemTemplateRef(): string {
		return this._itemTemplateRef;
	}
	set itemTemplateRef(value: string) {
		this._itemTemplateRef = value;
		this.markDirty("ItemTemplateRef");
	}
	private _groupHeaderTemplateRef: string = null;
	get groupHeaderTemplateRef(): string {
		return this._groupHeaderTemplateRef;
	}
	set groupHeaderTemplateRef(value: string) {
		this._groupHeaderTemplateRef = value;
		this.markDirty("GroupHeaderTemplateRef");
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


