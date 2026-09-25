import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebBreadcrumbDescription extends Description {
	static $t: Type = markType(WebBreadcrumbDescription, 'WebBreadcrumbDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBreadcrumb";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _current: boolean = false;
	get current(): boolean {
		return this._current;
	}
	set current(value: boolean) {
		this._current = value;
		this.markDirty("Current");
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


