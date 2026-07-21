import { Base, String_$type, Delegate_$type, Type, markType } from "./type";
import { ITypeDescriptionPropertyTransforms, ITypeDescriptionPropertyTransforms_$type } from "./ITypeDescriptionPropertyTransforms";
import { Dictionary$2 } from "./Dictionary$2";
import { DescriptionTreeAction } from "./DescriptionTreeAction";
import { DescriptionTreeNode } from "./DescriptionTreeNode";
import { DescriptionPropertyValue } from "./DescriptionPropertyValue";
import { TypeDescriptionPlatform } from "./TypeDescriptionPlatform";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";
import { stringStartsWith, stringReplace } from "./string";
import { dateTryParse } from "./dateExtended";
import { tryParseNumber } from "./number";

/**
 * @hidden 
 */
export class TypeDescriptionPropretyTransforms extends Base implements ITypeDescriptionPropertyTransforms {
	static $t: Type = markType(TypeDescriptionPropretyTransforms, 'TypeDescriptionPropretyTransforms', (<any>Base).$type, [ITypeDescriptionPropertyTransforms_$type]);
	private _transforms: Dictionary$2<string, (arg1: any, arg2: DescriptionTreeAction) => any> = new Dictionary$2<string, (arg1: any, arg2: DescriptionTreeAction) => any>(String_$type, Delegate_$type, 0);
	constructor() {
		super();
		this._transforms.item("VisibilityToBooleanTransform", (l: any, m: DescriptionTreeAction) => {
			if (l.toString().toLowerCase() == "visible") {
				return true;
			}
			return false;
		});
		this._transforms.item("NonStringPrimitive", (l: any, m: DescriptionTreeAction) => {
			let nl = l;
			if (typeof nl === 'string') {
				if (stringStartsWith((<string>nl), "@d:")) {
					nl = (<string>nl).substr(3);
				}
				let dt: Date;
				if (((() => { let $ret = dateTryParse(<string>nl, dt); dt = $ret.p1; return $ret.ret; })())) {
					return dt;
				}
				let dbl: number;
				if (((() => { let $ret = tryParseNumber(<string>nl, dbl); dbl = $ret.p1; return $ret.ret; })())) {
					return dbl;
				}
			}
			return nl;
		});
		this._transforms.item("FontFamilyTransform", (l: any, m: DescriptionTreeAction) => {
			let style = this.getCurrentStyle(m);
			let size = this.getCurrentSize(m);
			let weight = this.getCurrentWeight(m);
			let familyString = <string>l;
			if (familyString == null) {
				familyString = "Verdana";
			}
			return style + " " + weight + " " + size + " " + familyString;
		});
		this._transforms.item("FontStyleTransform", (l: any, m: DescriptionTreeAction) => {
			let family = this.getCurrentFamily(m);
			let size = this.getCurrentSize(m);
			let weight = this.getCurrentWeight(m);
			let styleString = <string>l;
			if (styleString == null) {
				styleString = "normal";
			}
			return styleString + " " + weight + " " + size + " " + family;
		});
		this._transforms.item("FontSizeTransform", (l: any, m: DescriptionTreeAction) => {
			let family = this.getCurrentFamily(m);
			let style = this.getCurrentStyle(m);
			let weight = this.getCurrentWeight(m);
			let sizeString = <string>l != null ? l.toString() + "px" : null;
			if (sizeString == null) {
				sizeString = "14px";
			}
			return style + " " + weight + " " + sizeString + " " + family;
		});
		this._transforms.item("FontWeightTransform", (l: any, m: DescriptionTreeAction) => {
			let family = this.getCurrentFamily(m);
			let size = this.getCurrentSize(m);
			let style = this.getCurrentStyle(m);
			let weightString = <string>l;
			if (weightString == null) {
				weightString = "normal";
			}
			weightString = weightString.toLowerCase();
			return style + " " + weightString + " " + size + " " + family;
		});
	}
	getCurrentFamily(action: DescriptionTreeAction): string {
		let prop = stringReplace(stringReplace(stringReplace(action.propertyName, "Style", "Family"), "Weight", "Family"), "Size", "Family");
		let family: string = "Verdana";
		let target = action.currentNode;
		if (target == null) {
			target = action.targetNode;
		}
		if (target != null) {
			if (target.has(prop)) {
				family = <string>target.get(prop).value;
				if (family == null) {
					family = "Verdana";
				}
			}
		}
		return family;
	}
	getCurrentStyle(action: DescriptionTreeAction): string {
		let prop = stringReplace(stringReplace(stringReplace(action.propertyName, "Family", "Style"), "Weight", "Style"), "Size", "Style");
		let style: string = "normal";
		let target = action.currentNode;
		if (target == null) {
			target = action.targetNode;
		}
		if (target != null) {
			if (target.has(prop)) {
				style = <string>target.get(prop).value;
				if (style == null) {
					style = "normal";
				}
			}
		}
		return style;
	}
	getCurrentSize(action: DescriptionTreeAction): string {
		let prop = stringReplace(stringReplace(stringReplace(action.propertyName, "Family", "Size"), "Weight", "Size"), "Style", "Size");
		let size: string = "14px";
		let target = action.currentNode;
		if (target == null) {
			target = action.targetNode;
		}
		if (target != null) {
			if (target.has(prop)) {
				size = <string>target.get(prop).value;
				if (size == null) {
					size = "14px";
				} else {
					size = size.toString() + "px";
				}
			}
		}
		return size;
	}
	getCurrentWeight(action: DescriptionTreeAction): string {
		let prop = stringReplace(stringReplace(stringReplace(action.propertyName, "Family", "Weight"), "Size", "Weight"), "Style", "Weight");
		let weight: string = "normal";
		let target = action.currentNode;
		if (target == null) {
			target = action.targetNode;
		}
		if (target != null) {
			if (target.has(prop)) {
				weight = <string>target.get(prop).value;
				if (weight == null) {
					weight = "normal";
				}
				weight = weight.toLowerCase();
			}
		}
		return weight;
	}
	transform(platform: TypeDescriptionPlatform, propertyValue: any, action: DescriptionTreeAction): any {
		let meta = action.propertyMetadata;
		if (meta == null) {
			return propertyValue;
		}
		let transformName = meta.getTransformName(platform);
		if (transformName != null && this._transforms.containsKey(transformName)) {
			let transform = this._transforms.item(transformName);
			return transform(propertyValue, action);
		}
		return propertyValue;
	}
}


