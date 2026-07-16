import { Base, IEnumerator$1, IEnumerator$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, Boolean_$type, typeCast, String_$type, fromEnum, Type, markType } from "./type";
import { Color } from "./Color";
import { Random } from "./Random";
import { Tuple$2 } from "./Tuple$2";
import { Brush } from "./Brush";
import { LinearGradientBrush } from "./LinearGradientBrush";
import { GradientStop } from "./GradientStop";
import { InterpolationMode } from "./InterpolationMode";
import { MathUtil } from "./MathUtil";
import { Dictionary$2 } from "./Dictionary$2";
import { truncate, intDivide } from "./number";

/**
 * @hidden 
 */
export class ColorUtil extends Base {
	static $t: Type = markType(ColorUtil, 'ColorUtil');
	private static r: Random = new Random(0);
	static randomColor(alpha: number): Color {
		return Color.fromArgb(alpha, <number>ColorUtil.r.next2(0, 255), <number>ColorUtil.r.next2(0, 255), <number>ColorUtil.r.next2(0, 255));
	}
	static randomHue(color: Color): Color {
		let ahsv: number[] = ColorUtil.getAHSV(color);
		return ColorUtil.fromAHSV(ahsv[0], <number>ColorUtil.r.next2(0, 359), ahsv[2], ahsv[3]);
	}
	static toGrayscale(color: Color): Color {
		let y: number = 0.299 * <number>color.r + 0.587 * <number>color.g + 0.114 * <number>color.b;
		let newC: Color = Color.fromArgb(color.a, <number>truncate(y), <number>truncate(y), <number>truncate(y));
		return newC;
	}
	static extractColor(brush: Brush): Tuple$2<boolean, Color> {
		let testColor: Color = Color.fromArgb(0, 0, 0, 0);
		if (brush == null) {
			return new Tuple$2<boolean, Color>(Boolean_$type, (<any>Color).$type, true, testColor);
		}
		if (typeCast<LinearGradientBrush>((<any>LinearGradientBrush).$type, brush) !== null) {
			if ((<LinearGradientBrush>brush).gradientStops == null || (<LinearGradientBrush>brush).gradientStops.length < 1) {
				return new Tuple$2<boolean, Color>(Boolean_$type, (<any>Color).$type, false, testColor);
			}
			testColor = (<LinearGradientBrush>brush).gradientStops[0].color;
		} else {
			testColor = (<Brush>brush).color;
		}
		return new Tuple$2<boolean, Color>(Boolean_$type, (<any>Color).$type, true, testColor);
	}
	static getInterpolation(minimum: Color, interpolation_: number, maximum_: Color, interpolationMode: InterpolationMode): Color {
		let min_ = minimum;
		switch (interpolationMode) {
			case InterpolationMode.HSV:
			{
				let b: number[] = ColorUtil.getAHSV(minimum);
				let e: number[] = ColorUtil.getAHSV(maximum_);
				let b1: number = b[1] >= 0 ? b[1] : e[1];
				let e1: number = e[1] >= 0 ? e[1] : b[1];
				if (b1 >= 0 && e1 >= 0 && Math.abs(e1 - b1) > 180) {
					if (e1 > b1) {
						b1 += 360;
					} else {
						e1 += 360;
					}
				}
				interpolation_ = Math.max(0, Math.min(1, interpolation_));
				return ColorUtil.fromAHSV(b[0] + interpolation_ * (e[0] - b[0]), b1 + interpolation_ * (e1 - b1), b[2] + interpolation_ * (e[2] - b[2]), b[3] + interpolation_ * (e[3] - b[3]));
			}

			case InterpolationMode.RGB: return Color.fromArgb(<number>((<any>min_)._a + interpolation_ * ((<any>maximum_)._a - (<any>min_)._a)), <number>((<any>min_)._r + interpolation_ * ((<any>maximum_)._r - (<any>min_)._r)), <number>((<any>min_)._g + interpolation_ * ((<any>maximum_)._g - (<any>min_)._g)), <number>((<any>min_)._b + interpolation_ * ((<any>maximum_)._b - (<any>min_)._b)));
		}

		return minimum;
	}
	static getAHSVInterpolation(minimum: number[], interpolation: number, maximum: number[]): Color {
		let b1: number = minimum[1] >= 0 ? minimum[1] : maximum[1];
		let e1: number = maximum[1] >= 0 ? maximum[1] : minimum[1];
		if (b1 >= 0 && e1 >= 0 && Math.abs(e1 - b1) > 180) {
			if (e1 > b1) {
				b1 += 360;
			} else {
				e1 += 360;
			}
		}
		interpolation = Math.max(0, Math.min(1, interpolation));
		return ColorUtil.fromAHSV(minimum[0] + interpolation * (maximum[0] - minimum[0]), b1 + interpolation * (e1 - b1), minimum[2] + interpolation * (maximum[2] - minimum[2]), minimum[3] + interpolation * (maximum[3] - minimum[3]));
	}
	static getLightened(color: Color, interpolation: number): Color {
		let ahsl: number[] = ColorUtil.getAHSL(color);
		if (interpolation < 0) {
			return ColorUtil.fromAHSL(ahsl[0], ahsl[1], ahsl[2], ahsl[3] * (1 - MathUtil.clamp(-interpolation, 0, 1)));
		} else {
			return ColorUtil.fromAHSL(ahsl[0], ahsl[1], ahsl[2], ahsl[3] + MathUtil.clamp(interpolation, 0, 1) * (1 - ahsl[3]));
		}
	}
	static getSaturated(color: Color, interpolation: number): Color {
		let ahsl: number[] = ColorUtil.getAHSL(color);
		if (interpolation < 0) {
			return ColorUtil.fromAHSL(ahsl[0], ahsl[1], ahsl[2] * (1 - MathUtil.clamp(-interpolation, 0, 1)), ahsl[3]);
		} else {
			return ColorUtil.fromAHSL(ahsl[0], ahsl[1], MathUtil.clamp(ahsl[2] + MathUtil.clamp(interpolation, 0, 1) * (1 - ahsl[2]), 0, 1), ahsl[3]);
		}
	}
	static getsRGB(color: Color): number[] {
		let sr = color.r / 255;
		let sg = color.g / 255;
		let sb = color.b / 255;
		let r = sr < 0.03928 ? sr / 12.92 : Math.pow(((sr + 0.055) / 1.055), 2.4);
		let g = sg < 0.03928 ? sg / 12.92 : Math.pow(((sg + 0.055) / 1.055), 2.4);
		let b = sb < 0.03928 ? sb / 12.92 : Math.pow(((sb + 0.055) / 1.055), 2.4);
		return <number[]>[ r, g, b ];
	}
	static getL(color: Color): number {
		let sRGB = ColorUtil.getsRGB(color);
		let r = sRGB[0];
		let g = sRGB[1];
		let b = sRGB[2];
		return 0.2126 * r + 0.7152 * g + 0.0722 * b;
	}
	static rGBAToRGB(color: Color, background: Color): Color {
		let a = color.a / 255;
		let r = color.r;
		let g = color.g;
		let b = color.b;
		let br = background.r;
		let bg = background.g;
		let bb = background.b;
		let newR = (1 - a) * br + a * r;
		let newG = (1 - a) * bg + a * g;
		let newB = (1 - a) * bb + a * b;
		return Color.fromArgb(255, <number>truncate(Math.round(newR)), <number>truncate(Math.round(newG)), <number>truncate(Math.round(newB)));
	}
	static getAHSL(color: Color): number[] {
		let ahsl: number[] = <number[]>new Array(4);
		let r: number = color.r / 255;
		let g: number = color.g / 255;
		let b: number = color.b / 255;
		let min: number = Math.min(Math.min(r, g), b);
		let max: number = Math.max(Math.max(r, g), b);
		let delta: number = max - min;
		ahsl[0] = color.a / 255;
		ahsl[3] = (max + min) / 2;
		if (delta == 0) {
			ahsl[1] = -1;
			ahsl[2] = 0;
		} else {
			ahsl[1] = ColorUtil.h(max, delta, r, g, b);
			ahsl[2] = ahsl[3] < 0.5 ? delta / (max + min) : delta / (2 - max - min);
		}
		return ahsl;
	}
	static getAHSV(color: Color): number[] {
		let a: number = color.a / 255;
		let r: number = color.r / 255;
		let g: number = color.g / 255;
		let b: number = color.b / 255;
		let min: number = Math.min(r, Math.min(g, b));
		let max: number = Math.max(r, Math.max(g, b));
		let delta: number = max - min;
		let ahsv: number[] = <number[]>new Array(4);
		ahsv[0] = a;
		ahsv[3] = max;
		if (delta == 0) {
			ahsv[1] = -1;
			ahsv[2] = 0;
		} else {
			ahsv[1] = ColorUtil.h(max, delta, r, g, b);
			ahsv[2] = delta / max;
		}
		return ahsv;
	}
	static fromAHSL(alpha: number, hue: number, saturation: number, lightness: number): Color {
		let r: number;
		let g: number;
		let b: number;
		if (saturation == 0) {
			r = lightness;
			g = lightness;
			b = lightness;
		} else {
			let q: number = lightness < 0.5 ? lightness * (1 + saturation) : lightness + saturation - (lightness * saturation);
			let p: number = 2 * lightness - q;
			let hk: number = hue / 360;
			r = ColorUtil.c(p, q, hk + 1 / 3);
			g = ColorUtil.c(p, q, hk);
			b = ColorUtil.c(p, q, hk - 1 / 3);
		}
		return Color.fromArgb(<number>truncate((alpha * 255)), <number>truncate((r * 255)), <number>truncate((g * 255)), <number>truncate((b * 255)));
	}
	static fromAHSV(alpha: number, hue: number, saturation: number, value: number): Color {
		let r: number;
		let g: number;
		let b: number;
		while (hue >= 360) {
			hue -= 360;
		}
		if (saturation == 0) {
			r = value;
			g = value;
			b = value;
		} else {
			hue /= 60;
			let i: number = Math.floor(hue);
			let f: number = hue - i;
			let p: number = value * (1 - saturation);
			let q: number = value * (1 - saturation * f);
			let t: number = value * (1 - saturation * (1 - f));
			switch (<number>truncate(i)) {
				case 0:
				r = value;
				g = t;
				b = p;
				break;

				case 1:
				r = q;
				g = value;
				b = p;
				break;

				case 2:
				r = p;
				g = value;
				b = t;
				break;

				case 3:
				r = p;
				g = q;
				b = value;
				break;

				case 4:
				r = t;
				g = p;
				b = value;
				break;

				default:
				r = value;
				g = p;
				b = q;
				break;

			}

		}
		return Color.fromArgb(<number>truncate((alpha * 255)), <number>truncate((r * 255)), <number>truncate((g * 255)), <number>truncate((b * 255)));
	}
	private static h(max: number, delta: number, r: number, g: number, b: number): number {
		let h: number = r == max ? (g - b) / delta : g == max ? 2 + (b - r) / delta : 4 + (r - g) / delta;
		h *= 60;
		if (h < 0) {
			h += 360;
		}
		return h;
	}
	static c(p: number, q: number, t: number): number {
		t = t < 0 ? t + 1 : t > 1 ? t - 1 : t;
		if (t < 1 / 6) {
			return p + ((q - p) * 6 * t);
		}
		if (t < 1 / 2) {
			return q;
		}
		if (t < 2 / 3) {
			return p + ((q - p) * 6 * (2 / 3 - t));
		}
		return p;
	}
	static colorToInt(color: Color): number {
		let aa: number = color.a / 255;
		let rr: number = <number>truncate((color.r * aa));
		let gg: number = <number>truncate((color.g * aa));
		let bb: number = <number>truncate((color.b * aa));
		return color.a << 24 | rr << 16 | gg << 8 | bb;
	}
	static getColor(brush: Brush): Color {
		return brush.color;
	}
	static colorToString(c: Color, useRgba: boolean): string {
		if (ColorUtil.knownNamesByColor == null) {
			ColorUtil.knownNamesByColor = new Dictionary$2<string, string>(String_$type, String_$type, 0);
			for (let key of fromEnum<string>(ColorUtil.knownColorsByName.keys)) {
				let val = ColorUtil.knownColorsByName.item(key);
				ColorUtil.knownNamesByColor.item(val, key);
			}
		}
		let hexStr: string = ColorUtil.toHex(c);
		if (ColorUtil.knownNamesByColor.containsKey(hexStr.toLowerCase())) {
			return ColorUtil.knownNamesByColor.item(hexStr.toLowerCase());
		}
		if (useRgba) {
			hexStr = ColorUtil.toRgba(c);
		}
		return hexStr;
	}
	static toRgba(c: Color): string {
		return "rgba(" + c.r + ", " + c.g + ", " + c.b + ", " + (c.a / 255) + ")";
	}
	static toHex(c: Color): string {
		if (c.a == 255) {
			return "#" + ColorUtil.convertToHex(c.r) + ColorUtil.convertToHex(c.g) + ColorUtil.convertToHex(c.b);
		} else {
			return "#" + ColorUtil.convertToHex(c.a) + ColorUtil.convertToHex(c.r) + ColorUtil.convertToHex(c.g) + ColorUtil.convertToHex(c.b);
		}
	}
	private static convertToHex(a: number): string {
		let div = intDivide(a, 16);
		let rem = a % 16;
		return ColorUtil.toHexDigit(div) + ColorUtil.toHexDigit(rem);
	}
	private static toHexDigit(div: number): string {
		switch (div) {
			case 0: return "0";
			case 1: return "1";
			case 2: return "2";
			case 3: return "3";
			case 4: return "4";
			case 5: return "5";
			case 6: return "6";
			case 7: return "7";
			case 8: return "8";
			case 9: return "9";
			case 10: return "a";
			case 11: return "b";
			case 12: return "c";
			case 13: return "d";
			case 14: return "e";
			case 15: return "f";
		}

		return "0";
	}
	static fromString(colorFormat: string): Color {
		let c: Color = new Color();
		c.colorString = colorFormat;
		return c;
	}
	static fromBrush(brush: Brush): Color {
		let ret = ((() => {
			let $ret = new Color();
			$ret.a = 255;
			return $ret;
		})());
		ret = ColorUtil.fromString(brush._fill);
		return ret;
	}
	static toBrush(color: Color): Brush {
		let brush: Brush = null;
		let colorStr = color.colorString;
		brush = ((() => {
			let $ret = new Brush();
			$ret.fill = colorStr;
			return $ret;
		})());
		return brush;
	}
	static knownNamesByColor: Dictionary$2<string, string> = null;
	static readonly knownColorsByName: Dictionary$2<string, string> = ((() => {
		let $ret = new Dictionary$2<string, string>(String_$type, String_$type, 0);
		$ret.addItem("transparent", "#000000");
		$ret.addItem("aliceblue", "#f0f8ff");
		$ret.addItem("antiquewhite", "#faebd7");
		$ret.addItem("aqua", "#00ffff");
		$ret.addItem("aquamarine", "#7fffd4");
		$ret.addItem("azure", "#f0ffff");
		$ret.addItem("beige", "#f5f5dc");
		$ret.addItem("bisque", "#ffe4c4");
		$ret.addItem("black", "#000000");
		$ret.addItem("blanchedalmond", "#ffebcd");
		$ret.addItem("blue", "#0000ff");
		$ret.addItem("blueviolet", "#8a2be2");
		$ret.addItem("brown", "#a52a2a");
		$ret.addItem("burlywood", "#deb887");
		$ret.addItem("cadetblue", "#5f9ea0");
		$ret.addItem("chartreuse", "#7fff00");
		$ret.addItem("chocolate", "#d2691e");
		$ret.addItem("coral", "#ff7f50");
		$ret.addItem("cornflowerblue", "#6495ed");
		$ret.addItem("cornsilk", "#fff8dc");
		$ret.addItem("crimson", "#dc143c");
		$ret.addItem("cyan", "#00ffff");
		$ret.addItem("darkblue", "#00008b");
		$ret.addItem("darkcyan", "#008b8b");
		$ret.addItem("darkgoldenrod", "#b8860b");
		$ret.addItem("darkgray", "#a9a9a9");
		$ret.addItem("darkgreen", "#006400");
		$ret.addItem("darkkhaki", "#bdb76b");
		$ret.addItem("darkmagenta", "#8b008b");
		$ret.addItem("darkolivegreen", "#556b2f");
		$ret.addItem("darkorange", "#ff8c00");
		$ret.addItem("darkorchid", "#9932cc");
		$ret.addItem("darkred", "#8b0000");
		$ret.addItem("darksalmon", "#e9967a");
		$ret.addItem("darkseagreen", "#8fbc8f");
		$ret.addItem("darkslateblue", "#483d8b");
		$ret.addItem("darkslategray", "#2f4f4f");
		$ret.addItem("darkturquoise", "#00ced1");
		$ret.addItem("darkviolet", "#9400d3");
		$ret.addItem("deeppink", "#ff1493");
		$ret.addItem("deepskyblue", "#00bfff");
		$ret.addItem("dimgray", "#696969");
		$ret.addItem("dodgerblue", "#1e90ff");
		$ret.addItem("feldspar", "#d19275");
		$ret.addItem("firebrick", "#b22222");
		$ret.addItem("floralwhite", "#fffaf0");
		$ret.addItem("forestgreen", "#228b22");
		$ret.addItem("fuchsia", "#ff00ff");
		$ret.addItem("gainsboro", "#dcdcdc");
		$ret.addItem("ghostwhite", "#f8f8ff");
		$ret.addItem("gold", "#ffd700");
		$ret.addItem("goldenrod", "#daa520");
		$ret.addItem("gray", "#808080");
		$ret.addItem("green", "#008000");
		$ret.addItem("greenyellow", "#adff2f");
		$ret.addItem("honeydew", "#f0fff0");
		$ret.addItem("hotpink", "#ff69b4");
		$ret.addItem("indianred", "#cd5c5c");
		$ret.addItem("indigo", "#4b0082");
		$ret.addItem("ivory", "#fffff0");
		$ret.addItem("khaki", "#f0e68c");
		$ret.addItem("lavender", "#e6e6fa");
		$ret.addItem("lavenderblush", "#fff0f5");
		$ret.addItem("lawngreen", "#7cfc00");
		$ret.addItem("lemonchiffon", "#fffacd");
		$ret.addItem("lightblue", "#add8e6");
		$ret.addItem("lightcoral", "#f08080");
		$ret.addItem("lightcyan", "#e0ffff");
		$ret.addItem("lightgoldenrodyellow", "#fafad2");
		$ret.addItem("lightgray", "#d3d3d3");
		$ret.addItem("lightgreen", "#90ee90");
		$ret.addItem("lightpink", "#ffb6c1");
		$ret.addItem("lightsalmon", "#ffa07a");
		$ret.addItem("lightseagreen", "#20b2aa");
		$ret.addItem("lightskyblue", "#87cefa");
		$ret.addItem("lightslateblue", "#8470ff");
		$ret.addItem("lightslategray", "#778899");
		$ret.addItem("lightsteelblue", "#b0c4de");
		$ret.addItem("lightyellow", "#ffffe0");
		$ret.addItem("lime", "#00ff00");
		$ret.addItem("limegreen", "#32cd32");
		$ret.addItem("linen", "#faf0e6");
		$ret.addItem("magenta", "#ff00ff");
		$ret.addItem("maroon", "#800000");
		$ret.addItem("mediumaquamarine", "#66cdaa");
		$ret.addItem("mediumblue", "#0000cd");
		$ret.addItem("mediumorchid", "#ba55d3");
		$ret.addItem("mediumpurple", "#9370d8");
		$ret.addItem("mediumseagreen", "#3cb371");
		$ret.addItem("mediumslateblue", "#7b68ee");
		$ret.addItem("mediumspringgreen", "#00fa9a");
		$ret.addItem("mediumturquoise", "#48d1cc");
		$ret.addItem("mediumvioletred", "#c71585");
		$ret.addItem("midnightblue", "#191970");
		$ret.addItem("mintcream", "#f5fffa");
		$ret.addItem("mistyrose", "#ffe4e1");
		$ret.addItem("moccasin", "#ffe4b5");
		$ret.addItem("navajowhite", "#ffdead");
		$ret.addItem("navy", "#000080");
		$ret.addItem("oldlace", "#fdf5e6");
		$ret.addItem("olive", "#808000");
		$ret.addItem("olivedrab", "#6b8e23");
		$ret.addItem("orange", "#ffa500");
		$ret.addItem("orangered", "#ff4500");
		$ret.addItem("orchid", "#da70d6");
		$ret.addItem("palegoldenrod", "#eee8aa");
		$ret.addItem("palegreen", "#98fb98");
		$ret.addItem("paleturquoise", "#afeeee");
		$ret.addItem("palevioletred", "#d87093");
		$ret.addItem("papayawhip", "#ffefd5");
		$ret.addItem("peachpuff", "#ffdab9");
		$ret.addItem("peru", "#cd853f");
		$ret.addItem("pink", "#ffc0cb");
		$ret.addItem("plum", "#dda0dd");
		$ret.addItem("powderblue", "#b0e0e6");
		$ret.addItem("purple", "#800080");
		$ret.addItem("red", "#ff0000");
		$ret.addItem("rosybrown", "#bc8f8f");
		$ret.addItem("royalblue", "#4169e1");
		$ret.addItem("saddlebrown", "#8b4513");
		$ret.addItem("salmon", "#fa8072");
		$ret.addItem("sandybrown", "#f4a460");
		$ret.addItem("seagreen", "#2e8b57");
		$ret.addItem("seashell", "#fff5ee");
		$ret.addItem("sienna", "#a0522d");
		$ret.addItem("silver", "#c0c0c0");
		$ret.addItem("skyblue", "#87ceeb");
		$ret.addItem("slateblue", "#6a5acd");
		$ret.addItem("slategray", "#708090");
		$ret.addItem("snow", "#fffafa");
		$ret.addItem("springgreen", "#00ff7f");
		$ret.addItem("steelblue", "#4682b4");
		$ret.addItem("tan", "#d2b48c");
		$ret.addItem("teal", "#008080");
		$ret.addItem("thistle", "#d8bfd8");
		$ret.addItem("tomato", "#ff6347");
		$ret.addItem("turquoise", "#40e0d0");
		$ret.addItem("violet", "#ee82ee");
		$ret.addItem("violetred", "#d02090");
		$ret.addItem("wheat", "#f5deb3");
		$ret.addItem("white", "#ffffff");
		$ret.addItem("whitesmoke", "#f5f5f5");
		$ret.addItem("yellow", "#ffff00");
		$ret.addItem("yellowgreen", "#9acd32");
		return $ret;
	})());
}


