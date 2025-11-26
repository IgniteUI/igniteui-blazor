import { markEnum, Type } from "./type";

export const enum DateTimeKind {
		Unspecified = 0,
		Utc = 1,
		Local = 2
}
export let DateTimeKind_$type = markEnum("DateTimeKind", "Unspecified,0|Utc,1|Local,2");
    

	export function dateKind() {
		return DateTimeKind.Local;
	};

	export function defaultDVDateParse(str: string): Date {
		return new Date(parseInt(str.replace("/Date(", "").replace(")/", ""), 10));
	}

	export function dateNow(): Date {
		return new Date();
	};
	
	export function dateMinValue(): Date {
		let d = new Date(1, 0, 1, 0, 0, 0, 0);
		d.setFullYear(1);
		return d;
	};
	export function dateMaxValue(): Date {
		return new Date(9999, 12, 31, 23, 59, 59, 0.9999999);
	};
	export function dateFromMilliseconds(value: number): Date {
		return new Date(value);
	};

	export function dateStdTimezoneOffset(date: Date): number {
		var jan, jul, janOffset, julOffset;
		jan = new Date(date.getFullYear(), 0, 1);
		jul = new Date(date.getFullYear(), 6, 1);
		julOffset = jul.getTimezoneOffset();
		janOffset = jan.getTimezoneOffset();
		return Math.max(janOffset, julOffset);
	};

	export function dateIsDST(date: Date): boolean {
		return date.getTimezoneOffset() < dateStdTimezoneOffset(date);
	};

	export function dateFromValues(year: number, month: number, day: number, hour: number, minute: number, second: number, millisecond: number): Date {
		return new Date(year, month - 1, day, hour, minute, second, millisecond);
	}
	export function dateFromTicks(ticks: number): Date {
		return new Date(ticks);
	}
	export function dateAddSeconds(value: Date, seconds: number): Date {
		return dateAddDays(value, seconds / 86400);
	}
	export function dateAddMinutes(value: Date, minutes: number): Date {
		return dateAddDays(value, minutes / 1440);
	}
	export function dateAddHours(value: Date, hours: number): Date {
		return dateAddDays(value, hours / 24);
	}
	export function dateAddDays(value: Date, days: number): Date {
		var result = new Date(+value + (days * 86400000));

		// Correct for any daylight saving time shifts
		if (!dateIsDST(value)) {
			if (dateIsDST(result)) {
				result = new Date(+result - 3600000);
			}
		} else {
			if (!dateIsDST(result)) {
				result = new Date(+result + 3600000);
			}
		}

		return result;
	}
	export function dateAddMonths(value: Date, num: number): Date {

		var result = new Date(value.getTime());
		var currentMonth = result.getMonth() + result.getFullYear() * 12;
		result.setMonth(result.getMonth() + num);
		var diff = result.getMonth() + result.getFullYear() * 12 - currentMonth;

		// If don't get the right number, set date to
		// last day of previous month
		if (diff != num) {
			result.setDate(0);
		}
		return result;
	}
	export function dateAddYears(value: Date, num: number): Date {
		var result = new Date(value.getTime());
		result.setFullYear(result.getFullYear() + num);
		return result;
	}
	
	export function dateIsLeapYear(year: number): boolean {
		return year % 4 === 0 && (year % 100 !== 0 || year % 400 == 0);
	}
	export function dateToFileTime(value: Date): number {
		return (+value - +new Date(1600, 11, 31, 19, 0, 0, 0)) * 10000;
	}
	export function dateFromFileTime(value: number): number {

		// TODO: Test this
		return +(value / 10000) + +new Date(1600, 11, 31, 19, 0, 0, 0);
	}	
	export function dateFromFileTimeUtc(value: number): Date {
        return new Date(+(value / 10000) + +Date.UTC(1600, 12, 1, 0, 0, 0, 0));
	}	
	
	export function dateGetMonth(value: Date) {
		return value.getMonth() + 1;
	}
	export function dateToday() {
		var r = new Date();
		r.setHours(0, 0, 0, 0);
		return r;
	}
	export function dateGetTimeOfDay(value: Date) {
		return (value.getHours() * 3600000) +
			(value.getMinutes() * 60000) +
			(value.getSeconds() * 1000) +
			value.getMilliseconds();
	}
	export function dateGetDate(value: Date) {
		return new Date(+value - dateGetTimeOfDay(value));
	}
	
	export function  dateEquals(d1: Date, d2: Date): boolean {
		return d2 instanceof Date && +d1 === +d2;
	}

	export function dateAdd(d: any, t: any) { return new Date(+d + t); };
    export function dateSubtract(d: any, t: any) { return new Date(+d - t); };

    export function daysInMonth(year: number, month: number): number {
        switch (month) {
            case 1: return 31; // Jan
            case 2: return dateIsLeapYear(year) ? 29 : 28; // Feb
            case 3: return 31; // Mar
            case 4: return 30; // Apr
            case 5: return 31; // May
            case 6: return 30; // Jun
            case 7: return 31; // Jul
            case 8: return 31; // Aug
            case 9: return 30; // Sep
            case 10: return 31; // Oct
            case 11: return 30; // Nov
            case 12: return 31; // Dec
        }

        // TODO: throw error here?
        return 0;
    }
