import { Base, IFormatProvider, IFormatProvider_$type, Type, markType } from "./type";
import { NotImplementedException } from "./NotImplementedException";
import { Environment } from "./Environment";
import { stringFormat, stringFormat1, stringFormat2 } from "./stringExtended";

/**
 * @hidden 
 */
export class StringBuilder extends Base {
	static $t: Type = markType(StringBuilder, 'StringBuilder');
	private _internal: string = null;
	private get internal(): string {
		return this._internal;
	}
	private set internal(value: string) {
		this._internal = value;
	}
	constructor(initNumber: number);
	constructor(initNumber: number, capacity: number);
	constructor(initNumber: number, value: string);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		super();
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				this.internal = "";
			}
			break;

			case 1:
			{
				let capacity: number = <number>_rest[0];
				{
					this.internal = "";
				}
			}
			break;

			case 2:
			{
				let value: string = <string>_rest[0];
				this.internal = value;
			}
			break;

		}

	}
	append4(obj: any): StringBuilder {
		if (obj != null) {
			this.append5(obj.toString());
		}
		return this;
	}
	append5(str_: string): StringBuilder {
		if (str_ != null)
        {
            this.internal = this.internal.concat(str_);
        };
		return this;
	}
	append7(builder: StringBuilder): StringBuilder {
		let str_: string = builder.toString();
		this.internal = this.internal.concat(str_);
		return this;
	}
	append1(chr_: string): StringBuilder {
		this.internal = this.internal.concat(chr_);
		return this;
	}
	append2(chr_: string, count_: number): StringBuilder {
		if (<boolean>(<any>chr_.repeat)) {
			this.internal = this.internal.concat(chr_.repeat(count_));
		} else {
			for (let i: number = 0; i < count_; i++) {
				this.internal = this.internal.concat(chr_);
			}
		}
		return this;
	}
	append3(value_: number): StringBuilder {
		this.internal = this.internal.concat(value_.toString());
		return this;
	}
	append6(value_: string, startIndex_: number, count_: number): StringBuilder {
		this.internal = this.internal.concat(value_.substr(startIndex_, count_));
		return this;
	}
	append(value_: string[], startIndex_: number, charCount_: number): StringBuilder {
		this.internal = this.internal.concat(value_.slice(startIndex_, startIndex_ + charCount_).join(''));
		return this;
	}
	appendLine(): StringBuilder {
		return this.appendLine1("");
	}
	appendLine1(str_: string): StringBuilder {
		if (str_ != null)
        {
            this.internal = this.internal.concat(str_);
        }
        this.internal = this.internal.concat(Environment.newLine);
		return this;
	}
	clear(): StringBuilder {
		this.internal = "";
		return this;
	}
	insert(index_: number, chr_: string): StringBuilder {
		if (index_ == this.length) {
			this.append1(chr_);
		} else {
			this.internal = this.internal.substring(0, index_).concat(chr_).concat(this.internal.substring(index_, this.internal.length));
		}
		return this;
	}
	insert1(index_: number, str_: string): StringBuilder {
		if (index_ == this.length) {
			this.append5(str_);
		} else {
			this.internal = this.internal.substring(0, index_).concat(str_).concat(this.internal.substring(index_, this.internal.length));
		}
		return this;
	}
	remove(startIndex_: number, length_: number): StringBuilder {
		this.internal = this.internal.substring(0, startIndex_).concat(this.internal.substring(startIndex_ + length_, this.internal.length));
		return this;
	}
	replace(oldCh_: string, newCh_: string): StringBuilder {
		this.internal = this.internal.replace(oldCh_, newCh_);
		return this;
	}
	toString(): string {
		return this.internal;
	}
	toString1(startIndex: number, length: number): string {
		return this.internal.substr(startIndex, length);
	}
	get length(): number {
		return this.internal.length;
	}
	set length(value: number) {
		if (value <= this.length) {
			this.internal = this.internal.substring(0, value);
		} else {
			throw new NotImplementedException(0);
		}
	}
	item(index_: number, value?: string): string {
		if (arguments.length === 2) {
			this.internal = this.internal.substring(0, index_).concat(value).concat(this.internal.substring(index_ + 1, this.internal.length));
			return value;
		} else {
			return this.internal.charAt(index_);
		}
	}
	appendFormat2(format: string, arg0: any): StringBuilder {
		return this.append5(stringFormat(format, arg0));
	}
	appendFormat1(format: string, ...args: any[]): StringBuilder {
		return this.append5(stringFormat1(format, ...args));
	}
	appendFormat(provider: IFormatProvider, format: string, ...args: any[]): StringBuilder {
		return this.append5(stringFormat2(provider, format, ...args));
	}
	appendFormat3(format: string, arg0: any, arg1: any): StringBuilder {
		return this.append5(stringFormat(format, arg0, arg1));
	}
	appendFormat4(format: string, arg0: any, arg1: any, arg2: any): StringBuilder {
		return this.append5(stringFormat(format, arg0, arg1, arg2));
	}
	private _capacity: number = 0;
	get capacity(): number {
		return this._capacity;
	}
	set capacity(value: number) {
		this._capacity = value;
	}
}


