import { WebButtonBaseDescription } from "./WebButtonBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebIconButtonDescription extends WebButtonBaseDescription {
	static $t: Type = markType(WebIconButtonDescription, 'WebIconButtonDescription', (<any>WebButtonBaseDescription).$type);
	protected get_type(): string {
		return "WebIconButton";
	}
	constructor() {
		super();
	}
	private _iconName: string = null;
	get iconName(): string {
		return this._iconName;
	}
	set iconName(value: string) {
		this._iconName = value;
		this.markDirty("IconName");
	}
	private _collection: string = null;
	get collection(): string {
		return this._collection;
	}
	set collection(value: string) {
		this._collection = value;
		this.markDirty("Collection");
	}
	private _mirrored: boolean = false;
	get mirrored(): boolean {
		return this._mirrored;
	}
	set mirrored(value: boolean) {
		this._mirrored = value;
		this.markDirty("Mirrored");
	}
	private _variant: string = null;
	get variant(): string {
		return this._variant;
	}
	set variant(value: string) {
		this._variant = value;
		this.markDirty("Variant");
	}
}


