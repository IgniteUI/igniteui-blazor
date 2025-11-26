import { Base, Type, markType } from "./type";
import { StringBuilder } from "./StringBuilder";

/**
 * @hidden 
 */
export class JsonWriter extends Base {
	static $t: Type = markType(JsonWriter, 'JsonWriter');
	private _builder: StringBuilder = new StringBuilder(0);
	private _indentLevel: number = 0;
	increaseIndent(): void {
		this._indentLevel++;
	}
	decreaseIndent(): void {
		this._indentLevel--;
	}
	_isNewLine: boolean = true;
	write(content: string): void {
		this.ensureIndent();
		this._builder.append5(content);
	}
	private ensureIndent(): void {
		if (this._isNewLine) {
			this._isNewLine = false;
			for (let i = 0; i < this._indentLevel; i++) {
				this._builder.append5("\t");
			}
		}
	}
	writeLine(content: string): void {
		this.ensureIndent();
		this._builder.appendLine1(content);
		this._isNewLine = true;
	}
	newLine(): void {
		this.ensureIndent();
		this._builder.appendLine();
		this._isNewLine = true;
	}
	toString(): string {
		return this._builder.toString();
	}
}


