import { Base, Type, markType, NotSupportedException } from "./type";
import { stringIsNullOrEmpty } from "./string";
import { stringFormat1 } from "./stringExtended";

/**
 * @hidden 
 */
export class Localization extends Base {
	static $t: Type = markType(Localization, 'Localization');
	static getStringOverride: (arg1: string, arg2: string) => string = null;
	private static _locales: any = null;
	static register(grouping: string, locale: {readonly [key: string] : string}): void {
		if (Localization._locales == null) {
			Localization._locales = {};
		}
		Localization._locales[grouping] = locale;
	}
	static getString(grouping: string, id: string, ...args: any[]): string {
		let resourceValue: string;
		if (Localization.getStringOverride != null) {
			resourceValue = Localization.getStringOverride(grouping, id);
		} else {
			let locale: any = null;
			if (Localization._locales != null) {
				locale = Localization._locales[grouping];
			}
			if (locale != null) {
				resourceValue = <string>(locale[id]);
			} else {
				throw new NotSupportedException(1, "string override must be specified for localization");
			}
		}
		if (stringIsNullOrEmpty(resourceValue)) {
			resourceValue = "";
		} else if (args != null && args.length > 0) {
			resourceValue = stringFormat1(resourceValue, ...args);
		}
		return resourceValue;
	}
	static setString(grouping: string, id: string, value: string): void {
		let locale: any = null;
		if (Localization._locales != null) {
			locale = Localization._locales[grouping];
		}
		if (locale != null) {
			locale[id] = value;
		} else {
			throw new NotSupportedException(1, "locale set for " + grouping + " was not loaded.");
		}
	}
	static isRegistered(grouping: string): boolean {
		return <boolean>(Localization._locales && Localization._locales[grouping] !== undefined);
	}
	static getCultureId(grouping: string): string {
		let lang = <string>(navigator.language ? navigator.language.toLowerCase() : "");
		let prefix = grouping + "-";
		while (true) {
			if (Localization.isRegistered(prefix + lang)) {
				return lang;
			}
			let langFallback = Localization.getRedirect(lang);
			if (langFallback != null && Localization.isRegistered(prefix + langFallback)) {
				return langFallback;
			}
			let idx = lang.lastIndexOf('-');
			if (idx <= 0) {
				break;
			}
			lang = lang.substr(0, idx);
		}
		return "en";
	}
	private static getRedirect(lang: string): string {
		switch (lang) {
			case "zh-hk":

			case "zh-tw":

			case "zh-mo": return "zh-Hant";
			case "zh-cn":

			case "zh-sg":

			case "zh": return "zh-Hans";
		}

		return null;
	}
}


