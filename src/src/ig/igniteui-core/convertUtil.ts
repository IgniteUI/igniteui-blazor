import { IFormatProvider, FormatException } from "./type";
import { dateMinValue } from "./date";
import { truncate } from "./number";

export class ConvertUtil {
    static toBoolean(v: any, provider?: IFormatProvider): boolean {
        if (v.toBoolean) {
            return <boolean>v.toBoolean(provider);
        }
        if (typeof v === 'string' || v instanceof String) {
            if (v.toLowerCase().trim() == "true") {
                return true;
            }
            return false;
        }
        return !!v;
    }
    static toString1(v: any, provider?: IFormatProvider): string {
        if (v.toString1) {
            return <string>v.toString1(provider);
        }
        if (typeof v === 'string' || v instanceof String) {
            return <string>v;
        }
        return v.toString();
    }
    static toChar(v: any, provider?: IFormatProvider): string {
        if (v.toChar) {
            return <string>v.toChar(provider);
        }
        if (typeof v === 'string' || v instanceof String) {
            return v[0];
        }
        return String.fromCharCode(+v);
    }
     static toDateTime(v: any, provider?: IFormatProvider): Date {
        if (v.toDateTime) {
            return <Date>v.toDateTime(provider);
        }
        if (v == null) {
            return dateMinValue();
        }
        var str = v.toString();
        if (/^\s*([0-9]{1,4}\s*((\s+[ap]m?)|(((:\s*[0-9]{1,2}\s*){1,2}(\.\s*[0-9]+)?)(\s+[ap]m?)?)))\s*$/i.test(str)) {
            // The string can be a time string only, in which case we should return today at that time.
            str = new Date().toDateString() + " " + str;
        }
         var dt = new Date(Date.parse(str));

         if (isNaN(+dt))
             throw new FormatException(0, "");

         return dt;
    }
    static convertToNumber(meth: string, v: any, minValue: number, maxValue: number, trunc: boolean, provider?: IFormatProvider, throwOnNaN?: boolean): number {
        if (v[meth]) {
            return v[meth](provider);
        }
        let numberVal = 0;
        if (typeof v === 'string' || v instanceof String) {
            numberVal = parseFloat(<string>v);

            if (throwOnNaN === true && isNaN(numberVal)) {
                throw new FormatException(0, "The string cannot be converted to a number");
            }
        } else {
            numberVal = +v;
        }
        if (numberVal < minValue) {
            numberVal = minValue;
        }
        if (numberVal > maxValue) {
            numberVal = maxValue;
        }
        if (trunc) {
            numberVal = truncate(numberVal);
        }
        return numberVal;
    }
    static toByte(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toByte", v, 0, 255, true, provider, true);
    }
    static toDecimal(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toDecimal", v, Number.NEGATIVE_INFINITY, Number.POSITIVE_INFINITY, false, provider, true);
    }
    static toDouble(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toDouble", v, Number.NEGATIVE_INFINITY, Number.POSITIVE_INFINITY, false, provider);
    }
    static toInt16(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toInt16", v, -32768, 32767, true, provider, true);
    }
    static toInt32(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toInt32", v, -2147483648, 2147483647, true, provider, true);
    }
    static toInt64(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toInt64", v, -9223372036854775808, 9223372036854775807, true, provider, true);
    }
    static toSByte(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toSByte", v, -128, 127, true, provider, true);
    }
    static toUInt16(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toUInt16", v, 0, 65535, true, provider, true);
    }
    static toUInt32(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toUInt32", v, 0, 4294967295, true, provider, true);
    }
    static toUInt64(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toUInt64", v, 0, 18446744073709551615, true, provider, true);
    }
    static toSingle(v: any, provider?: IFormatProvider): number {
        return ConvertUtil.convertToNumber("toSingle", v, Number.NEGATIVE_INFINITY, Number.POSITIVE_INFINITY, false, provider);
    }
}