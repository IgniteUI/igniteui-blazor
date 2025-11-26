import { Base, Type, markType } from "./type";
import { StringBuilder } from "./StringBuilder";

/**
 * @hidden 
 */
export class DescriptionSerializerBuilder extends Base {
	static $t: Type = markType(DescriptionSerializerBuilder, 'DescriptionSerializerBuilder');
	private _sb: StringBuilder = null;
	toString(): string {
		return this._sb.toString();
	}
	constructor() {
		super();
		this._sb = new StringBuilder(0);
	}
	private _tabLevel: number = 0;
	private _newLine: boolean = false;
	append(val: string): void {
		this.appendTabs();
		this._sb.append5(val);
	}
	appendLine(val: string): void {
		this.appendTabs();
		this._sb.appendLine1(val);
		this._newLine = true;
	}
	private appendTabs(): void {
		if (this._newLine) {
			for (let i = 0; i < this._tabLevel; i++) {
				this._sb.append5("\t");
			}
			this._newLine = false;
		}
	}
	increaseTabLevel(): void {
		this._tabLevel++;
	}
	decreaseTabLevel(): void {
		this._tabLevel--;
	}
}


