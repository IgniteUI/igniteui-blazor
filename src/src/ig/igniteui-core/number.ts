export function log10(value: number): number {
		return Math.log(value) / Math.log(10);
}
export function toDouble(value: number): number {
		return +value;
}
export let toDecimal = toDouble;
export function compareTo(value: number, other: number) {
	if (value == other) {
		return 0;
	}

	if (value < other) {
			return -1;
	}
	return 1;
}
export function isInfinity(value: number): boolean {
		return !isFinite(value) && !isNaN_(value);
}
export function intDivide(value: number, divisor: number): number {
	var result = value / divisor;
	return truncate(result);
}
export function truncate(val: number): number {
	if (val >= 0) {
		return Math.floor(val);
	} else {
		return Math.ceil(val);
	}
}
export function logBase(n: number, n2: number): number {
	return Math.log(n) / Math.log(n2);
}
export function tryParseNumber(s: string, v?: number) : { p1: number, ret: boolean } {
	var value = Number(s);
	if (value !== null && isFinite(value) && s.trim().length !== 0) {
		return {
			p1: value,
			ret: true
		};
	} else {
		return {
			p1: 0,
			ret: false
		};
	}
}
export function isNegativeInfinity(v: number) {
	return v == Number.NEGATIVE_INFINITY;
}
export function isPositiveInfinity(v: number) {
	return v == Number.POSITIVE_INFINITY;
}
export function isNaN_(v: number) {
	return v !== v; // http://us6.campaign-archive1.com/?u=2cc20705b76fa66ab84a6634f&id=43bf7f05e9
}
