import { ValueType, IComparable, IComparable_$type, IComparable$1, IComparable$1_$type, IEquatable$1, IEquatable$1_$type, Base, IFormatProvider, IFormatProvider_$type, FormatException, BaseError, typeCast, Nullable$1, markStruct, Type } from "./type";
import { IFormattable, IFormattable_$type } from "./IFormattable";
import { ArgumentNullException } from "./ArgumentNullException";
import { Guid_GuidStyles } from "./Guid_GuidStyles";
import { IndexOutOfRangeException } from "./IndexOutOfRangeException";
import { ArgumentException } from "./ArgumentException";
import { CultureInfo } from "./culture";
import { Guid_GuidParseThrowStyle } from "./Guid_GuidParseThrowStyle";
import { Guid_ParseFailureKind } from "./Guid_ParseFailureKind";
import { intSToU, parseInt32_2, NumberStyles, tryParseInt32_2 } from "./numberExtended";
import { createGuid, stringCreateFromCharArraySlice, stringIsNullOrEmpty } from "./string";

/**
 * @hidden 
 */
export class Guid extends ValueType implements IFormattable, IComparable, IComparable$1<Guid>, IEquatable$1<Guid> {
	static $t: Type = markStruct(Guid, 'Guid', (<any>ValueType).$type, [IFormattable_$type, IComparable_$type, IComparable$1_$type.specialize(-1), IEquatable$1_$type.specialize(-1)]).initSelfReferences();
	constructor(initNumber: number, b: number[]);
	constructor(initNumber: number, a: number, b: number, c: number, d: number, e: number, f: number, g: number, h: number, i: number, j: number, k: number);
	constructor(initNumber: number, g: string);
	constructor();
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber?: number, ..._rest: any[]) {
		super();
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0: break;
			case 1:
			{
				let b: number[] = <number[]>_rest[0];
				this._a = (((b[3] << 24) | (b[2] << 16)) | (b[1] << 8)) | b[0];
				this._b = <number>((b[5] << 8) | b[4]);
				this._c = <number>((b[7] << 8) | b[6]);
				this._d = b[8];
				this._e = b[9];
				this._f = b[10];
				this._g = b[11];
				this._h = b[12];
				this._i = b[13];
				this._j = b[14];
				this._k = b[15];
			}
			break;

			case 2:
			{
				let a: number = <number>_rest[0];
				let b: number = <number>_rest[1];
				let c: number = <number>_rest[2];
				let d: number = <number>_rest[3];
				let e: number = <number>_rest[4];
				let f: number = <number>_rest[5];
				let g: number = <number>_rest[6];
				let h: number = <number>_rest[7];
				let i: number = <number>_rest[8];
				let j: number = <number>_rest[9];
				let k: number = <number>_rest[10];
				this._a = a;
				this._b = b;
				this._c = c;
				this._d = d;
				this._e = e;
				this._f = f;
				this._g = g;
				this._h = h;
				this._i = i;
				this._j = j;
				this._k = k;
			}
			break;

			case 3:
			{
				let g: string = <string>_rest[0];
				if (g == null) {
					throw new ArgumentNullException(0, "g");
				}
				let result: Guid_GuidResult = new Guid_GuidResult();
				result.init(Guid_GuidParseThrowStyle.All);
				if (!((() => { let $ret = Guid.tryParseGuid(g, Guid_GuidStyles.Any, result); result = $ret.p2; return $ret.ret; })())) {
					throw result.getGuidParseException();
				}
				this._a = result.parsedGuid._a;
				this._b = result.parsedGuid._b;
				this._c = result.parsedGuid._c;
				this._d = result.parsedGuid._d;
				this._e = result.parsedGuid._e;
				this._f = result.parsedGuid._f;
				this._g = result.parsedGuid._g;
				this._h = result.parsedGuid._h;
				this._i = result.parsedGuid._i;
				this._j = result.parsedGuid._j;
				this._k = result.parsedGuid._k;
			}
			break;

		}

	}
	static readonly empty: Guid = new Guid(0);
	private _a: number = 0;
	private _b: number = 0;
	private _c: number = 0;
	private _d: number = 0;
	private _e: number = 0;
	private _f: number = 0;
	private _g: number = 0;
	private _h: number = 0;
	private _i: number = 0;
	private _j: number = 0;
	private _k: number = 0;
	compareTo(value: Guid): number {
		let result = this._a - value._a;
		if (result != 0) {
			return result;
		}
		result = this._b - value._b;
		if (result != 0) {
			return result;
		}
		result = this._c - value._c;
		if (result != 0) {
			return result;
		}
		result = this._d - value._d;
		if (result != 0) {
			return result;
		}
		result = this._e - value._e;
		if (result != 0) {
			return result;
		}
		result = this._f - value._f;
		if (result != 0) {
			return result;
		}
		result = this._g - value._g;
		if (result != 0) {
			return result;
		}
		result = this._h - value._h;
		if (result != 0) {
			return result;
		}
		result = this._i - value._i;
		if (result != 0) {
			return result;
		}
		result = this._j - value._j;
		if (result != 0) {
			return result;
		}
		result = this._k - value._k;
		return result;
	}
	compareToObject(value: any): number {
		if (typeCast<Guid>((<any>Guid).$type, value) !== null) {
			return this.compareTo(<Guid>value);
		}
		return 1;
	}
	equals(g: Guid): boolean {
		return Guid.l_op_Equality(this, g);
	}
	static newGuid(): Guid {
		return new Guid(3, <string>(createGuid()));
	}
	toByteArray(): number[] {
		return <number[]>[ (<number>this._a), (<number>(this._a >> 8)), (<number>(this._a >> 16)), (<number>(this._a >> 24)), (<number>this._b), (<number>(this._b >> 8)), (<number>this._c), (<number>(this._c >> 8)), this._d, this._e, this._f, this._g, this._h, this._i, this._j, this._k ];
	}
	toString(): string {
		return this.toString1("D", null);
	}
	toString2(format: string): string {
		return this.toString1(format, null);
	}
	toString1(format: string, provider: IFormatProvider): string {
		let chArray: string[];
		if ((format == null) || (format.length == 0)) {
			format = "D";
		}
		let offset: number = 0;
		let length: number = 38;
		let flag: boolean = true;
		let flag2: boolean = false;
		if (format.length != 1) {
			throw new FormatException(0);
		}
		let ch: string = format.charAt(0);
		switch (ch) {
			case 'D':

			case 'd':
			chArray = <string[]>new Array(36);
			length = 36;
			break;

			case 'N':

			case 'n':
			chArray = <string[]>new Array(32);
			length = 32;
			flag = false;
			break;

			case 'B':

			case 'b':
			chArray = <string[]>new Array(38);
			chArray[offset++] = '{';
			chArray[37] = '}';
			break;

			case 'P':

			case 'p':
			chArray = <string[]>new Array(38);
			chArray[offset++] = '(';
			chArray[37] = ')';
			break;

			default:
			if ((ch != 'X') && (ch != 'x')) {
				throw new FormatException(0);
			}
			chArray = <string[]>new Array(68);
			chArray[offset++] = '{';
			chArray[67] = '}';
			length = 68;
			flag = false;
			flag2 = true;
			break;

		}

		if (flag2) {
			chArray[offset++] = '0';
			chArray[offset++] = 'x';
			offset = Guid.hexsToChars(chArray, offset, this._a >> 24, this._a >> 16);
			offset = Guid.hexsToChars(chArray, offset, this._a >> 8, this._a);
			chArray[offset++] = ',';
			chArray[offset++] = '0';
			chArray[offset++] = 'x';
			offset = Guid.hexsToChars(chArray, offset, this._b >> 8, this._b);
			chArray[offset++] = ',';
			chArray[offset++] = '0';
			chArray[offset++] = 'x';
			offset = Guid.hexsToChars(chArray, offset, this._c >> 8, this._c);
			chArray[offset++] = ',';
			chArray[offset++] = '{';
			offset = Guid.hexsToChars1(chArray, offset, this._d, this._e, true);
			chArray[offset++] = ',';
			offset = Guid.hexsToChars1(chArray, offset, this._f, this._g, true);
			chArray[offset++] = ',';
			offset = Guid.hexsToChars1(chArray, offset, this._h, this._i, true);
			chArray[offset++] = ',';
			offset = Guid.hexsToChars1(chArray, offset, this._j, this._k, true);
			chArray[offset++] = '}';
		} else {
			offset = Guid.hexsToChars(chArray, offset, this._a >> 24, this._a >> 16);
			offset = Guid.hexsToChars(chArray, offset, this._a >> 8, this._a);
			if (flag) {
				chArray[offset++] = '-';
			}
			offset = Guid.hexsToChars(chArray, offset, this._b >> 8, this._b);
			if (flag) {
				chArray[offset++] = '-';
			}
			offset = Guid.hexsToChars(chArray, offset, this._c >> 8, this._c);
			if (flag) {
				chArray[offset++] = '-';
			}
			offset = Guid.hexsToChars(chArray, offset, this._d, this._e);
			if (flag) {
				chArray[offset++] = '-';
			}
			offset = Guid.hexsToChars(chArray, offset, this._f, this._g);
			offset = Guid.hexsToChars(chArray, offset, this._h, this._i);
			offset = Guid.hexsToChars(chArray, offset, this._j, this._k);
		}
		return stringCreateFromCharArraySlice(chArray, 0, length);
	}
	private static hexsToChars(guidChars: string[], offset: number, a: number, b: number): number {
		return Guid.hexsToChars1(guidChars, offset, a, b, false);
	}
	private static hexsToChars1(guidChars: string[], offset: number, a: number, b: number, hex: boolean): number {
		if (hex) {
			guidChars[offset++] = '0';
			guidChars[offset++] = 'x';
		}
		guidChars[offset++] = Guid.hexToChar(a >> 4);
		guidChars[offset++] = Guid.hexToChar(a);
		if (hex) {
			guidChars[offset++] = ',';
			guidChars[offset++] = '0';
			guidChars[offset++] = 'x';
		}
		guidChars[offset++] = Guid.hexToChar(b >> 4);
		guidChars[offset++] = Guid.hexToChar(b);
		return offset;
	}
	private static hexToChar(a: number): string {
		a &= 15;
		return ((a > 9) ? (<string>String.fromCharCode(((a - 10) + 97))) : (<string>String.fromCharCode((a + 48))));
	}
	static tryParse(input: string, result: Guid): { ret: boolean; p1: Guid; } {
		let result2: Guid_GuidResult = new Guid_GuidResult();
		result2.init(Guid_GuidParseThrowStyle.None);
		if (((() => { let $ret = Guid.tryParseGuid(input, Guid_GuidStyles.Any, result2); result2 = $ret.p2; return $ret.ret; })())) {
			result = result2.parsedGuid;
			return {
				ret: true,
				p1: result

			};
		}
		result = Guid.empty;
		return {
			ret: false,
			p1: result

		};
	}
	private static tryParseGuid(g: string, flags: Guid_GuidStyles, result: Guid_GuidResult): { ret: boolean; p2?: Guid_GuidResult; } {
		if (g == null) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
			return {
				ret: false,
				p2: result

			};
		}
		let guidString: string = g.trim();
		if (guidString.length == 0) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
			return {
				ret: false,
				p2: result

			};
		}
		let flag: boolean = guidString.indexOf('-', 0) >= 0;
		if (flag) {
			if ((flags & (Guid_GuidStyles.DigitFormat | Guid_GuidStyles.AllowDashes)) == Guid_GuidStyles.None) {
				result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
				return {
					ret: false,
					p2: result

				};
			}
		} else if ((flags & Guid_GuidStyles.DigitFormat) != Guid_GuidStyles.None) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
			return {
				ret: false,
				p2: result

			};
		}
		let flag2: boolean = guidString.indexOf('{', 0) >= 0;
		if (flag2) {
			if ((flags & (Guid_GuidStyles.RequireBraces | Guid_GuidStyles.AllowBraces)) == Guid_GuidStyles.None) {
				result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
				return {
					ret: false,
					p2: result

				};
			}
		} else if ((flags & Guid_GuidStyles.RequireBraces) != Guid_GuidStyles.None) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
			return {
				ret: false,
				p2: result

			};
		}
		if (guidString.indexOf('(', 0) >= 0) {
			if ((flags & (Guid_GuidStyles.RequireParenthesis | Guid_GuidStyles.AllowParenthesis)) == Guid_GuidStyles.None) {
				result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
				return {
					ret: false,
					p2: result

				};
			}
		} else if ((flags & Guid_GuidStyles.RequireParenthesis) != Guid_GuidStyles.None) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidUnrecognized");
			return {
				ret: false,
				p2: result

			};
		}
		try {
			if (flag) {
				return {
					ret: ((() => { let $ret = Guid.tryParseGuidWithDashes(guidString, result); result = $ret.p1; return $ret.ret; })()),
					p2: result

				};
			}
			if (flag2) {
				return {
					ret: ((() => { let $ret = Guid.tryParseGuidWithHexPrefix(guidString, result); result = $ret.p1; return $ret.ret; })()),
					p2: result

				};
			}
			return {
				ret: ((() => { let $ret = Guid.tryParseGuidWithNoStyle(guidString, result); result = $ret.p1; return $ret.ret; })()),
				p2: result

			};
		}
		catch (e) {
			let e1 = typeCast<IndexOutOfRangeException>((<any>IndexOutOfRangeException).$type, e);
			if (e1 != null) {
				result.setFailure3(Guid_ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", null, null, e1);
				return {
					ret: false,
					p2: result

				};
			}
			let e2 = typeCast<ArgumentException>((<any>ArgumentException).$type, e);
			if (e2 != null) {
				result.setFailure3(Guid_ParseFailureKind.FormatWithInnerException, "Format_GuidUnrecognized", null, null, e2);
				return {
					ret: false,
					p2: result

				};
			}
			throw e;
		}
	}
	private static tryParseGuidWithDashes(guidString: string, result: Guid_GuidResult): { ret: boolean; p1?: Guid_GuidResult; } {
		let num2: number;
		let num3: number;
		let num4: number;
		let num: number = 0;
		let parsePos: number = 0;
		if (guidString.charAt(0) == '{') {
			if ((guidString.length != 38) || (guidString.charAt(37) != '}')) {
				result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidInvLen");
				return {
					ret: false,
					p1: result

				};
			}
			num = 1;
		} else if (guidString.charAt(0) == '(') {
			if ((guidString.length != 38) || (guidString.charAt(37) != ')')) {
				result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidInvLen");
				return {
					ret: false,
					p1: result

				};
			}
			num = 1;
		} else if (guidString.length != 36) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidInvLen");
			return {
				ret: false,
				p1: result

			};
		}
		if (((guidString.charAt(8 + num) != '-') || (guidString.charAt(13 + num) != '-')) || ((guidString.charAt(18 + num) != '-') || (guidString.charAt(23 + num) != '-'))) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidDashes");
			return {
				ret: false,
				p1: result

			};
		}
		parsePos = num;
		if (!((() => { let $ret = Guid.stringToInt1(guidString, parsePos, 8, 8192, num2, result); parsePos = $ret.p1; num2 = $ret.p4; result = $ret.p5; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		result.parsedGuid._a = num2;
		parsePos++;
		if (!((() => { let $ret = Guid.stringToInt1(guidString, parsePos, 4, 8192, num2, result); parsePos = $ret.p1; num2 = $ret.p4; result = $ret.p5; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		result.parsedGuid._b = <number>num2;
		parsePos++;
		if (!((() => { let $ret = Guid.stringToInt1(guidString, parsePos, 4, 8192, num2, result); parsePos = $ret.p1; num2 = $ret.p4; result = $ret.p5; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		result.parsedGuid._c = <number>num2;
		parsePos++;
		if (!((() => { let $ret = Guid.stringToInt1(guidString, parsePos, 4, 8192, num2, result); parsePos = $ret.p1; num2 = $ret.p4; result = $ret.p5; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		parsePos++;
		num = parsePos;
		if (!((() => { let $ret = Guid.stringToInt1(guidString, parsePos, 4, 8192, num3, result); parsePos = $ret.p1; num3 = $ret.p4; result = $ret.p5; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		if (!((() => { let $ret = Guid.stringToInt1(guidString, parsePos, 8, 8192, num4, result); parsePos = $ret.p1; num4 = $ret.p4; result = $ret.p5; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		result.parsedGuid._d = <number>((num2 >> 8) & 255);
		result.parsedGuid._e = <number>(num2 & 255);
		num2 = num3;
		result.parsedGuid._f = <number>((num2 >> 8) & 255);
		result.parsedGuid._g = <number>(num2 & 255);
		num2 = num4;
		result.parsedGuid._h = <number>((num2 >> 24) & 255);
		result.parsedGuid._i = <number>((num2 >> 16) & 255);
		result.parsedGuid._j = <number>((num2 >> 8) & 255);
		result.parsedGuid._k = <number>(num2 & 255);
		return {
			ret: true,
			p1: result

		};
	}
	private static eatAllWhitespace(str: string): string {
		let length: number = 0;
		let chArray: string[] = <string[]>new Array(str.length);
		for (let i: number = 0; i < str.length; i++) {
			let c: string = str.charAt(i);
			if (!/\s/i.test(c)) {
				chArray[length++] = c;
			}
		}
		return stringCreateFromCharArraySlice(chArray, 0, length);
	}
	private static tryParseGuidWithHexPrefix(guidString: string, result: Guid_GuidResult): { ret: boolean; p1?: Guid_GuidResult; } {
		let startIndex: number = 0;
		let length: number = 0;
		guidString = Guid.eatAllWhitespace(guidString);
		if (stringIsNullOrEmpty(guidString) || (guidString.charAt(0) != '{')) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidBrace");
			return {
				ret: false,
				p1: result

			};
		}
		if (!Guid.isHexPrefix(guidString, 1)) {
			result.setFailure2(Guid_ParseFailureKind.Format, "Format_GuidHexPrefix", "{0xdddddddd, etc}");
			return {
				ret: false,
				p1: result

			};
		}
		startIndex = 3;
		length = guidString.indexOf(',', startIndex) - startIndex;
		if (length <= 0) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidComma");
			return {
				ret: false,
				p1: result

			};
		}
		if (!((() => { let $ret = Guid.stringToInt(guidString.substr(startIndex, length), -1, 4096, result.parsedGuid._a, result); result.parsedGuid._a = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		if (!Guid.isHexPrefix(guidString, (startIndex + length) + 1)) {
			result.setFailure2(Guid_ParseFailureKind.Format, "Format_GuidHexPrefix", "{0xdddddddd, 0xdddd, etc}");
			return {
				ret: false,
				p1: result

			};
		}
		startIndex = (startIndex + length) + 3;
		length = guidString.indexOf(',', startIndex) - startIndex;
		if (length <= 0) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidComma");
			return {
				ret: false,
				p1: result

			};
		}
		if (!((() => { let $ret = Guid.stringToShort(guidString.substr(startIndex, length), -1, 4096, result.parsedGuid._b, result); result.parsedGuid._b = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		if (!Guid.isHexPrefix(guidString, (startIndex + length) + 1)) {
			result.setFailure2(Guid_ParseFailureKind.Format, "Format_GuidHexPrefix", "{0xdddddddd, 0xdddd, 0xdddd, etc}");
			return {
				ret: false,
				p1: result

			};
		}
		startIndex = (startIndex + length) + 3;
		length = guidString.indexOf(',', startIndex) - startIndex;
		if (length <= 0) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidComma");
			return {
				ret: false,
				p1: result

			};
		}
		if (!((() => { let $ret = Guid.stringToShort(guidString.substr(startIndex, length), -1, 4096, result.parsedGuid._c, result); result.parsedGuid._c = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		if ((guidString.length <= ((startIndex + length) + 1)) || (guidString.charAt((startIndex + length) + 1) != '{')) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidBrace");
			return {
				ret: false,
				p1: result

			};
		}
		length++;
		let buffer: number[] = <number[]>new Array(8);
		for (let i: number = 0; i < 8; i++) {
			if (!Guid.isHexPrefix(guidString, (startIndex + length) + 1)) {
				result.setFailure2(Guid_ParseFailureKind.Format, "Format_GuidHexPrefix", "{... { ... 0xdd, ...}}");
				return {
					ret: false,
					p1: result

				};
			}
			startIndex = (startIndex + length) + 3;
			if (i < 7) {
				length = guidString.indexOf(',', startIndex) - startIndex;
				if (length <= 0) {
					result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidComma");
					return {
						ret: false,
						p1: result

					};
				}
			} else {
				length = guidString.indexOf('}', startIndex) - startIndex;
				if (length <= 0) {
					result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidBraceAfterLastNumber");
					return {
						ret: false,
						p1: result

					};
				}
			}
			let num4: number = <number>intSToU(parseInt32_2(guidString.substr(startIndex, length), NumberStyles.HexNumber));
			if (num4 > 255) {
				result.setFailure1(Guid_ParseFailureKind.Format, "Overflow_Byte");
				return {
					ret: false,
					p1: result

				};
			}
			buffer[i] = <number>num4;
		}
		result.parsedGuid._d = buffer[0];
		result.parsedGuid._e = buffer[1];
		result.parsedGuid._f = buffer[2];
		result.parsedGuid._g = buffer[3];
		result.parsedGuid._h = buffer[4];
		result.parsedGuid._i = buffer[5];
		result.parsedGuid._j = buffer[6];
		result.parsedGuid._k = buffer[7];
		if ((((startIndex + length) + 1) >= guidString.length) || (guidString.charAt((startIndex + length) + 1) != '}')) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidEndBrace");
			return {
				ret: false,
				p1: result

			};
		}
		if (((startIndex + length) + 1) != (guidString.length - 1)) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_ExtraJunkAtEnd");
			return {
				ret: false,
				p1: result

			};
		}
		return {
			ret: true,
			p1: result

		};
	}
	private static tryParseGuidWithNoStyle(guidString: string, result: Guid_GuidResult): { ret: boolean; p1?: Guid_GuidResult; } {
		let num2: number;
		let num3: number;
		let num4: number;
		let startIndex: number = 0;
		if (guidString.length != 32) {
			result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidInvLen");
			return {
				ret: false,
				p1: result

			};
		}
		for (let i: number = 0; i < guidString.length; i++) {
			let c: string = guidString.charAt(i);
			if ((c.charCodeAt(0) < '0'.charCodeAt(0)) || (c.charCodeAt(0) > '9'.charCodeAt(0))) {
				let ch2: string = c.toUpperCase();
				if ((ch2.charCodeAt(0) < 'A'.charCodeAt(0)) || (ch2.charCodeAt(0) > 'F'.charCodeAt(0))) {
					result.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidInvalidChar");
					return {
						ret: false,
						p1: result

					};
				}
			}
		}
		if (!((() => { let $ret = Guid.stringToInt(guidString.substr(startIndex, 8), -1, 4096, result.parsedGuid._a, result); result.parsedGuid._a = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		startIndex += 8;
		if (!((() => { let $ret = Guid.stringToShort(guidString.substr(startIndex, 4), -1, 4096, result.parsedGuid._b, result); result.parsedGuid._b = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		startIndex += 4;
		if (!((() => { let $ret = Guid.stringToShort(guidString.substr(startIndex, 4), -1, 4096, result.parsedGuid._c, result); result.parsedGuid._c = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		startIndex += 4;
		if (!((() => { let $ret = Guid.stringToInt(guidString.substr(startIndex, 4), -1, 4096, num2, result); num2 = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		startIndex += 4;
		if (!((() => { let $ret = Guid.stringToInt(guidString.substr(startIndex, 4), -1, startIndex, num3, result); num3 = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		startIndex += 4;
		if (!((() => { let $ret = Guid.stringToInt(guidString.substr(startIndex, 8), -1, startIndex, num4, result); num4 = $ret.p3; result = $ret.p4; return $ret.ret; })())) {
			return {
				ret: false,
				p1: result

			};
		}
		result.parsedGuid._d = <number>((num2 >> 8) & 255);
		result.parsedGuid._e = <number>(num2 & 255);
		num2 = <number>num3;
		result.parsedGuid._f = <number>((num2 >> 8) & 255);
		result.parsedGuid._g = <number>(num2 & 255);
		num2 = <number>num4;
		result.parsedGuid._h = <number>((num2 >> 24) & 255);
		result.parsedGuid._i = <number>((num2 >> 16) & 255);
		result.parsedGuid._j = <number>((num2 >> 8) & 255);
		result.parsedGuid._k = <number>(num2 & 255);
		return {
			ret: true,
			p1: result

		};
	}
	private static stringToShort(str: string, requiredLength: number, flags: number, result: number, parseResult: Guid_GuidResult): { ret: boolean; p3: number; p4?: Guid_GuidResult; } {
		let parsePos: number = 0;
		return {
			ret: ((() => { let $ret = Guid.stringToShort1(str, parsePos, requiredLength, flags, result, parseResult); parsePos = $ret.p1; result = $ret.p4; parseResult = $ret.p5; return $ret.ret; })()),
			p3: result,
			p4: parseResult

		};
	}
	private static stringToShort1(str: string, parsePos: number, requiredLength: number, flags: number, result: number, parseResult: Guid_GuidResult): { ret: boolean; p1?: number; p4: number; p5?: Guid_GuidResult; } {
		let num: number;
		result = 0;
		let flag: boolean = ((() => { let $ret = Guid.stringToInt1(str, parsePos, requiredLength, flags, num, parseResult); parsePos = $ret.p1; num = $ret.p4; parseResult = $ret.p5; return $ret.ret; })());
		result = <number>num;
		return {
			ret: flag,
			p1: parsePos,
			p4: result,
			p5: parseResult

		};
	}
	private static stringToInt(str: string, requiredLength: number, flags: number, result: number, parseResult: Guid_GuidResult): { ret: boolean; p3: number; p4?: Guid_GuidResult; } {
		let parsePos: number = 0;
		return {
			ret: ((() => { let $ret = Guid.stringToInt1(str, parsePos, requiredLength, flags, result, parseResult); parsePos = $ret.p1; result = $ret.p4; parseResult = $ret.p5; return $ret.ret; })()),
			p3: result,
			p4: parseResult

		};
	}
	private static stringToInt1(str: string, parsePos: number, requiredLength: number, flags: number, result: number, parseResult: Guid_GuidResult): { ret: boolean; p1?: number; p4: number; p5?: Guid_GuidResult; } {
		result = 0;
		let num: number = parsePos;
		try {
			if (requiredLength == -1) {
				let temp = str.length - parsePos;
				while (true) {
					if (((() => { let $ret = tryParseInt32_2(str.substr(parsePos, temp), NumberStyles.HexNumber, CultureInfo.invariantCulture, result); result = $ret.p3; return $ret.ret; })())) {
						break;
					}
					temp--;
				}
				parsePos += temp;
			} else {
				result = parseInt32_2(str.substr(parsePos, requiredLength), NumberStyles.HexNumber);
				parsePos += requiredLength;
			}
		}
		catch (exception2) {
			if (parseResult.throwStyle != Guid_GuidParseThrowStyle.None) {
				throw exception2;
			}
			parseResult.setFailure(exception2);
			return {
				ret: false,
				p1: parsePos,
				p4: result,
				p5: parseResult

			};
		}
		if (requiredLength != -1 && (parsePos - num) != requiredLength) {
			parseResult.setFailure1(Guid_ParseFailureKind.Format, "Format_GuidInvalidChar");
			return {
				ret: false,
				p1: parsePos,
				p4: result,
				p5: parseResult

			};
		}
		return {
			ret: true,
			p1: parsePos,
			p4: result,
			p5: parseResult

		};
	}
	private static isHexPrefix(str: string, i: number): boolean {
		return (((str.length > (i + 1)) && (str.charAt(i) == '0')) && (str.charAt(i + 1).toLowerCase() == 'x'));
	}
	static l_op_Inequality(a: Guid, b: Guid): boolean {
		return !(Guid.l_op_Equality(a, b));
	}
	static l_op_Inequality_Lifted(a: Nullable$1<Guid>, b: Nullable$1<Guid>): boolean {
		if (!a.hasValue) {
			return b.hasValue;
		} else if (!b.hasValue) {
			return true;
		}
		return Guid.l_op_Inequality(a.value, b.value);
	}
	static l_op_Equality(a: Guid, b: Guid): boolean {
		return a._a == b._a && a._b == b._b && a._c == b._c && a._d == b._d && a._e == b._e && a._f == b._f && a._g == b._g && a._h == b._h && a._i == b._i && a._j == b._j && a._k == b._k;
	}
	static l_op_Equality_Lifted(a: Nullable$1<Guid>, b: Nullable$1<Guid>): boolean {
		if (!a.hasValue) {
			return !b.hasValue;
		} else if (!b.hasValue) {
			return false;
		}
		return Guid.l_op_Equality(a.value, b.value);
	}
}

/**
 * @hidden 
 */
export class Guid_GuidResult extends ValueType {
	static $t: Type = markStruct(Guid_GuidResult, 'Guid_GuidResult');
	constructor() {
		super();
	}
	parsedGuid: Guid = new Guid();
	throwStyle: Guid_GuidParseThrowStyle = <Guid_GuidParseThrowStyle>0;
	m_failure: Guid_ParseFailureKind = <Guid_ParseFailureKind>0;
	m_failureMessageID: string = null;
	m_failureMessageFormatArgument: any = null;
	m_failureArgumentName: string = null;
	m_innerException: BaseError = null;
	init(canThrow: Guid_GuidParseThrowStyle): void {
		this.parsedGuid = Guid.empty;
		this.throwStyle = canThrow;
	}
	setFailure(nativeException: BaseError): void {
		this.m_failure = Guid_ParseFailureKind.NativeException;
		this.m_innerException = nativeException;
	}
	setFailure1(failure: Guid_ParseFailureKind, failureMessageID: string): void {
		this.setFailure3(failure, failureMessageID, null, null, null);
	}
	setFailure2(failure: Guid_ParseFailureKind, failureMessageID: string, failureMessageFormatArgument: any): void {
		this.setFailure3(failure, failureMessageID, failureMessageFormatArgument, null, null);
	}
	setFailure3(failure: Guid_ParseFailureKind, failureMessageID: string, failureMessageFormatArgument: any, failureArgumentName: string, innerException: BaseError): void {
		this.m_failure = failure;
		this.m_failureMessageID = failureMessageID;
		this.m_failureMessageFormatArgument = failureMessageFormatArgument;
		this.m_failureArgumentName = failureArgumentName;
		this.m_innerException = innerException;
		if (this.throwStyle != Guid_GuidParseThrowStyle.None) {
			throw this.getGuidParseException();
		}
	}
	getGuidParseException(): BaseError {
		switch (this.m_failure) {
			case Guid_ParseFailureKind.ArgumentNull: return new ArgumentNullException(0, this.m_failureArgumentName);
			case Guid_ParseFailureKind.Format: return new FormatException(0);
			case Guid_ParseFailureKind.FormatWithParameter: return new FormatException(0);
			case Guid_ParseFailureKind.NativeException: return this.m_innerException;
			case Guid_ParseFailureKind.FormatWithInnerException: return new FormatException(2, "The format of the Guid was incorrect.", this.m_innerException);
		}

		return new FormatException(0);
	}
}


