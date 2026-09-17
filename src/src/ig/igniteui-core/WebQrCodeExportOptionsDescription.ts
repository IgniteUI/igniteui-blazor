import { Description } from "./Description";
import { Type, markType } from "./type";

/**
 * @hidden
 */
export class WebQrCodeExportOptionsDescription extends Description {
	static $t: Type = markType(WebQrCodeExportOptionsDescription, 'WebQrCodeExportOptionsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebQrCodeExportOptions";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "QrCodeExportOptions";
	constructor() {
		super();
	}
	private _fileName: string = null;
	get fileName(): string {
		return this._fileName;
	}
	set fileName(value: string) {
		this._fileName = value;
		this.markDirty("FileName");
	}
	private _format: string = null;
	get format(): string {
		return this._format;
	}
	set format(value: string) {
		this._format = value;
		this.markDirty("Format");
	}
	private _scale: number = 0;
	get scale(): number {
		return this._scale;
	}
	set scale(value: number) {
		this._scale = value;
		this.markDirty("Scale");
	}
	private _download: boolean = false;
	get download(): boolean {
		return this._download;
	}
	set download(value: boolean) {
		this._download = value;
		this.markDirty("Download");
	}
}


