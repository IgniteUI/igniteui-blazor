import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebIconDescription extends Description {
	static $t: Type = markType(WebIconDescription, 'WebIconDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebIcon";
	}
	get type(): string {
		return this.get_type();
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
}


