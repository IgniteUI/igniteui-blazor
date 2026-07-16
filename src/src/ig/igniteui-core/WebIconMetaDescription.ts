import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebIconMetaDescription extends Description {
	static $t: Type = markType(WebIconMetaDescription, 'WebIconMetaDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebIconMeta";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "IconMeta";
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
}


