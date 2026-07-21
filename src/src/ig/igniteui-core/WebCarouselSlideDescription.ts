import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCarouselSlideDescription extends Description {
	static $t: Type = markType(WebCarouselSlideDescription, 'WebCarouselSlideDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCarouselSlide";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _active: boolean = false;
	get active(): boolean {
		return this._active;
	}
	set active(value: boolean) {
		this._active = value;
		this.markDirty("Active");
	}
}


