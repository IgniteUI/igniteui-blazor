import { Base, Type, markType } from "./type";
import { isNaN_, isInfinity } from "./number";

/**
 * @hidden 
 */
export class MathUtil extends Base {
	static $t: Type = markType(MathUtil, 'MathUtil');
	static readonly pHI: number = (1 + Math.sqrt(5)) / 2;
	static readonly sQRT2: number = Math.sqrt(2);
	static asinh(angle: number): number {
		return Math.log(angle + Math.sqrt(angle * angle + 1));
	}
	static closeEnough(value1: number, value2: number): boolean {
		if (Math.abs(value1 - value2) < 1E-05) {
			return true;
		}
		return false;
	}
	static numberToMinDecimalsString(number_: number): string {
		return number_.toString();
	}
	static hypot(x: number, y: number): number {
		return Math.sqrt(x * x + y * y);
	}
	static sqr(x: number): number {
		return x * x;
	}
	static isPlotable(value: number): boolean {
		return !isNaN_(value) && !isInfinity(value);
	}
	static clamp(value: number, minimum: number, maximum: number): number {
		return Math.min(maximum, Math.max(minimum, value));
	}
	static radians(degrees: number): number {
		return Math.PI * degrees / 180;
	}
	static readonly degreeAsRadian: number = Math.PI / 180;
	static degrees(radians: number): number {
		return 180 * radians / Math.PI;
	}
	static min3(v1: number, v2: number, v3: number): number {
		return Math.min(v1, Math.min(v2, v3));
	}
	static max3(v1: number, v2: number, v3: number): number {
		return Math.max(v1, Math.max(v2, v3));
	}
	static min(...a: number[]): number {
		let min: number = a[0];
		for (let i: number = 1; i < a.length; ++i) {
			min = Math.min(min, a[i]);
		}
		return min;
	}
	static minArray(a: number[]): number {
		let min: number = a[0];
		for (let i: number = 1; i < a.length; ++i) {
			min = Math.min(min, a[i]);
		}
		return min;
	}
	static max(...a: number[]): number {
		let max: number = a[0];
		for (let i: number = 1; i < a.length; ++i) {
			max = Math.max(max, a[i]);
		}
		return max;
	}
	static maxArray(a: number[]): number {
		let max: number = a[0];
		for (let i: number = 1; i < a.length; ++i) {
			max = Math.max(max, a[i]);
		}
		return max;
	}
	static sumArray(a: number[]): number {
		if (a.length == 0) {
			return 0;
		}
		let sum = 0;
		for (let i = 0; i < a.length; i++) {
			let value = a[i];
			if (isNaN_(value)) {
				continue;
			}
			if (isInfinity(value)) {
				continue;
			}
			sum += value;
		}
		return sum;
	}
	static averageArray(a: number[]): number {
		if (a.length == 0) {
			return 0;
		}
		let sum = 0;
		let count = 0;
		for (let i = 0; i < a.length; i++) {
			let value = a[i];
			if (isNaN_(value)) {
				continue;
			}
			if (isInfinity(value)) {
				continue;
			}
			sum += value;
			count++;
		}
		return sum / count;
	}
}


