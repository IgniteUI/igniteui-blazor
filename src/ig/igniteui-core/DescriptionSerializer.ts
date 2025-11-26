import { Base, NotSupportedException, typeCast, Date_$type, Boolean_$type, Type, markType } from "./type";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { Description } from "./Description";
import { DescriptionTreeBuilderOptions } from "./DescriptionTreeBuilderOptions";
import { DescriptionTreeBuilder } from "./DescriptionTreeBuilder";
import { DescriptionTreeNode } from "./DescriptionTreeNode";
import { DescriptionSerializerBuilder } from "./DescriptionSerializerBuilder";
import { DescriptionResult } from "./DescriptionResult";
import { JsonDictionaryParser } from "./JsonDictionaryParser";
import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { JsonDictionaryObject } from "./JsonDictionaryObject";
import { JsonDictionaryValue } from "./JsonDictionaryValue";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";
import { TypeDescriptionWellKnownType } from "./TypeDescriptionWellKnownType";
import { EmbeddedRefDescription } from "./EmbeddedRefDescription";
import { JsonDictionaryArray } from "./JsonDictionaryArray";
import { PointDescription } from "./PointDescription";
import { SizeDescription } from "./SizeDescription";
import { RectDescription } from "./RectDescription";
import { JsonDictionaryValueType } from "./JsonDictionaryValueType";
import { List$1 } from "./List$1";
import { DescriptionPropertyValue } from "./DescriptionPropertyValue";
import { NotImplementedException } from "./NotImplementedException";
import { Convert } from "./Convert";
import { truncate, isNaN_, isPositiveInfinity, isNegativeInfinity } from "./number";
import { stringEndsWith, stringStartsWith, stringReplace } from "./string";

/**
 * @hidden 
 */
export class DescriptionSerializer extends Base {
	static $t: Type = markType(DescriptionSerializer, 'DescriptionSerializer');
	constructor() {
		super();
	}
	private _throwOnMissingDescription: boolean = false;
	get throwOnMissingDescription(): boolean {
		return this._throwOnMissingDescription;
	}
	set throwOnMissingDescription(value: boolean) {
		this._throwOnMissingDescription = value;
	}
	isModified(context: TypeDescriptionContext, desc: Description, clearModified: boolean): boolean {
		let options = ((() => {
			let $ret = new DescriptionTreeBuilderOptions();
			$ret.checkModifiedFlag = true;
			$ret.clearModifiedFlag = clearModified;
			return $ret;
		})());
		DescriptionTreeBuilder.createTreeWithMoreOptions(context, desc, options);
		return options.isModified;
	}
	serialize(context: TypeDescriptionContext, desc: Description): string {
		let tree = DescriptionTreeBuilder.createTree(context, desc);
		let builder = new DescriptionSerializerBuilder();
		this.serializeHelper(context, tree, builder);
		return builder.toString();
	}
	serializeTree(context: TypeDescriptionContext, tree: DescriptionTreeNode, builder: DescriptionSerializerBuilder): void {
		this.serializeHelper(context, tree, builder);
	}
	deserialize(context: TypeDescriptionContext, json: string): DescriptionResult {
		let parser = new JsonDictionaryParser();
		let item = parser.parse(json);
		return this.deserializeItem(context, item, false, null, false);
	}
	private _forcePascal: boolean = false;
	get forcePascal(): boolean {
		return this._forcePascal;
	}
	set forcePascal(value: boolean) {
		this._forcePascal = value;
	}
	private _transformMemberPaths: boolean = false;
	private _customMemberPathTransformer: (arg1: string) => string = null;
	private _skipProperties: boolean = false;
	deserializeItem(context: TypeDescriptionContext, item: JsonDictionaryItem, transformMemberPaths: boolean, customMemberPathTransformer: (arg1: string) => string, skipProperties: boolean): DescriptionResult {
		if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, item) !== null && (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, item)).type == JsonDictionaryValueType.NullValue) {
			return ((() => {
				let $ret = new DescriptionResult();
				$ret.result = null;
				return $ret;
			})());
		}
		if (!(typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, item) !== null)) {
			throw new NotSupportedException(1, "expected an object at the root of the json");
		}
		let obj = <JsonDictionaryObject>item;
		if (transformMemberPaths) {
			this._transformMemberPaths = true;
			this._customMemberPathTransformer = customMemberPathTransformer;
		}
		if (skipProperties) {
			this._skipProperties = true;
		}
		let ret = this.convertObjToDescription(context, obj);
		this._transformMemberPaths = false;
		this._customMemberPathTransformer = null;
		this._skipProperties = false;
		return ret;
	}
	setDescriptionProperty(context: TypeDescriptionContext, desc: Description, key: string, propertyMetaData: TypeDescriptionMetadata, value: any): void {
		if (this.forcePascal) {
			context.setDescriptionPropertyPascal(desc, key, propertyMetaData, value);
		} else {
			context.setDescriptionProperty(desc, key, propertyMetaData, value);
		}
	}
	private convertObjToDescription(context: TypeDescriptionContext, obj: JsonDictionaryObject, wellKnownType: TypeDescriptionWellKnownType = TypeDescriptionWellKnownType.ExportedType): DescriptionResult {
		if (!obj.containsKey("type") || <string>(<JsonDictionaryValue>obj.item("type")).value == "EmbeddedRef") {
			if (obj.containsKey("refType")) {
				let embed: EmbeddedRefDescription = new EmbeddedRefDescription();
				embed.refType = <string>(<JsonDictionaryValue>obj.item("refType")).value;
				if (obj.containsKey("id")) {
					embed.value = <string>(<JsonDictionaryValue>obj.item("id")).value;
				} else {
					embed.value = <string>(<JsonDictionaryValue>obj.item("value")).value;
				}
				let ret: DescriptionResult = new DescriptionResult();
				ret.result = embed;
				return ret;
			} else {
				throw new NotSupportedException(1, "expected type to be defined for the json object");
			}
		}
		let res: DescriptionResult = new DescriptionResult();
		let type = <string>(<JsonDictionaryValue>obj.item("type")).value;
		let desc = <Description>context.createDescriptionForType(type);
		res.result = desc;
		if (desc == null) {
			if (this.throwOnMissingDescription) {
				throw new NotSupportedException(1, "missing description for type: " + type);
			}
			res.addWarning("couldn't find registered description for type: " + type);
			return res;
		}
		let keys = obj.getKeys();
		for (let i = 0; i < keys.length; i++) {
			let key = keys[i];
			let meta = context.getMetadata(type, TypeDescriptionContext.toPascal(key));
			if (meta == null) {
				if (TypeDescriptionContext.toPascal(key) == "Name") {
					let t = "string";
					let v = <JsonDictionaryValue>obj.item(key);
					let val = this.convertDescriptionValue(res, context, v, t);
					desc.name = <string>val;
				}
				continue;
			}
			let value = obj.item(key);
			if (meta.knownType == TypeDescriptionWellKnownType.Rect || meta.knownType == TypeDescriptionWellKnownType.Point || meta.knownType == TypeDescriptionWellKnownType.Size || meta.knownType == TypeDescriptionWellKnownType.PixelRect || meta.knownType == TypeDescriptionWellKnownType.PixelSize || meta.knownType == TypeDescriptionWellKnownType.PixelPoint) {
				let v1: any = value;
				switch (meta.knownType) {
					case TypeDescriptionWellKnownType.PixelRect:

					case TypeDescriptionWellKnownType.Rect:
					v1 = DescriptionSerializer.convertObjToRect(context, <JsonDictionaryObject>value);
					break;

					case TypeDescriptionWellKnownType.PixelSize:

					case TypeDescriptionWellKnownType.Size:
					v1 = DescriptionSerializer.convertObjToSize(context, <JsonDictionaryObject>value);
					break;

					case TypeDescriptionWellKnownType.PixelPoint:

					case TypeDescriptionWellKnownType.Point:
					v1 = DescriptionSerializer.convertObjToPoint(context, <JsonDictionaryObject>value);
					break;

				}

				if (!this._skipProperties) {
					this.setDescriptionProperty(context, desc, key, meta, v1);
				}
			} else if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, value) !== null) {
				let o = <JsonDictionaryObject>value;
				let subDesc = this.convertObjToDescription(context, o, meta.knownType);
				res.addWarnings(subDesc);
				this.setDescriptionProperty(context, desc, key, meta, subDesc.result);
			} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, value) !== null) {
				let a = <JsonDictionaryArray>value;
				let eleType_o: string = "string";
				let isCollection = false;
				if (meta.knownType == TypeDescriptionWellKnownType.Array) {
					eleType_o = meta.specificExternalType;
				}
				if (meta.knownType == TypeDescriptionWellKnownType.Collection) {
					isCollection = true;
					eleType_o = meta.collectionElementType;
				}
				if (meta.knownType == TypeDescriptionWellKnownType.DoubleCollection) {
					isCollection = true;
					eleType_o = meta.specificExternalType;
				}
				let subArr = this.convertArrayToDescriptionArray(res, context, a, eleType_o);
				if (this._transformMemberPaths) {
					if (meta.propertyName.toLowerCase() == "includedproperties" || meta.propertyName.toLowerCase() == "excludedproperties" || meta.propertyName.toLowerCase() == "includedcolumns" || meta.propertyName.toLowerCase() == "excludedcolumns") {
						let strs = <any[]>subArr;
						let newArr: any[] = <any[]>new Array(strs.length);
						for (let x = 0; x < strs.length; x++) {
							newArr[x] = this.transformMemberPathValue(strs[x]);
						}
						subArr = newArr;
					}
				}
				if (!this._skipProperties || isCollection) {
					this.setDescriptionProperty(context, desc, key, meta, subArr);
				}
			} else {
				let t1 = meta.specificExternalType;
				if (t1 == null && meta.knownType == TypeDescriptionWellKnownType.Date) {
					t1 = "DateTime";
				}
				if (t1 == null && meta.knownType == TypeDescriptionWellKnownType.TimeSpan) {
					t1 = "Timespan";
				}
				if (t1 == null) {
					t1 = "String";
				}
				let v2 = <JsonDictionaryValue>value;
				let val1 = this.convertDescriptionValue(res, context, v2, t1);
				if (this._transformMemberPaths) {
					if (stringEndsWith(meta.propertyName, "MemberPath") || stringEndsWith(meta.propertyName.toLowerCase(), "field") || meta.propertyName.toLowerCase() == "member" || meta.propertyName.toLowerCase() == "membername" || (type != null && stringEndsWith(type, "Axis") && meta.propertyName.toLowerCase() == "label")) {
						val1 = this.transformMemberPathValue(val1);
					}
				}
				if (!this._skipProperties) {
					this.setDescriptionProperty(context, desc, key, meta, val1);
				}
			}
		}
		return res;
	}
	private transformMemberPathValue(v: any): any {
		if (v == null) {
			return null;
		}
		let valStr_o: string = v.toString();
		if (this._customMemberPathTransformer != null) {
			v = this._customMemberPathTransformer(valStr_o);
		} else {
			if (stringStartsWith(valStr_o, "{")) {
				if (!stringStartsWith(valStr_o, "{[")) {
					valStr_o = valStr_o.substr(1, valStr_o.length - 2);
					valStr_o = "{[" + valStr_o + "]}";
					v = valStr_o;
				}
			} else {
				if (!stringStartsWith(valStr_o, "[")) {
					valStr_o = "[" + valStr_o + "]";
					v = valStr_o;
				}
			}
		}
		return v;
	}
	static fromDoubleValue(val: any): number {
		if (val == null) {
			return NaN;
		}
		if (typeof val === 'string') {
			let strVal = <string>val;
			if (strVal == "@dbl:INFINITY") {
				return Number.POSITIVE_INFINITY;
			}
			if (strVal == "@dbl:-INFINITY") {
				return Number.NEGATIVE_INFINITY;
			}
			if (strVal == "Infinity") {
				return Number.POSITIVE_INFINITY;
			}
			if (strVal == "-Infinity") {
				return Number.NEGATIVE_INFINITY;
			}
		}
		return <number>val;
	}
	static convertObjToPoint(context: TypeDescriptionContext, obj: JsonDictionaryObject): any {
		if (obj == null) {
			return null;
		}
		let x: number = NaN;
		let y: number = NaN;
		if (obj.containsKey("x")) {
			x = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("x")).value);
		}
		if (obj.containsKey("y")) {
			y = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("y")).value);
		}
		return ((() => {
			let $ret = new PointDescription();
			$ret.x = x;
			$ret.y = y;
			return $ret;
		})());
	}
	static convertObjToSize(context: TypeDescriptionContext, obj: JsonDictionaryObject): any {
		if (obj == null) {
			return null;
		}
		let width: number = NaN;
		let height: number = NaN;
		if (obj.containsKey("width")) {
			width = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("width")).value);
		}
		if (obj.containsKey("height")) {
			height = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("height")).value);
		}
		return ((() => {
			let $ret = new SizeDescription();
			$ret.width = width;
			$ret.height = height;
			return $ret;
		})());
	}
	static convertObjToRect(context: TypeDescriptionContext, obj: JsonDictionaryObject): any {
		if (obj == null) {
			return null;
		}
		let left: number = NaN;
		let top: number = NaN;
		let width: number = NaN;
		let height: number = NaN;
		if (obj.containsKey("left")) {
			left = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("left")).value);
		}
		if (obj.containsKey("top")) {
			top = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("top")).value);
		}
		if (obj.containsKey("width")) {
			width = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("width")).value);
		}
		if (obj.containsKey("height")) {
			height = DescriptionSerializer.fromDoubleValue((<JsonDictionaryValue>obj.item("height")).value);
		}
		return ((() => {
			let $ret = new RectDescription();
			$ret.left = left;
			$ret.top = top;
			$ret.width = width;
			$ret.height = height;
			return $ret;
		})());
	}
	private convertArrayToDescriptionArray(res: DescriptionResult, context: TypeDescriptionContext, a: JsonDictionaryArray, eleType: string): any[] {
		let ret = <any[]>new Array(a.items.length);
		for (let i = 0; i < a.items.length; i++) {
			let value = a.items[i];
			if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, value) !== null) {
				let o = <JsonDictionaryObject>value;
				let subDesc = this.convertObjToDescription(context, o);
				res.addWarnings(subDesc);
				ret[i] = subDesc.result;
			} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, value) !== null) {
				let sa = <JsonDictionaryArray>value;
				let subArr = this.convertArrayToDescriptionArray(res, context, sa, eleType);
				ret[i] = subArr;
			} else {
				let v = <JsonDictionaryValue>value;
				let val = this.convertDescriptionValue(res, context, v, eleType);
				ret[i] = val;
			}
		}
		return ret;
	}
	private convertDescriptionValue(res: DescriptionResult, context: TypeDescriptionContext, v: JsonDictionaryValue, targetType: string): any {
		switch (v.type) {
			case JsonDictionaryValueType.BooleanValue: return v.value;
			case JsonDictionaryValueType.NullValue:
			switch (targetType.toLowerCase()) {
				case "float": return NaN;
				case "double": return NaN;
			}

			return v.value;

			case JsonDictionaryValueType.NumberValue: switch (targetType.toLowerCase()) {
				case "int": return <number>truncate(<number>v.value);
				case "double":
				if (v.value == null) {
					return NaN;
				} else if (typeof v.value === 'string' && (<string>v.value == "Infinity" || <string>v.value == "INFINITY")) {
					return Number.POSITIVE_INFINITY;
				} else if (typeof v.value === 'string' && (<string>v.value == "-Infinity" || <string>v.value == "-INFINITY")) {
					return Number.NEGATIVE_INFINITY;
				}
				return <number>v.value;

				case "timespan": return <number>truncate(<number>v.value);
				case "short": return <number>truncate(<number>v.value);
				case "long": return <number>truncate(<number>v.value);
				case "float":
				if (v.value == null) {
					return NaN;
				}
				return <number><number>v.value;

				case "byte": return <number>truncate(<number>v.value);
				default:
				res.addWarning("unexpected target type for number: " + targetType);
				return v.value;

			}

			case JsonDictionaryValueType.StringValue:
			if (targetType.toLowerCase() == "datetime") {
				let val_: string = <string>v.value;
				let d: Date = <Date>(new Date(val_));
				return d;
			}
			return v.value;

			default:
			res.addWarning("unexpected value type");
			return v.value;

		}

	}
	private serializeHelper(context: TypeDescriptionContext, tree: DescriptionTreeNode, builder: DescriptionSerializerBuilder): void {
		if (tree == null) {
			builder.append("null");
			return;
		}
		let needsNewLine: boolean = false;
		builder.appendLine("{");
		builder.increaseTabLevel();
		builder.append("\"type\": \"" + tree.type + "\"");
		let items = tree.items();
		for (let i = 0; i < items.count; i++) {
			let item = items._inner[i];
			if (item.propertyName.toLowerCase() == "type") {
				continue;
			}
			if (i >= 0) {
				builder.appendLine(",");
			}
			this.serializeItem(context, tree, item, builder);
			needsNewLine = true;
		}
		if (needsNewLine) {
			builder.appendLine("");
		}
		builder.decreaseTabLevel();
		builder.appendLine("}");
	}
	private camelize(val: string): string {
		if (val == null) {
			return null;
		}
		return val.substr(0, 1).toLowerCase() + val.substr(1);
	}
	private serializeItem(context: TypeDescriptionContext, owner: DescriptionTreeNode, item: DescriptionPropertyValue, builder: DescriptionSerializerBuilder): void {
		if (item.propertyName.toLowerCase() == "type") {
			return;
		}
		builder.append("\"" + this.camelize(item.propertyName) + "\": ");
		this.serializeValue(context, owner, item, item.value, builder);
	}
	private serializeRect(context: TypeDescriptionContext, owner: RectDescription, builder: DescriptionSerializerBuilder): void {
		builder.append("{");
		builder.append("\"width\":");
		this.serializePrimitiveValue(context, "Double", owner.width, builder);
		builder.append(",\"height\":");
		this.serializePrimitiveValue(context, "Double", owner.height, builder);
		builder.append(",\"left\":");
		this.serializePrimitiveValue(context, "Double", owner.left, builder);
		builder.append(",\"top\":");
		this.serializePrimitiveValue(context, "Double", owner.top, builder);
		builder.append("}");
	}
	private serializePoint(context: TypeDescriptionContext, owner: PointDescription, builder: DescriptionSerializerBuilder): void {
		builder.append("{");
		builder.append("\"x\":");
		this.serializePrimitiveValue(context, "Double", owner.x, builder);
		builder.append(",\"y\":");
		this.serializePrimitiveValue(context, "Double", owner.y, builder);
		builder.append("}");
	}
	private serializeSize(context: TypeDescriptionContext, owner: SizeDescription, builder: DescriptionSerializerBuilder): void {
		builder.append("{");
		builder.append("\"width\":");
		this.serializePrimitiveValue(context, "Double", owner.width, builder);
		builder.append(",\"height\":");
		this.serializePrimitiveValue(context, "Double", owner.height, builder);
		builder.append("}");
	}
	private serializeValue(context: TypeDescriptionContext, owner: DescriptionTreeNode, item: DescriptionPropertyValue, value: any, builder: DescriptionSerializerBuilder): void {
		let knownType = TypeDescriptionWellKnownType.string1;
		if (item.metadata != null) {
			knownType = item.metadata.knownType;
		}
		switch (knownType) {
			case TypeDescriptionWellKnownType.BrushCollection:

			case TypeDescriptionWellKnownType.ColorCollection:

			case TypeDescriptionWellKnownType.DoubleCollection:

			case TypeDescriptionWellKnownType.Array:
			{
				builder.append("[");
				let arr = <any[]>item.value;
				let t = item.metadata.specificType;
				if (item.metadata.knownType == TypeDescriptionWellKnownType.BrushCollection || item.metadata.knownType == TypeDescriptionWellKnownType.ColorCollection) {
					t = "String";
				}
				if (t == null) {
					t = item.metadata.specificExternalType;
				}
				for (let i = 0; i < arr.length; i++) {
					if (i > 0) {
						builder.append(", ");
					}
					let itemT = t;
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr[i]) !== null) {
						this.serializeHelper(context, <DescriptionTreeNode>arr[i], builder);
					} else {
						this.serializePrimitiveValue(context, t, arr[i], builder);
					}
				}
				builder.append("]");
			}
			break;

			case TypeDescriptionWellKnownType.boolean1:
			this.serializePrimitiveValue(context, "Boolean", value, builder);
			break;

			case TypeDescriptionWellKnownType.Collection:
			{
				builder.append("[");
				let arr1 = <any[]>item.value;
				let t1 = item.metadata.collectionElementType;
				for (let i1 = 0; i1 < arr1.length; i1++) {
					if (i1 > 0) {
						builder.append(", ");
					}
					let itemT1 = t1;
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr1[i1]) !== null) {
						this.serializeHelper(context, <DescriptionTreeNode>arr1[i1], builder);
					} else {
						this.serializePrimitiveValue(context, t1, arr1[i1], builder);
					}
				}
				builder.append("]");
			}
			break;

			case TypeDescriptionWellKnownType.Brush:

			case TypeDescriptionWellKnownType.MethodRef:

			case TypeDescriptionWellKnownType.EventRef:

			case TypeDescriptionWellKnownType.DataRef:

			case TypeDescriptionWellKnownType.TemplateRef:

			case TypeDescriptionWellKnownType.Color:

			case TypeDescriptionWellKnownType.string1:
			this.serializePrimitiveValue(context, "String", value, builder);
			break;

			case TypeDescriptionWellKnownType.Date:
			this.serializePrimitiveValue(context, "DateTime", value, builder);
			break;

			case TypeDescriptionWellKnownType.ExportedType:
			if (item.metadata.specificExternalType.toLowerCase() == "string") {
				this.serializePrimitiveValue(context, "String", value, builder);
			} else {
				this.serializeHelper(context, <DescriptionTreeNode>value, builder);
			}
			break;

			case TypeDescriptionWellKnownType.Pixel:

			case TypeDescriptionWellKnownType.Number:
			this.serializePrimitiveValue(context, "Double", value, builder);
			break;

			case TypeDescriptionWellKnownType.PixelRect:

			case TypeDescriptionWellKnownType.Rect:
			this.serializeRect(context, <RectDescription>value, builder);
			break;

			case TypeDescriptionWellKnownType.PixelSize:

			case TypeDescriptionWellKnownType.Size:
			this.serializeSize(context, <SizeDescription>value, builder);
			break;

			case TypeDescriptionWellKnownType.PixelPoint:

			case TypeDescriptionWellKnownType.Point:
			this.serializePoint(context, <PointDescription>value, builder);
			break;

			case TypeDescriptionWellKnownType.TimeSpan:
			this.serializePrimitiveValue(context, "TimeSpan", value, builder);
			break;

			case TypeDescriptionWellKnownType.Unknown:
			if (value == null) {
				this.serializePrimitiveValue(context, "String", value, builder);
			} else if (typeof value === 'string') {
				this.serializePrimitiveValue(context, "String", value, builder);
			} else if (typeof value === 'number' || typeof value === 'number' || typeof value === 'number') {
				this.serializePrimitiveValue(context, "Number", value, builder);
			} else if (typeCast<Date>(Date_$type, value) !== null) {
				this.serializePrimitiveValue(context, "DateTime", value, builder);
			} else if (typeCast<boolean>(Boolean_$type, value) !== null) {
				this.serializePrimitiveValue(context, "Boolean", value, builder);
			} else if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, value) !== null) {
				this.serializeHelper(context, <DescriptionTreeNode>value, builder);
			} else {
				throw new NotImplementedException(0);
			}
			break;

			case TypeDescriptionWellKnownType.Void:

			case TypeDescriptionWellKnownType.DataTemplate:

			case TypeDescriptionWellKnownType.IList: throw new NotImplementedException(0);
		}

	}
	private serializePrimitiveValue(context: TypeDescriptionContext, t: string, v: any, builder: DescriptionSerializerBuilder): void {
		switch (t.toLowerCase()) {
			case "string":
			if (v == null) {
				builder.append("null");
			} else {
				builder.append("\"" + stringReplace((<string>v), "\"", "\\\"") + "\"");
			}
			break;

			case "number":

			case "double":
			if (v == null) {
				builder.append("null");
			} else {
				if (!(typeof v === 'number')) {
					v = Convert.toDouble3(v);
				}
				if (isNaN_(<number>v)) {
					builder.append("null");
				} else if (isPositiveInfinity(<number>v)) {
					builder.append("\"Infinity\"");
				} else if (isNegativeInfinity(<number>v)) {
					builder.append("\"-Infinity\"");
				} else {
					builder.append(v.toString());
				}
			}
			break;

			case "int":
			if (v == null) {
				builder.append("null");
			} else {
				builder.append(v.toString());
			}
			break;

			case "float":
			if (v == null) {
				builder.append("null");
			} else {
				if (typeof v === 'number' && isNaN_(<number>v)) {
					builder.append("null");
				} else {
					builder.append(v.toString());
				}
			}
			break;

			case "long":
			if (v == null) {
				builder.append("null");
			} else {
				builder.append(v.toString());
			}
			break;

			case "decimal":
			if (v == null) {
				builder.append("null");
			} else {
				builder.append(v.toString());
			}
			break;

			case "timespan":
			if (v == null) {
				builder.append("null");
			} else {
				builder.append(v.toString());
			}
			break;

			case "boolean":
			builder.append(v == null ? "false" : v.toString().toLowerCase());
			break;

			case "datetime":
			if (v == null) {
				builder.append("null");
			} else {
				let v_ = v;
				builder.append("\"" + <string>(v_.toJSON()) + "\"");
			}
			break;

		}

	}
}


