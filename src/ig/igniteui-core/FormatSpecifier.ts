import { Base, String_$type, Type, markType } from "./type";
import { Dictionary$2 } from "./Dictionary$2";
import { stringContains } from "./string";

/**
 * @hidden 
 */
export class FormatSpecifier extends Base {
	static $t: Type = markType(FormatSpecifier, 'FormatSpecifier');
	constructor() {
		super();
		this.initializeCultureLookup();
	}
	toIntl(): any {
		return null;
	}
	getLocalCulture(): string {
		let language = <string>(window.navigator.language);
		if (stringContains(language, "-")) {
			return language;
		} else {
			if (this._cultureLookup.containsKey(language)) {
				return this._cultureLookup.item(language);
			}
			return language;
		}
	}
	private _cultureLookup: Dictionary$2<string, string> = null;
	private initializeCultureLookup(): void {
		this._cultureLookup = new Dictionary$2<string, string>(String_$type, String_$type, 0);
		this._cultureLookup.addItem("af", "af-ZA");
		this._cultureLookup.addItem("ar", "ar-EG");
		this._cultureLookup.addItem("az", "az-AZ");
		this._cultureLookup.addItem("be", "be-BY");
		this._cultureLookup.addItem("bg", "bg-BG");
		this._cultureLookup.addItem("ca", "ca-ES");
		this._cultureLookup.addItem("cs", "cs-CZ");
		this._cultureLookup.addItem("Cy", "Cy-sr-SP");
		this._cultureLookup.addItem("da", "da-DK");
		this._cultureLookup.addItem("de", "de-DE");
		this._cultureLookup.addItem("div", "div-MV");
		this._cultureLookup.addItem("el", "el-GR");
		this._cultureLookup.addItem("en", "en-US");
		this._cultureLookup.addItem("es", "es-ES");
		this._cultureLookup.addItem("et", "et-EE");
		this._cultureLookup.addItem("eu", "eu-ES");
		this._cultureLookup.addItem("fa", "fa-IR");
		this._cultureLookup.addItem("fi", "fi-FI");
		this._cultureLookup.addItem("fo", "fo-FO");
		this._cultureLookup.addItem("fr", "fr-FR");
		this._cultureLookup.addItem("gl", "gl-ES");
		this._cultureLookup.addItem("gu", "gu-IN");
		this._cultureLookup.addItem("he", "he-IL");
		this._cultureLookup.addItem("hi", "hi-IN");
		this._cultureLookup.addItem("hr", "hr-HR");
		this._cultureLookup.addItem("hu", "hu-HU");
		this._cultureLookup.addItem("hy", "hy-AM");
		this._cultureLookup.addItem("id", "id-ID");
		this._cultureLookup.addItem("is", "is-IS");
		this._cultureLookup.addItem("it", "it-IT");
		this._cultureLookup.addItem("ja", "ja-JP");
		this._cultureLookup.addItem("ka", "ka-GE");
		this._cultureLookup.addItem("kk", "kk-KZ");
		this._cultureLookup.addItem("kl", "kl-GL");
		this._cultureLookup.addItem("km", "km-KH");
		this._cultureLookup.addItem("kn", "kn-IN");
		this._cultureLookup.addItem("ko", "ko-KR");
		this._cultureLookup.addItem("kok", "kok-IN");
		this._cultureLookup.addItem("ky", "ky-KZ");
		this._cultureLookup.addItem("Lt", "Lt-az-AZ");
		this._cultureLookup.addItem("lt", "lt-LT");
		this._cultureLookup.addItem("lv", "lv-LV");
		this._cultureLookup.addItem("mk", "mk-MK");
		this._cultureLookup.addItem("mn", "mn-MN");
		this._cultureLookup.addItem("mr", "mr-IN");
		this._cultureLookup.addItem("ms", "ms-MY");
		this._cultureLookup.addItem("nb", "nb-NO");
		this._cultureLookup.addItem("nl", "nl-NL");
		this._cultureLookup.addItem("nn", "nn-NO");
		this._cultureLookup.addItem("pa", "pa-IN");
		this._cultureLookup.addItem("pl", "pl-PL");
		this._cultureLookup.addItem("prs", "prs-AF");
		this._cultureLookup.addItem("ps", "ps-AF");
		this._cultureLookup.addItem("pt", "pt-PT");
		this._cultureLookup.addItem("ro", "ro-RO");
		this._cultureLookup.addItem("ru", "ru-RU");
		this._cultureLookup.addItem("sa", "sa-IN");
		this._cultureLookup.addItem("si", "si-LK");
		this._cultureLookup.addItem("sk", "sk-SK");
		this._cultureLookup.addItem("sl", "sl-SI");
		this._cultureLookup.addItem("sq", "sq-AL");
		this._cultureLookup.addItem("sr", "sr-Latn-ME");
		this._cultureLookup.addItem("sv", "sv-FI");
		this._cultureLookup.addItem("sw", "sw-KE");
		this._cultureLookup.addItem("syr", "syr-SY");
		this._cultureLookup.addItem("ta", "ta-IN");
		this._cultureLookup.addItem("te", "te-IN");
		this._cultureLookup.addItem("th", "th-TH");
		this._cultureLookup.addItem("tk", "tk-TM");
		this._cultureLookup.addItem("tr", "tr-TR");
		this._cultureLookup.addItem("tt", "tt-RU");
		this._cultureLookup.addItem("uk", "uk-UA");
		this._cultureLookup.addItem("ur", "ur-PK");
		this._cultureLookup.addItem("vi", "vi-VN");
		this._cultureLookup.addItem("zh", "zh-CN");
	}
	toPlatform(): any {
		return this.toIntl();
		return null;
	}
}


