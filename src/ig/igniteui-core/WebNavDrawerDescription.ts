import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebNavDrawerDescription extends Description {
	static $t: Type = markType(WebNavDrawerDescription, 'WebNavDrawerDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebNavDrawer";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _position: string = null;
	get position(): string {
		return this._position;
	}
	set position(value: string) {
		this._position = value;
		this.markDirty("Position");
	}
	private _open: boolean = false;
	get open(): boolean {
		return this._open;
	}
	set open(value: boolean) {
		this._open = value;
		this.markDirty("Open");
	}
}


