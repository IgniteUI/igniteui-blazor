import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebFilteringOptionsDescription extends Description {
	static $t: Type = markType(WebFilteringOptionsDescription, 'WebFilteringOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebFilteringOptions";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _filterKey: string = null;
	get filterKey(): string {
		return this._filterKey;
	}
	set filterKey(value: string) {
		this._filterKey = value;
		this.markDirty("FilterKey");
	}
	private _caseSensitive: boolean = false;
	get caseSensitive(): boolean {
		return this._caseSensitive;
	}
	set caseSensitive(value: boolean) {
		this._caseSensitive = value;
		this.markDirty("CaseSensitive");
	}
	private _matchDiacritics: boolean = false;
	get matchDiacritics(): boolean {
		return this._matchDiacritics;
	}
	set matchDiacritics(value: boolean) {
		this._matchDiacritics = value;
		this.markDirty("MatchDiacritics");
	}
}


