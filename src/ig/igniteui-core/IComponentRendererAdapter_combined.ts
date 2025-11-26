import { Base, Type, IEnumerator$1, IEnumerator$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, Enum, String_$type, Delegate_$type, fromEnum, EnumUtil, markType } from "./type";
import { DescriptionRef } from "./DescriptionRef";
import { TypeDescriptionPlatform, TypeDescriptionPlatform_$type } from "./TypeDescriptionPlatform";
import { Dictionary$2 } from "./Dictionary$2";
import { HashSet$1 } from "./HashSet$1";
import { List$1 } from "./List$1";
import { TypeDescriptionWellKnownType, TypeDescriptionWellKnownType_$type } from "./TypeDescriptionWellKnownType";
import { Description } from "./Description";
import { stringContains, stringReplace, stringStartsWith } from "./string";
import { tryParseInt32_1 } from "./numberExtended";

/**
 * @hidden 
 */
export interface IComponentRendererAdapter { 
	createObject(type: string, container: any, context: TypeDescriptionContext, nameContext: string): any;
createColorCollection(colors: any[]): any;
createBrushCollection(brushes: any[]): any;
createDoubleCollection(lengths: any[]): any;
coerceToEnum(type: string, context: TypeDescriptionContext, value: string): any;
onUIThread(container: any, action: () => void): void;
setOrUpdateCollectionOnTarget(container: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, context: TypeDescriptionContext, target: any, value: any): void;
onPendingRef(target: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, sourceRef: DescriptionRef): void;
setPropertyValue(target: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, value: any, oldValue: any, sourceRef: DescriptionRef): void;
getPropertyValue(target: any, propertyName: string): any;
forPropertyValueItem(target: any, propertyName: string, forItem: (arg1: any) => void): void;
clearContainer(container: any, context: TypeDescriptionContext, continueAction: (arg1: boolean) => void): void;
getRootObject(container: any): any;
clearCollection(target: any, propertyName: string, metadata: TypeDescriptionMetadata): void;
addItemToCollection(propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any, newIndex: number, item: any): void;
resetPropertyOnTarget(container: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any): void;
replaceItemInCollection(propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any, newIndex: number, item: any): void;
removeItemFromCollection(propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any, oldIndex: number): void;
replaceRootItem(container: any, type: string, context: TypeDescriptionContext, continueAction: (arg1: boolean) => void): void;
removeRootItem(container: any, context: TypeDescriptionContext, continueAction: (arg1: boolean) => void): void;
flushChanges(container: any): void;
executeMethod(target: any, methodName: string, argumentValues: any[], argumentMetadata: TypeDescriptionMetadata[], value: (arg1: any) => void): void;
createHandler(eventName: string, eventHandlerTypeName: string, eventArgsTypeName: string, context: TypeDescriptionContext, callback: (arg1: any, arg2: any) => void): any;
disposeHandler(callback: (arg1: any, arg2: any) => void): void;
serializeBrush(value: any): any;
serializeColor(value: any): any;
serializeBrushCollection(value: any): any;
serializePoint(value: any): any;
serializeSize(value: any): any;
serializeRect(value: any): any;
serializePixelPoint(value: any): any;
serializePixelSize(value: any): any;
serializePixelRect(value: any): any;
serializeColorCollection(value: any): any;
serializeTimespan(value: any): any;
serializeDoubleCollection(value: any): any;
publicCollectionAsObjectArray(value: any): any[];
}

/**
 * @hidden 
 */
export let IComponentRendererAdapter_$type = new Type(null, 'IComponentRendererAdapter');

/**
 * @hidden 
 */
export class TypeDescriptionContext extends Base {
	static $t: Type = markType(TypeDescriptionContext, 'TypeDescriptionContext');
	constructor(adapter: IComponentRendererAdapter, platform: TypeDescriptionPlatform) {
		super();
		this._adapter = adapter;
		this._platform = platform;
	}
	private _metadataStore: Dictionary$2<string, any> = new Dictionary$2<string, any>(String_$type, (<any>Base).$type, 0);
	private _constructorStore: Dictionary$2<string, () => any> = new Dictionary$2<string, () => any>(String_$type, Delegate_$type, 0);
	private _adapter: IComponentRendererAdapter = null;
	private _platform: TypeDescriptionPlatform = <TypeDescriptionPlatform>0;
	private _seenMetadata: HashSet$1<Dictionary$2<string, string>> = new HashSet$1<Dictionary$2<string, string>>((<any>Dictionary$2).$type.specialize(String_$type, String_$type), 0);
	markSeen(metadata: Dictionary$2<string, string>): void {
		if (!this._seenMetadata.contains(metadata)) {
			this._seenMetadata.add_1(metadata);
		}
	}
	hasMetadata(metadata: Dictionary$2<string, string>): boolean {
		return this._seenMetadata.contains(metadata);
	}
	register(typeName: string, metadata: Dictionary$2<string, string>): void {
		this._seenMetadata.add_1(metadata);
		this._metadataStore.item(typeName, metadata);
	}
	registerDescriptionConstructor(typeName: string, construct: () => any): void {
		this._constructorStore.item(typeName, construct);
	}
	static toPascal(key_: string): string {
		if (key_ == null) {
			return null;
		}
		return key_.substr(0, 1).toUpperCase() + key_.substr(1);
	}
	static toCamel(key_: string): string {
		if (key_ == null) {
			return null;
		}
		return key_.substr(0, 1).toLowerCase() + key_.substr(1);
	}
	getAllKeys(): string[] {
		let keys: List$1<string> = new List$1<string>(String_$type, 0);
		for (let key of fromEnum<string>(this._metadataStore.keys)) {
			keys.add(key);
		}
		return keys.toArray();
	}
	getAllProperties(typeName: string): string[] {
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			return new List$1<string>(String_$type, 1, metaStore.keys).toArray();
		} else {
			return null;
		}
	}
	getMetadata(typeName: string, propertyName: string): TypeDescriptionMetadata {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName)) {
			return <TypeDescriptionMetadata>this._metadataStore.item(typeName + "@@" + propertyName);
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metadata: TypeDescriptionMetadata = new TypeDescriptionMetadata();
			metadata.owningContext = this;
			metadata.owningType = typeName;
			metadata.propertyName = propertyName;
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName)) {
				this._metadataStore.item(typeName + "@@" + propertyName, null);
				return null;
			}
			let meta = <string>metaStore.item(propertyName);
			let knownType_o: string = "Unknown";
			let specificType: string = null;
			let specificExternalType: string = null;
			let collectionElementType: string = null;
			if (stringContains(meta, "[")) {
				let start = meta.indexOf('[');
				let end = meta.indexOf(']');
				if (start != 0 && meta.charAt(start - 1) == ',') {
					start = start - 1;
				}
				if (end != meta.length - 1 && meta.charAt(end + 1) == ',') {
					end = end + 1;
				}
				let argsMetaText = meta.substr(start, end - start + 1);
				meta = stringReplace(meta, argsMetaText, "");
			}
			let superMetaPars = meta.split(')');
			if (superMetaPars.length > 1) {
				let nameMap = superMetaPars[0];
				meta = superMetaPars[1];
				nameMap = stringReplace(nameMap, "(", "");
				let nameParts = nameMap.split(',');
				for (let i = 0; i < nameParts.length; i++) {
					let name = nameParts[i];
					let mappingParts = name.split(':');
					let plat = mappingParts[0];
					let platName = mappingParts[1];
					let platform: TypeDescriptionPlatform = TypeDescriptionPlatform.Angular;
					switch (plat) {
						case "web":
						metadata.addMapping(TypeDescriptionPlatform.Angular, platName);
						metadata.addMapping(TypeDescriptionPlatform.React, platName);
						metadata.addMapping(TypeDescriptionPlatform.WebComponents, platName);
						metadata.addMapping(TypeDescriptionPlatform.JQuery, platName);
						continue;

						case "xam":
						metadata.addMapping(TypeDescriptionPlatform.XamarinAndroid, platName);
						metadata.addMapping(TypeDescriptionPlatform.XamariniOS, platName);
						metadata.addMapping(TypeDescriptionPlatform.XamarinForms, platName);
						metadata.addMapping(TypeDescriptionPlatform.UWP, platName);
						metadata.addMapping(TypeDescriptionPlatform.WinUI, platName);
						continue;

						case "w":
						platform = TypeDescriptionPlatform.WPF;
						break;

						case "a":
						platform = TypeDescriptionPlatform.Angular;
						break;

						case "r":
						platform = TypeDescriptionPlatform.React;
						break;

						case "j":
						platform = TypeDescriptionPlatform.JQuery;
						break;

						case "wc":
						platform = TypeDescriptionPlatform.WebComponents;
						break;

						case "xf":
						platform = TypeDescriptionPlatform.XamarinForms;
						break;

						case "xa":
						platform = TypeDescriptionPlatform.XamarinAndroid;
						break;

						case "xi":
						platform = TypeDescriptionPlatform.XamariniOS;
						break;

						case "wf":
						platform = TypeDescriptionPlatform.WindowsForms;
						break;

						case "uwp":
						platform = TypeDescriptionPlatform.UWP;
						break;

						case "winui":
						platform = TypeDescriptionPlatform.WinUI;
						break;

						case "k":
						platform = TypeDescriptionPlatform.Kotlin;
						break;

						case "s":
						platform = TypeDescriptionPlatform.Swift;
						break;

						case "p":
						metadata.addMapping(TypeDescriptionPlatform.XamarinAndroid, platName);
						metadata.addMapping(TypeDescriptionPlatform.XamariniOS, platName);
						metadata.addMapping(TypeDescriptionPlatform.XamarinForms, platName);
						metadata.addMapping(TypeDescriptionPlatform.UWP, platName);
						metadata.addMapping(TypeDescriptionPlatform.WinUI, platName);
						metadata.addMapping(TypeDescriptionPlatform.WindowsForms, platName);
						metadata.addMapping(TypeDescriptionPlatform.Kotlin, platName);
						metadata.addMapping(TypeDescriptionPlatform.Swift, platName);
						continue;

					}

					metadata.addMapping(platform, platName);
				}
			}
			let metaParts = meta.split(':');
			if (metaParts.length >= 4) {
				collectionElementType = metaParts[3];
			}
			if (metaParts.length >= 3) {
				specificType = metaParts[2];
			}
			if (metaParts.length >= 2) {
				specificExternalType = metaParts[1];
			}
			if (metaParts.length >= 1) {
				knownType_o = metaParts[0];
			}
			if (stringStartsWith(knownType_o, "*")) {
				metadata.mustBeFirst = true;
				knownType_o = knownType_o.substr(1);
			}
			metadata.knownType = EnumUtil.getEnumValue<TypeDescriptionWellKnownType>(TypeDescriptionWellKnownType_$type, EnumUtil.parse(TypeDescriptionWellKnownType_$type, knownType_o, true));
			metadata.specificExternalType = specificExternalType;
			metadata.specificType = specificType;
			if (metadata.knownType == TypeDescriptionWellKnownType.EventRef) {
				if (collectionElementType == "customEvent") {
					metadata.isCustomEvent = true;
				}
				if (collectionElementType == "skipWCPrefix") {
					metadata.skipWCEventPrefix = true;
				}
			}
			metadata.collectionElementType = collectionElementType;
			this._metadataStore.item(typeName + "@@" + propertyName, metadata);
			return metadata;
		}
		return null;
	}
	getEventArgsType(typeName: string, eventName: string): string {
		if (this._metadataStore.containsKey(typeName + "@@" + eventName + "@args")) {
			return <string>this._metadataStore.item(typeName + "@@" + eventName + "@args");
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(eventName + "@args")) {
				this._metadataStore.item(typeName + "@@" + eventName + "@args", null);
				return null;
			}
			let args = <string>metaStore.item(eventName + "@args");
			this._metadataStore.item(typeName + "@@" + eventName + "@args", args);
			return args;
		}
		return null;
	}
	createDescriptionForType(type: string): any {
		if (this._constructorStore.containsKey(type)) {
			return this._constructorStore.item(type)();
		}
		return null;
	}
	setDescriptionProperty(desc: Description, key: string, propertyMetaData: TypeDescriptionMetadata, value: any): void {
		if (TypeDescriptionMetadata.shouldCamelize(this._platform)) {
			key = TypeDescriptionContext.toCamel(key);
		} else {
			key = TypeDescriptionContext.toPascal(key);
		}
		this._adapter.setPropertyValue(desc, key, propertyMetaData, value, null, null);
	}
	setDescriptionPropertyPascal(desc: Description, key: string, propertyMetaData: TypeDescriptionMetadata, value: any): void {
		key = TypeDescriptionContext.toPascal(key);
		this._adapter.setPropertyValue(desc, key, propertyMetaData, value, null, null);
	}
	createColorCollection(colors: any[]): any {
		return this._adapter.createColorCollection(colors);
	}
	createBrushCollection(brushes: any[]): any {
		return this._adapter.createBrushCollection(brushes);
	}
	createDoubleCollection(brushes: any[]): any {
		return this._adapter.createDoubleCollection(brushes);
	}
	createObject(type: string, container: any, nameContext: string): any {
		return this._adapter.createObject(type, container, this, nameContext);
	}
	coerceToEnum(type: string, newValue: string, metadata: TypeDescriptionMetadata, allowIntCoerce: boolean): any {
		if (metadata != null && metadata.owningType != null && metadata.propertyName != null && this.hasEnumValues(metadata.owningType, metadata.propertyName) && allowIntCoerce) {
			let names = this.getEnumNames(metadata.owningType, metadata.propertyName);
			let values = this.getEnumValues(metadata.owningType, metadata.propertyName);
			if (names != null && values != null) {
				for (let i = 0; i < names.length; i++) {
					if (names[i].toLowerCase() == newValue.toLowerCase()) {
						let val: number = 0;
						if (((() => { let $ret = tryParseInt32_1(values[i], val); val = $ret.p1; return $ret.ret; })())) {
							return val;
						}
					}
				}
			}
		}
		return this._adapter.coerceToEnum(type, this, newValue);
	}
	getEnumNames(typeName: string, propertyName: string): string[] {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@names")) {
			return <string[]>this._metadataStore.item(typeName + "@@" + propertyName + "@names");
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@names")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@names", null);
				return null;
			}
			let names = <string>metaStore.item(propertyName + "@names");
			let namesSplit = names.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@names", namesSplit);
			return namesSplit;
		} else {
			return null;
		}
	}
	hasEnumValues(typeName: string, propertyName: string): boolean {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@constantValues")) {
			return this._metadataStore.item(typeName + "@@" + propertyName + "@constantValues") != null;
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@constantValues")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@constantValues", null);
				return false;
			}
			let names = <string>metaStore.item(propertyName + "@constantValues");
			let namesSplit = names.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@constantValues", namesSplit);
			return namesSplit != null;
		} else {
			return false;
		}
	}
	getEnumValues(typeName: string, propertyName: string): string[] {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@constantValues")) {
			return <string[]>this._metadataStore.item(typeName + "@@" + propertyName + "@constantValues");
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@constantValues")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@constantValues", null);
				return null;
			}
			let names = <string>metaStore.item(propertyName + "@constantValues");
			let namesSplit = names.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@constantValues", namesSplit);
			return namesSplit;
		} else {
			return null;
		}
	}
	hasNameBinding(typeName: string, propertyName: string): boolean {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@nameBinding")) {
			return <string>this._metadataStore.item(typeName + "@@" + propertyName + "@nameBinding") == "true";
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@nameBinding")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@nameBinding", null);
				return false;
			}
			let isBinding = <string>metaStore.item(propertyName + "@nameBinding");
			this._metadataStore.item(typeName + "@@" + propertyName + "@names", isBinding);
			return isBinding == "true";
		} else {
			return false;
		}
	}
	getMustSetInCodePlatforms(typeName: string, propertyName: string): string[] {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@mustSetInCode")) {
			return <string[]>this._metadataStore.item(typeName + "@@" + propertyName + "@mustSetInCode");
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@mustSetInCode")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@mustSetInCode", null);
				return null;
			}
			let platforms = <string>metaStore.item(propertyName + "@mustSetInCode");
			let platformsSplit = platforms.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@mustSetInCode", platformsSplit);
			return platformsSplit;
		} else {
			return null;
		}
	}
	getStringUnionPlatforms(typeName: string, propertyName: string): string[] {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@stringUnion")) {
			return <string[]>this._metadataStore.item(typeName + "@@" + propertyName + "@stringUnion");
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@stringUnion")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@stringUnion", null);
				return null;
			}
			let platforms = <string>metaStore.item(propertyName + "@stringUnion");
			let platformsSplit = platforms.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@stringUnion", platformsSplit);
			return platformsSplit;
		} else {
			return null;
		}
	}
	hasMustSetInCodePlatforms(typeName: string, propertyName: string): boolean {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@mustSetInCode")) {
			return <string[]>this._metadataStore.item(typeName + "@@" + propertyName + "@mustSetInCode") != null;
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@mustSetInCode")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@mustSetInCode", null);
				return false;
			}
			let platforms = <string>metaStore.item(propertyName + "@mustSetInCode");
			let platformsSplit = platforms.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@mustSetInCode", platformsSplit);
			return true;
		} else {
			return false;
		}
	}
	hasStringUnionPlatforms(typeName: string, propertyName: string): boolean {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@stringUnion")) {
			return <string[]>this._metadataStore.item(typeName + "@@" + propertyName + "@stringUnion") != null;
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@stringUnion")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@stringUnion", null);
				return false;
			}
			let platforms = <string>metaStore.item(propertyName + "@stringUnion");
			let platformsSplit = platforms.split(';');
			this._metadataStore.item(typeName + "@@" + propertyName + "@stringUnion", platformsSplit);
			return true;
		} else {
			return false;
		}
	}
	hasQueryListName(typeName: string, propertyName: string): boolean {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@ngQueryList")) {
			return <string>this._metadataStore.item(typeName + "@@" + propertyName + "@ngQueryList") != null;
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@ngQueryList")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@ngQueryList", null);
				return false;
			}
			let listName = <string>metaStore.item(propertyName + "@ngQueryList");
			this._metadataStore.item(typeName + "@@" + propertyName + "@ngQueryList", listName);
			return listName != null;
		} else {
			return false;
		}
	}
	getQueryListName(typeName: string, propertyName: string): string {
		if (this._metadataStore.containsKey(typeName + "@@" + propertyName + "@ngQueryList")) {
			return <string>this._metadataStore.item(typeName + "@@" + propertyName + "@ngQueryList");
		}
		if (this._metadataStore.containsKey(typeName)) {
			let metaStore = <Dictionary$2<string, string>>this._metadataStore.item(typeName);
			if (!metaStore.containsKey(propertyName + "@ngQueryList")) {
				this._metadataStore.item(typeName + "@@" + propertyName + "@ngQueryList", null);
				return null;
			}
			let listName = <string>metaStore.item(propertyName + "@ngQueryList");
			this._metadataStore.item(typeName + "@@" + propertyName + "@ngQueryList", listName);
			return listName;
		} else {
			return null;
		}
	}
	isPrimitiveType(type: string): boolean {
		return type == "int" || type == "Int32" || type == "short" || type == "Int16" || type == "double" || type == "Double" || type == "float" || type == "Float" || type == "Single" || type == "single" || type == "DateTime" || type == "decimal" || type == "Decimal" || type == "long" || type == "Int64" || type == "byte" || type == "bool" || type == "string" || type == "String";
	}
}

/**
 * @hidden 
 */
export class TypeDescriptionMetadata extends Base {
	static $t: Type = markType(TypeDescriptionMetadata, 'TypeDescriptionMetadata');
	private _owningContext: TypeDescriptionContext = null;
	get owningContext(): TypeDescriptionContext {
		return this._owningContext;
	}
	set owningContext(value: TypeDescriptionContext) {
		this._owningContext = value;
	}
	private _owningType: string = null;
	get owningType(): string {
		return this._owningType;
	}
	set owningType(value: string) {
		this._owningType = value;
	}
	private _propertyName: string = null;
	get propertyName(): string {
		return this._propertyName;
	}
	set propertyName(value: string) {
		this._propertyName = value;
	}
	private _knownType: TypeDescriptionWellKnownType = <TypeDescriptionWellKnownType>0;
	get knownType(): TypeDescriptionWellKnownType {
		return this._knownType;
	}
	set knownType(value: TypeDescriptionWellKnownType) {
		this._knownType = value;
	}
	private _specificType: string = null;
	get specificType(): string {
		return this._specificType;
	}
	set specificType(value: string) {
		this._specificType = value;
	}
	private _specificExternalType: string = null;
	get specificExternalType(): string {
		return this._specificExternalType;
	}
	set specificExternalType(value: string) {
		this._specificExternalType = value;
	}
	private _collectionElementType: string = null;
	get collectionElementType(): string {
		return this._collectionElementType;
	}
	set collectionElementType(value: string) {
		this._collectionElementType = value;
	}
	private _isCustomEvent: boolean = false;
	get isCustomEvent(): boolean {
		return this._isCustomEvent;
	}
	set isCustomEvent(value: boolean) {
		this._isCustomEvent = value;
	}
	private _skipWCEventPrefix: boolean = false;
	get skipWCEventPrefix(): boolean {
		return this._skipWCEventPrefix;
	}
	set skipWCEventPrefix(value: boolean) {
		this._skipWCEventPrefix = value;
	}
	private _mustBeFirst: boolean = false;
	get mustBeFirst(): boolean {
		return this._mustBeFirst;
	}
	set mustBeFirst(value: boolean) {
		this._mustBeFirst = value;
	}
	private _mappings: Dictionary$2<TypeDescriptionPlatform, string> = new Dictionary$2<TypeDescriptionPlatform, string>(TypeDescriptionPlatform_$type, String_$type, 0);
	private _transformNames: Dictionary$2<TypeDescriptionPlatform, string> = new Dictionary$2<TypeDescriptionPlatform, string>(TypeDescriptionPlatform_$type, String_$type, 0);
	addMapping(platform: TypeDescriptionPlatform, platName: string): void {
		if (stringContains(platName, "/")) {
			let platNameParts = platName.split('/');
			this._transformNames.item(platform, platNameParts[1].trim());
			this._mappings.item(platform, platNameParts[0].trim());
		} else {
			this._mappings.item(platform, platName.trim());
		}
	}
	static camelize(name: string): string {
		if (name == null || name.length == 0) {
			return name;
		}
		return name.substr(0, 1).toLowerCase() + name.substr(1);
	}
	static toPascal(name: string): string {
		if (name == null || name.length == 0) {
			return name;
		}
		return name.substr(0, 1).toUpperCase() + name.substr(1);
	}
	getPlatformName(platform: TypeDescriptionPlatform): string {
		let name_o: string = "";
		if (this._mappings.containsKey(platform)) {
			name_o = this._mappings.item(platform);
		} else {
			name_o = this.propertyName;
		}
		if (TypeDescriptionMetadata.shouldCamelize(platform)) {
			return TypeDescriptionMetadata.camelize(name_o);
		}
		return name_o;
	}
	static shouldCamelize(platform: TypeDescriptionPlatform): boolean {
		if (platform == TypeDescriptionPlatform.Angular || platform == TypeDescriptionPlatform.JQuery || platform == TypeDescriptionPlatform.WebComponents || platform == TypeDescriptionPlatform.React || platform == TypeDescriptionPlatform.Swift) {
			return true;
		}
		return false;
	}
	getTransformName(platform: TypeDescriptionPlatform): string {
		if (this._transformNames.containsKey(platform)) {
			return this._transformNames.item(platform);
		} else {
			return null;
		}
	}
}


