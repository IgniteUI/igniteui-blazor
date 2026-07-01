import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebThemeProviderDescription extends Description {
	static $t: Type = markType(WebThemeProviderDescription, 'WebThemeProviderDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebThemeProvider";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _theme: string = null;
	get theme(): string {
		return this._theme;
	}
	set theme(value: string) {
		this._theme = value;
		this.markDirty("Theme");
	}
	private _variant: string = null;
	get variant(): string {
		return this._variant;
	}
	set variant(value: string) {
		this._variant = value;
		this.markDirty("Variant");
	}
}


