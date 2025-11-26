import { dateIsDST, dateAddHours, dateIsLeapYear } from "./date";
import { CultureInfo } from "./culture";
import { markEnum, FormatException } from "./type";
import { stringEscapeRegExp } from "./string"; 

export function dateToString(value: Date, provider: any) {
    return dateToStringFormat(value, "s", provider);
};

export function fromOADate(value: number): Date {
    var days = Math.floor(value);
    var result = new Date(1899, 11, 30 + days);

    if (value !== days) {
        result = new Date(+result + Math.round((value - days) * 86400000));
    }

    return result;
}
export function toOADate(value: Date): number {
    var u1 = Date.UTC(value.getFullYear(), value.getMonth(), value.getDate(),
        value.getHours(), value.getMinutes(), value.getSeconds(), value.getMilliseconds());
    var u2 = Date.UTC(1899, 11, 30);
    return (u1 - u2) / 86400000;
}

class IntlCache {
    monthShortJP: Intl.DateTimeFormat;
    [ind: string]: Intl.DateTimeFormat;
}

let intlCache: IntlCache = new IntlCache();

export function dateToStringFormat(value: Date, format: string, provider?: CultureInfo) {
    var result;
    provider = provider || CultureInfo.currentCulture; // TODO: Use the provider below
    var mmm = function (value: Date, provider: CultureInfo) {
        // On some browsers, the ja-JP month short formatting seems to not match .NET"s "MMM" formatting
        var cultureName = provider.name;
        if (cultureName == "ja-JP") {
            if ((<any>window).Intl) {
                if (!intlCache.monthShortJP) {
                    intlCache.monthShortJP = new Intl.DateTimeFormat("en-US", { month: "numeric" });
                }
                result = intlCache.monthShortJP.format(value).replace(/\u200E/g, "");
            } else {
                result = value.toLocaleString("en-US", { month: "numeric" })
                    .replace(/\u200E/g, "");
            }
        } else {
            if ((<any>window).Intl) {
                if (!intlCache['mmm_' + provider.name]) {
                    intlCache['mmm_' + provider.name] = new Intl.DateTimeFormat(provider.name, { month: "short" });
                }
                result = intlCache['mmm_' + provider.name].format(value)
                    .replace(/\u200E/g, "");
            } else {
                result = value.toLocaleString(provider.name, { month: "short" })
                    .replace(/\u200E/g, "");
            }
        }

        if (result.indexOf(" ") >= 0) {

            // Date.toLocaleString is not supported fully
            // TODO: Handle other cultures?
            return ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"][value.getMonth()];
        }

        return result;
    };
    var tt = function (value: Date, abbr: boolean) {
        var h = value.getHours();
        var designator = h <= 11 ? "AM" : "PM";

        if ((<any>window).Intl) {
            var d = new Date(+value);
            d.setHours(h, 0, 0, 0);
            var culture = provider.name;

            // account for left-to-right marker ie/edge inject
            var r = /\d|[\u200E]/g;
            var withAmPm = applyFormat("tt:" + abbr, { hour12: true, hour: "2-digit" }, d)
                .replace(r, "");
            var nonAmPm = applyFormat("tt:" + abbr, { hour12: false, hour: "2-digit" }, d)
                .replace(r, "");
            var pattern = stringEscapeRegExp(nonAmPm);
            var amPm = withAmPm.replace(new RegExp("\\s*" + pattern + "\\s*"), "").trim();

            // ie & edge will not include the culture's am/pm designator
            // and they instead include some erroneous extra characters.
            // if that's the case then we'll just use the previous fallback
            if (amPm.replace(/[.,:;]/g, "").length > 0) {
                designator = amPm;
            }
        }

        if (abbr && designator) {
            designator = designator.charAt(0);
        }

        return designator;
    };
    var applyFormat = function (format: string, options: Intl.DateTimeFormatOptions, otherVal?: any) {
        let val = value;
        if (otherVal) {
            val = otherVal;
        }
        if ((<any>window).Intl) {
            if (!intlCache[format + "_" + provider.name]) {
                intlCache[format + "_" + provider.name] = new Intl.DateTimeFormat(provider.name, options);
            }
            var formatter = intlCache[format + "_" + provider.name];
            return formatter.format(val);
        }
        return val.toLocaleString(provider.name, options);
    };
    switch (format) {
        case "s":
            {
                var s = new Date(Date.UTC(value.getFullYear(), value.getMonth(), value.getDate(),
                    value.getHours(), value.getMinutes(), value.getSeconds())).toISOString();
                var d = s.lastIndexOf(".");
                if (d < 0) {
                    return s;
                }

                return s.slice(0, d);
            }

        case "MMMM":
            return applyFormat(format, { month: "long" })
                .replace(/\u200E/g, "");

        case "ddd":
            return applyFormat(format, { weekday: "short" })
                .replace(/\u200E/g, "");

        case "dddd":
            result = applyFormat(format, { weekday: "long" })
                .replace(/\u200E/g, "");

            if (result.indexOf(" ") >= 0) {

                // Date.toLocaleString is not supported fully
                // TODO: Handle other cultures?
                return ["Sunday", "Monday", "Tuesday", "Wednesday",
                    "Thursday", "Friday", "Saturday"][value.getDay()];
            }

            return result;

        case "%h":
            return value.getHours();;
        case "%m":
            return value.getMinutes();
        case "%s":
            return value.getSeconds();
        case "%t":
            return tt(value, true);
        case "d":  // short date
            return applyFormat(format, {});
        case "D": // long date
            return applyFormat(format, { weekday: "long", month: "long", day: "numeric", year: "numeric" });
        case "f": // full datetime (short time)
            return applyFormat(format, {
                weekday: "long", month: "long", day: "numeric", year: "numeric",
                hour: "numeric", minute: "numeric"
            });
        case "F": // full datetime (long time)
            return applyFormat(format, {
                weekday: "long", month: "long", day: "numeric", year: "numeric",
                hour: "numeric", minute: "numeric", second: "numeric"
            });
        case "g": // general (short time)
            return applyFormat(format, {
                month: "numeric", day: "numeric", year: "numeric",
                hour: "numeric", minute: "numeric"
            });
        case "G": // general (long time)
            return applyFormat(format, {
                month: "numeric", day: "numeric", year: "numeric",
                hour: "numeric", minute: "numeric", second: "numeric"
            });
        case "M": // month/day
        case "m":
            return applyFormat(format, { month: "long", day: "numeric" });
        case "t": // short time
            return applyFormat(format, { hour: "numeric", minute: "numeric" });
        case "T": // long time
            return applyFormat(format, { hour: "numeric", minute: "numeric", second: "numeric" });
        //return value.toLocaleTimeString();
        case "Y": // year/month
        case "y":
            return applyFormat(format, { year: "numeric", month: "long" });
    }
    result = format;
    var year = value.getFullYear().toString();
    result = result.replace("yyyy", year);
    result = result.replace("yy", year.substr(-2));
    result = result.replace("MMM", mmm(value, provider));
    result = result.replace("MM", (value.getMonth() + 1).toString().replace(/^(\d)$/, "0$1"));
    result = result.replace("dd", value.getDate().toString().replace(/^(\d)$/, "0$1"));
    var hours = value.getHours();
    result = result.replace("HH", hours.toString().replace(/^(\d)$/, "0$1"));
    result = result.replace("hh", (hours % 12 == 0 ? 12 : hours % 12).toString().replace(/^(\d)$/, "0$1"));
    result = result.replace("tt", tt(value, false));
    result = result.replace("mm", value.getMinutes().toString().replace(/^(\d)$/, "0$1"));
    result = result.replace("ss", value.getSeconds().toString().replace(/^(\d)$/, "0$1"));
    result = result.replace("ff", Math.round(value.getMilliseconds() / 10).toString().replace(/^(\d)$/, "0$1")); // hundredths of a second
    return result;
}

export function dateTryParse(s: string, result?: Date): { p1: Date, ret: boolean } {
    var date = new Date(s);
    if (date == null || isNaN(+date)) {

        // IE8 does not support this format, so parse it manually
        var r = /(\d{4})-(\d{2})-(\d{2})(?:T(\d{2}):(\d{2}):(\d{2}))?/.exec(s);
        if (r) {
            if (r[ 4 ]) {
                return {
                    p1: new Date(+r[ 1 ], +r[ 2 ] - 1, +r[ 3 ],
                        +r[ 4 ], +r[ 5 ], +r[ 6 ]), ret: true
                };
            } else {
                return { p1: new Date(+r[ 1 ], +r[ 2 ] - 1, +r[ 3 ]), ret: true };
            }
        }

        return { p1: null, ret: false };
    }

    // TODO: Use the current date separator/date format here here?
    if (date.getFullYear() < 1930 && /\d+\/\d+\/\d\d(?!\d)/.test(s)) {
        date.setFullYear(date.getFullYear() + 100);
    }

    return { p1: date, ret: true };
}

export function dateParseExact(s: string, format: string = null, provider: any = null) {
    // TODO: Use the format and provider
    var r = dateTryParse(s);

    if (!r.ret) {
        throw new FormatException(1, "Unknown date format");
    }

    return r.p1;
}
export function toLocalTime(value: Date): Date {

    // TODO: Implement
    return value;
}
export function toUniversalTime(value: Date) {

    // TODO: Implement
    return value;
}

let _requiresISOCorrection: boolean =  !isNaN(+new Date("2000-01-01T00:00:00")) &&
    new Date("2000-01-01T00:00:00").getHours() !== 0;
let _requiresISODateCorrection: boolean = !isNaN(+new Date("2000-01-01")) &&
    new Date("2000-01-01").getHours() !== 0;
export function dateParse(s: string, provider?: any) {
    provider = provider || CultureInfo.currentCulture; // TODO: Use the provider below
    var result;

    var isoTest = /(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2})(?:\.| )?(\d*)?/.exec(s);
    if (isoTest) {
        result = dateParseExact(isoTest[ 1 ]);
        if (isoTest[ 2 ]) {
            var ms = Number("0." + isoTest[ 2 ]) * 1000;
            result = new Date(+result + ms);
        }

        if (!_requiresISOCorrection) {
            return result;
        }
    } else {
        result = dateParseExact(s);
        if (!_requiresISODateCorrection) {
            return result;
        }
    }

    return new Date(result.getUTCFullYear(), result.getUTCMonth(), result.getUTCDate(),
        result.getUTCHours(), result.getUTCMinutes(),
        result.getUTCSeconds(), result.getUTCMilliseconds());
}
let _longDateFormatOptions: Intl.DateTimeFormatOptions = {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric"
};
export function toLongDateString(value: Date): string {
    return value.toLocaleString(CultureInfo.currentCulture.name,
        _longDateFormatOptions).replace(/\u200E/g, "");
}
let _longTimeFormatOptions: Intl.DateTimeFormatOptions = { 
    hour: "numeric", 
    minute: "numeric", 
    second: "numeric" 
};
export function toLongTimeString(value: Date): string {
    return value.toLocaleString(CultureInfo.currentCulture.name,
        _longTimeFormatOptions).replace(/\u200E/g, "");
}

export enum DayOfWeek {
    Sunday = 0,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}
export let DayOfWeek_$type = markEnum("DayOfWeek", "Sunday,0|Monday,1|Tuesday,2|Wednesday,3|Thursday,4|Friday,5|Saturday,6");
