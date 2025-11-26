import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebListItemDescription extends Description {
	static $t: Type = markType(WebListItemDescription, 'WebListItemDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebListItem";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _selected: boolean = false;
	get selected(): boolean {
		return this._selected;
	}
	set selected(value: boolean) {
		this._selected = value;
		this.markDirty("Selected");
	}
}


