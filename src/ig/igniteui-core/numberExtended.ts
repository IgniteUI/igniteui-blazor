import { CultureInfo } from "./culture";
import { FormatException } from "./type";

export const enum NumberStyles {
		None = 0,
		AllowLeadingWhite = 1,
		AllowTrailingWhite = 2,
		AllowLeadingSign = 4,
		Integer = 7,
		AllowTrailingSign = 8,
		AllowParentheses = 16,
		AllowDecimalPoint = 32,
		AllowThousands = 64,
		Number = 111,
		AllowExponent = 128,
		Float = 167,
		AllowCurrencySymbol = 256,
		Currency = 383,
		Any = 511,
		AllowHexSpecifier = 512,
		HexNumber = 515
}

export function ieeeRemainder(a: number, b: number): number {
    var r = Math.abs(a % b);
    if (isNaN(r) || r == b || r <= Math.abs(b) / 2.0) {
        return r;
    } else {
        return Math.sign(a) * (r - b);
    }
};
export function numberToString1(value: number, provider: CultureInfo): string {
    return value.toLocaleString(provider.name, { useGrouping: false }); // TODO: Figure out how to use the provider correctly here
}

export function tryParseNumber1(s: string, style: NumberStyles, provider: any, v?: number): { p3: number, ret: boolean } {
    // TODO: had to change the provider from CultureInfo to any but that's not ideal. see what we should do here
    var value,
        i,
        currentCharCode;

    provider = provider || CultureInfo.currentCulture;

    /*jslint bitwise: true */
    if ((style & NumberStyles.AllowLeadingWhite) !== 0) {
        var index = 0;
        while (index < s.length && s[index] === ' ') {
            index++;
        }
        s = s.substring(index)
    }

    if ((style & NumberStyles.AllowTrailingWhite) !== 0) {
        var index = s.length - 1;
        while (index >= 0 && s[index] === ' ') {
            index--;
        }
        s = s.substring(0, index + 1);
    }

    if (s.length != s.trim().length) {
        return {
            p3: 0,
            ret: false
        };
    }

    var numberFormat = provider.numberFormat;

    if (style & NumberStyles.AllowCurrencySymbol) {
        // TODO: Use the locale specific symbol from the provider here
        if (s[ 0 ] == "$") {
            s = s.slice(1);
        }
    }

    var multiplier = 1;
    var hadParentheses = false;
    if (style & NumberStyles.AllowParentheses) {
        if (s[ 0 ] == "(" && s[ s.length - 1 ] == ")") {
            hadParentheses = true;
            multiplier *= -1;
            s = s.slice(1, -1);
        }
    }

    if (style & NumberStyles.AllowCurrencySymbol) {
        // TODO: Use the locale specific symbol from the provider here
        if (s[ 0 ] == "$") {
            s = s.slice(1);
        }
    }

    if (style & NumberStyles.AllowLeadingSign) {
        var positiveSign = numberFormat.positiveSign;
        var negativeSign = numberFormat.negativeSign;
        if (s[ 0 ] == positiveSign || s[ 0 ] == negativeSign) {

            if (hadParentheses) {
                return {
                    p3: 0,
                    ret: false
                };
            }

            if (s[ 0 ] == negativeSign) {
                multiplier *= -1;
            }

            s = s.slice(1);
        }
    }

    if (style & NumberStyles.AllowTrailingSign) {
        // TODO
    }

    if (style & NumberStyles.AllowDecimalPoint) {

        if (style & NumberStyles.AllowExponent) {
            // TODO
        }

        if (style & NumberStyles.AllowThousands) {
            var decimalSeparator = numberFormat.numberDecimalSeparator;
            var groupSeparator = numberFormat.numberGroupSeparator;

            var hitDecimalSeparator = false;
            for (i = 0; i < s.length; i++) {
                switch (s[ i ]) {
                    case groupSeparator:
                        if (hitDecimalSeparator) {
                            return {
                                p3: 0,
                                ret: false
                            };
                        }

                        s = s.slice(0, i) + s.slice(i + 1);
                        i--;
                        break;

                    case decimalSeparator:
                        hitDecimalSeparator = true;
                        if (decimalSeparator != ".") {
                            s = s.slice(0, i) + "." + s.slice(i + 1);
                        }
                        break;
                }
            }
        }

        value = Number(s);

        if (value !== null && isFinite(value) && s.trim().length !== 0) {
            return {
                p3: value * multiplier,
                ret: true
            };
        }
    } else {
        var zeroCharCode = "0".charCodeAt(0);
        var nineCharCode = "9".charCodeAt(0);

        value = 0;

        if (style & NumberStyles.AllowHexSpecifier) {
            var aCharCode = "a".charCodeAt(0);
            var fCharCode = "f".charCodeAt(0);
            var ACharCode = "A".charCodeAt(0);
            var FCharCode = "F".charCodeAt(0);

            for (i = 0; i < s.length; i++) {
                value *= 16;

                currentCharCode = s[ i ].charCodeAt(0);
                if (zeroCharCode <= currentCharCode && currentCharCode <= nineCharCode) {
                    value += (currentCharCode - zeroCharCode);
                } else if (aCharCode <= currentCharCode && currentCharCode <= fCharCode) {
                    value += (currentCharCode - aCharCode) + 10;
                } else if (ACharCode <= currentCharCode && currentCharCode <= FCharCode) {
                    value += (currentCharCode - ACharCode) + 10;
                } else {
                    return {
                        p3: 0,
                        ret: false
                    };
                }
            }
        } else {
            for (i = 0; i < s.length; i++) {
                value *= 10;

                currentCharCode = s[ i ].charCodeAt(0);
                if (zeroCharCode <= currentCharCode && currentCharCode <= nineCharCode) {
                    value += (currentCharCode - zeroCharCode);
                } else {
                    return {
                        p3: 0,
                        ret: false
                    };
                }
            }
        }

        return {
            p3: value * multiplier,
            ret: true
        };
    }

    return {
        p3: 0,
        ret: false
    };
}

export function parseNumber(s: string, provider: CultureInfo) {
    return parseNumber1(s, 231 as NumberStyles, provider);
}

export function parseNumber1(s: string, style: NumberStyles, provider?: CultureInfo) {
    var r = tryParseNumber1(s, style, provider);
    if (!r.ret) {
        throw new FormatException(1, "Incorrect number format");
    }

    return r.p3;
}

export function parseInt8_1(s: string, provider?: CultureInfo): number { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -128, 127);
};

export function parseInt8_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -128, 127, style);
};

export function parseInt16_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -32768, 32767);
};

export function parseInt16_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -32768, 32767, style);
};

export function parseInt32_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -2147483648, 2147483647);
};

export function parseInt32_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -2147483648, 2147483647, style);
};

export function parseInt64_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -9223372036854775808, 9223372036854775807);
};

export function parseInt64_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, -9223372036854775808, 9223372036854775807, style);
};

export function parseUInt8_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 255);
};

export function parseUInt8_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 255, style);
};

export function parseUInt16_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 65535);
};

export function parseUInt16_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 65535, style);
};

export function parseUInt32_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 4294967295);
};

export function parseUInt32_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 4294967295, style);
};

export function parseUInt64_1(s: string, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 18446744073709551615);
};

export function parseUInt64_2(s: string, style: NumberStyles, provider?: CultureInfo) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return parseIntCore(s, provider, 0, 18446744073709551615, style);
};

export function parseIntCore(s: string, provider: CultureInfo, min: number, max: number, style: NumberStyles = NumberStyles.Integer) {
    var r = tryParseIntCore(s, provider, min, max, style);

    if (!r.ret) {
        throw new FormatException(1, "Incorrect number format");
    }

    return r.p3;
};

export function tryParseInt8_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, -128, 127);
};

export function tryParseInt8_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, -128, 127, style);
};

export function tryParseInt16_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, -32768, 32767);
};

export function tryParseInt16_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, -32768, 32767, style);
};

export function tryParseInt32_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, -2147483648, 2147483647);
};

export function tryParseInt32_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, -2147483648, 2147483647, style);
};

export function tryParseInt64_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, -9223372036854775808, 9223372036854775807);
};

export function tryParseInt64_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, -9223372036854775808, 9223372036854775807, style);
};

export function tryParseUInt8_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, 0, 255);
};

export function tryParseUInt8_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, 0, 255, style);
};

export function tryParseUInt16_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, 0, 65535);
};

export function tryParseUInt16_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, 0, 65535, style);
};

export function tryParseUInt32_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, 0, 4294967295);
};

export function tryParseUInt32_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, 0, 4294967295, style);
};

export function tryParseUInt64_1(s: string, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, null, 0, 18446744073709551615);
};

export function tryParseUInt64_2(s: string, style: NumberStyles, provider?: CultureInfo, value?: number) { // jscs:ignore requireCamelCaseOrUpperCaseIdentifiers
    return tryParseIntCore(s, provider, 0, 18446744073709551615, style);
};

export function tryParseIntCore(s: string, provider: CultureInfo, min: number, max: number, style: NumberStyles = NumberStyles.Integer): { p1: number, p3: number, ret: boolean } {
    /*jshint eqnull:true */
    /*jslint bitwise: true */
    //style = style != null ? style : NumberStyles.integer; // Don't use || here, because 0 could be a valid style
    provider = provider || CultureInfo.currentCulture;

    var r = tryParseNumber1(s, style, provider);

    if ((style & NumberStyles.AllowHexSpecifier) && max < r.p3) {
        r.p3 -= (-min * 2);
    }

    if (!r.ret || r.p3 < min || max < r.p3 || r.p3 % 1 !== 0) {
        return {
            p1: 0,
            p3: 0,
            ret: false
        };
    }

    let ret = {
        p1: r.p3,
        p3: r.p3,
        ret: r.ret
    }

    return ret;
}

export function numberToString(number: number, provider?: any): string {
    return numberToString2(number, "G", provider);
};

let gFormatOptions = { useGrouping: false, maximumSignificantDigits: 15 };
let zeroFormatOptions = {
    useGrouping: false,
    maximumSignificantDigits: 15,
    maximumFractionDigits: 0
};

export function numberToString2(number: number, format: string, provider?: any): string {
    // TODO: had to change the provider from CultureInfo to any but that's not ideal. see what we should do here
    provider = provider || CultureInfo.currentCulture;

    switch (format) {
        case "G":
            return number.toLocaleString(provider.name, gFormatOptions);

        case "R":
        case "r":
            return number.toString()
                .replace(".", provider.numberFormat.numberDecimalSeparator);
    }

    if (format.match(/[0\#\.]+/)) {
        var isValid = true;
        var formatIndexOfDecimalSeparator = format.indexOf(".");
        var decimalFormat = formatIndexOfDecimalSeparator == -1 ? "" :
            format.substring(formatIndexOfDecimalSeparator + 1);
        var numberString = number.toFixed(decimalFormat.length).toString();
        var numberIndexOfDecimalSeparator = numberString.indexOf(".");
        var integralPart = numberIndexOfDecimalSeparator == -1 ? numberString :
            numberString.substring(0, numberIndexOfDecimalSeparator);
        var integralFormat = formatIndexOfDecimalSeparator == -1 ? format :
            format.substring(0, formatIndexOfDecimalSeparator);
        while (integralFormat.length < integralPart.length) {
            integralFormat = "0" + integralFormat;
        }
        while (integralPart.length < integralFormat.length) {
            integralPart = "0" + integralPart;
        }
        var formattedIntegralPart = "";
        var digit;
        for (var ii = integralFormat.length - 1; ii >= 0; ii--) {
            if (integralFormat[ii] == "0") {
                formattedIntegralPart = integralPart[ii] + formattedIntegralPart;
            } else if (integralFormat[ii] == "#") {
                digit = integralPart.substring(0, ii + 1).match(/[1-9]/) ?
                    integralPart[ii] : "";
                formattedIntegralPart = digit + formattedIntegralPart;
            } else {
                isValid = false;
            }
        }
        var decimalPart = numberIndexOfDecimalSeparator == -1 ? "" :
            numberString.substring(numberIndexOfDecimalSeparator + 1);
        var formattedDecimalPart = "";
        for (var jj = 0; jj < decimalFormat.length; jj++) {
            if (decimalFormat[jj] == "0") {
                formattedDecimalPart += decimalPart[jj];
            } else if (decimalFormat[jj] == "#") {
                digit = decimalPart.length > jj && (decimalPart[jj] != "0" || decimalPart.substring(jj).match(/[1-9]/)) ?
                    decimalPart[jj] : "";
                formattedDecimalPart += digit;
            } else {
                isValid = false;
            }
        }
        if (isValid) {
            return formattedIntegralPart +
                (formattedDecimalPart.length > 0 ? "." + formattedDecimalPart : "");
        }
    }
    throw new FormatException(1, "Unsupported format code: " + format);
}

export function intToString(number: number, provider?: any): string {
    return intToString1(number, "G", provider);
};

export function intToString1(number: number, format: string, provider?: any): string {
    provider = provider || CultureInfo.currentCulture;

    if (format && format.length) {
        if (format[ 0 ] == "X") {
            number = intSToU(number);

            var result = number.toString(16).toUpperCase();
            if (format.length !== 1) {
                var digits = +format.substr(1);
                if (!isFinite(digits)) {
                    throw new Error("Unsupported format code: " + format);
                }

                while (result.length < digits) {
                    result = "0" + result;
                }
            }

            return result;
        }
    }

    switch (format) {
        case "G":
            return number.toLocaleString(provider.name, gFormatOptions);
    }

    throw new Error("Unsupported format code: " + format);
}

export function intSToU(number: number): number {
    if (number < 0) {
        number = number + 1 + 0xFFFFFFFF;
    }

    return number;
}

export function u32BitwiseAnd (a: number, b: number): number {
    var r = a & b;

    if (r < 0) {
        r += 4294967296;
    }

    return r;
}

export function u32BitwiseOr(a: number, b: number): number {
    var r = a | b;

    if (r < 0) {
        r += 4294967296;
    }

    return r;
}

export function u32BitwiseXor(a: number, b: number): number {
    var r = a ^ b;

    if (r < 0) {
        r += 4294967296;
    }

    return r;
}

export function u32LS(a: number, b: number): number {
    var r = a << b;

    if (r < 0) {
        r += 4294967296;
    }

    return r;
}

export function decimalAdjust(type: string, value: any, exp: number) {

    // If the exp is undefined or zero...
    if (typeof exp === "undefined" || +exp === 0) {
        return (<any>Math)[ type ](value);
    }
    value = +value;
    exp = +exp;

    // If the value is not a number or the exp is not an integer...
    if (isNaN(value) || !(typeof exp === "number" && exp % 1 === 0)) {
        return NaN;
    }

    // Shift
    value = value.toString().split("e");
    value = (<any>Math)[ type ](+(value[ 0 ] + "e" + (value[ 1 ] ? (+value[ 1 ] - exp) : -exp)));

    // Shift back
    value = value.toString().split("e");
    return +(value[ 0 ] + "e" + (value[ 1 ] ? (+value[ 1 ] + exp) : exp));
}

export function round10(value: any, exp: number) {
    return decimalAdjust("round", value, exp);
}
export function round10N(value: any, exp: number) {
    return decimalAdjust("round", value, -exp);
}
export function floor10(value: any, exp: number) {
    return decimalAdjust("floor", value, exp);
}
export function ceil10(value: any, exp: number) {
    return decimalAdjust("ceil", value, exp);
}
