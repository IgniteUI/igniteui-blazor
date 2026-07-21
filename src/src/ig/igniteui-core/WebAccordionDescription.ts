import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebAccordionDescription extends Description {
	static $t: Type = markType(WebAccordionDescription, 'WebAccordionDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebAccordion";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _singleExpand: boolean = false;
	get singleExpand(): boolean {
		return this._singleExpand;
	}
	set singleExpand(value: boolean) {
		this._singleExpand = value;
		this.markDirty("SingleExpand");
	}
	private _opening: string = null;
	get openingRef(): string {
		return this._opening;
	}
	set openingRef(value: string) {
		this._opening = value;
		this.markDirty("OpeningRef");
	}
	private _opened: string = null;
	get openedRef(): string {
		return this._opened;
	}
	set openedRef(value: string) {
		this._opened = value;
		this.markDirty("OpenedRef");
	}
	private _closing: string = null;
	get closingRef(): string {
		return this._closing;
	}
	set closingRef(value: string) {
		this._closing = value;
		this.markDirty("ClosingRef");
	}
	private _closed: string = null;
	get closedRef(): string {
		return this._closed;
	}
	set closedRef(value: string) {
		this._closed = value;
		this.markDirty("ClosedRef");
	}
}


