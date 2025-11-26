import { FormatSpecifier } from "./FormatSpecifier";
import { Dictionary$2 } from "./Dictionary$2";
import { Base, String_$type, Type, markType } from "./type";
import { stringIsNullOrEmpty } from "./string";

/**
 * @hidden 
 */
export class NumberFormatSpecifier extends FormatSpecifier {
	static $t: Type = markType(NumberFormatSpecifier, 'NumberFormatSpecifier', (<any>FormatSpecifier).$type);
	constructor() {
		super();
		this.useGrouping = true;
		this.minimumIntegerDigits = -1;
		this.minimumFractionDigits = -1;
		this.maximumFractionDigits = -1;
		this.minimumSignificantDigits = -1;
		this.maximumSignificantDigits = -1;
		this.initializeCurrencyCodes();
	}
	private _currencyCodes: Dictionary$2<string, string> = new Dictionary$2<string, string>(String_$type, String_$type, 0);
	private initializeCurrencyCodes(): void {
		this._currencyCodes = new Dictionary$2<string, string>(String_$type, String_$type, 0);
		this._currencyCodes.addItem("ps-AF", "AFN");
		this._currencyCodes.addItem("prs-AF", "AFN");
		this._currencyCodes.addItem("sq-AL", "ALL");
		this._currencyCodes.addItem("ar-DZ", "DZD");
		this._currencyCodes.addItem("es-AR", "ARS");
		this._currencyCodes.addItem("hy-AM", "AMD");
		this._currencyCodes.addItem("pt-AO", "AOA");
		this._currencyCodes.addItem("en-AU", "AUD");
		this._currencyCodes.addItem("de-AT", "EUR");
		this._currencyCodes.addItem("az-AZ", "AZN");
		this._currencyCodes.addItem("Lt-az-AZ", "AZN");
		this._currencyCodes.addItem("Cy-az-AZ", "AZN");
		this._currencyCodes.addItem("ar-BH", "BHD");
		this._currencyCodes.addItem("eu-ES", "EUR");
		this._currencyCodes.addItem("be-BY", "BYN");
		this._currencyCodes.addItem("nl-BE", "EUR");
		this._currencyCodes.addItem("fr-BE", "EUR");
		this._currencyCodes.addItem("en-BZ", "BZD");
		this._currencyCodes.addItem("es-BO", "BOV");
		this._currencyCodes.addItem("pt-BR", "BRL");
		this._currencyCodes.addItem("ms-BN", "USD");
		this._currencyCodes.addItem("bg-BG", "BGN");
		this._currencyCodes.addItem("km-KH", "KHR");
		this._currencyCodes.addItem("en-CA", "CAD");
		this._currencyCodes.addItem("fr-CA", "CAD");
		this._currencyCodes.addItem("en-CB", "USA");
		this._currencyCodes.addItem("ca-ES", "EUR");
		this._currencyCodes.addItem("es-CL", "CLP");
		this._currencyCodes.addItem("zh-CN", "CNY");
		this._currencyCodes.addItem("zh-CHS", "CNY");
		this._currencyCodes.addItem("zh-CHT", "CNY");
		this._currencyCodes.addItem("es-CO", "COU");
		this._currencyCodes.addItem("es-CR", "CRC");
		this._currencyCodes.addItem("hr-HR", "HRK");
		this._currencyCodes.addItem("cs-CZ", "CZK");
		this._currencyCodes.addItem("da-DK", "DKK");
		this._currencyCodes.addItem("es-DO", "DOP");
		this._currencyCodes.addItem("es-EC", "USD");
		this._currencyCodes.addItem("ar-EG", "USD");
		this._currencyCodes.addItem("es-SV", "SVC");
		this._currencyCodes.addItem("et-EE", "EUR");
		this._currencyCodes.addItem("fo-FO", "DKK");
		this._currencyCodes.addItem("kl-GL", "DKK");
		this._currencyCodes.addItem("fi-FI", "EUR");
		this._currencyCodes.addItem("sv-FI", "EUR");
		this._currencyCodes.addItem("fr-FR", "EUR");
		this._currencyCodes.addItem("gl-ES", "EUR");
		this._currencyCodes.addItem("ka-GE", "GEL");
		this._currencyCodes.addItem("de-DE", "EUR");
		this._currencyCodes.addItem("el-GR", "EUR");
		this._currencyCodes.addItem("es-GT", "GTQ");
		this._currencyCodes.addItem("es-HN", "HNL");
		this._currencyCodes.addItem("zh-HK", "HKD");
		this._currencyCodes.addItem("hu-HU", "HUF");
		this._currencyCodes.addItem("is-IS", "ISK");
		this._currencyCodes.addItem("gu-IN", "INR");
		this._currencyCodes.addItem("hi-IN", "INR");
		this._currencyCodes.addItem("kn-IN", "INR");
		this._currencyCodes.addItem("kok-IN", "INR");
		this._currencyCodes.addItem("mr-IN", "INR");
		this._currencyCodes.addItem("pa-IN", "INR");
		this._currencyCodes.addItem("sa-IN", "INR");
		this._currencyCodes.addItem("ta-IN", "INR");
		this._currencyCodes.addItem("te-IN", "INR");
		this._currencyCodes.addItem("id-ID", "IDR");
		this._currencyCodes.addItem("fa-IR", "IRR");
		this._currencyCodes.addItem("ar-IQ", "IQD");
		this._currencyCodes.addItem("en-IE", "EUR");
		this._currencyCodes.addItem("he-IL", "ILS");
		this._currencyCodes.addItem("it-IT", "EUR");
		this._currencyCodes.addItem("en-JM", "JMD");
		this._currencyCodes.addItem("ja-JP", "JPY");
		this._currencyCodes.addItem("ar-JO", "JOD");
		this._currencyCodes.addItem("kk-KZ", "KZT");
		this._currencyCodes.addItem("ky-KZ", "KZT");
		this._currencyCodes.addItem("sw-KE", "KES");
		this._currencyCodes.addItem("ko-KR", "KPW");
		this._currencyCodes.addItem("ar-KW", "KWD");
		this._currencyCodes.addItem("ky-KG", "KGS");
		this._currencyCodes.addItem("lv-LV", "EUR");
		this._currencyCodes.addItem("ar-LB", "LBP");
		this._currencyCodes.addItem("ar-LY", "LYD");
		this._currencyCodes.addItem("de-LI", "CHF");
		this._currencyCodes.addItem("lt-LT", "EUR");
		this._currencyCodes.addItem("fr-LU", "EUR");
		this._currencyCodes.addItem("de-LU", "EUR");
		this._currencyCodes.addItem("zh-MO", "MOP");
		this._currencyCodes.addItem("ms-MY", "MYR");
		this._currencyCodes.addItem("div-MV", "MVR");
		this._currencyCodes.addItem("es-MX", "MXN");
		this._currencyCodes.addItem("fr-MC", "EUR");
		this._currencyCodes.addItem("mn-MN", "MNT");
		this._currencyCodes.addItem("sr-Latn-ME", "EUR");
		this._currencyCodes.addItem("ar-MA", "MAD");
		this._currencyCodes.addItem("mk-MK", "MKD");
		this._currencyCodes.addItem("en-NZ", "NZD");
		this._currencyCodes.addItem("es-NI", "NIO");
		this._currencyCodes.addItem("nb-NO", "NOK");
		this._currencyCodes.addItem("nn-NO", "NOK");
		this._currencyCodes.addItem("ar-OM", "OMR");
		this._currencyCodes.addItem("ur-PK", "PKR");
		this._currencyCodes.addItem("es-PA", "PAB");
		this._currencyCodes.addItem("es-PY", "PYG");
		this._currencyCodes.addItem("es-PE", "PEN");
		this._currencyCodes.addItem("en-PH", "PHP");
		this._currencyCodes.addItem("pl-PL", "PLN");
		this._currencyCodes.addItem("pt-PT", "EUR");
		this._currencyCodes.addItem("es-PR", "USD");
		this._currencyCodes.addItem("ar-QA", "QAR");
		this._currencyCodes.addItem("ro-RO", "RON");
		this._currencyCodes.addItem("ru-RU", "RUB");
		this._currencyCodes.addItem("tt-RU", "RUB");
		this._currencyCodes.addItem("ar-SA", "SAR");
		this._currencyCodes.addItem("sr-SP", "RSD");
		this._currencyCodes.addItem("Lt-sr-SP", "RSD");
		this._currencyCodes.addItem("Cy-sr-SP", "XOF");
		this._currencyCodes.addItem("zh-SG", "SGD");
		this._currencyCodes.addItem("sk-SK", "EUR");
		this._currencyCodes.addItem("sl-SI", "EUR");
		this._currencyCodes.addItem("af-ZA", "ZAR");
		this._currencyCodes.addItem("en-ZA", "ZAR");
		this._currencyCodes.addItem("es-ES", "EUR");
		this._currencyCodes.addItem("sv-SE", "EUR");
		this._currencyCodes.addItem("si-LK", "LKR");
		this._currencyCodes.addItem("fr-CH", "CHF");
		this._currencyCodes.addItem("de-CH", "CHF");
		this._currencyCodes.addItem("it-CH", "CHF");
		this._currencyCodes.addItem("ar-SY", "SYP");
		this._currencyCodes.addItem("syr-SY", "SYP");
		this._currencyCodes.addItem("zh-TW", "TWD");
		this._currencyCodes.addItem("th-TH", "THB");
		this._currencyCodes.addItem("nl-NL", "EUR");
		this._currencyCodes.addItem("en-TT", "TTD");
		this._currencyCodes.addItem("ar-TN", "TND");
		this._currencyCodes.addItem("tr-TR", "TRY");
		this._currencyCodes.addItem("tk-TM", "TMT");
		this._currencyCodes.addItem("uk-UA", "UAH");
		this._currencyCodes.addItem("ar-AE", "AED");
		this._currencyCodes.addItem("en-GB", "GBP");
		this._currencyCodes.addItem("en-US", "USD");
		this._currencyCodes.addItem("es-UY", "UYU");
		this._currencyCodes.addItem("uz-UZ", "UZS");
		this._currencyCodes.addItem("Cy-uz-UZ", "UZS");
		this._currencyCodes.addItem("Lt-uz-UZ", "UZS");
		this._currencyCodes.addItem("es-VE", "VED");
		this._currencyCodes.addItem("vi-VN", "VND");
		this._currencyCodes.addItem("ar-YE", "YER");
		this._currencyCodes.addItem("en-ZW", "ZWL");
	}
	toIntl(): any {
		let options = {};
		if (this.compactDisplay != null) {
			this.setOption(options, "compactDisplay", this.compactDisplay);
		}
		if (this.currency != null) {
			this.setOption(options, "currency", this.currency);
		}
		if (this.currencyDisplay != null) {
			this.setOption(options, "currencyDisplay", this.currencyDisplay);
		}
		if (this.currencySign != null) {
			this.setOption(options, "currencySign", this.currencySign);
		}
		if (this.localeMatcher != null) {
			this.setOption(options, "localeMatcher", this.localeMatcher);
		}
		if (this.notation != null) {
			this.setOption(options, "notation", this.notation);
		}
		if (this.numberingSystem != null) {
			this.setOption(options, "numberingSystem", this.numberingSystem);
		}
		if (this.signDisplay != null) {
			this.setOption(options, "signDisplay", this.signDisplay);
		}
		if (this.style != null) {
			this.setOption(options, "style", this.style);
		}
		if (this.unit != null) {
			this.setOption(options, "unit", this.unit);
		}
		if (this.unitDisplay != null) {
			this.setOption(options, "unitDisplay", this.unitDisplay);
		}
		if (this.useGrouping != true) {
			this.setOption(options, "useGrouping", this.useGrouping);
		}
		if (this.minimumIntegerDigits != -1) {
			this.setOption(options, "minimumIntegerDigits", this.minimumIntegerDigits);
		}
		if (this.minimumFractionDigits != -1) {
			this.setOption(options, "minimumFractionDigits", this.minimumFractionDigits);
		}
		if (this.maximumFractionDigits != -1) {
			this.setOption(options, "maximumFractionDigits", this.maximumFractionDigits);
		}
		if (this.minimumSignificantDigits != -1) {
			this.setOption(options, "minimumSignificantDigits", this.minimumSignificantDigits);
		}
		if (this.maximumSignificantDigits != -1) {
			this.setOption(options, "maximumSignificantDigits", this.maximumSignificantDigits);
		}
		let locale_ = stringIsNullOrEmpty(this.locale) ? this.getLocalCulture() : this.locale;
		if (this.style == "currency") {
			if (this.currencyCode != null) {
				this.setOption(options, "currency", this.currencyCode);
			} else if (this._currencyCodes.containsKey(locale_)) {
				this.setOption(options, "currency", this._currencyCodes.item(locale_));
			}
		}
		let options_ = options;
		return new Intl.NumberFormat(locale_, options_);
	}
	private setOption(options_: any, propertyName_: string, value_: any): void {
		options_[propertyName_] = value_;
	}
	toPlatform(): any {
		return this.toIntl();
	}
	private _locale: string = null;
	get locale(): string {
		return this._locale;
	}
	set locale(value: string) {
		this._locale = value;
	}
	private _compactDisplay: string = null;
	get compactDisplay(): string {
		return this._compactDisplay;
	}
	set compactDisplay(value: string) {
		this._compactDisplay = value;
	}
	private _currency: string = null;
	get currency(): string {
		return this._currency;
	}
	set currency(value: string) {
		this._currency = value;
	}
	private _currencyDisplay: string = null;
	get currencyDisplay(): string {
		return this._currencyDisplay;
	}
	set currencyDisplay(value: string) {
		this._currencyDisplay = value;
	}
	private _currencySign: string = null;
	get currencySign(): string {
		return this._currencySign;
	}
	set currencySign(value: string) {
		this._currencySign = value;
	}
	private _currencyCode: string = null;
	get currencyCode(): string {
		return this._currencyCode;
	}
	set currencyCode(value: string) {
		this._currencyCode = value;
	}
	private _localeMatcher: string = null;
	get localeMatcher(): string {
		return this._localeMatcher;
	}
	set localeMatcher(value: string) {
		this._localeMatcher = value;
	}
	private _notation: string = null;
	get notation(): string {
		return this._notation;
	}
	set notation(value: string) {
		this._notation = value;
	}
	private _numberingSystem: string = null;
	get numberingSystem(): string {
		return this._numberingSystem;
	}
	set numberingSystem(value: string) {
		this._numberingSystem = value;
	}
	private _signDisplay: string = null;
	get signDisplay(): string {
		return this._signDisplay;
	}
	set signDisplay(value: string) {
		this._signDisplay = value;
	}
	private _style: string = null;
	get style(): string {
		return this._style;
	}
	set style(value: string) {
		this._style = value;
	}
	private _unit: string = null;
	get unit(): string {
		return this._unit;
	}
	set unit(value: string) {
		this._unit = value;
	}
	private _unitDisplay: string = null;
	get unitDisplay(): string {
		return this._unitDisplay;
	}
	set unitDisplay(value: string) {
		this._unitDisplay = value;
	}
	private _useGrouping: boolean = false;
	get useGrouping(): boolean {
		return this._useGrouping;
	}
	set useGrouping(value: boolean) {
		this._useGrouping = value;
	}
	private _minimumIntegerDigits: number = 0;
	get minimumIntegerDigits(): number {
		return this._minimumIntegerDigits;
	}
	set minimumIntegerDigits(value: number) {
		this._minimumIntegerDigits = value;
	}
	private _minimumFractionDigits: number = 0;
	get minimumFractionDigits(): number {
		return this._minimumFractionDigits;
	}
	set minimumFractionDigits(value: number) {
		this._minimumFractionDigits = value;
	}
	private _maximumFractionDigits: number = 0;
	get maximumFractionDigits(): number {
		return this._maximumFractionDigits;
	}
	set maximumFractionDigits(value: number) {
		this._maximumFractionDigits = value;
	}
	private _minimumSignificantDigits: number = 0;
	get minimumSignificantDigits(): number {
		return this._minimumSignificantDigits;
	}
	set minimumSignificantDigits(value: number) {
		this._minimumSignificantDigits = value;
	}
	private _maximumSignificantDigits: number = 0;
	get maximumSignificantDigits(): number {
		return this._maximumSignificantDigits;
	}
	set maximumSignificantDigits(value: number) {
		this._maximumSignificantDigits = value;
	}
}


