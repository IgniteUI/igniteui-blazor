import { Description } from "./Description";
import { WebTileDescription } from "./WebTileDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTileChangeStateEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebTileChangeStateEventArgsDetailDescription, 'WebTileChangeStateEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTileChangeStateEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TileChangeStateEventArgsDetail";
	constructor() {
		super();
	}
	private _tile: WebTileDescription = null;
	get tile(): WebTileDescription {
		return this._tile;
	}
	set tile(value: WebTileDescription) {
		this._tile = value;
		this.markDirty("Tile");
	}
	private _state: boolean = false;
	get state(): boolean {
		return this._state;
	}
	set state(value: boolean) {
		this._state = value;
		this.markDirty("State");
	}
}


