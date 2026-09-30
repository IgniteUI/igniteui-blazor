import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebBreadcrumbsDescription extends Description {
	static $t: Type = markType(WebBreadcrumbsDescription, 'WebBreadcrumbsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBreadcrumbs";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _separator: string = null;
	get separator(): string {
		return this._separator;
	}
	set separator(value: string) {
		this._separator = value;
		this.markDirty("Separator");
	}
}


