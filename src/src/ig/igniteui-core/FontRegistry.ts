import { Base, String_$type, Type, markType } from "./type";
import { Dictionary$2 } from "./Dictionary$2";

/**
 * @hidden 
 */
export class FontRegistry extends Base {
	static $t: Type = markType(FontRegistry, 'FontRegistry');
	private static _instance: FontRegistry = null;
	static get instance(): FontRegistry {
		if (FontRegistry._instance == null) {
			FontRegistry._instance = new FontRegistry();
		}
		return FontRegistry._instance;
	}
	private _fonts: Dictionary$2<string, any> = new Dictionary$2<string, any>(String_$type, (<any>Base).$type, 0);
	registerFont(key: string, font: any): void {
		if (this._fonts.containsKey(key)) {
			this._fonts.item(key, font);
		} else {
			this._fonts.addItem(key, font);
		}
	}
	removeFont(key: string): void {
		if (this._fonts.containsKey(key)) {
			this._fonts.removeItem(key);
		}
	}
	getFont(key: string): any {
		if (this._fonts.containsKey(key)) {
			return this._fonts.item(key);
		}
		return null;
	}
}


