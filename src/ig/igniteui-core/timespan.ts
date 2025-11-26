import { truncate } from "./number";

    export function timeSpanInit1(h: number, m: number, s: number) {
		return (h * 3600000) + (m * 60000) + (s * 1000);
	};
	export function  timeSpanInit2(d: number, h: number, m: number, s: number, ms: number) {
		return (d * 86400000) + (h * 3600000) + (m * 60000) + (s * 1000) + ms;
	};
	export function  timeSpanInit3(d: number, h: number, m: number, s: number) {
		return (d * 86400000) + (h * 3600000) + (m * 60000) + (s * 1000);
	};

	export function timeSpanTotalDays(t: number) { return t / 86400000; };
	export function timeSpanTotalHours(t: number) { return t / 3600000; };
	export function timeSpanTotalMilliseconds(t: number) { return t; };
	export function timeSpanTotalMinutes(t: number) { return t / 60000; };
	export function timeSpanTotalSeconds(t: number) { return t / 1000; };

	export function timeSpanFromDays(v: number) { return v * 86400000; };
	export function timeSpanFromHours(v: number) { return v * 3600000; };
	export function timeSpanFromMilliseconds(v: number) { return v; };
	export function timeSpanFromMinutes(v: number) { return v * 60000; };
	export function timeSpanFromSeconds(v: number) { return v * 1000; };
	export function timeSpanFromTicks(v: number) { return v / 10000; };

	export function timeSpanDays(t: number) { return truncate(t / 86400000); };
	export function timeSpanHours(t: number) { return truncate((t / 3600000) % 24); };
	export function timeSpanMilliseconds(t: number) { return t % 1000; };
	export function timeSpanMinutes(t: number) { return truncate((t / 60000) % 60); };
	export function timeSpanSeconds(t: number) { return truncate((t / 1000) % 60); };
	export function timeSpanTicks(t: number) { return truncate(t * 10000); };

	export function timeSpanNegate(t: number) { return -t; };