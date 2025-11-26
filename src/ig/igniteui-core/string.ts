import { IEnumerable$1, Type, FormatException } from "./type";
import { getEnumerator } from "./type";
import { EnumUtil } from './type';

export const enum CompareOptions {
    None = 0,
    IgnoreCase = 1,
    IgnoreNonSpace = 2,
    IgnoreSymbols = 4,
    IgnoreKanaType = 8,
    IgnoreWidth = 16,
    OrdinalIgnoreCase = 268435456,
    StringSort = 536870912,
    Ordinal = 1073741824
}

export const enum StringComparison {
    CurrentCulture = 0,
    CurrentCultureIgnoreCase = 1,
    InvariantCulture = 2,
    InvariantCultureIgnoreCase = 3,
    Ordinal = 4,
    OrdinalIgnoreCase = 5
}



export function stringStartsWith(str: string, check: string): boolean {
    return str.indexOf(check) == 0;
}
export function stringEndsWith(str: string, check: string): boolean {
    let ind = str.lastIndexOf(check);
    return ind >= 0 && ind == str.length - check.length;
}
export function stringContains(str: string, check: string): boolean {
    return str.indexOf(check) != -1;
}

export function stringCreateFromCharArray(charArray: string[]): string {
    return charArray.join("");
}

export function stringCreateFromChar(char: string, count: number): string {
    var ret = "";
    for (var i = 0; i < count; i++) {
        ret = ret + char;
    }

    return ret;
}

export function stringCreateFromCharArraySlice(charArray: string[], start: number, length: number): string {
    var ret = "";
    for (var i = 0; i < length; i++) {
        ret = ret + charArray[start + i];
    }

    return ret;
};

export function stringToCharArray(target: string): string[] {
    return target.split("");
}

export function stringCopyToCharArray(source: string, sourceIndex: number, destination: string[], destinationIndex: number, count: number) {
    for (var i = destinationIndex; i < destinationIndex + count; i++) {
        destination[i] = source.charAt(sourceIndex + i - destinationIndex);
    }
};

export function stringIsDigit(str: string, index?: number): boolean {
    index = index || 0;
    var ch = str.charAt(index);
    if (ch >= "0" && ch <= "9") {
        return true;
    }

    return false;
};

export function charMaxValue(): string {
    return "\uffff";
}

export function charMinValue(): string {
    return "\u0000";
}

// static toDateTime(target: string) {
// 	var result = new Date(target);
// 	if (!isNaN(+result)) {
// 		return result;
// 	}

// 	// TODO: Cache this regex?
// 	if (/^((([0-9]{1,4})\s*(\s+((a|p)m?)\s*))|(([0-9]{1,4})\s*:\s*([0-9]?[0-9])\s*(:\s*([0-9]?[0-9])\s*(.\s*([0-9]{0,4})[0-9]*\s*)?)?(\s+((a|p)m?)\s*)?)|(\s*([0-9]?[0-9])\s*:\s*([0-9]?[0-9])\s*.\s*([0-9]{0,4})[0-9]*\s*(\s+((a|p)m?)\s*)?))$/i.test(target)) {
// 		// The string can be a time string only, in which case we should return today at that time.
// 		return new Date(new Date().toDateString() + " " + target);
// 	}

// 	throw new FormatException(1, "The string cannot be converted to a date");
// }
// static toDecimal(target: string) {
// 	var result = +target;

// 	if (isNaN(result)) {
// 		new FormatException(1, "The string cannot be converted to a number");
// 	}

// 	return result;
// }
export function stringToString1(target: string): string {
    return target.toString();
}

export function stringRemove(target: string, index: number, count?: number): string {
    if (!count || ((index + count) > target.length)) {
        return target.substr(0, index);
    }
    return target.substr(0, index) + target.substr(index + count);
}

export function stringCompareTo(target: string, other: string) {
    if (target == other) {
        return 0;
    }
    if (target < other) {
        return -1;
    }
    return 1;
}
export let stringCompare = stringCompareTo;

export function stringIsNullOrEmpty(target: string) { return !target || target.length < 1; }
export function stringIsNullOrWhiteSpace(target: string) { return !target || target.trim().length < 1; }
export function stringEmpty() { return ""; }
export function stringEquals(target: string, other: string) { return target == other }

export function stringEscapeRegExp(str: string): string {
    return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
}

export function createGuid() {
    /*jslint bitwise: true */
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }

    return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() +
        S4() + S4()).toLowerCase();
}

export function stringConcat(...s: string[]) {
    return String.prototype.concat(...s);
}

export function stringReplace(str: string, oldValue: string, newValue: string): string {
    return str.replace(new RegExp(stringEscapeRegExp(oldValue), "g"), newValue);
}

export function stringJoin(sep: string, ...vals: string[]): string {
    return vals.join(sep);
}

export function stringJoin1<T>($t: Type, sep: string, vals: IEnumerable$1<T>) {
    var result;
    var en = getEnumerator(vals);
    while (en.moveNext()) {
        var v = en.current.toString();

        if (result === undefined) {
            result = v;
        } else {
            result += sep + v;
        }
    }

    return result;
}

export function stringToString$1<T>($t: Type, v: any) {
    if (v !== null && $t) {
        if ($t.isNullable) {
            $t = <Type>$t.typeArguments[0];
        }

        if ($t.isEnumType) {
            return EnumUtil.getName($t, v);
        }
    }

    return v.toString();
}

export function stringToLocaleLower(str: string, locale?: any): string {
    if (locale == null || locale.name == null)
        return str.toLocaleLowerCase();

    return (<any>str).toLocaleLowerCase(locale.name);
}

export function stringToLocaleUpper(str: string, locale?: any): string {
    if (locale == null || locale.name == null)
        return str.toLocaleUpperCase();

    return (<any>str).toLocaleUpperCase(locale.name);
}
