import { Type, IEnumerable$1 } from "./type";
import { CultureInfo } from "./culture";
import { StringComparison, CompareOptions, stringCompareTo, stringEscapeRegExp } from "./string"; 
import { intToString1, numberToString2 } from "./numberExtended";
import { IFormatProvider } from "./type";
import { getEnumerator } from "./type";

export const enum StringSplitOptions {
    None = 0,
    RemoveEmptyEntries = 1
}


export function startsWith1(target: string, s: string, comparisonType: StringComparison) {
    if (target.length < s.length) {
        return false;
    }

    return stringCompare1(target.slice(0, s.length), s, comparisonType || 0) === 0;
};
export function endsWith1(target: string, s: string, comparisonType: StringComparison) {
    if (target.length < s.length) {
        return false;
    }

    return stringCompare1(target.slice(-s.length), s, comparisonType || 0) === 0;
};
export function isLower(target: string): boolean {
    return target.toLowerCase() === target;
}
const letter = /^\p{L}/u;
const letterOrDigit = /^[\p{L}\p{Nd}]/u;
const decimalDigit = /^\p{Nd}/u;
const number = /^\p{N}/u;

export function isLetterOrDigit(target: string): boolean {
    return letterOrDigit.test(target);
}
export function isLetter(c: string): boolean {
    return letter.test(c);
}

export function isDigit1(c: string, index: number): boolean {
    return isDigit(c[ index ]);
}

export function isDigit(c: string): boolean {
    return decimalDigit.test(c);
}

export function isNumber(c: string): boolean {
    return number.test(c);
}

export function padLeft(target: string, len: number, c: string): string {
    var s = target;
    c = c || " ";
    while (s.length < len) {
        s = c + s;
    }
    return s;
}

export function reverse(target: string): string {
    /* Inverts the order of the characters in a string.
        returnType="string" Returns a new inverted string.
    */
    var s = "";
    for (var i = target.length - 1; i >= 0; i--) {
        s += target[ i ];
    }
    return s;
};

export function padRight(target: string, len: number, c: string): string {
    var s = target;
    c = c || " ";
    while (s.length < len) {
        s += c;
    }
    return s;
};

export function indexOfAny(target: string, chars: string[]) {
    var s = target.toString();

    for (var i = 0; i < s.length; i++) {
        if (chars.indexOf(s[ i ]) > -1) {
            return i;
        }
    }

    return -1;
}


export function lastIndexOfAny(target: string, chars: string[]) {

    var s = target.toString();

    for (var i = s.length - 1; i >= 0; i--) {
        if (chars.indexOf(s[ i ]) > -1) {
            return i;
        }
    }

    return -1;
}

export function stringFormat(format: string, ...rest:any[]): string {
    return stringFormat1(format, ...rest);
};

export function stringFormat1(format: string, ...args: any[]): string {
    return stringFormat2(CultureInfo.currentCulture, format, ...args);
};

export function stringFormat2(provider: any, format: string, ...args: any[]): string {
    // TODO: what is going on with provider here??
    // TODO: Use the provider somehow
    return format.replace(/{(\d+)(?::)?([^}]*)?}/g, function (match, number, format) {
        var arg = args[ number ];

        if (arg === void 0) {
            return match;
        }

        if (arg === null) {
            return "";
        }

        if (format) {
            if (format[ 0 ] === "X") {
                return intToString1(arg, format, provider);
            } else {
                return numberToString2(arg, format, provider);
            }
        }

        return arg;
    });
};

export function stringCompare1(strA: string, strB: string, comparisonType: StringComparison) {

    if (!strA) {
        return !strB ? 0 : -1;
    } else if (!strB) {
        return 1;
    }

    // TODO: Make sure this is right
    switch (comparisonType) {
        case StringComparison.CurrentCulture:
            return CultureInfo.currentCulture
                .compareInfo.compare4(strA, strB);
        case StringComparison.CurrentCultureIgnoreCase:
            return CultureInfo.currentCulture.compareInfo
                .compare4(strA.toLowerCase(), strB.toLowerCase());
        case StringComparison.InvariantCulture:
        case StringComparison.Ordinal:
            return stringCompareTo(strA, strB);
        case StringComparison.InvariantCultureIgnoreCase:
        case StringComparison.OrdinalIgnoreCase:
            return stringCompareTo(strA.toLowerCase(), strB.toLowerCase());
        default:
            break;
    }

    return 0;
};

export function stringCompare2(strA: string, strB: string, culture: CultureInfo, options: CompareOptions) {
    return culture.compareInfo.compare5(strA, strB, options);
};

export function stringCompare3(strA: string, indexA: number, strB: string, indexB: number, length: number) {
    var v1 = strA.substr(indexA, length);
    var v2 = strB.substr(indexB, length);
    return stringCompare1(v1, v2, StringComparison.CurrentCulture);
};

export function stringCompareOrdinal(strA: string, indexA: number, strB: string, indexB: number, length: number) {
    var v1 = strA.substr(indexA, length);
    var v2 = strB.substr(indexB, length);
    return stringCompare1(v1, v2, StringComparison.Ordinal);
};

export function stringEquals1(strA: string, strB: string, comparisonType: StringComparison) {
    return stringCompare1(strA, strB, comparisonType) == 0;
};

export function stringInsert(str: string, index: number, value: string): string {
    return str.substr(0, index) + value + str.substr(index);
};

// https://developer.mozilla.org/en-US/docs/Web/API/WindowBase64/Base64_encoding_and_decoding
export function b64toUint8Array(b64Data: string, nBlocksSize?: number) {
    /*jslint bitwise: true */
    function b64ToUint6(nChr: number) {

        return nChr > 64 && nChr < 91 ?
            nChr - 65
            : nChr > 96 && nChr < 123 ?
            nChr - 71
            : nChr > 47 && nChr < 58 ?
            nChr + 4
            : nChr === 43 ?
            62
            : nChr === 47 ?
            63
            :
            0;

    }

    var
        sB64Enc = b64Data.replace(/[^A-Za-z0-9\+\/]/g, ""), nInLen = sB64Enc.length,
        nOutLen = nBlocksSize ?
            Math.ceil((nInLen * 3 + 1 >> 2) / nBlocksSize) * nBlocksSize :
            nInLen * 3 + 1 >> 2, taBytes;

    if (typeof (<any>window).Uint8Array === "function") {
        taBytes = new Uint8Array(nOutLen);
    } else {
        taBytes = new Array(nOutLen);
    }

    for (var nMod3, nMod4, nUint24 = 0, nOutIdx = 0, nInIdx = 0; nInIdx < nInLen; nInIdx++) {
        nMod4 = nInIdx & 3;
        nUint24 |= b64ToUint6(sB64Enc.charCodeAt(nInIdx)) << 18 - 6 * nMod4;
        if (nMod4 === 3 || nInLen - nInIdx === 1) {
            for (nMod3 = 0; nMod3 < 3 && nOutIdx < nOutLen; nMod3++, nOutIdx++) {
                taBytes[ nOutIdx ] = nUint24 >>> (16 >>> nMod3 & 24) & 255;
            }
            nUint24 = 0;

        }
    }

    return taBytes;
};

// https://developer.mozilla.org/en-US/docs/Web/API/WindowBase64/Base64_encoding_and_decoding
export function uint8ArraytoB64(aBytes: number[]) {
    /*jslint bitwise: true */
    function uint6ToB64(nUint6: number) {

        return nUint6 < 26 ?
            nUint6 + 65
            : nUint6 < 52 ?
            nUint6 + 71
            : nUint6 < 62 ?
            nUint6 - 4
            : nUint6 === 62 ?
            43
            : nUint6 === 63 ?
            47
            :
            65;

    }

    var nMod3 = 2, sB64Enc = "";

    for (var nLen = aBytes.length, nUint24 = 0, nIdx = 0; nIdx < nLen; nIdx++) {
        nMod3 = nIdx % 3;
        if (nIdx > 0 && (nIdx * 4 / 3) % 76 === 0) { sB64Enc += "\r\n"; }
        nUint24 |= aBytes[ nIdx ] << (16 >>> nMod3 & 24);
        if (nMod3 === 2 || aBytes.length - nIdx === 1) {
            sB64Enc += String.fromCharCode(uint6ToB64(nUint24 >>> 18 & 63),
                uint6ToB64(nUint24 >>> 12 & 63),
                uint6ToB64(nUint24 >>> 6 & 63),
                uint6ToB64(nUint24 & 63));
            nUint24 = 0;
        }
    }

    return sB64Enc.substr(0, sB64Enc.length - 2 + nMod3) +
        (nMod3 === 2 ? "" : nMod3 === 1 ? "=" : "==");

}
    
export function stringSplit(value: string, separators: string[], options: StringSplitOptions): string[] {
    var r = "",
        i;
    for (i = 0; i < separators.length; i++) {

        if (i !== 0) {
            r += "|";
        }

        r += stringEscapeRegExp(separators[ i ]);
    }

    var result = value.split(new RegExp(r));

    for (i = result.length - 1; i >= 0; i--) {
        /*jslint bitwise: true */
        if ((result[ i ].length === 0 &&
            (options & StringSplitOptions.RemoveEmptyEntries)) ||
                separators.indexOf(result[ i ]) > -1) {
            result.splice(i, 1);
        }
    }

    return result;
};

export function trim(target: string, ...rest: string[]): string {
    if (rest.length == 0)
        return target.trim();

    var parts = stringSplit(target, rest, StringSplitOptions.None);
    return parts.join("");
}

export function trimStart(target: string, ...rest: any[]): string {
    if (target.length === 0) {
        return target;
    }

    var args: string[]; 
    if (rest.length == 0)
        args = [" "];
    else if (rest.length == 1 && Array.isArray(rest[0])) {
        args = <string[]><any>rest[0];
    } else {
        args = <string[]>rest;
    }

	var i = 0;
	for (; i < target.length && args.indexOf(target.charAt(i)) > -1; i++) { }
	return target.substring(i);
}

export function	trimEnd(target: string, ...rest: any[]): string {
    if (target.length === 0) {
        return target;
    }

    var args: string[];
    if (rest.length == 0)
        args = [" "];
    else if (rest.length == 1 && Array.isArray(rest[0])) {
        args = <string[]><any>rest[0];
    } else {
        args = <string[]>rest;
    }

	var i = target.length - 1;
	for (; i >= 0 && args.indexOf(target.charAt(i)) > -1; i--) { }
	return target.substring(0, i + 1);
}