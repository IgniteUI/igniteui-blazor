import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebQrCodeDescription extends Description {
	static $t: Type = markType(WebQrCodeDescription, 'WebQrCodeDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebQrCode";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _value: string = null;
	get value(): string {
		return this._value;
	}
	set value(value: string) {
		this._value = value;
		this.markDirty("Value");
	}
	private _version: number = 0;
	get version(): number {
		return this._version;
	}
	set version(value: number) {
		this._version = value;
		this.markDirty("Version");
	}
	private _errorLevel: string = null;
	get errorLevel(): string {
		return this._errorLevel;
	}
	set errorLevel(value: string) {
		this._errorLevel = value;
		this.markDirty("ErrorLevel");
	}
	private _size: number = 0;
	get size(): number {
		return this._size;
	}
	set size(value: number) {
		this._size = value;
		this.markDirty("Size");
	}
	private _margin: number = 0;
	get margin(): number {
		return this._margin;
	}
	set margin(value: number) {
		this._margin = value;
		this.markDirty("Margin");
	}
	private _logoSrc: string = null;
	get logoSrc(): string {
		return this._logoSrc;
	}
	set logoSrc(value: string) {
		this._logoSrc = value;
		this.markDirty("LogoSrc");
	}
	private _logoSize: number = 0;
	get logoSize(): number {
		return this._logoSize;
	}
	set logoSize(value: number) {
		this._logoSize = value;
		this.markDirty("LogoSize");
	}
	private _logoMargin: number = 0;
	get logoMargin(): number {
		return this._logoMargin;
	}
	set logoMargin(value: number) {
		this._logoMargin = value;
		this.markDirty("LogoMargin");
	}
	private _dotStyle: string = null;
	get dotStyle(): string {
		return this._dotStyle;
	}
	set dotStyle(value: string) {
		this._dotStyle = value;
		this.markDirty("DotStyle");
	}
	private _squareStyle: string = null;
	get squareStyle(): string {
		return this._squareStyle;
	}
	set squareStyle(value: string) {
		this._squareStyle = value;
		this.markDirty("SquareStyle");
	}
}


