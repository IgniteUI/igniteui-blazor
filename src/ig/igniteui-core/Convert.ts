import { Base, IFormatProvider, IFormatProvider_$type, IConvertible, IConvertible_$type, typeCast, Date_$type, Type, markType, typeGetValue, Enum } from "./type";
import { CultureInfo } from "./culture";
import { NotImplementedException } from "./NotImplementedException";
import { truncate, isNaN_ } from "./number";
import { unwrapNullable } from "./nullable";
import { b64toUint8Array, uint8ArraytoB64 } from "./stringExtended";
import { ConvertUtil } from "./convertUtil";
import { dateMinValue, dateFromTicks } from "./date";
import { dateParse } from "./dateExtended";
import { parseNumber } from "./numberExtended";

/**
 * @hidden 
 */
export class Convert extends Base {
	static $t: Type = markType(Convert, 'Convert');
	static toDouble5(value: number): number {
		return <number>value;
	}
	static toDouble1(value: number): number {
		return <number>value;
	}
	static toDouble(value: number): number {
		return <number>value;
	}
	static toDouble2(value: number): number {
		return <number>value;
	}
	static toDecimal(value: number): number {
		return <number>value;
	}
	static toDecimal3(value: number): number {
		return <number>value;
	}
	static toDecimal1(value: number): number {
		return <number>value;
	}
	static toInt32(value: number): number {
		if (value >= 0) {
			let ret: number = <number>truncate(Math.floor(<number>value));
			if (ret != value) {
				let diff1: number = value - ret;
				let diff2: number = Math.ceil(value) - value;
				if (diff1 > diff2 || ((diff1 == diff2) && (ret & 1) > 0)) {
					ret++;
				}
			}
			return ret;
		} else {
			let ret1: number = <number>truncate(Math.ceil(<number>value));
			if (ret1 != value) {
				let diff11: number = ret1 - value;
				let diff21: number = value - Math.floor(value);
				if (diff11 > diff21 || ((diff11 == diff21) && (ret1 & 1) > 0)) {
					ret1--;
				}
			}
			return ret1;
		}
	}
	static toInt322(value: string): number {
		return parseInt(value);
	}
	static toDouble3(value: any): number {
		return Convert.toDouble4(value, CultureInfo.currentCulture);
	}
	static toDouble4(value: any, provider: IFormatProvider): number {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return 0;
		}
		let result = <number>(+valueResolved);
		if (isNaN_(result)) {
			return ConvertUtil.toDouble((<IConvertible><any>valueResolved), provider);
		}
		return result;
	}
	static toInt321(value: any): number {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return 0;
		}
		let result = <number>(+valueResolved);
		if (isNaN_(result)) {
			return ConvertUtil.toInt32((<IConvertible><any>valueResolved), CultureInfo.currentCulture);
		}
		return result;
	}
	static toInt64(value: any): number {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return 0;
		}
		let result = <number>(+valueResolved);
		if (isNaN_(result)) {
			return ConvertUtil.toInt64((<IConvertible><any>valueResolved), CultureInfo.currentCulture);
		}
		return result;
	}
	static toDecimal2(value: any): number {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return 0;
		}
		let result = <number>(+valueResolved);
		if (isNaN_(result)) {
			return ConvertUtil.toDecimal((<IConvertible><any>valueResolved), CultureInfo.currentCulture);
		}
		return result;
	}
	static toByte(value: boolean): number {
		return <number>(value ? 1 : 0);
	}
	static toByte1(value: any): number {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return 0;
		}
		let result = <number>(+valueResolved);
		if (isNaN_(result)) {
			return ConvertUtil.toByte((<IConvertible><any>valueResolved), CultureInfo.currentCulture);
		}
		return result;
	}
	static toBoolean(value: any): boolean {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return false;
		}
		return <boolean>(!!valueResolved);
	}
	static toDateTime(value: any): Date {
		let valueResolved = <any>(typeGetValue(unwrapNullable(value)));
		if (valueResolved == null) {
			return dateMinValue();
		}
		if (typeCast<Date>(Date_$type, valueResolved) !== null) {
			return <Date>valueResolved;
		}
		let result = <number>(+valueResolved);
		if (!isNaN_(result)) {
			return dateFromTicks(result);
		}
		return dateParse(valueResolved.toString());
	}
	static toChar(value: number): string {
		return <string>String.fromCharCode(value);
	}
	static toChar1(value: number): string {
		return <string>String.fromCharCode(value);
	}
	static toDouble6(value: string, provider: IFormatProvider): number {
		return parseNumber(value, <CultureInfo>provider);
	}
	static toUInt16(value: boolean): number {
		return <number>(value ? 1 : 0);
	}
	static toBoolean1(value: number): boolean {
		return value != 0;
	}
	static toUInt32(value: number): number {
		return value;
	}
	static fromBase64String(s: string): number[] {
		return <number[]>(b64toUint8Array(s));
	}
	static toBase64String(inArray: number[]): string {
		return <string>(uint8ArraytoB64(inArray));
	}
	static toByte2(value: string, fromBase: number): number {
		throw new NotImplementedException(0);
	}
}


