import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebCircularGradientDescription extends Description {
	static $t: Type = markType(WebCircularGradientDescription, 'WebCircularGradientDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebCircularGradient";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _offset: string = null;
	get offset(): string {
		return this._offset;
	}
	set offset(value: string) {
		this._offset = value;
		this.markDirty("Offset");
	}
	private _color: string = null;
	get color(): string {
		return this._color;
	}
	set color(value: string) {
		this._color = value;
		this.markDirty("Color");
	}
	private _opacity: number = 0;
	get opacity(): number {
		return this._opacity;
	}
	set opacity(value: number) {
		this._opacity = value;
		this.markDirty("Opacity");
	}
}


