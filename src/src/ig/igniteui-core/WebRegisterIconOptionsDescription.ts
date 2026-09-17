import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebRegisterIconOptionsDescription extends Description {
	static $t: Type = markType(WebRegisterIconOptionsDescription, 'WebRegisterIconOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebRegisterIconOptions";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "RegisterIconOptions";
	constructor() {
		super();
	}
	private _collection: string = null;
	get collection(): string {
		return this._collection;
	}
	set collection(value: string) {
		this._collection = value;
		this.markDirty("Collection");
	}
	private _stripMeta: boolean = false;
	get stripMeta(): boolean {
		return this._stripMeta;
	}
	set stripMeta(value: boolean) {
		this._stripMeta = value;
		this.markDirty("StripMeta");
	}
}


