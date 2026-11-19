import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebComboSearchInputEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebComboSearchInputEventArgsDetailDescription, 'WebComboSearchInputEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComboSearchInputEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComboSearchInputEventArgsDetail";
	constructor() {
		super();
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


