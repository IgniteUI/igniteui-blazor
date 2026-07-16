import { Description } from "./Description";
import { WebTileChangeStateEventArgsDetailDescription } from "./WebTileChangeStateEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTileChangeStateEventArgsDescription extends Description {
	static $t: Type = markType(WebTileChangeStateEventArgsDescription, 'WebTileChangeStateEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTileChangeStateEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TileChangeStateEventArgs";
	constructor() {
		super();
	}
	private _detail: WebTileChangeStateEventArgsDetailDescription = null;
	get detail(): WebTileChangeStateEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebTileChangeStateEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


