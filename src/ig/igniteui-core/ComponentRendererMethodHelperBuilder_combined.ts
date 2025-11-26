import { Base, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, fromEnum, Type, markType, typeGetValue, typeCast, Array_$type, Boolean_$type, EnumUtil } from "./type";
import { List$1 } from "./List$1";
import { JsonDictionaryObject } from "./JsonDictionaryObject";
import { JsonDictionaryArray } from "./JsonDictionaryArray";
import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { JsonDictionaryValue } from "./JsonDictionaryValue";
import { JsonDictionaryValueType } from "./JsonDictionaryValueType";
import { TypeDescriptionWellKnownType, TypeDescriptionWellKnownType_$type } from "./TypeDescriptionWellKnownType";
import { EmbeddedRefDescription } from "./EmbeddedRefDescription";
import { PointDescription } from "./PointDescription";
import { SizeDescription } from "./SizeDescription";
import { RectDescription } from "./RectDescription";
import { dateToStringFormat } from "./dateExtended";
import { isInfinity, isPositiveInfinity, isNaN_ } from "./number";

/**
 * @hidden 
 */
export class ComponentRendererMethodHelperBuilder extends Base {
	static $t: Type = markType(ComponentRendererMethodHelperBuilder, 'ComponentRendererMethodHelperBuilder');
	private _methodName: string = null;
	private _targetRef: string = null;
	private _argBuilders: List$1<ComponentRendererMethodHelperArgumentBuilder> = new List$1<ComponentRendererMethodHelperArgumentBuilder>((<any>ComponentRendererMethodHelperArgumentBuilder).$type, 0);
	private _return: ComponentRendererMethodHelperReturnBuilder = null;
	get methodName(): string {
		return this._methodName;
	}
	get targetRef(): string {
		return this._targetRef;
	}
	getArgumentCount(): number {
		return this._argBuilders.count;
	}
	getArgument(index: number): ComponentRendererMethodHelperArgumentBuilder {
		return this._argBuilders._inner[index];
	}
	hasReturn(): boolean {
		return this._return != null;
	}
	getReturn(): ComponentRendererMethodHelperReturnBuilder {
		return this._return;
	}
	constructor(methodName: string, targetRef: string) {
		super();
		this._methodName = methodName;
		this._targetRef = targetRef;
	}
	private _reusable: boolean = false;
	get reusable(): boolean {
		return this._reusable;
	}
	set reusable(value: boolean) {
		this._reusable = value;
	}
	destroy(): void {
		for (let a of fromEnum<ComponentRendererMethodHelperArgumentBuilder>(this._argBuilders)) {
			a.destroyCore();
		}
		if (this._return != null) {
			this._return.destroyCore();
		}
	}
	argument(): ComponentRendererMethodHelperArgumentBuilder {
		let arg = new ComponentRendererMethodHelperArgumentBuilder(this);
		this._argBuilders.add(arg);
		return arg;
	}
	$return(): ComponentRendererMethodHelperReturnBuilder {
		let arg = new ComponentRendererMethodHelperReturnBuilder(this);
		this._return = arg;
		return arg;
	}
	build(): string {
		let call: JsonDictionaryObject = new JsonDictionaryObject();
		let args = new JsonDictionaryArray();
		call.item("args", args);
		let argsArr: JsonDictionaryItem[] = <JsonDictionaryItem[]>new Array(this._argBuilders.count);
		let i: number = 0;
		for (let a of fromEnum<ComponentRendererMethodHelperArgumentBuilder>(this._argBuilders)) {
			argsArr[i] = a.toJson();
			i++;
		}
		args.items = argsArr;
		if (this._targetRef != null) {
			call.item("targetRef", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._targetRef;
				return $ret;
			})()));
		}
		call.item("methodName", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = this._methodName;
			return $ret;
		})()));
		if (this._return == null) {
			this.$return().asVoid();
		}
		call.item("return", this._return.toJson());
		let ret = call.toJson();
		if (!this._reusable) {
			this.destroy();
		}
		return ret;
	}
}

/**
 * @hidden 
 */
export class ComponentRendererMethodHelperArgumentBuilder extends Base {
	static $t: Type = markType(ComponentRendererMethodHelperArgumentBuilder, 'ComponentRendererMethodHelperArgumentBuilder');
	private _owner: ComponentRendererMethodHelperBuilder = null;
	constructor(owner: ComponentRendererMethodHelperBuilder) {
		super();
		this._owner = owner;
	}
	destroy(): void {
		if (this._owner != null) {
			this._owner.destroy();
		}
	}
	destroyCore(): void {
		this._owner = null;
	}
	argument(): ComponentRendererMethodHelperArgumentBuilder {
		return this._owner.argument();
	}
	$return(): ComponentRendererMethodHelperReturnBuilder {
		return this._owner.$return();
	}
	build(): string {
		return this._owner.build();
	}
	private _wellKnownType: TypeDescriptionWellKnownType = <TypeDescriptionWellKnownType>0;
	private _specificType: string = null;
	private _specificExternalType: string = null;
	private _collectionElementType: string = null;
	private _value: JsonDictionaryItem = null;
	get wellKnownType(): TypeDescriptionWellKnownType {
		return this._wellKnownType;
	}
	get specificExternalType(): string {
		return this._specificExternalType;
	}
	get specificType(): string {
		return this._specificType;
	}
	get collectionElementType(): string {
		return this._collectionElementType;
	}
	get value(): JsonDictionaryItem {
		return this._value;
	}
	asInt(value: number): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "int";
		this._value = ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = <number>value;
			return $ret;
		})());
		return this;
	}
	asIntArray(value: number[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "int";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				arr.items[i] = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.NumberValue;
					$ret.value = <number>value[i];
					return $ret;
				})());
			}
			this._value = arr;
		}
		return this;
	}
	asDouble(value: number): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "double";
		this._value = this.toDoubleValue(value);
		return this;
	}
	asPixel(value: number): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Pixel;
		this._specificType = null;
		this._specificExternalType = "double";
		this._value = this.toDoubleValue(value);
		return this;
	}
	asDate(value: Date): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Date;
		this._specificType = null;
		this._specificExternalType = null;
		this._value = ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = dateToStringFormat(value, "o");
			return $ret;
		})());
		return this;
	}
	asDateArray(value: Date[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "DateTime";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				arr.items[i] = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = dateToStringFormat(value[i], "o");
					return $ret;
				})());
			}
			this._value = arr;
		}
		return this;
	}
	asDoubleArray(value: number[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "double";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				if (isInfinity(value[i])) {
					if (isPositiveInfinity(value[i])) {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.StringValue;
							$ret.value = "@dbl:INFINITY";
							return $ret;
						})());
					} else {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.StringValue;
							$ret.value = "@dbl:-INFINITY";
							return $ret;
						})());
					}
				} else {
					if (isNaN_(<number>value[i])) {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.NullValue;
							$ret.value = null;
							return $ret;
						})());
					} else {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.NumberValue;
							$ret.value = <any>value[i];
							return $ret;
						})());
					}
				}
			}
			this._value = arr;
		}
		return this;
	}
	asShort(value: number): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "short";
		this._value = ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = <number>value;
			return $ret;
		})());
		return this;
	}
	asShortArray(value: number[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "short";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				arr.items[i] = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.NumberValue;
					$ret.value = <number>value[i];
					return $ret;
				})());
			}
			this._value = arr;
		}
		return this;
	}
	asLong(value: number): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "long";
		this._value = ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = <number>value;
			return $ret;
		})());
		return this;
	}
	asLongArray(value: number[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "long";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				arr.items[i] = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.NumberValue;
					$ret.value = <number>value[i];
					return $ret;
				})());
			}
			this._value = arr;
		}
		return this;
	}
	asFloat(value: number): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "float";
		if (isInfinity(value)) {
			if (isPositiveInfinity(value)) {
				this._value = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = "@flt:INFINITY";
					return $ret;
				})());
			} else {
				this._value = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = "@flt:-INFINITY";
					return $ret;
				})());
			}
		} else {
			if (isNaN_(<number>value)) {
				this._value = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.NullValue;
					$ret.value = null;
					return $ret;
				})());
			} else {
				this._value = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.NumberValue;
					$ret.value = <any>value;
					return $ret;
				})());
			}
		}
		return this;
	}
	asFloatArray(value: number[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "float";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				if (isInfinity(value[i])) {
					if (isPositiveInfinity(value[i])) {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.StringValue;
							$ret.value = "@dbl:INFINITY";
							return $ret;
						})());
					} else {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.StringValue;
							$ret.value = "@dbl:-INFINITY";
							return $ret;
						})());
					}
				} else {
					if (isNaN_(<number>value[i])) {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.NullValue;
							$ret.value = null;
							return $ret;
						})());
					} else {
						arr.items[i] = ((() => {
							let $ret = new JsonDictionaryValue();
							$ret.type = JsonDictionaryValueType.NumberValue;
							$ret.value = <any>value[i];
							return $ret;
						})());
					}
				}
			}
			this._value = arr;
		}
		return this;
	}
	asEnum1(enumType: Type, value: any): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.ExportedType;
		this._specificType = "string";
		this._specificExternalType = enumType.typeName;
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = value.toString();
				return $ret;
			})());
		}
		return this;
	}
	asEnum(enumType: string, value: any): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.ExportedType;
		this._specificType = "string";
		this._specificExternalType = enumType;
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = value.toString();
				return $ret;
			})());
		}
		return this;
	}
	asString(value: string): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.string1;
		this._specificType = null;
		this._specificExternalType = "string";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = value;
				return $ret;
			})());
		}
		return this;
	}
	asStringArray(value: string[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "string";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				arr.items[i] = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = value[i];
					return $ret;
				})());
			}
			this._value = arr;
		}
		return this;
	}
	asEmbeddedRefArray(value: EmbeddedRefDescription[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = null;
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				let refr = value[i];
				let dic = new JsonDictionaryObject();
				dic.item("refType", ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = refr.refType;
					return $ret;
				})()));
				dic.item("id", ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = refr.value;
					return $ret;
				})()));
				arr.items[i] = dic;
			}
			this._value = arr;
		}
		return this;
	}
	asPublicTypeRef(typeName: string, refr: EmbeddedRefDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.ExportedType;
		this._specificType = typeName;
		this._specificExternalType = typeName;
		if (refr == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let dic = new JsonDictionaryObject();
			dic.item("refType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = "name";
				return $ret;
			})()));
			dic.item("id", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = refr.value;
				return $ret;
			})()));
			this._value = dic;
		}
		return this;
	}
	asEmbeddedRef(refr: EmbeddedRefDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.ExportedType;
		this._specificType = null;
		this._specificExternalType = null;
		if (refr == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let dic = new JsonDictionaryObject();
			dic.item("refType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = refr.refType;
				return $ret;
			})()));
			dic.item("id", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = refr.value;
				return $ret;
			})()));
			this._value = dic;
		}
		return this;
	}
	private toDoubleValue(d: number): JsonDictionaryValue {
		if (isInfinity(d)) {
			if (isPositiveInfinity(d)) {
				return ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = "@dbl:INFINITY";
					return $ret;
				})());
			} else {
				return ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.StringValue;
					$ret.value = "@dbl:-INFINITY";
					return $ret;
				})());
			}
		}
		return ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = isNaN_(d) ? JsonDictionaryValueType.NullValue : JsonDictionaryValueType.NumberValue;
			$ret.value = isNaN_(d) ? null : <any>d;
			return $ret;
		})());
	}
	asPoint(point: PointDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Point;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("x", this.toDoubleValue(point.x));
		dic.item("y", this.toDoubleValue(point.y));
		this._value = dic;
		return this;
	}
	asPixelPoint(point: PointDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.PixelPoint;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("x", this.toDoubleValue(point.x));
		dic.item("y", this.toDoubleValue(point.y));
		this._value = dic;
		return this;
	}
	asSize(size: SizeDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Size;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("width", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = size.width;
			return $ret;
		})()));
		dic.item("height", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = size.height;
			return $ret;
		})()));
		this._value = dic;
		return this;
	}
	asPixelSize(size: SizeDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.PixelSize;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("width", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = size.width;
			return $ret;
		})()));
		dic.item("height", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = size.height;
			return $ret;
		})()));
		this._value = dic;
		return this;
	}
	asRect(rect: RectDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Rect;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("left", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.left;
			return $ret;
		})()));
		dic.item("top", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.top;
			return $ret;
		})()));
		dic.item("width", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.width;
			return $ret;
		})()));
		dic.item("height", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.height;
			return $ret;
		})()));
		this._value = dic;
		return this;
	}
	asPixelRect(rect: RectDescription): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.PixelRect;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("left", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.left;
			return $ret;
		})()));
		dic.item("top", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.top;
			return $ret;
		})()));
		dic.item("width", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.width;
			return $ret;
		})()));
		dic.item("height", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.NumberValue;
			$ret.value = rect.height;
			return $ret;
		})()));
		this._value = dic;
		return this;
	}
	asMethodRef(refName: string): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.MethodRef;
		this._specificType = null;
		this._specificExternalType = null;
		let dic = new JsonDictionaryObject();
		dic.item("refType", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = "name";
			return $ret;
		})()));
		dic.item("id", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = refName;
			return $ret;
		})()));
		this._value = dic;
		return this;
	}
	asBool(value: boolean): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.string1;
		this._specificType = null;
		this._specificExternalType = "boolean";
		this._value = ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.BooleanValue;
			$ret.value = value;
			return $ret;
		})());
		return this;
	}
	asPrimitive(value: any): ComponentRendererMethodHelperArgumentBuilder {
		if (value == null) {
			this._wellKnownType = TypeDescriptionWellKnownType.Unknown;
			this._specificType = null;
			this._specificExternalType = null;
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		}
		if (typeof value === 'number') {
			return this.asDouble(<number>value);
		}
		if (typeof value === 'number') {
			return this.asInt(typeGetValue(value));
		}
		if (typeof value === 'number') {
			return this.asLong(typeGetValue(value));
		}
		if (typeof value === 'number') {
			return this.asFloat(<number>value);
		}
		if (typeof value === 'number') {
			return this.asShort(typeGetValue(value));
		}
		if (typeof value === 'number') {
			return this.asDouble(<number><number>value);
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			if ((<any[]>value).length > 0) {
				let first = (<any[]>value)[0];
				if (typeof first === 'number') {
					return this.asDoubleArray(<number[]>value);
				}
			}
		}
		if (typeof value === 'string') {
			return this.asString(<string>value);
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			if ((<any[]>value).length > 0) {
				let first1 = (<any[]>value)[0];
				if (typeof first1 === 'string') {
					return this.asStringArray(<string[]>value);
				}
			}
		}
		if (typeCast<boolean>(Boolean_$type, value) !== null) {
			return this.asBool(<boolean>value);
		}
		if (typeCast<EmbeddedRefDescription>((<any>EmbeddedRefDescription).$type, value) !== null) {
			return this.asEmbeddedRef(<EmbeddedRefDescription>value);
		}
		return this.asString(<string>value);
	}
	asPrimitiveArray(value: any): ComponentRendererMethodHelperArgumentBuilder {
		if (value == null) {
			this._wellKnownType = TypeDescriptionWellKnownType.Unknown;
			this._specificType = null;
			this._specificExternalType = null;
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			if ((<any[]>value).length > 0) {
				let first = (<any[]>value)[0];
				if (typeof first === 'number') {
					return this.asDoubleArray(<number[]>value);
				}
			}
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			if ((<any[]>value).length > 0) {
				let first1 = (<any[]>value)[0];
				if (typeof first1 === 'string') {
					return this.asStringArray(<string[]>value);
				}
			}
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			if ((<any[]>value).length > 0) {
				let first2 = (<any[]>value)[0];
				if (typeCast<boolean>(Boolean_$type, first2) !== null) {
					return this.asBoolArray(<boolean[]>value);
				}
			}
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			if ((<any[]>value).length > 0) {
				let first3 = (<any[]>value)[0];
				if (typeCast<EmbeddedRefDescription>((<any>EmbeddedRefDescription).$type, first3) !== null) {
					return this.asEmbeddedRefArray(<EmbeddedRefDescription[]>value);
				}
			}
		}
		return this.asStringArray(<string[]>value);
	}
	asBoolArray(value: boolean[]): ComponentRendererMethodHelperArgumentBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "boolean";
		if (value == null) {
			this._value = ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				$ret.value = null;
				return $ret;
			})());
		} else {
			let arr = new JsonDictionaryArray();
			arr.items = <JsonDictionaryItem[]>new Array(value.length);
			for (let i: number = 0; i < value.length; i++) {
				arr.items[i] = ((() => {
					let $ret = new JsonDictionaryValue();
					$ret.type = JsonDictionaryValueType.BooleanValue;
					$ret.value = value[i];
					return $ret;
				})());
			}
			this._value = arr;
		}
		return this;
	}
	toJson(): JsonDictionaryItem {
		let o = new JsonDictionaryObject();
		o.item("knownType", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = EnumUtil.getName<TypeDescriptionWellKnownType>(TypeDescriptionWellKnownType_$type, this._wellKnownType);
			return $ret;
		})()));
		if (this._specificType != null) {
			o.item("specificType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._specificType;
				return $ret;
			})()));
		}
		if (this._specificExternalType != null) {
			o.item("specificExternalType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._specificExternalType;
				return $ret;
			})()));
		}
		if (this._collectionElementType != null) {
			o.item("collectionElementType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._collectionElementType;
				return $ret;
			})()));
		}
		if (this._value != null) {
			o.item("value", this._value);
		} else {
			o.item("value", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NullValue;
				return $ret;
			})()));
		}
		return o;
	}
}

/**
 * @hidden 
 */
export class ComponentRendererMethodHelperReturnBuilder extends Base {
	static $t: Type = markType(ComponentRendererMethodHelperReturnBuilder, 'ComponentRendererMethodHelperReturnBuilder');
	private _owner: ComponentRendererMethodHelperBuilder = null;
	constructor(owner: ComponentRendererMethodHelperBuilder) {
		super();
		this._owner = owner;
	}
	destroy(): void {
		if (this._owner != null) {
			this._owner.destroy();
		}
	}
	destroyCore(): void {
		this._owner = null;
	}
	argument(): ComponentRendererMethodHelperArgumentBuilder {
		return this._owner.argument();
	}
	$return(): ComponentRendererMethodHelperReturnBuilder {
		return this._owner.$return();
	}
	build(): string {
		let ret = this._owner.build();
		return ret;
	}
	private _wellKnownType: TypeDescriptionWellKnownType = <TypeDescriptionWellKnownType>0;
	private _specificType: string = null;
	private _specificExternalType: string = null;
	private _collectionElementType: string = null;
	get wellKnownType(): TypeDescriptionWellKnownType {
		return this._wellKnownType;
	}
	get specificExternalType(): string {
		return this._specificExternalType;
	}
	get specificType(): string {
		return this._specificType;
	}
	get collectionElementType(): string {
		return this._collectionElementType;
	}
	asInt(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "int";
		return this;
	}
	asPrimitive(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Unknown;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asIntArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "int";
		return this;
	}
	asDouble(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "double";
		return this;
	}
	asPixel(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Pixel;
		this._specificType = null;
		this._specificExternalType = "double";
		return this;
	}
	asDoubleArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "double";
		return this;
	}
	asDate(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Date;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asShort(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "short";
		return this;
	}
	asShortArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "short";
		return this;
	}
	asLong(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "long";
		return this;
	}
	asLongArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "long";
		return this;
	}
	asPrimitiveArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asPublicTypeRefArray(publicTypeName: string): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = publicTypeName;
		return this;
	}
	asFloat(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Number;
		this._specificType = null;
		this._specificExternalType = "float";
		return this;
	}
	asFloatArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "float";
		return this;
	}
	asEnum(enumType: Type): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.ExportedType;
		this._specificType = enumType.typeName;
		this._specificExternalType = "string";
		return this;
	}
	asString(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.string1;
		this._specificType = null;
		this._specificExternalType = "string";
		return this;
	}
	asStringArray(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Array;
		this._specificType = null;
		this._specificExternalType = "string";
		return this;
	}
	asPublicTypeRef(typeName: string): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.ExportedType;
		this._specificType = typeName;
		this._specificExternalType = typeName;
		let dic = new JsonDictionaryObject();
		dic.item("refType", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = "name";
			return $ret;
		})()));
		dic.item("id", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = "name";
			return $ret;
		})()));
		return this;
	}
	asPoint(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Point;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asPixelPoint(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.PixelPoint;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asPIxelSize(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.PixelSize;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asSize(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Size;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asPixelRect(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.PixelRect;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asRect(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Rect;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asMethodRef(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.MethodRef;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
	asBool(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.boolean1;
		this._specificType = null;
		this._specificExternalType = "boolean";
		return this;
	}
	toJson(): JsonDictionaryItem {
		let o = new JsonDictionaryObject();
		o.item("knownType", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = EnumUtil.getName<TypeDescriptionWellKnownType>(TypeDescriptionWellKnownType_$type, this._wellKnownType);
			return $ret;
		})()));
		if (this._specificType != null) {
			o.item("specificType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._specificType;
				return $ret;
			})()));
		}
		if (this._specificExternalType != null) {
			o.item("specificExternalType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._specificExternalType;
				return $ret;
			})()));
		}
		if (this._collectionElementType != null) {
			o.item("collectionElementType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this._collectionElementType;
				return $ret;
			})()));
		}
		return o;
	}
	asVoid(): ComponentRendererMethodHelperReturnBuilder {
		this._wellKnownType = TypeDescriptionWellKnownType.Void;
		this._specificType = null;
		this._specificExternalType = null;
		return this;
	}
}


