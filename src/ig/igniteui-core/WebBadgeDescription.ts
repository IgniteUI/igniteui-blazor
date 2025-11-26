import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebBadgeDescription extends Description {
	static $t: Type = markType(WebBadgeDescription, 'WebBadgeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebBadge";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _variant: string = null;
	get variant(): string {
		return this._variant;
	}
	set variant(value: string) {
		this._variant = value;
		this.markDirty("Variant");
	}
	private _outlined: boolean = false;
	get outlined(): boolean {
		return this._outlined;
	}
	set outlined(value: boolean) {
		this._outlined = value;
		this.markDirty("Outlined");
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


