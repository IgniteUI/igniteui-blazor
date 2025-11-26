import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export abstract class WebButtonBaseDescription extends Description {
	static $t: Type = markType(WebButtonBaseDescription, 'WebButtonBaseDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebButtonBase";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _displayType: string = null;
	get displayType(): string {
		return this._displayType;
	}
	set displayType(value: string) {
		this._displayType = value;
		this.markDirty("DisplayType");
	}
	private _href: string = null;
	get href(): string {
		return this._href;
	}
	set href(value: string) {
		this._href = value;
		this.markDirty("Href");
	}
	private _download: string = null;
	get download(): string {
		return this._download;
	}
	set download(value: string) {
		this._download = value;
		this.markDirty("Download");
	}
	private _target: string = null;
	get target(): string {
		return this._target;
	}
	set target(value: string) {
		this._target = value;
		this.markDirty("Target");
	}
	private _rel: string = null;
	get rel(): string {
		return this._rel;
	}
	set rel(value: string) {
		this._rel = value;
		this.markDirty("Rel");
	}
	private _disabled: boolean = false;
	get disabled(): boolean {
		return this._disabled;
	}
	set disabled(value: boolean) {
		this._disabled = value;
		this.markDirty("Disabled");
	}
	private _clicked: string = null;
	get clickedRef(): string {
		return this._clicked;
	}
	set clickedRef(value: string) {
		this._clicked = value;
		this.markDirty("ClickedRef");
	}
	private _focus: string = null;
	get focusRef(): string {
		return this._focus;
	}
	set focusRef(value: string) {
		this._focus = value;
		this.markDirty("FocusRef");
	}
	private _blur: string = null;
	get blurRef(): string {
		return this._blur;
	}
	set blurRef(value: string) {
		this._blur = value;
		this.markDirty("BlurRef");
	}
}


