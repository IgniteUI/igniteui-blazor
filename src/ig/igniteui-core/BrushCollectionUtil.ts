import { Base, IList$1, IList$1_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, fromEnum, String_$type, Type, markType } from "./type";
import { Brush } from "./Brush";
import { BrushCollection } from "./BrushCollection";
import { List$1 } from "./List$1";
import { MathUtil } from "./MathUtil";
import { InterpolationMode } from "./InterpolationMode";
import { Color } from "./Color";
import { ColorUtil } from "./ColorUtil";
import { truncate, isNaN_ } from "./number";

/**
 * @hidden 
 */
export class BrushCollectionUtil extends Base {
	static $t: Type = markType(BrushCollectionUtil, 'BrushCollectionUtil');
	static getInterpolatedBrush(target: BrushCollection, index: number): Brush {
		if (isNaN_(index)) {
			return null;
		}
		index = MathUtil.clamp(index, 0, target.count - 1);
		let i: number = <number>truncate(Math.floor(index));
		if (i == index) {
			return target.item(i);
		}
		return BrushCollectionUtil.interpolateBrushes(index - i, target.item(i), target.item(i + 1), target.interpolationMode);
	}
	private static interpolateBrushes(p: number, minBrush: Brush, maxBrush: Brush, InterpolationMode: InterpolationMode): Brush {
		let minFill: Color = minBrush.color;
		let maxFill: Color = maxBrush.color;
		let interp: Color = ColorUtil.getInterpolation(minFill, p, maxFill, InterpolationMode);
		let b: Brush = new Brush();
		b.color = interp;
		return b;
	}
	static from(colors: IList$1<Color>): BrushCollection {
		let brushes = new BrushCollection();
		for (let color of fromEnum<Color>(colors)) {
			let brush = ColorUtil.toBrush(color);
			if (brush != null) {
				brushes.add(brush);
			}
		}
		return brushes;
	}
	static toBrushCollection(v: any): BrushCollection {
		if (v == null) {
			return null;
		}
		let isRGB = true;
		if (typeof v === 'string') {
			let str_ = <string>v;
			str_ = str_.trim();
			let stringArr = <string[]>(str_.split(/[\s,]+(?![^(]*\))/gm));
			for (let i = 0; i < stringArr.length; i++) {
				stringArr[i] = stringArr[i].trim();
			}
			v = stringArr;
		}
		let val = v != null ? (<string[]>v)[0] : null;
		if (typeof val === 'string' && val == "HSV" || val == "RGB") {
			if ((<string[]>v)[0] == "HSV") {
				isRGB = false;
			}
			let newV = <string[]>new Array((<string[]>v).length - 1);
			for (let i1 = 1; i1 < (<string[]>v)[0].length; i1++) {
				newV[i1] = (<string[]>v)[i1];
			}
			v = newV;
		}
		let brushCollection = new BrushCollection();
		for (let i2 = 0; v != null && i2 < (<string[]>v).length; i2++) {
			let brush = Brush.create((<string[]>v)[i2]);
			brushCollection.add(brush);
		}
		return brushCollection;
	}
	static fromBrushCollection(v: BrushCollection): string[] {
		if (v == null) {
			return null;
		}
		let internalCollection = v;
		let ret = new List$1<string>(String_$type, 0);
		for (let i = 0; i < internalCollection.count; i++) {
			let brush = internalCollection.item(i);
			if (brush != null) {
				ret.add(brush._fill);
			}
		}
		return ret.toArray();
	}
}


