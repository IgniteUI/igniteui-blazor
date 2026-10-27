import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebBaseComboBoxDescription extends Description {
	static $t: Type = markType(WebBaseComboBoxDescription, 'WebBaseComboBoxDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBaseComboBox";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
	private _scrollStrategy: string = null;
	get scrollStrategy(): string {
		return this._scrollStrategy;
	}
	set scrollStrategy(value: string) {
		this._scrollStrategy = value;
		this.markDirty("ScrollStrategy");
	}
}


