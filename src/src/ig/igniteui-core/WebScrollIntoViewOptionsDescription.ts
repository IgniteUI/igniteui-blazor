import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebScrollIntoViewOptionsDescription extends Description {
	static $t: Type = markType(WebScrollIntoViewOptionsDescription, 'WebScrollIntoViewOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebScrollIntoViewOptions";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ScrollIntoViewOptions";
	constructor() {
		super();
	}
	private _behavior: string = null;
	get behavior(): string {
		return this._behavior;
	}
	set behavior(value: string) {
		this._behavior = value;
		this.markDirty("Behavior");
	}
	private _block: string = null;
	get block(): string {
		return this._block;
	}
	set block(value: string) {
		this._block = value;
		this.markDirty("Block");
	}
	private _inline: string = null;
	get inline(): string {
		return this._inline;
	}
	set inline(value: string) {
		this._inline = value;
		this.markDirty("Inline");
	}
}


