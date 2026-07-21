import { Description } from "./Description";
import { WebTileDescription } from "./WebTileDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTileComponentEventArgsDescription extends Description {
	static $t: Type = markType(WebTileComponentEventArgsDescription, 'WebTileComponentEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTileComponentEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TileComponentEventArgs";
	constructor() {
		super();
	}
	private _detail: WebTileDescription = null;
	get detail(): WebTileDescription {
		return this._detail;
	}
	set detail(value: WebTileDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


