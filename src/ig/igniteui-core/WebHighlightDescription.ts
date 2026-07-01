import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebHighlightDescription extends Description {
	static $t: Type = markType(WebHighlightDescription, 'WebHighlightDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebHighlight";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _caseSensitive: boolean = false;
	get caseSensitive(): boolean {
		return this._caseSensitive;
	}
	set caseSensitive(value: boolean) {
		this._caseSensitive = value;
		this.markDirty("CaseSensitive");
	}
	private _searchText: string = null;
	get searchText(): string {
		return this._searchText;
	}
	set searchText(value: string) {
		this._searchText = value;
		this.markDirty("SearchText");
	}
}


