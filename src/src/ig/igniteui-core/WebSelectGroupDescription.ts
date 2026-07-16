import { Description } from "./Description";
import { WebSelectItemDescription } from "./WebSelectItemDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSelectGroupDescription extends Description {
	static $t: Type = markType(WebSelectGroupDescription, 'WebSelectGroupDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSelectGroup";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _items: WebSelectItemDescription[] = null;
	get items(): WebSelectItemDescription[] {
		return this._items;
	}
	set items(value: WebSelectItemDescription[]) {
		this._items = value;
		this.markDirty("Items");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
}


