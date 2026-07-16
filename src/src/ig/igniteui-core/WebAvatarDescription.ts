import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebAvatarDescription extends Description {
	static $t: Type = markType(WebAvatarDescription, 'WebAvatarDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebAvatar";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _src: string = null;
	get src(): string {
		return this._src;
	}
	set src(value: string) {
		this._src = value;
		this.markDirty("Src");
	}
	private _alt: string = null;
	get alt(): string {
		return this._alt;
	}
	set alt(value: string) {
		this._alt = value;
		this.markDirty("Alt");
	}
	private _initials: string = null;
	get initials(): string {
		return this._initials;
	}
	set initials(value: string) {
		this._initials = value;
		this.markDirty("Initials");
	}
	private _shape: string = null;
	get shape(): string {
		return this._shape;
	}
	set shape(value: string) {
		this._shape = value;
		this.markDirty("Shape");
	}
}


