import { Base, Boolean_$type, typeCast, String_$type, Type, markType } from "./type";
import { Brush } from "./Brush";
import { Color } from "./Color";
import { LinearGradientBrush } from "./LinearGradientBrush";
import { GradientStop } from "./GradientStop";
import { ColorUtil } from "./ColorUtil";
import { Tuple$2 } from "./Tuple$2";
import { InterpolationMode } from "./InterpolationMode";
import { DomWrapper, DomWrapper_$type, DomRenderer, DomRenderer_$type } from "./dom";
import { BrushCollection } from "./BrushCollection";
import { List$1 } from "./List$1";
import { CssGradientUtil } from "./CssGradientUtil";
import { truncate } from "./number";
import { stringJoin1 } from "./string";

/**
 * @hidden 
 */
export class BrushUtil extends Base {
	static $t: Type = markType(BrushUtil, 'BrushUtil');
	static areEqual(b1: Brush, b2: Brush): boolean {
		if (b1 == null && b2 == null) {
			return true;
		} else if ((b1 != null && b2 == null) || (b1 == null && b2 != null)) {
			return false;
		}
		if ((b1._fill == null && b2._fill != null) || (b1._fill != null && b2._fill == null)) {
			return false;
		} else if (b1._fill == null && b2._fill == null) {
			return b1.color.equals(b2.color) && b1.isGradient == b2.isGradient && b1.isImageFill == b2.isImageFill && b1.isRadialGradient == b2.isRadialGradient;
		} else {
			return Base.equalsStatic(b1, b2);
		}
	}
	static getLightened(brush: Brush, interpolation: number): Brush {
		if (brush == null) {
			return brush;
		}
		if (brush.isGradient) {
			let newBrush = (<LinearGradientBrush>brush).clone();
			for (let i = 0; i < newBrush.gradientStops.length; i++) {
				let currentStop = newBrush.gradientStops[i];
				currentStop.color = ColorUtil.getLightened(currentStop.color, interpolation);
			}
			return newBrush;
		} else {
			let l = ColorUtil.getLightened(brush.color, interpolation);
			return ((() => {
				let $ret = new Brush();
				$ret.color = l;
				return $ret;
			})());
		}
	}
	static getSaturated(brush: Brush, interpolation: number): Brush {
		if (brush == null) {
			return brush;
		}
		if (brush.isGradient) {
			let newBrush = (<LinearGradientBrush>brush).clone();
			for (let i = 0; i < newBrush.gradientStops.length; i++) {
				let currentStop = newBrush.gradientStops[i];
				currentStop.color = ColorUtil.getSaturated(currentStop.color, interpolation);
			}
			return newBrush;
		} else {
			let l = ColorUtil.getSaturated(brush.color, interpolation);
			return ((() => {
				let $ret = new Brush();
				$ret.color = l;
				return $ret;
			})());
		}
	}
	static getOpaque(brush: Brush, opacity: number): Brush {
		if (brush == null) {
			return brush;
		}
		if (brush.isGradient) {
			let newBrush = (<LinearGradientBrush>brush).clone();
			for (let i = 0; i < newBrush.gradientStops.length; i++) {
				let currentStop = newBrush.gradientStops[i];
				currentStop.color = BrushUtil.getOpaque1(currentStop.color, opacity);
			}
			return newBrush;
		} else {
			let l = BrushUtil.getOpaque1(brush.color, opacity);
			return ((() => {
				let $ret = new Brush();
				$ret.color = l;
				return $ret;
			})());
		}
	}
	static getOpaque1(color: Color, opacity: number): Color {
		return Color.fromArgb(<number>truncate(Math.round(255 * opacity)), color.r, color.g, color.b);
	}
	private static extractTestColor(brush: Brush): Tuple$2<boolean, Color> {
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
	static toGrayscale(brush: Brush): Brush {
		if (brush == null) {
			return brush;
		}
		if (typeCast<LinearGradientBrush>((<any>LinearGradientBrush).$type, brush) !== null) {
			let newBrush = (<LinearGradientBrush>brush).clone();
			for (let i = 0; i < newBrush.gradientStops.length; i++) {
				let currentStop = newBrush.gradientStops[i];
				currentStop.color = ColorUtil.toGrayscale(currentStop.color);
			}
			return newBrush;
		} else {
			let newBrush1 = ((() => {
				let $ret = new Brush();
				$ret.color = (<Brush>brush).color;
				return $ret;
			})());
			newBrush1.color = ColorUtil.toGrayscale(newBrush1.color);
			return newBrush1;
		}
	}
	static getContrastingBrush(brush: Brush, darkBrush: Brush, lightBrush: Brush): Brush {
		let ret = BrushUtil.extractTestColor(brush);
		let testColor: Color = new Color();
		if (!ret.item1) {
			return darkBrush;
		} else {
			testColor = ret.item2;
		}
		let lightColor: Color = new Color();
		let darkColor: Color = new Color();
		let dark = BrushUtil.extractTestColor(darkBrush);
		if (!dark.item1) {
			return darkBrush;
		}
		darkColor = dark.item2;
		let light = BrushUtil.extractTestColor(lightBrush);
		if (!light.item1) {
			return darkBrush;
		}
		lightColor = light.item2;
		let rgb = ColorUtil.rGBAToRGB(testColor, lightColor);
		let l1 = ColorUtil.getL(rgb);
		let l2 = ColorUtil.getL(lightColor);
		let lightRatio = (Math.max(l1, l2) + 0.05) / (Math.min(l1, l2) + 0.05);
		rgb = ColorUtil.rGBAToRGB(testColor, darkColor);
		l1 = ColorUtil.getL(rgb);
		l2 = ColorUtil.getL(darkColor);
		let darkRatio = (Math.max(l1, l2) + 0.05) / (Math.min(l1, l2) + 0.05);
		if (darkRatio > lightRatio) {
			return darkBrush;
		}
		return lightBrush;
	}
	static getContrasting(brush: Brush, darkColor: Color, lightColor: Color): Brush {
		let testColor: Color = new Color();
		if (typeCast<LinearGradientBrush>((<any>LinearGradientBrush).$type, brush) !== null) {
			if ((<LinearGradientBrush>brush).gradientStops == null || (<LinearGradientBrush>brush).gradientStops.length < 1) {
				let b = new Brush();
				b.color = darkColor;
				return b;
			}
			testColor = (<LinearGradientBrush>brush).gradientStops[0].color;
		} else {
			testColor = (<Brush>brush).color;
		}
		let rgb = ColorUtil.rGBAToRGB(testColor, lightColor);
		let l1 = ColorUtil.getL(rgb);
		let l2 = ColorUtil.getL(lightColor);
		let lightRatio = (Math.max(l1, l2) + 0.05) / (Math.min(l1, l2) + 0.05);
		rgb = ColorUtil.rGBAToRGB(testColor, darkColor);
		l1 = ColorUtil.getL(rgb);
		l2 = ColorUtil.getL(darkColor);
		let darkRatio = (Math.max(l1, l2) + 0.05) / (Math.min(l1, l2) + 0.05);
		if (darkRatio > lightRatio) {
			return ((() => {
				let $ret = new Brush();
				$ret.color = darkColor;
				return $ret;
			})());
		}
		return ((() => {
			let $ret = new Brush();
			$ret.color = lightColor;
			return $ret;
		})());
	}
	static modifyOpacity(brush: Brush, opacity: number): Brush {
		if (brush == null) {
			return brush;
		}
		if (brush.isGradient) {
			let newBrush = (<LinearGradientBrush>brush).clone();
			for (let i = 0; i < newBrush.gradientStops.length; i++) {
				let currentStop = newBrush.gradientStops[i];
				currentStop.color = Color.fromArgb(<number>truncate(Math.round(currentStop.color.a * opacity)), currentStop.color.r, currentStop.color.g, currentStop.color.b);
			}
			return newBrush;
		} else {
			let l = Color.fromArgb(<number>truncate(Math.round(brush.color.a * opacity)), brush.color.r, brush.color.g, brush.color.b);
			return ((() => {
				let $ret = new Brush();
				$ret.color = l;
				return $ret;
			})());
		}
	}
	static getInterpolation(minimum: Brush, interpolation: number, maximum: Brush, interpolationMode: InterpolationMode): Brush {
		let target: Brush = new Brush();
		if (minimum == null && maximum == null) {
			target._fill = "transparent";
			return target;
		}
		let minSolid: Brush = null, maxSolid: Brush = null;
		let minLinear: LinearGradientBrush = null, maxLinear: LinearGradientBrush = null;
		if (minimum == null) {
			let c: Color = maximum.isGradient ? ((() => {
				let $ret = new Color();
				$ret.a = 0;
				$ret.r = 255;
				$ret.g = 255;
				$ret.b = 255;
				return $ret;
			})()) : ((() => {
				let $ret = new Color();
				$ret.a = 0;
				$ret.r = maximum.color.r;
				$ret.g = maximum.color.g;
				$ret.b = maximum.color.b;
				return $ret;
			})());
			minSolid = ((() => {
				let $ret = new Brush();
				$ret.color = c;
				return $ret;
			})());
		} else {
			if (minimum.isGradient) {
				minLinear = <LinearGradientBrush>minimum;
			} else {
				minSolid = minimum;
			}
		}
		if (maximum == null) {
			let c1: Color = minimum.isGradient ? ((() => {
				let $ret = new Color();
				$ret.a = 0;
				$ret.r = 255;
				$ret.g = 255;
				$ret.b = 255;
				return $ret;
			})()) : ((() => {
				let $ret = new Color();
				$ret.a = 0;
				$ret.r = minimum.color.r;
				$ret.g = minimum.color.g;
				$ret.b = minimum.color.b;
				return $ret;
			})());
			maxSolid = ((() => {
				let $ret = new Brush();
				$ret.color = c1;
				return $ret;
			})());
		} else {
			if (maximum.isGradient) {
				maxLinear = <LinearGradientBrush>maximum;
			} else {
				maxSolid = maximum;
			}
		}
		if (minSolid != null && maxSolid != null) {
			return BrushUtil.solidSolid(minSolid, interpolation, maxSolid, interpolationMode);
		}
		if (minSolid != null && maxLinear != null) {
			return BrushUtil.solidLinear(minSolid, interpolation, maxLinear, interpolationMode);
		}
		if (minLinear != null && maxSolid != null) {
			return BrushUtil.solidLinear(maxSolid, 1 - interpolation, minLinear, interpolationMode);
		}
		if (minLinear != null && maxLinear != null) {
			return BrushUtil.linearLinear(minLinear, interpolation, maxLinear, interpolationMode);
		}
		return target;
	}
	static fromArgb(a: number, r: number, g: number, b: number): Brush {
		let br = new Brush();
		br.color = Color.fromArgb(a, r, g, b);
		return br;
	}
	private static solidSolid(min: Brush, p: number, max: Brush, interpolationMode: InterpolationMode): Brush {
		let b = new Brush();
		b.color = ColorUtil.getInterpolation(min.color, p, max.color, interpolationMode);
		return b;
	}
	private static solidLinear(min: Brush, p: number, max: LinearGradientBrush, interpolationMode: InterpolationMode): Brush {
		let b = new LinearGradientBrush();
		b.gradientStops = BrushUtil.gradientStops1(min.color, p, max.gradientStops, interpolationMode);
		if (max.useCustomDirection) {
			b.useCustomDirection = true;
			b.startX = max.startX;
			b.startY = max.startY;
			b.endX = max.endX;
			b.endY = max.endY;
		}
		return b;
	}
	private static linearLinear(min: LinearGradientBrush, p: number, max: LinearGradientBrush, interpolationMode: InterpolationMode): Brush {
		let b = new LinearGradientBrush();
		b.gradientStops = BrushUtil.gradientStops(min.gradientStops, p, max.gradientStops, interpolationMode);
		if (min.useCustomDirection || max.useCustomDirection) {
			b.useCustomDirection = true;
			b.startX = min.startX + p * (max.startX - min.startX);
			b.startY = min.startY + p * (max.startY - min.startY);
			b.endX = (1 - p) * min.endX + p * max.endX;
			b.endY = (1 - p) * min.endY + p * max.endY;
		}
		return b;
	}
	private static gradientStops1(min: Color, p: number, max: GradientStop[], interpolationMode: InterpolationMode): GradientStop[] {
		let gradientStopCollection: GradientStop[] = <GradientStop[]>new Array(max.length);
		for (let i: number = 0; i < max.length; ++i) {
			gradientStopCollection[i] = ((() => {
				let $ret = new GradientStop();
				$ret.offset = max[i].offset;
				$ret.color = ColorUtil.getInterpolation(min, p, max[i].color, interpolationMode);
				return $ret;
			})());
		}
		return gradientStopCollection;
	}
	private static gradientStops(min: GradientStop[], p: number, max: GradientStop[], interpolationMode: InterpolationMode): GradientStop[] {
		let minimumCount: number = Math.min(min.length, max.length);
		let maxCount: number = Math.max(min.length, max.length);
		let gradientStopCollection: GradientStop[] = <GradientStop[]>new Array(maxCount);
		let i: number = 0;
		for (; i < minimumCount; ++i) {
			gradientStopCollection[i] = ((() => {
				let $ret = new GradientStop();
				$ret.offset = (1 - p) * min[i].offset + p * max[i].offset;
				$ret.color = ColorUtil.getInterpolation(min[i].color, p, max[i].color, interpolationMode);
				return $ret;
			})());
		}
		for (; i < min.length; ++i) {
			gradientStopCollection[i] = ((() => {
				let $ret = new GradientStop();
				$ret.offset = (1 - p) * min[i].offset + p * max[max.length - 1].offset;
				$ret.color = ColorUtil.getInterpolation(min[i].color, p, max[max.length - 1].color, interpolationMode);
				return $ret;
			})());
		}
		for (; i < max.length; ++i) {
			gradientStopCollection[i] = ((() => {
				let $ret = new GradientStop();
				$ret.offset = (1 - p) * min[min.length - 1].offset + p * max[i].offset;
				$ret.color = ColorUtil.getInterpolation(min[min.length - 1].color, p, max[i].color, interpolationMode);
				return $ret;
			})());
		}
		return gradientStopCollection;
	}
	static getCssBrushColors(className: string, obj: DomWrapper): Brush[] {
		let brushes: Brush[] = <Brush[]>new Array(2);
		obj.addClass(className);
		let fill: Brush = new Brush();
		fill._fill = obj.getStyleProperty("background-color");
		let outline: Brush = new Brush();
		outline._fill = obj.getStyleProperty("border-top-color");
		obj.removeClass(className);
		brushes[0] = fill;
		brushes[1] = outline;
		return brushes;
	}
	static getBrushCollection(palleteName_: string, container_: DomRenderer, brushes: BrushCollection, outlines: BrushCollection, defaultColors: string[] = null): { p2: BrushCollection; p3: BrushCollection; } {
		brushes = new BrushCollection();
		outlines = new BrushCollection();
		let tempBrush: Brush;
		let names = new List$1<string>(String_$type, 0);
		names.add("background-color");
		names.add("border-top-color");
		container_.startCSSQuery();
		let palette = container_.getCssDefaultValuesForClassCollection("ui-" + palleteName_ + "-palette-", names.toArray());
		let numPaletteColors: number = palette.length;
		if (numPaletteColors == 0) {
			if (defaultColors == null) {
				defaultColors = <string[]>[ "#B1BFC9", "#50a8be", "#798995", "#fc6754", "#4F606C", "#fec33c", "#374650", "#3c6399", "#162C3B", "#91af49" ];
			}
			for (let i: number = 0; i < defaultColors.length - 1; i += 2) {
				tempBrush = new Brush();
				tempBrush._fill = defaultColors[i];
				outlines.add(tempBrush);
				tempBrush = new Brush();
				tempBrush._fill = defaultColors[i + 1];
				brushes.add(tempBrush);
			}
		}
		for (let i1 = 0; i1 < numPaletteColors; i1++) {
			let fillBrush = new Brush();
			fillBrush._fill = palette[i1][0];
			let outlineBrush = new Brush();
			outlineBrush._fill = palette[i1][1];
			brushes.add(fillBrush);
			outlines.add(outlineBrush);
		}
		container_.endCSSQuery();
		return {
			p2: brushes,
			p3: outlines

		};
	}
	static getGradientBrushCollection(fillGradientPaletteName: string, outlineGradientPaletteName: string, paletteName: string, container_: DomRenderer, brushes: BrushCollection, outlines: BrushCollection, defaultColors: string[] = null): { p4: BrushCollection; p5: BrushCollection; } {
		brushes = new BrushCollection();
		outlines = new BrushCollection();
		if (defaultColors == null) {
			defaultColors = <string[]>[ "#B1BFC9", "#50a8be", "#798995", "#fc6754", "#4F606C", "#fec33c", "#374650", "#3c6399", "#162C3B", "#91af49" ];
		}
		container_.startCSSQuery();
		let names = new List$1<string>(String_$type, 0);
		names.add("background-image");
		let fillsPalette = container_.getCssDefaultValuesForClassCollection(fillGradientPaletteName, names.toArray());
		let numFillsPaletteColors: number = fillsPalette.length;
		let outlinesPalette = container_.getCssDefaultValuesForClassCollection(outlineGradientPaletteName, names.toArray());
		let numOutlinesPaletteColors: number = outlinesPalette.length;
		for (let i: number = 0; i < numFillsPaletteColors; i++) {
			brushes.add(CssGradientUtil.brushFromGradientString(fillsPalette[i][0]));
		}
		for (let i1: number = 0; i1 < numOutlinesPaletteColors; i1++) {
			outlines.add(CssGradientUtil.brushFromGradientString(outlinesPalette[i1][0]));
		}
		names.clear();
		let fillIndex: number = 0;
		let outlineIndex: number = 0;
		let numPaletteColors: number = Math.min(numFillsPaletteColors, numOutlinesPaletteColors);
		let palette: string[][] = null;
		if (numFillsPaletteColors == 0) {
			names.add("background-color");
		}
		if (numOutlinesPaletteColors == 0) {
			names.add("border-top-color");
			outlineIndex = numFillsPaletteColors == 0 ? 1 : 0;
		}
		if (names.count > 0) {
			palette = container_.getCssDefaultValuesForClassCollection(paletteName, names.toArray());
			numPaletteColors = palette.length;
		}
		if (numFillsPaletteColors == 0) {
			if (numPaletteColors > 0) {
				for (let i2 = 0; i2 < numPaletteColors; i2++) {
					let fillBrush = new Brush();
					fillBrush._fill = palette[i2][fillIndex];
					brushes.add(fillBrush);
				}
			} else {
				for (let i3: number = 0; i3 < defaultColors.length - 1; i3 += 2) {
					let fillBrush1 = new Brush();
					fillBrush1 = new Brush();
					fillBrush1._fill = defaultColors[i3 + 1];
					brushes.add(fillBrush1);
				}
			}
		}
		if (numOutlinesPaletteColors == 0) {
			if (numPaletteColors > 0) {
				for (let i4 = 0; i4 < numPaletteColors; i4++) {
					let outlineBrush = new Brush();
					outlineBrush._fill = palette[i4][outlineIndex];
					outlines.add(outlineBrush);
				}
			} else {
				for (let i5: number = 0; i5 < defaultColors.length - 1; i5 += 2) {
					let outlineBrush1 = new Brush();
					outlineBrush1._fill = defaultColors[i5];
					outlines.add(outlineBrush1);
				}
			}
		}
		container_.endCSSQuery();
		return {
			p4: brushes,
			p5: outlines

		};
	}
	static getGradientBrush(gradientClassName: string, className: string, cssProp: string, container_: DomRenderer, fallbackColor: string): Brush {
		let b: Brush = null;
		container_.startCSSQuery();
		let gradientString = container_.getCssDefaultPropertyValue(gradientClassName, "background-image");
		if (gradientString != null) {
			b = CssGradientUtil.brushFromGradientString(gradientString);
		}
		if (b == null) {
			b = new Brush();
			let fillColor = container_.getCssDefaultPropertyValue(className, cssProp);
			b._fill = fillColor != null ? fillColor : fallbackColor;
		}
		container_.endCSSQuery();
		return b;
	}
	static toRgba(brush: Brush): string {
		if (brush == null) {
			return "null";
		}
		if (brush.isGradient) {
			let colors = new List$1<string>(String_$type, 0);
			let gradientStops = (<LinearGradientBrush>brush).gradientStops;
			for (let i = 0; i < gradientStops.length; i++) {
				let color = gradientStops[i].color;
				colors.add(ColorUtil.toRgba(color));
			}
			return stringJoin1<string>(String_$type, " ", colors);
		} else {
			return ColorUtil.toRgba(brush.color);
		}
	}
	static toHex(brush: Brush): string {
		if (brush == null) {
			return "null";
		}
		if (brush.isGradient) {
			let colors = new List$1<string>(String_$type, 0);
			let gradientStops = (<LinearGradientBrush>brush).gradientStops;
			for (let i = 0; i < gradientStops.length; i++) {
				let color = gradientStops[i].color;
				colors.add(ColorUtil.toHex(color));
			}
			return stringJoin1<string>(String_$type, " ", colors);
		} else {
			return ColorUtil.toHex(brush.color);
		}
	}
}


