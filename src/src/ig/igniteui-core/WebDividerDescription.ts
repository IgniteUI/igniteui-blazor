import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebDividerDescription extends Description {
	static $t: Type = markType(WebDividerDescription, 'WebDividerDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebDivider";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _vertical: boolean = false;
	get vertical(): boolean {
		return this._vertical;
	}
	set vertical(value: boolean) {
		this._vertical = value;
		this.markDirty("Vertical");
	}
	private _middle: boolean = false;
	get middle(): boolean {
		return this._middle;
	}
	set middle(value: boolean) {
		this._middle = value;
		this.markDirty("Middle");
	}
	private _lineType: string = null;
	get lineType(): string {
		return this._lineType;
	}
	set lineType(value: string) {
		this._lineType = value;
		this.markDirty("LineType");
	}
}


