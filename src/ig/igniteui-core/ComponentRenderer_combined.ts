import { Base, IEnumerator$1, IEnumerator$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, IEnumerable, IEnumerable_$type, BaseError, Point, Type, Enum, IList, IList_$type, fromEnum, typeCast, Date_$type, Boolean_$type, Array_$type, String_$type, typeGetValue, Delegate_$type, runOn, Number_$type, EnumUtil, fromEn, markType, getInstanceType } from "./type";
import { IComponentRendererAdapter } from "./IComponentRendererAdapter";
import { TypeDescriptionContext } from "./TypeDescriptionContext";
import { ITypeDescriptionPropertyTransforms } from "./ITypeDescriptionPropertyTransforms";
import { IComponentRendererSerializationProvider } from "./IComponentRendererSerializationProvider";
import { Dictionary$2 } from "./Dictionary$2";
import { List$1 } from "./List$1";
import { DateColumnCache } from "./DateColumnCache";
import { ComponentRendererReferenceResolverEventArgs } from "./ComponentRendererReferenceResolverEventArgs";
import { Description } from "./Description";
import { DescriptionTreeNode } from "./DescriptionTreeNode";
import { Queue$1 } from "./Queue$1";
import { DescriptionTreeAction } from "./DescriptionTreeAction";
import { DescriptionRef } from "./DescriptionRef";
import { TypeDescriptionPlatform } from "./TypeDescriptionPlatform";
import { TypeDescriptionPropretyTransforms } from "./TypeDescriptionPropretyTransforms";
import { DescriptionSerializerBuilder } from "./DescriptionSerializerBuilder";
import { DescriptionSerializer } from "./DescriptionSerializer";
import { JsonDictionaryParser } from "./JsonDictionaryParser";
import { JsonDictionaryItem } from "./JsonDictionaryItem";
import { DescriptionResult } from "./DescriptionResult";
import { DescriptionTreeBuilder } from "./DescriptionTreeBuilder";
import { DescriptionPropertyValue } from "./DescriptionPropertyValue";
import { JsonDictionaryObject } from "./JsonDictionaryObject";
import { JsonDictionaryValue } from "./JsonDictionaryValue";
import { JsonDictionaryArray } from "./JsonDictionaryArray";
import { NotImplementedException } from "./NotImplementedException";
import { ComponentRendererMethodHelper } from "./ComponentRendererMethodHelper";
import { ComponentRendererMethodHelperBuilder } from "./ComponentRendererMethodHelperBuilder";
import { ComponentRendererMethodHelperArgumentBuilder } from "./ComponentRendererMethodHelperArgumentBuilder";
import { EmbeddedRefDescription } from "./EmbeddedRefDescription";
import { ComponentRendererMethodHelperReturnBuilder } from "./ComponentRendererMethodHelperReturnBuilder";
import { Guid } from "./Guid";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";
import { Convert } from "./Convert";
import { DiffApplyInfo } from "./DiffApplyInfo";
import { DescriptionTreeReconciler } from "./DescriptionTreeReconciler";
import { HashSet$1 } from "./HashSet$1";
import { GlobalAnimationState } from "./GlobalAnimationState";
import { TypeDescriptionWellKnownType, TypeDescriptionWellKnownType_$type } from "./TypeDescriptionWellKnownType";
import { FastReflectionHelper } from "./FastReflectionHelper";
import { Size } from "./Size";
import { Rect } from "./Rect";
import { Brush } from "./Brush";
import { Color } from "./Color";
import { CSSColorUtil } from "./CSSColorUtil";
import { StringDescription } from "./StringDescription";
import { NumberDescription } from "./NumberDescription";
import { PlatformAPIHelper } from "./PlatformAPIHelper";
import { PointDescription } from "./PointDescription";
import { SizeDescription } from "./SizeDescription";
import { RectDescription } from "./RectDescription";
import { BrushDescription } from "./BrushDescription";
import { ColorDescription } from "./ColorDescription";
import { DescriptionTreeActionType } from "./DescriptionTreeActionType";
import { TypeDescriptionPropretyTransformsMultipleSets } from "./TypeDescriptionPropretyTransformsMultipleSets";
import { TypeDescriptionPropretyTransformsMultipleSetsInfo } from "./TypeDescriptionPropretyTransformsMultipleSetsInfo";
import { Tuple$2 } from "./Tuple$2";
import { DescriptionRefValueChangedEventArgs } from "./DescriptionRefValueChangedEventArgs";
import { DescriptionRefTargetInfo } from "./DescriptionRefTargetInfo";
import { RefValueChangedTarget } from "./RefValueChangedTarget";
import { FontRegistry } from "./FontRegistry";
import { truncate } from "./number";
import { JsonDictionaryValueType } from "./JsonDictionaryValueType";
import { ComponentRendererAdapter } from "./ComponentRendererAdapter";
import { stringIsNullOrEmpty, stringEndsWith, stringStartsWith, stringReplace } from "./string";
import { dateTryParse, dateParse } from "./dateExtended";

/**
 * @hidden 
 */
export class ComponentRenderer extends Base {
	static $t: Type = markType(ComponentRenderer, 'ComponentRenderer');
	private static _defaultInstance: ComponentRenderer = null;
	static get defaultInstance(): ComponentRenderer {
		return ComponentRenderer._defaultInstance;
	}
	static set defaultInstance(value: ComponentRenderer) {
		ComponentRenderer._defaultInstance = value;
	}
	private _adapter: IComponentRendererAdapter = null;
	get adapter(): IComponentRendererAdapter {
		return this._adapter;
	}
	set adapter(value: IComponentRendererAdapter) {
		this._adapter = value;
	}
	private _context: TypeDescriptionContext = null;
	get context(): TypeDescriptionContext {
		return this._context;
	}
	set context(value: TypeDescriptionContext) {
		this._context = value;
	}
	private _isProceedOnErrorEnabled: boolean = false;
	get isProceedOnErrorEnabled(): boolean {
		return this._isProceedOnErrorEnabled;
	}
	set isProceedOnErrorEnabled(value: boolean) {
		this._isProceedOnErrorEnabled = value;
	}
	private _transformer: ITypeDescriptionPropertyTransforms = null;
	get transformer(): ITypeDescriptionPropertyTransforms {
		return this._transformer;
	}
	set transformer(value: ITypeDescriptionPropertyTransforms) {
		this._transformer = value;
	}
	private _cleanupMethods: TypeDescriptionCleanups = null;
	get cleanupMethods(): TypeDescriptionCleanups {
		return this._cleanupMethods;
	}
	set cleanupMethods(value: TypeDescriptionCleanups) {
		this._cleanupMethods = value;
	}
	static platform: TypeDescriptionPlatform = TypeDescriptionPlatform.Angular;
	constructor() {
		super();
		let componentRendererAdapter = this.createAdapter();
		this.adapter = componentRendererAdapter;
		let adapter_ = this.adapter;
		let platformString = <string>((adapter_ as any)._platform);
		if (platformString == "Igc") {
			ComponentRenderer.platform = TypeDescriptionPlatform.WebComponents;
		} else if (platformString == "Igr") {
			ComponentRenderer.platform = TypeDescriptionPlatform.React;
		}
		this.context = new TypeDescriptionContext(this.adapter, ComponentRenderer.platform);
		this.transformer = new TypeDescriptionPropretyTransforms();
		this.cleanupMethods = new TypeDescriptionCleanups(this);
	}
	private createAdapter(): IComponentRendererAdapter {
		return <IComponentRendererAdapter><any>(new ComponentRendererAdapter());
	}
	private _serializationProvider: IComponentRendererSerializationProvider = null;
	get serializationProvider(): IComponentRendererSerializationProvider {
		return this._serializationProvider;
	}
	set serializationProvider(value: IComponentRendererSerializationProvider) {
		this._serializationProvider = value;
	}
	toJson(getContainerName: (arg1: any) => string): string {
		if (this._serializationProvider == null) {
			this._errors.add("No serialization provider available");
			return null;
		}
		let ret: DescriptionSerializerBuilder = new DescriptionSerializerBuilder();
		let ser: DescriptionSerializer = new DescriptionSerializer();
		ret.appendLine("{");
		ret.increaseTabLevel();
		ret.appendLine("\"descriptions\": {");
		ret.increaseTabLevel();
		let first: boolean = true;
		for (let key of fromEnum<any>(this._currentDescription.keys)) {
			if (first) {
				first = false;
			} else {
				ret.appendLine(",");
			}
			let container = key;
			let tree = this._currentDescription.item(key);
			let containerName_o: string = "root";
			containerName_o = getContainerName(container);
			if (stringIsNullOrEmpty(containerName_o)) {
				containerName_o = "root";
			}
			ret.append("\"" + containerName_o + "\"");
			ret.append(": ");
			ser.serializeTree(this.context, tree, ret);
		}
		ret.appendLine("");
		ret.decreaseTabLevel();
		ret.append("}");
		if (this._userValues.count > 0) {
			ret.append(",");
		}
		ret.appendLine("");
		if (this._userValues.count > 0) {
			first = true;
			ret.appendLine("refs: {");
			ret.increaseTabLevel();
			for (let key1 of fromEnum<string>(this._userValues.keys)) {
				if (!this._serializationProvider.canSerializeRef(key1, this._userValues.item(key1))) {
					continue;
				}
				if (first) {
					first = false;
				} else {
					ret.appendLine(",");
				}
				this._serializationProvider.serializeRef(ret, key1, this._userValues.item(key1));
			}
			ret.appendLine("");
			ret.decreaseTabLevel();
			ret.appendLine("}");
		}
		ret.decreaseTabLevel();
		ret.appendLine("}");
		return ret.toString();
	}
	createObjectFromJson(json: string, container: any): any {
		let par: JsonDictionaryParser = new JsonDictionaryParser();
		let dict = par.parse(json);
		let ser: DescriptionSerializer = new DescriptionSerializer();
		let transformMemberPaths: boolean = false;
		let res = ser.deserializeItem(this.context, dict, transformMemberPaths, null, false);
		if (res.result == null) {
			return null;
		}
		let node = DescriptionTreeBuilder.createTree(this.context, res.result);
		let state: ContainerState = this._states.item(container);
		if (node.has("Type")) {
			let t = <string>node.get("Type").value;
			return this.createObject(t, node, container, state, true, -1, null);
		} else {
			return this.convertJsonObjectToObject(typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, dict), null, null);
		}
	}
	private isPrimitiveValue(v: any): boolean {
		if (typeof v === 'string') {
			return true;
		}
		if (typeof v === 'number' || typeof v === 'number' || typeof v === 'number' || typeof v === 'number' || typeof v === 'number' || typeof v === 'number' || typeof v === 'number') {
			return true;
		}
		if (typeCast<Date>(Date_$type, v) !== null) {
			return true;
		}
		if (typeCast<boolean>(Boolean_$type, v) !== null) {
			return true;
		}
		return false;
	}
	private isCollection(v: any): boolean {
		if (typeCast<any[]>(Array_$type, v) !== null) {
			return true;
		}
		if (typeCast<IEnumerable>(IEnumerable_$type, v) !== null) {
			return true;
		}
		return false;
	}
	loadJson(json: string, getContainer: (arg1: string) => any): void {
		this.loadJsonHelper(json, getContainer, null, false, false, false);
	}
	loadJsonOverlay(json: string, getContainer: (arg1: string) => any, getTarget: (arg1: string) => any): void {
		this.loadJsonHelper(json, getContainer, getTarget, true, false, true);
	}
	loadJsonDelta(json: string, getContainer: (arg1: string) => any, skipApply: boolean): void {
		this.loadJsonHelper(json, getContainer, null, true, skipApply, false);
	}
	private _preserveKeyOrder: boolean = false;
	get preserveKeyOrder(): boolean {
		return this._preserveKeyOrder;
	}
	set preserveKeyOrder(value: boolean) {
		this._preserveKeyOrder = value;
	}
	protected shouldForcePascal(): boolean {
		return false;
	}
	protected onSkipAlterDataCasingAltered(value: boolean): void {
	}
	private _allowNullForRemove: boolean = false;
	get allowNullForRemove(): boolean {
		return this._allowNullForRemove;
	}
	set allowNullForRemove(value: boolean) {
		this._allowNullForRemove = value;
	}
	private _cleanupUnusedOnRender: boolean = false;
	get cleanupUnusedOnRender(): boolean {
		return this._cleanupUnusedOnRender;
	}
	set cleanupUnusedOnRender(value: boolean) {
		this._cleanupUnusedOnRender = value;
	}
	private _skipSystemRefsClean: number = 0;
	get skipSystemRefsClean(): number {
		return this._skipSystemRefsClean;
	}
	set skipSystemRefsClean(value: number) {
		this._skipSystemRefsClean = value;
	}
	private _errors: List$1<string> = new List$1<string>(String_$type, 0);
	private logError(str: string): void {
		this._errors.add(str);
	}
	hasErrors(): boolean {
		return this._errors.count > 0;
	}
	getErrors(): string[] {
		return this._errors.toArray();
	}
	clearErrors(): void {
		this._errors.clear();
	}
	private loadJsonHelper(json: string, getContainer: (arg1: string) => any, getTarget: (arg1: string) => any, isDelta: boolean, skipApply: boolean, skipCreate: boolean): void {
		let par: JsonDictionaryParser = new JsonDictionaryParser();
		let dict = par.parse(json);
		let ser: DescriptionSerializer = new DescriptionSerializer();
		ser.throwOnMissingDescription = this.isProceedOnErrorEnabled;
		ser.forcePascal = this.shouldForcePascal();
		let lastContainer: any = null;
		if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, dict) !== null) {
			let obj = <JsonDictionaryObject>dict;
			if (obj.containsKey("skipAlterDataCasing")) {
				let skipAlterDataCasing: JsonDictionaryValue = <JsonDictionaryValue>obj.item("skipAlterDataCasing");
				if ((<boolean>skipAlterDataCasing.value)) {
					this.onSkipAlterDataCasingAltered(true);
				}
			} else {
				this.onSkipAlterDataCasingAltered(false);
			}
			let animationIdleRefName: string = null;
			if (obj.containsKey("animationIdleRef") || obj.containsKey("hasAnimations")) {
				animationIdleRefName = "AnimationIdleHandler";
				if (obj.containsKey("animationIdleRef")) {
					animationIdleRefName = <string>(<JsonDictionaryValue>obj.item("animationIdleRef")).value;
				}
			}
			let animationIdleTimeout: number = 0;
			if (obj.containsKey("animationIdleTimeout")) {
				animationIdleTimeout = <number>truncate(<number>(<JsonDictionaryValue>obj.item("animationIdleTimeout")).value);
			}
			if (obj.containsKey("descriptions")) {
				let descriptions = obj.item("descriptions");
				if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, descriptions) !== null) {
					let descObj = <JsonDictionaryObject>descriptions;
					let keys = descObj.getKeys();
					for (let i = 0; i < keys.length; i++) {
						let key = keys[i];
						let item = descObj.item(key);
						let cont = getContainer(key);
						if (cont == null) {
							continue;
						}
						lastContainer = cont;
						let transformMemberPaths: boolean = false;
						let customMemberPathTransformer = this.resolveCustomMemberPathTransformer();
						if (customMemberPathTransformer != null) {
							transformMemberPaths = true;
						}
						let d: DescriptionResult = null;
						if (this.isProceedOnErrorEnabled) {
							try {
								d = ser.deserializeItem(this.context, item, transformMemberPaths, customMemberPathTransformer, false);
							}
							catch (e) {
								this.logError("error deserializing item: " + e.toString());
								return;
							}
						} else {
							d = ser.deserializeItem(this.context, item, transformMemberPaths, customMemberPathTransformer, false);
						}
						if (d.result == null && !this.allowNullForRemove) {
							continue;
						}
						if (skipCreate) {
							if (!this.states.containsKey(cont)) {
								let dInit: DescriptionResult = null;
								if (this.isProceedOnErrorEnabled) {
									try {
										dInit = ser.deserializeItem(this.context, item, transformMemberPaths, customMemberPathTransformer, true);
									}
									catch (e1) {
										this.logError("error deserializing item: " + e1.toString());
										return;
									}
								} else {
									dInit = ser.deserializeItem(this.context, item, transformMemberPaths, customMemberPathTransformer, true);
								}
								this.renderHelper(dInit.result, cont, isDelta, true, this.cleanupUnusedOnRender, animationIdleRefName, animationIdleTimeout);
								animationIdleRefName = null;
								let c = getTarget(key);
								let s = this.states.item(cont);
								this.ensureComponentsOverlay(this._currentDescription.item(cont), c, s, cont);
							}
						}
						this.renderHelper(d.result, cont, isDelta, skipApply, this.cleanupUnusedOnRender, animationIdleRefName, animationIdleTimeout);
						animationIdleRefName = null;
					}
				}
			}
			if (obj.containsKey("refs") && lastContainer != null) {
				let refs = obj.item("refs");
				let handled = this.processRefs(refs);
				if (!handled && typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, refs) !== null) {
					let refsObj = <JsonDictionaryObject>refs;
					let keys1 = refsObj.getKeys();
					for (let i1 = 0; i1 < keys1.length; i1++) {
						let key1 = keys1[i1];
						let item1 = refsObj.item(key1);
						if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, item1) !== null) {
							let val = <JsonDictionaryValue>item1;
							this.provideRefValue(lastContainer, key1, val.value);
						} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, item1) !== null) {
							let valArr = <JsonDictionaryArray>item1;
							let array: any[] = this.convertJSonArrayToObjectArray(valArr, null, null);
							this.provideRefValue(lastContainer, key1, array);
						} else {
							let itemObj = this.convertJsonObjectToObject(<JsonDictionaryObject>item1, null, null);
							this.provideRefValue(lastContainer, key1, itemObj);
						}
					}
				}
			}
			if (obj.containsKey("refMessages") && lastContainer != null) {
				let refMessages = obj.item("refMessages");
				let handled1 = this.processRefs(refMessages);
				if (!handled1 && typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, refMessages) !== null) {
					let messagesArray = <JsonDictionaryArray>refMessages;
					if (messagesArray.items != null && messagesArray.items.length > 0) {
						for (let i2 = 0; i2 < messagesArray.items.length; i2++) {
							let targetContainer = lastContainer;
							let msg = messagesArray.items[i2];
							if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, msg) !== null && (<JsonDictionaryObject>msg).containsKey("descriptionKey")) {
								targetContainer = getContainer((<JsonDictionaryValue>(<JsonDictionaryObject>msg).item("descriptionKey")).value.toString());
							}
							this.processRefMessage(targetContainer, msg);
						}
					}
				}
			}
			if (obj.containsKey("modules") && lastContainer != null) {
				let modules = obj.item("modules");
				this.processModules(modules);
			}
			if (obj.containsKey("strings") && lastContainer != null) {
				let strings = obj.item("strings");
				this.processStrings(strings);
			}
			if (obj.containsKey("onInit") && lastContainer != null) {
				let initializers = obj.item("onInit");
				this.processOnInit(initializers);
			}
			if (obj.containsKey("onViewInit") && lastContainer != null) {
				let initializers1 = obj.item("onViewInit");
				this.processOnViewInit(initializers1);
			}
		}
	}
	private processRefMessage(cont: any, msg: JsonDictionaryItem): void {
		if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, msg) !== null) {
			let msgo = <JsonDictionaryObject>msg;
			if (msgo.containsKey("type")) {
				let type = <string>(<JsonDictionaryValue>msgo.item("type")).value;
				switch (type) {
					case "refChanged":
					this.onRefChangedMessage(cont, msgo);
					break;

					case "refClearItems":
					this.onRefClearItemsMessage(cont, msgo);
					break;

					case "refNotifyInsertItem":
					this.onRefNotifyInsertItemMessage(cont, msgo);
					break;

					case "refNotifyRemoveItem":
					this.onRefNotifyRemoveItemMessage(cont, msgo);
					break;

					case "refNotifySetItem":
					this.onRefNotifySetItemMessage(cont, msgo);
					break;

					case "refNotifyUpdateItem":
					this.onRefNotifyUpdateItemMessage(cont, msgo);
					break;

				}

			}
		}
	}
	private onRefNotifyUpdateItemMessage(cont: any, msgo: JsonDictionaryObject): void {
		throw new NotImplementedException(0);
	}
	private onRefNotifySetItemMessage(cont: any, msgo: JsonDictionaryObject): void {
		if (!msgo.containsKey("refName")) {
			return;
		}
		let refNameItem = msgo.item("refName");
		if (!(typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, refNameItem) !== null)) {
			return;
		}
		let refName = <string>(<JsonDictionaryValue>refNameItem).value;
		let index: number = -1;
		if (msgo.containsKey("index")) {
			let indexItem = msgo.item("index");
			if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, indexItem) !== null) {
				if (typeof (<JsonDictionaryValue>indexItem).value === 'number') {
					index = typeGetValue((<JsonDictionaryValue>indexItem).value);
				}
				if (typeof (<JsonDictionaryValue>indexItem).value === 'number') {
					index = <number>truncate(Math.round(<number>(<JsonDictionaryValue>indexItem).value));
				}
			}
		}
		if (index == -1) {
			return;
		}
		let oldItem: any = null;
		let newItem: any = null;
		let dci: DateColumnCache = this.resolveDateColumnInfo(msgo, refName);
		if (msgo.containsKey("oldItem")) {
			oldItem = this.convertJsonObjectToObject(<JsonDictionaryObject>msgo.item("oldItem"), dci, null);
		}
		if (msgo.containsKey("newItem")) {
			newItem = this.convertJsonObjectToObject(<JsonDictionaryObject>msgo.item("newItem"), dci, null);
		}
		let oldKey: string = null;
		let newKey: string = null;
		if (oldItem != null) {
			oldKey = this.getDSItemKey(oldItem);
		}
		if (newItem != null) {
			newKey = this.getDSItemKey(newItem);
		}
		let currValue = this.resolveRefValueImmediate(cont, refName);
		if (currValue == null) {
			return;
		}
		if (this._itemMaps.containsKey(refName)) {
			let map = this._itemMaps.item(refName);
			if (newKey != null) {
				map.item(newKey, newItem);
			}
		}
		if (this._reverseItemMaps.containsKey(refName)) {
			let reverseMap = this._reverseItemMaps.item(refName);
			if (newKey != null && newItem != null) {
				reverseMap.item(newItem, newKey);
			}
		}
		this.setDSItemAtIndex(currValue, index, newItem);
		if (this.hasNotifyMethods) {
			if (this.getRootDescriptionName(cont) == "DataGrid") {
				let call = ComponentRendererMethodHelper.call("notifySetItem").argument().asInt(index).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = oldKey;
					return $ret;
				})())).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = newKey;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call, (s: string) => {
				});
			} else {
				let call1 = ComponentRendererMethodHelper.call("notifySetItem").argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "name";
					$ret.value = refName;
					return $ret;
				})())).argument().asInt(index).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = oldKey;
					return $ret;
				})())).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = newKey;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call1, (s: string) => {
				});
			}
		}
		if (this._itemMaps.containsKey(refName)) {
			let map1 = this._itemMaps.item(refName);
			let reverseMap1 = this._reverseItemMaps.item(refName);
			let old: any = null;
			if (oldKey != null && map1.containsKey(oldKey)) {
				old = map1.item(oldKey);
			}
			if (old != null && reverseMap1.containsKey(old)) {
				reverseMap1.removeItem(old);
			}
			if (oldKey != null && map1.containsKey(oldKey)) {
				map1.removeItem(oldKey);
			}
		}
	}
	private onRefNotifyRemoveItemMessage(cont: any, msgo: JsonDictionaryObject): void {
		if (!msgo.containsKey("refName")) {
			return;
		}
		let refNameItem = msgo.item("refName");
		if (!(typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, refNameItem) !== null)) {
			return;
		}
		let refName = <string>(<JsonDictionaryValue>refNameItem).value;
		let index: number = -1;
		if (msgo.containsKey("index")) {
			let indexItem = msgo.item("index");
			if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, indexItem) !== null) {
				if (typeof (<JsonDictionaryValue>indexItem).value === 'number') {
					index = typeGetValue((<JsonDictionaryValue>indexItem).value);
				}
				if (typeof (<JsonDictionaryValue>indexItem).value === 'number') {
					index = <number>truncate(Math.round(<number>(<JsonDictionaryValue>indexItem).value));
				}
			}
		}
		if (index == -1) {
			return;
		}
		let oldItem: any = null;
		let dci: DateColumnCache = this.resolveDateColumnInfo(msgo, refName);
		if (msgo.containsKey("oldItem")) {
			oldItem = this.convertJsonObjectToObject(<JsonDictionaryObject>msgo.item("oldItem"), dci, null);
		}
		let oldKey: string = null;
		let newKey: string = null;
		if (oldItem != null) {
			oldKey = this.getDSItemKey(oldItem);
		}
		let currValue = this.resolveRefValueImmediate(cont, refName);
		if (currValue == null) {
			return;
		}
		this.removeDSItemAtIndex(currValue, index);
		if (this.hasNotifyMethods) {
			if (this.getRootDescriptionName(cont) == "DataGrid") {
				let call = ComponentRendererMethodHelper.call("notifyRemoveItem").argument().asInt(index).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = oldKey;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call, (s: string) => {
				});
			} else {
				let call1 = ComponentRendererMethodHelper.call("notifyRemoveItem").argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "name";
					$ret.value = refName;
					return $ret;
				})())).argument().asInt(index).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = oldKey;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call1, (s: string) => {
				});
			}
		}
		if (this._itemMaps.containsKey(refName)) {
			let map = this._itemMaps.item(refName);
			let reverseMap = this._reverseItemMaps.item(refName);
			let old: any = null;
			if (oldKey != null && map.containsKey(oldKey)) {
				old = map.item(oldKey);
			}
			if (old != null && reverseMap.containsKey(old)) {
				reverseMap.removeItem(old);
			}
			if (oldKey != null && map.containsKey(oldKey)) {
				map.removeItem(oldKey);
			}
		}
	}
	private onRefNotifyInsertItemMessage(cont: any, msgo: JsonDictionaryObject): void {
		if (!msgo.containsKey("refName")) {
			return;
		}
		let refNameItem = msgo.item("refName");
		if (!(typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, refNameItem) !== null)) {
			return;
		}
		let refName = <string>(<JsonDictionaryValue>refNameItem).value;
		let index: number = -1;
		if (msgo.containsKey("index")) {
			let indexItem = msgo.item("index");
			if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, indexItem) !== null) {
				if (typeof (<JsonDictionaryValue>indexItem).value === 'number') {
					index = typeGetValue((<JsonDictionaryValue>indexItem).value);
				}
				if (typeof (<JsonDictionaryValue>indexItem).value === 'number') {
					index = <number>truncate(Math.round(<number>(<JsonDictionaryValue>indexItem).value));
				}
			}
		}
		if (index == -1) {
			return;
		}
		let newItem: any = null;
		let dci: DateColumnCache = this.resolveDateColumnInfo(msgo, refName);
		if (msgo.containsKey("newItem")) {
			newItem = this.convertJsonObjectToObject(<JsonDictionaryObject>msgo.item("newItem"), dci, null);
		}
		let newKey: string = null;
		if (newItem != null) {
			newKey = this.getDSItemKey(newItem);
		}
		let currValue = this.resolveRefValueImmediate(cont, refName);
		if (currValue == null) {
			return;
		}
		if (this._itemMaps.containsKey(refName)) {
			let map = this._itemMaps.item(refName);
			if (newKey != null) {
				map.item(newKey, newItem);
			}
		}
		if (this._reverseItemMaps.containsKey(refName)) {
			let reverseMap = this._reverseItemMaps.item(refName);
			if (newItem != null && newKey != null) {
				reverseMap.item(newItem, newKey);
			}
		}
		this.insertDSItemAtIndex(currValue, index, newItem);
		if (this.hasNotifyMethods) {
			if (this.getRootDescriptionName(cont) == "DataGrid") {
				let call = ComponentRendererMethodHelper.call("notifyInsertItem").argument().asInt(index).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = newKey;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call, (s: string) => {
				});
			} else {
				let call1 = ComponentRendererMethodHelper.call("notifyInsertItem").argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "name";
					$ret.value = refName;
					return $ret;
				})())).argument().asInt(index).argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "uuid";
					$ret.value = newKey;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call1, (s: string) => {
				});
			}
		}
	}
	protected get hasNotifyMethods(): boolean {
		return true;
	}
	private getDSItemKey(newItem: any): string {
		if (newItem == null) {
			return null;
		}
		if (this.dSItemHasProperty(newItem, "___id")) {
			return this.getDSItemProperty(newItem, "___id").toString();
		}
		let newKey = Guid.newGuid().toString();
		this.setDSItemProperty(newItem, "___id", newKey);
		return newKey;
	}
	private resolveDateColumnInfo(msgo: JsonDictionaryObject, refName: string): DateColumnCache {
		if (this._dateCacheInfo.containsKey(refName)) {
			return this._dateCacheInfo.item(refName);
		}
		if (msgo.containsKey("dateCache")) {
			let dc = msgo.item("dateCache");
			if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, dc) !== null) {
				let arr = <JsonDictionaryArray>dc;
				if (arr.items != null && arr.items.length > 0) {
					let cache: DateColumnCache = new DateColumnCache();
					this._dateCacheInfo.item(refName, cache);
					this.extractCacheInfo(arr, cache);
					return cache;
				}
			}
		}
		return null;
	}
	private extractCacheInfo(arr: JsonDictionaryArray, cache: DateColumnCache): void {
		for (let i: number = 0; i < arr.items.length; i++) {
			let item = arr.items[i];
			if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, item) !== null) {
				if (typeof (<JsonDictionaryValue>item).value === 'string') {
					cache.addDateColumn(<string>(<JsonDictionaryValue>item).value);
				}
			} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, item) !== null) {
				let sub = cache.addIndexedCache(i);
				this.extractCacheInfo(<JsonDictionaryArray>item, sub);
			}
		}
	}
	private onRefClearItemsMessage(cont: any, msgo: JsonDictionaryObject): void {
		if (!msgo.containsKey("refName")) {
			return;
		}
		let refNameItem = msgo.item("refName");
		if (!(typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, refNameItem) !== null)) {
			return;
		}
		let refName = <string>(<JsonDictionaryValue>refNameItem).value;
		let refValue: any = msgo.item("refValue");
		this._itemMaps.item(refName, new Dictionary$2<string, any>(String_$type, (<any>Base).$type, 0));
		this._reverseItemMaps.item(refName, new Dictionary$2<any, string>((<any>Base).$type, String_$type, 0));
		let map = this._itemMaps.item(refName);
		let reverseMap = this._reverseItemMaps.item(refName);
		let dci: DateColumnCache = null;
		if (msgo.containsKey("dateCache")) {
			dci = this.resolveDateColumnInfo(msgo, refName);
		}
		let assignedValue: any = null;
		let refValueArray: JsonDictionaryArray = null;
		if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, refValue) !== null) {
			refValueArray = <JsonDictionaryArray>refValue;
			if (refValueArray.items != null && refValueArray.items.length > 0) {
				let arr = this.convertJSonArrayToObjectArray(<JsonDictionaryArray>refValue, null, dci);
				assignedValue = arr;
				for (let i = 0; i < refValueArray.items.length; i++) {
					let objItem = arr[i];
					let item = refValueArray.items[i];
					if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, item) !== null && (<JsonDictionaryObject>item).containsKey("___id")) {
						let oItem = <JsonDictionaryObject>item;
						let idItem = oItem.item("___id");
						if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, idItem) !== null) {
							map.item(<string>(<JsonDictionaryValue>idItem).value, objItem);
							if (objItem != null) {
								reverseMap.item(objItem, <string>(<JsonDictionaryValue>idItem).value);
							}
						}
					}
				}
			}
		}
		if (msgo.containsKey("dataIntents")) {
			this.processDataIntents(<JsonDictionaryObject>msgo.item("dataIntents"), refName, refValue, refValueArray);
		}
		let currValue = this.resolveRefValueImmediate(cont, refName);
		if (currValue != null) {
			this.resizeDS(currValue, refValueArray.items.length);
			for (let i1 = 0; i1 < refValueArray.items.length; i1++) {
				let item1 = this.getDSItemAtIndex(assignedValue, i1);
				this.setDSItemAtIndex(currValue, i1, item1);
			}
		}
		if (this.hasNotifyMethods) {
			if (this.getRootDescriptionName(cont) == "DataGrid") {
				let call = ComponentRendererMethodHelper.call("notifyClearItems").$return().asVoid().build();
				this.executeMethod(cont, call, (s: string) => {
				});
			} else {
				let call1 = ComponentRendererMethodHelper.call("notifyClearItems").argument().asEmbeddedRef(((() => {
					let $ret = new EmbeddedRefDescription();
					$ret.refType = "name";
					$ret.value = refName;
					return $ret;
				})())).$return().asVoid().build();
				this.executeMethod(cont, call1, (s: string) => {
				});
			}
		}
	}
	private _dateCacheInfo: Dictionary$2<string, DateColumnCache> = new Dictionary$2<string, DateColumnCache>(String_$type, (<any>DateColumnCache).$type, 0);
	private _itemMaps: Dictionary$2<string, Dictionary$2<string, any>> = new Dictionary$2<string, Dictionary$2<string, any>>(String_$type, (<any>Dictionary$2).$type.specialize(String_$type, (<any>Base).$type), 0);
	private _reverseItemMaps: Dictionary$2<string, Dictionary$2<any, string>> = new Dictionary$2<string, Dictionary$2<any, string>>(String_$type, (<any>Dictionary$2).$type.specialize((<any>Base).$type, String_$type), 0);
	private onRefChangedMessage(cont: any, msgo: JsonDictionaryObject): void {
		if (!msgo.containsKey("refName")) {
			return;
		}
		let refNameItem = msgo.item("refName");
		if (!(typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, refNameItem) !== null)) {
			return;
		}
		let refName = <string>(<JsonDictionaryValue>refNameItem).value;
		let refValue: any = msgo.item("refValue");
		this._itemMaps.item(refName, new Dictionary$2<string, any>(String_$type, (<any>Base).$type, 0));
		this._reverseItemMaps.item(refName, new Dictionary$2<any, string>((<any>Base).$type, String_$type, 0));
		let map = this._itemMaps.item(refName);
		let reverseMap = this._reverseItemMaps.item(refName);
		let dci: DateColumnCache = null;
		if (msgo.containsKey("dateCache")) {
			dci = this.resolveDateColumnInfo(msgo, refName);
		}
		let assignedValue: any = null;
		let refValueArray: JsonDictionaryArray = null;
		if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, refValue) !== null) {
			refValueArray = <JsonDictionaryArray>refValue;
			assignedValue = this.extractArray(refName, refValueArray, map, reverseMap, dci);
		}
		assignedValue = this.toResizableDS(assignedValue);
		if (msgo.containsKey("dataIntents")) {
			this.processDataIntents(<JsonDictionaryObject>msgo.item("dataIntents"), refName, assignedValue, refValueArray);
		}
		if (assignedValue != null) {
			this._skipRemoveItemMaps = true;
		}
		this.provideRefValue(cont, refName, assignedValue);
		if (assignedValue != null) {
			this._skipRemoveItemMaps = false;
		}
	}
	private extractArray(refName: string, refValueArray: JsonDictionaryArray, map: Dictionary$2<string, any>, reverseMap: Dictionary$2<any, string>, dci: DateColumnCache): any {
		let assignedValue: any = null;
		if (refValueArray.items != null && refValueArray.items.length > 0) {
			let arr = this.convertJSonArrayToObjectArray(refValueArray, null, dci);
			assignedValue = arr;
			this.buildArrayItemMap(refName, refValueArray, arr, map, reverseMap);
		}
		return assignedValue;
	}
	private buildArrayItemMap(refName: string, refValueArray: JsonDictionaryArray, arr: any[], map: Dictionary$2<string, any>, reverseMap: Dictionary$2<any, string>): void {
		for (let i = 0; i < refValueArray.items.length; i++) {
			let objItem = arr[i];
			let item = refValueArray.items[i];
			if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, item) !== null) {
				this.buildArrayItemMap(refName, <JsonDictionaryArray>item, <any[]>objItem, map, reverseMap);
			} else {
				let key = this.getDSItemKey(objItem);
				if (key != null) {
					map.item(key, objItem);
					if (objItem != null) {
						reverseMap.item(objItem, key);
					}
				}
			}
		}
	}
	private processDataIntents(dataIntents: JsonDictionaryObject, refName: string, refValue: any, refValueArray: JsonDictionaryArray): void {
		if (dataIntents != null && dataIntents.containsKey("subProps") && <boolean>(<JsonDictionaryValue>(<JsonDictionaryObject>dataIntents).item("subProps")).value) {
			if (refValueArray.items != null && refValueArray.items.length > 0 && refValueArray.items[0] != null) {
				if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, refValueArray.items[0]) !== null) {
					for (let i = 0; i < refValueArray.items.length; i++) {
						this.processDataIntents(<JsonDictionaryObject>dataIntents.item("subIntents"), refName, this.getDSItemAtIndex(refValue, 0), <JsonDictionaryArray>refValueArray.items[i]);
					}
				} else {
					this.processDataIntents(<JsonDictionaryObject>dataIntents.item("subIntents"), refName, this.getDSItemAtIndex(refValue, 0), <JsonDictionaryArray>refValueArray.items[0]);
				}
			}
			return;
		}
		let intent: Dictionary$2<string, string[]> = new Dictionary$2<string, string[]>(String_$type, Array_$type, 0);
		let $t = dataIntents.getKeys();
		for (let i1 = 0; i1 < $t.length; i1++) {
			let key = $t[i1];
			if (dataIntents.containsKey(key) && (<JsonDictionaryObject>dataIntents.item(key)).containsKey("subProps") && <boolean>(<JsonDictionaryValue>(<JsonDictionaryObject>dataIntents.item(key)).item("subProps")).value) {
				if (refValueArray.items != null && refValueArray.items.length > 0 && (<JsonDictionaryObject>refValueArray.items[0]).containsKey(key)) {
					this.processDataIntents(<JsonDictionaryObject>dataIntents.item("subIntents"), refName, this.getDSItemProperty(this.getDSItemAtIndex(refValue, 0), key), <JsonDictionaryArray>(<JsonDictionaryObject>refValueArray.items[0]).item(key));
				}
			} else {
				let arr: JsonDictionaryArray = <JsonDictionaryArray>dataIntents.item(key);
				let intents: string[] = null;
				if (arr != null && arr.items != null && arr.items.length > 0) {
					intents = <string[]>new Array(arr.items.length);
					for (let i2 = 0; i2 < arr.items.length; i2++) {
						intents[i2] = (<JsonDictionaryValue>arr.items[i2]).value.toString();
					}
				}
				intent.item(key, intents);
			}
		}
	}
	provideRefValueFromJson(container: any, refName: string, json: string): void {
		let key = refName;
		let p: JsonDictionaryParser = new JsonDictionaryParser();
		let item = p.parse(json);
		if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, item) !== null) {
			let val = <JsonDictionaryValue>item;
			this.provideRefValue(container, key, val.value);
		} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, item) !== null) {
			let valArr = <JsonDictionaryArray>item;
			let array: any[] = this.convertJSonArrayToObjectArray(valArr, null, null);
			this.provideRefValue(container, key, array);
		} else {
			let itemObj = this.convertJsonObjectToObject(<JsonDictionaryObject>item, null, null);
			this.provideRefValue(container, key, itemObj);
		}
	}
	private _memberPathTransformers: List$1<(memberPath: string) => string> = new List$1<(memberPath: string) => string>(Delegate_$type, 0);
	addMemberPathTransformer(handler: (memberPath: string) => string): void {
		this._memberPathTransformers.add(handler);
	}
	removeMemberPathTransformer(handler: (memberPath: string) => string): void {
		this._memberPathTransformers.remove(handler);
	}
	private _referenceResolvers: List$1<(refName: string, args: ComponentRendererReferenceResolverEventArgs) => void> = new List$1<(refName: string, args: ComponentRendererReferenceResolverEventArgs) => void>(Delegate_$type, 0);
	addReferenceResolver(handler: (refName: string, args: ComponentRendererReferenceResolverEventArgs) => void): void {
		this._referenceResolvers.add(handler);
	}
	removeReferenceResolver(handler: (refName: string, args: ComponentRendererReferenceResolverEventArgs) => void): void {
		this._referenceResolvers.remove(handler);
	}
	private tryResolveReference(refName: string): ComponentRendererReferenceResolverEventArgs {
		if (this._referenceResolvers.count == 0) {
			return null;
		}
		let args = new ComponentRendererReferenceResolverEventArgs();
		for (let i = 0; i < this._referenceResolvers.count; i++) {
			this._referenceResolvers._inner[i](refName, args);
		}
		return args;
	}
	protected resolveCustomMemberPathTransformer(): (arg1: string) => string {
		if (this._memberPathTransformers.count > 0) {
			return (s: string) => {
				let currVal = s;
				for (let i = 0; i < this._memberPathTransformers.count; i++) {
					currVal = this._memberPathTransformers._inner[i](currVal);
				}
				return currVal;
			};
		}
		return null;
	}
	processRefs(refs: JsonDictionaryItem): boolean {
		return false;
	}
	processRefMessages(refMessages: JsonDictionaryItem): boolean {
		return false;
	}
	processModules(modules: JsonDictionaryItem): boolean {
		return false;
	}
	processStrings(strings: JsonDictionaryItem): boolean {
		return false;
	}
	processOnInit(initializers: JsonDictionaryItem): boolean {
		return false;
	}
	processOnViewInit(initializers: JsonDictionaryItem): boolean {
		return false;
	}
	private getPlatformPropertyName(propertyName: string, platform: TypeDescriptionPlatform, propertyMetadata: TypeDescriptionMetadata): string {
		let name = propertyName;
		if (propertyMetadata != null) {
			name = propertyMetadata.getPlatformName(platform);
		} else {
			if (TypeDescriptionMetadata.shouldCamelize(platform)) {
				name = TypeDescriptionMetadata.camelize(name);
			}
		}
		if (stringEndsWith(name, "Ref")) {
			name = name.substr(0, name.length - ("Ref").length);
		}
		return name;
	}
	private ensureComponentsOverlay(desc: DescriptionTreeNode, rootTarget: any, state: ContainerState, container: any): void {
		if (!state.hasComponentObject(rootTarget)) {
			state.addComponent(<any>container, this._currentDescription.item(container).id, rootTarget, this._currentDescription.item(container), runOn(this, this.provideRefValueCore), -1);
		}
		for (let item of fromEnum<DescriptionPropertyValue>(desc.items())) {
			this.ensureComponentsOverlayHelper(this.getPlatformPropertyName(item.propertyName, ComponentRenderer.platform, item.metadata), item.value, rootTarget, state, container, desc.id);
		}
	}
	private ensureComponentsOverlayHelper(path: string, value: any, rootTarget: any, state: ContainerState, container: any, parentId: number): void {
		if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, value) !== null) {
			let node = <DescriptionTreeNode>value;
			let val = this.adapter.getPropertyValue(rootTarget, path);
			if (!state.hasComponentObject(val) && !state.hasComponent(node.id)) {
				state.addComponent(container, node.id, val, node, runOn(this, this.provideRefValueCore), parentId);
			}
			for (let item of fromEnum<DescriptionPropertyValue>(node.items())) {
				this.ensureComponentsOverlayHelper(path + "." + this.getPlatformPropertyName(item.propertyName, ComponentRenderer.platform, item.metadata), item.value, rootTarget, state, container, node.id);
			}
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			let nodeMap: Dictionary$2<number, DescriptionTreeNode> = new Dictionary$2<number, DescriptionTreeNode>(Number_$type, (<any>DescriptionTreeNode).$type, 0);
			let i: number = 0;
			let overlayCount = (<any[]>value).length;
			let arr = <any[]>value;
			for (let i1 = 0; i1 < arr.length; i1++) {
				let item1 = arr[i1];
				if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, item1) !== null) {
					let node1 = <DescriptionTreeNode>item1;
					nodeMap.item(i, node1);
				}
				i++;
			}
			let coll = this.adapter.getPropertyValue(rootTarget, path);
			i = 0;
			this.adapter.forPropertyValueItem(rootTarget, path, (it: any) => {
				if (nodeMap.containsKey(i)) {
					let node = nodeMap.item(i);
					if (!state.hasComponentObject(it) && !state.hasComponent(node.id)) {
						state.addComponent(container, node.id, it, node, runOn(this, this.provideRefValueCore), parentId);
					}
					for (let item of fromEnum<DescriptionPropertyValue>(node.items())) {
						this.ensureComponentsOverlayHelper(path + "." + item.propertyName, item.value, rootTarget, state, container, node.id);
					}
				}
				i++;
			});
			if (overlayCount > 0 && i == 0) {
				let actualPath = this.transformToActualGetterPath(path);
				this.adapter.forPropertyValueItem(rootTarget, actualPath, (it: any) => {
					if (nodeMap.containsKey(i)) {
						let node = nodeMap.item(i);
						if (!state.hasComponentObject(it) && !state.hasComponent(node.id)) {
							state.addComponent(container, node.id, it, node, runOn(this, this.provideRefValueCore), parentId);
						}
						for (let item of fromEnum<DescriptionPropertyValue>(node.items())) {
							this.ensureComponentsOverlayHelper(actualPath + "." + item.propertyName, item.value, rootTarget, state, container, node.id);
						}
					}
					i++;
				});
			}
		}
	}
	private transformToActualGetterPath(path: string): string {
		let ind = path.lastIndexOf('.');
		let before: string = null;
		let name: string = path;
		if (ind > 0) {
			before = path.substr(0, ind);
			name = path.substr(ind + 1);
		}
		name = "actual" + this.pascal(name);
		if (before != null) {
			return before + "." + name;
		}
		return name;
	}
	private pascal(name: string): string {
		return name.substr(0, 1).toUpperCase() + name.substr(1);
	}
	private convertJSonArrayToObjectArray(valArr: JsonDictionaryArray, keyPath: string, dateColumns: DateColumnCache): any[] {
		let array = <any[]>new Array(valArr.items.length);
		let dc: DateColumnCache = dateColumns;
		if (valArr.items.length > 0 && dc == null) {
			let firstItem = valArr.items[0];
			if (firstItem != null && typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, firstItem) !== null) {
				let firstItemObj = <JsonDictionaryObject>firstItem;
				if (firstItemObj.containsKey("___dateColumnsCache")) {
					let cache = firstItemObj.item("___dateColumnsCache");
					if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, cache) !== null) {
						let arr = <JsonDictionaryArray>cache;
						if (arr.items != null && arr.items.length > 0) {
							dc = new DateColumnCache();
							for (let i: number = 0; i < arr.items.length; i++) {
								dc.addDateColumn((<JsonDictionaryValue>arr.items[i]).value.toString());
							}
						}
					}
				}
			}
		}
		for (let j = 0; j < valArr.items.length; j++) {
			if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, valArr.items[j]) !== null) {
				let currDC: DateColumnCache = dc;
				if (currDC != null) {
					currDC = currDC.getIndexedCache(j);
				}
				let itemObj = this.convertJSonArrayToObjectArray(<JsonDictionaryArray>valArr.items[j], keyPath, currDC);
				array[j] = itemObj;
			} else if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, valArr.items[j]) !== null) {
				let itemVal = this.convertJsonValueToObject(<JsonDictionaryValue>valArr.items[j], dc, keyPath);
				array[j] = itemVal;
			} else {
				let itemObj1 = this.convertJsonObjectToObject(<JsonDictionaryObject>valArr.items[j], dc, keyPath);
				array[j] = itemObj1;
			}
		}
		return array;
	}
	private dSItemHasProperty(v_: any, key_: string): boolean {
		if (v_ == null) {
			return false;
		}
		return <boolean>(v_[key_] !== undefined ? true : false);
	}
	private getDSItemProperty(v_: any, key_: string): any {
		if (v_ == null) {
			return null;
		}
		return v_[key_] ? v_[key_] : null;
	}
	private setDSItemProperty(v_: any, key_: string, value_: any): void {
		if (v_ == null) {
			return;
		}
		v_[key_] = value_;
	}
	private toResizableDS(a: any): any {
		let arr = <any[]>a;
		if (arr == null) {
			return null;
		}
		return arr;
	}
	private setDSItemAtIndex(ds_: any, index_: number, item_: any): void {
		ds_[index_] = item_;
	}
	private getRootDescriptionName(cont: any): string {
		if (this._currentDescription.containsKey(cont)) {
			let currTree = this._currentDescription.item(cont);
			if (currTree != null) {
				if (currTree.has("Type")) {
					return currTree.get("Type").toString();
				}
			}
		}
		return null;
	}
	private resizeDS(ds_: any, length_: number): void {
		ds_.length = length_;
	}
	private removeDSItemAtIndex(ds_: any, index_: number): void {
		ds_.splice(index_, 1);
	}
	private insertDSItemAtIndex(ds_: any, index_: number, newItem_: any): void {
		ds_.splice(index_, 0, newItem_);
	}
	private getDSItemAtIndex(ds_: any, index_: number): any {
		return ds_[index_];
	}
	private convertJsonObjectToObject(item: JsonDictionaryObject, dateColumns: DateColumnCache, keyPath: string): any {
		if (item == null) {
			return null;
		}
		let obj = {};
		let dict_ = obj;
		let keys = item.getKeys();
		for (let i = 0; i < keys.length; i++) {
			let key_ = keys[i];
			if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, item.item(key_)) !== null) {
				let val_ = this.convertJsonObjectToObject(<JsonDictionaryObject>item.item(key_), dateColumns, keyPath != null ? keyPath + "." + key_ : key_);
				dict_[key_] = val_;
			} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, item.item(key_)) !== null) {
				let val_ = this.convertJSonArrayToObjectArray(<JsonDictionaryArray>item.item(key_), keyPath != null ? keyPath + "." + key_ : key_, dateColumns);
				dict_[key_] = val_;
			} else {
				let val_ = (<JsonDictionaryValue>item.item(key_)).value;
				if (dateColumns != null && dateColumns.isDateColumn(key_)) {
					if (typeof val_ === 'string' && stringStartsWith((<string>val_), "@d:")) {
						val_ = (<string>val_).substr(3);
					}
					val_ = this.convertToDate(val_);
				}
				dict_[key_] = val_;
			}
		}
		return dict_;
	}
	private convertJsonValueToObject(item: JsonDictionaryValue, dateColumns: DateColumnCache, keyPath: string): any {
		let val_ = item.value;
		if (dateColumns != null && dateColumns.isDateColumn(keyPath)) {
			if (typeof val_ === 'string' && stringStartsWith((<string>val_), "@d:")) {
				val_ = (<string>val_).substr(3);
			}
			val_ = this.convertToDate(val_);
		}
		if (typeof val_ === 'string') {
			let strVal = <string>val_;
			if (strVal == "@dbl:INFINITY") {
				val_ = Number.POSITIVE_INFINITY;
			}
			if (strVal == "@dbl:-INFINITY") {
				val_ = Number.NEGATIVE_INFINITY;
			}
		}
		return val_;
	}
	private convertToDate(val_: any): any {
		if (typeof val_ === 'string') {
			let date: Date;
			if (((() => { let $ret = dateTryParse(<string>val_, date); date = $ret.p1; return $ret.ret; })())) {
				return date;
			}
		}
		if (typeCast<Date>(Date_$type, val_) !== null) {
			return val_;
		}
		let ret = Convert.toDateTime(val_);
		return ret;
	}
	private _pendingDescription: Dictionary$2<any, Description> = new Dictionary$2<any, Description>((<any>Base).$type, (<any>Description).$type, 0);
	private _pendingDescriptionIsDelta: Dictionary$2<any, boolean> = new Dictionary$2<any, boolean>((<any>Base).$type, Boolean_$type, 0);
	private _pendingDescriptionSkipApply: Dictionary$2<any, boolean> = new Dictionary$2<any, boolean>((<any>Base).$type, Boolean_$type, 0);
	private _pendingCleanupUnusedRefs: Dictionary$2<any, boolean> = new Dictionary$2<any, boolean>((<any>Base).$type, Boolean_$type, 0);
	private _pendingAnimationIdleRefNames: Dictionary$2<any, string> = new Dictionary$2<any, string>((<any>Base).$type, String_$type, 0);
	private _pendingAnimationIdleTimeout: Dictionary$2<any, number> = new Dictionary$2<any, number>((<any>Base).$type, Number_$type, 0);
	private _currentDescription: Dictionary$2<any, DescriptionTreeNode> = new Dictionary$2<any, DescriptionTreeNode>((<any>Base).$type, (<any>DescriptionTreeNode).$type, 0);
	private _states: Dictionary$2<any, ContainerState> = new Dictionary$2<any, ContainerState>((<any>Base).$type, (<any>ContainerState).$type, 0);
	get states(): Dictionary$2<any, ContainerState> {
		return this._states;
	}
	private _queuedActions: Dictionary$2<any, Queue$1<Queue$1<DescriptionTreeAction>>> = new Dictionary$2<any, Queue$1<Queue$1<DescriptionTreeAction>>>((<any>Base).$type, (<any>Queue$1).$type.specialize((<any>Queue$1).$type.specialize((<any>DescriptionTreeAction).$type)), 0);
	private _idleActions: Dictionary$2<any, List$1<() => void>> = new Dictionary$2<any, List$1<() => void>>((<any>Base).$type, (<any>List$1).$type.specialize(Delegate_$type), 0);
	private _actionsRunning: Dictionary$2<any, boolean> = new Dictionary$2<any, boolean>((<any>Base).$type, Boolean_$type, 0);
	private _refs: Dictionary$2<string, DescriptionRef> = new Dictionary$2<string, DescriptionRef>(String_$type, (<any>DescriptionRef).$type, 0);
	private _systemValues: Dictionary$2<string, any> = new Dictionary$2<string, any>(String_$type, (<any>Base).$type, 0);
	private _userValues: Dictionary$2<string, any> = new Dictionary$2<string, any>(String_$type, (<any>Base).$type, 0);
	private _refNameLookup: Dictionary$2<any, string> = new Dictionary$2<any, string>((<any>Base).$type, String_$type, 0);
	private _callbackLookup: Dictionary$2<string, (arg1: any, arg2: any) => void> = new Dictionary$2<string, (arg1: any, arg2: any) => void>(String_$type, Delegate_$type, 0);
	private _cleanup: any = null;
	cleanup(container: any, cleanupUnusedRefs: boolean): void {
		this._cleanup = container;
		this.renderHelper(null, container, false, false, cleanupUnusedRefs, null, 0);
	}
	render(description: Description, container: any): void {
		this.renderHelper(description, container, false, false, this.cleanupUnusedOnRender, null, 0);
	}
	private renderHelper(description: Description, container: any, isDelta: boolean, skipApply: boolean, cleanupUnusedRefs: boolean, animationIdleRefName: string, animationIdleTimeout: number): void {
		if (!this._states.containsKey(container)) {
			this._states.item(container, new ContainerState());
			this._states.item(container).container = container;
		}
		this._pendingDescription.item(container, description);
		this._pendingDescriptionIsDelta.item(container, isDelta);
		this._pendingDescriptionSkipApply.item(container, skipApply);
		this._pendingCleanupUnusedRefs.item(container, cleanupUnusedRefs);
		this._pendingAnimationIdleRefNames.item(container, animationIdleRefName);
		this._pendingAnimationIdleTimeout.item(container, animationIdleTimeout);
		this.onUIThread(container, () => this.renderCore(container));
	}
	private onUIThread(c: any, p: () => void): void {
		this.adapter.onUIThread(c, p);
	}
	private renderCore(container: any): void {
		if (this._pendingDescription.containsKey(container)) {
			let isDelta = this._pendingDescriptionIsDelta.containsKey(container) ? this._pendingDescriptionIsDelta.item(container) : false;
			let skipApply = this._pendingDescriptionSkipApply.containsKey(container) ? this._pendingDescriptionSkipApply.item(container) : false;
			let desc = this._pendingDescription.item(container);
			let animationIdleRefName = this._pendingAnimationIdleRefNames.containsKey(container) ? this._pendingAnimationIdleRefNames.item(container) : null;
			let animationIdleTimeout = this._pendingAnimationIdleTimeout.containsKey(container) ? this._pendingAnimationIdleTimeout.item(container) : 0;
			this._pendingDescription.removeItem(container);
			let newTree = DescriptionTreeBuilder.createTreeWithOptions(this.context, desc, this.preserveKeyOrder);
			let oldTree: DescriptionTreeNode = null;
			if (this._currentDescription.containsKey(container)) {
				oldTree = this._currentDescription.item(container);
				this._currentOldTree = oldTree;
			}
			let diff = DescriptionTreeReconciler.diffTrees(oldTree, newTree, isDelta);
			if (!skipApply && isDelta) {
				newTree = oldTree.clone();
				DescriptionTreeReconciler.applyDiff(oldTree, diff);
				let swap = oldTree;
				oldTree = newTree;
				newTree = swap;
				diff = DescriptionTreeReconciler.diffTrees(oldTree, newTree, false);
			}
			if (skipApply) {
				let info = DescriptionTreeReconciler.applyDiff(oldTree, diff);
				if (!this._states.containsKey(container)) {
					this._states.addItem(container, ((() => {
						let $ret = new ContainerState();
						$ret.container = container;
						return $ret;
					})()));
				}
				let state = this._states.item(container);
				if (info.removedIds.count > 0) {
					for (let removed of fromEnum<number>(info.removedIds)) {
						if (state.hasComponent(removed)) {
							let c = state.getComponent(removed);
							this.destroyObject(container, c, state);
						}
					}
				}
				if (info.newPropertyValues.count > 0) {
					for (let a of fromEnum<DescriptionTreeAction>(info.newPropertyValues)) {
						let target = this.getTarget(container, state, a);
						let newVal = this.adapter.getPropertyValue(target, a.propertyName);
						if (newVal != null) {
							if (!state.hasComponentObject(newVal)) {
								let newNode = a.newValue;
								if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newNode) !== null) {
									let id = (<DescriptionTreeNode>newNode).id;
									state.addComponent(container, id, newVal, (<DescriptionTreeNode>newNode), runOn(this, this.provideRefValueCore), a.targetNode.id);
								}
							}
						}
					}
				}
				if (info.newRootContent.count > 0) {
					for (let a1 of fromEnum<DescriptionTreeAction>(info.newRootContent)) {
						let newVal1 = a1.newValue;
						if (newVal1 != null) {
							if (!state.hasComponentObject(newVal1)) {
								let newNode1 = a1.newValue;
								if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newNode1) !== null) {
									let id1 = (<DescriptionTreeNode>newNode1).id;
									this._currentDescription.item(container, (<DescriptionTreeNode>newNode1));
								}
							}
						}
					}
				}
				if (info.newCollectionContent.count > 0) {
					for (let a2 of fromEnum<DescriptionTreeAction>(info.newCollectionContent)) {
						let arr = <any[]>a2.targetNode.get(a2.propertyName).value;
						if (arr == null) {
							continue;
						}
						let map: Dictionary$2<string, DescriptionTreeNode> = new Dictionary$2<string, DescriptionTreeNode>(String_$type, (<any>DescriptionTreeNode).$type, 0);
						for (let i = 0; i < arr.length; i++) {
							let currItem = arr[i];
							if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, currItem) !== null) {
								let currNode = <DescriptionTreeNode>currItem;
								if (currNode.has("Name")) {
									map.item(<string>currNode.get("Name").value, currNode);
								}
							}
						}
						if (map.count == 0) {
							continue;
						}
						let target1 = this.getTarget(container, state, a2);
						let newCol = this.adapter.getPropertyValue(target1, a2.propertyName);
						if (newCol != null) {
							let i1: number = 0;
							this.adapter.forPropertyValueItem(target1, a2.propertyName, (item: any) => {
								if (map.containsKey(this.adapter.getPropertyValue(item, "Name").toString())) {
									if (!state.hasComponentObject(item)) {
										let newNode = map.item(this.adapter.getPropertyValue(item, "Name").toString());
										if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newNode) !== null) {
											let id = (<DescriptionTreeNode>newNode).id;
											state.addComponent(container, id, item, (<DescriptionTreeNode>newNode), runOn(this, this.provideRefValueCore), a2.targetNode.id);
										}
									}
								}
								i1++;
							});
						}
					}
				}
			} else {
				this._currentDescription.item(container, newTree);
			}
			if (!skipApply) {
				if (animationIdleRefName != null && this.hasUserRef(animationIdleRefName)) {
					let idleRef = this._userValues.item(animationIdleRefName);
					if (idleRef != null) {
						let version = GlobalAnimationState.instance.getAnimationIdleVersionNumber();
						if (animationIdleTimeout > 0) {
							GlobalAnimationState.instance.queueForAnimationIdleWithTimeout((timeout: boolean) => (<(timeout: boolean) => void>idleRef)(timeout), version, animationIdleTimeout);
						} else {
							GlobalAnimationState.instance.queueForAnimationIdle(() => (<(timeout: boolean) => void>idleRef)(false), version);
						}
					}
				}
				this.renderDiff(container, diff);
			}
			if (this._pendingCleanupUnusedRefs.item(container)) {
				this.removeUnusedRefs(container);
			}
		}
		if (this._cleanup != null) {
			if (this._states.containsKey(container)) {
				this._states.removeItem(container);
			}
			if (this._pendingDescription.containsKey(this._cleanup)) {
				this._pendingDescription.removeItem(this._cleanup);
			}
			if (this._pendingDescriptionIsDelta.containsKey(this._cleanup)) {
				this._pendingDescriptionIsDelta.removeItem(this._cleanup);
			}
			if (this._pendingDescriptionSkipApply.containsKey(this._cleanup)) {
				this._pendingDescriptionSkipApply.removeItem(this._cleanup);
			}
			if (this._pendingCleanupUnusedRefs.containsKey(this._cleanup)) {
				this._pendingCleanupUnusedRefs.removeItem(this._cleanup);
			}
			if (this._currentDescription.containsKey(this._cleanup)) {
				this._currentDescription.removeItem(this._cleanup);
			}
			if (this._queuedActions.containsKey(this._cleanup)) {
				this._queuedActions.removeItem(this._cleanup);
			}
			if (this._idleActions.containsKey(this._cleanup)) {
				this._idleActions.removeItem(this._cleanup);
			}
			if (this._actionsRunning.containsKey(this._cleanup)) {
				this._actionsRunning.removeItem(this._cleanup);
			}
			if (this._pendingAnimationIdleTimeout.containsKey(this._cleanup)) {
				this._pendingAnimationIdleTimeout.removeItem(this._cleanup);
			}
			if (this._pendingAnimationIdleRefNames.containsKey(this._cleanup)) {
				this._pendingAnimationIdleRefNames.removeItem(this._cleanup);
			}
			this._cleanup = null;
		}
		this._currentOldTree = null;
	}
	getTargetValue(descriptionType: string, propertyName: string, target: any): any {
		let metadata = this.context.getMetadata(descriptionType, this.pascal(propertyName));
		let propName = metadata.getPlatformName(ComponentRenderer.platform);
		return this.adapter.getPropertyValue(target, propName);
	}
	setTargetValue(descriptionType: string, propertyName: string, target: any, newValue: any, oldValue: any): void {
		let metadata = this.context.getMetadata(descriptionType, this.pascal(propertyName));
		let propName = metadata.getPlatformName(ComponentRenderer.platform);
		this.adapter.setPropertyValue(target, propName, metadata, newValue, oldValue, null);
	}
	createHandlerWithDescription(cont: any, refName: string, descriptionType: string, eventName: string, callback: (arg1: Description) => void): void {
		if (callback != null) {
			this.createHandler(cont, refName, descriptionType, eventName, (sender: any, args: any, argsType: string) => {
				let description = this.convertToDescription(args, argsType, TypeDescriptionWellKnownType.ExportedType);
				let oldTree = DescriptionTreeBuilder.createTree(this.context, description);
				callback(description);
			});
		} else {
			this.createHandler(cont, refName, descriptionType, eventName, null);
		}
	}
	createHandlerWithJSON(cont: any, refName: string, descriptionType: string, eventName: string, callback: (arg1: string) => void): void {
		if (callback != null) {
			this.createHandler(cont, refName, descriptionType, eventName, (sender: any, args: any, argsType: string) => {
				let description = this.convertToDescription(args, argsType, TypeDescriptionWellKnownType.ExportedType);
				let ser = new DescriptionSerializer();
				let json = ser.serialize(this.context, description);
				callback(json);
			});
		} else {
			this.createHandler(cont, refName, descriptionType, eventName, null);
		}
	}
	createHandlerWithRaw(cont: any, refName: string, descriptionType: string, eventName: string, callback: (arg1: any) => void): void {
		if (callback != null) {
			this.createHandler(cont, refName, descriptionType, eventName, (sender: any, args: any, argsType: string) => callback(args));
		} else {
			this.createHandler(cont, refName, descriptionType, eventName, null);
		}
	}
	private createHandler(cont: any, refName: string, descriptionType: string, eventName: string, callback: (arg1: any, arg2: any, arg3: string) => void): void {
		if (callback != null) {
			let evName = this.pascal(eventName);
			let metadata = this.context.getMetadata(descriptionType, evName);
			let argsType = this.context.getEventArgsType(descriptionType, evName);
			if (argsType != null) {
				let cb = (sender: any, args: any) => callback(sender, args, argsType);
				this._callbackLookup.item(refName, cb);
				let handler = this.adapter.createHandler(evName, metadata.specificExternalType, argsType, this.context, cb);
				this.provideRefValueCore(null, refName, handler, true);
			}
		} else {
			if (this._callbackLookup.containsKey(refName)) {
				this.adapter.disposeHandler(this._callbackLookup.item(refName));
				this.provideRefValueCore(null, refName, null, true);
			}
		}
	}
	waitForAnimationIdle(container: any, animationIdleTimeout: number, onResult: () => void): void {
		let version = GlobalAnimationState.instance.getAnimationIdleVersionNumber();
		if (animationIdleTimeout > 0) {
			GlobalAnimationState.instance.queueForAnimationIdleWithTimeout((timeout: boolean) => onResult(), version, animationIdleTimeout);
		} else {
			GlobalAnimationState.instance.queueForAnimationIdle(() => onResult(), version);
		}
	}
	executeMethod(container: any, jsonArguments: string, onResult: (arg1: string) => void): void {
		this.onUIThread(container, () => {
			let par: JsonDictionaryParser = new JsonDictionaryParser();
			let dict = par.parse(jsonArguments);
			if (!this._currentDescription.containsKey(container)) {
				onResult(null);
				return;
			}
			if (!this._states.containsKey(container)) {
				this._states.item(container, new ContainerState());
				this._states.item(container).container = container;
			}
			let state = this._states.item(container);
			let ser: DescriptionSerializer = new DescriptionSerializer();
			ser.throwOnMissingDescription = this.isProceedOnErrorEnabled;
			ser.forcePascal = this.shouldForcePascal();
			let tree = this._currentDescription.item(container);
			let target: any = null;
			let argumentValues: List$1<any> = new List$1<any>((<any>Base).$type, 0);
			let argumentMetadata: List$1<TypeDescriptionMetadata> = new List$1<TypeDescriptionMetadata>((<any>TypeDescriptionMetadata).$type, 0);
			if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, dict) !== null) {
				let targetRef: string = null;
				let obj = <JsonDictionaryObject>dict;
				if (obj.containsKey("targetRef")) {
					targetRef = this.extractArgString(obj, "targetRef");
				}
				let methodName: string = null;
				if (obj.containsKey("methodName")) {
					methodName = this.extractArgString(obj, "methodName");
				}
				if (targetRef != null) {
					if (this._systemValues.containsKey(targetRef)) {
						target = this._systemValues.item(targetRef);
					}
				} else {
					target = state.getComponent(tree.id);
				}
				if (obj.containsKey("args")) {
					let args = <JsonDictionaryArray>obj.item("args");
					for (let i = 0; i < args.items.length; i++) {
						let arg = args.items[i];
						let action = this.jsonArgumentToAction(tree, arg, ser);
						let value: any = null;
						if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null && (<DescriptionTreeNode>action.newValue).type == "EmbeddedRef") {
							value = this.resolveEmbeddedRef(container, <DescriptionTreeNode>action.newValue);
						} else {
							value = this.getCoercedValue(action, container, state);
						}
						if (action.propertyMetadata.knownType == TypeDescriptionWellKnownType.ExportedType && action.propertyMetadata.specificType == "string") {
							value = this.coerceToEnum(action.propertyMetadata.specificExternalType, <string>value, action.propertyMetadata);
						}
						argumentValues.add1(value);
						argumentMetadata.add(action.propertyMetadata);
					}
				}
				let returnMetadata: TypeDescriptionMetadata = null;
				if (obj.containsKey("return")) {
					let arg1 = obj.item("return");
					let action1 = this.jsonArgumentToAction(tree, arg1, ser);
					returnMetadata = action1.propertyMetadata;
				}
				this.adapter.executeMethod(target, methodName, argumentValues.toArray(), argumentMetadata.toArray(), (res: any) => {
					let resString = this.serializeReturnValue(res, returnMetadata);
					onResult(resString);
				});
			}
		});
	}
	executeMethodWithBuilder(container: any, builder: ComponentRendererMethodHelperBuilder, onResult: (arg1: string) => void): void {
		let par: JsonDictionaryParser = new JsonDictionaryParser();
		if (!this._states.containsKey(container)) {
			this._states.item(container, new ContainerState());
			this._states.item(container).container = container;
		}
		let state = this._states.item(container);
		let ser: DescriptionSerializer = new DescriptionSerializer();
		ser.throwOnMissingDescription = this.isProceedOnErrorEnabled;
		ser.forcePascal = this.shouldForcePascal();
		let tree = this._currentDescription.item(container);
		let target: any = null;
		let argumentValues: List$1<any> = new List$1<any>((<any>Base).$type, 0);
		let argumentMetadata: List$1<TypeDescriptionMetadata> = new List$1<TypeDescriptionMetadata>((<any>TypeDescriptionMetadata).$type, 0);
		if (builder != null) {
			let targetRef: string = builder.targetRef;
			let methodName: string = builder.methodName;
			if (targetRef != null) {
				if (this._systemValues.containsKey(targetRef)) {
					target = this._systemValues.item(targetRef);
				}
			} else {
				target = state.getComponent(tree.id);
			}
			let argsCount: number = builder.getArgumentCount();
			for (let i = 0; i < argsCount; i++) {
				let arg = builder.getArgument(i);
				let action = this.argumentBuilderToAction(tree, arg, ser);
				let value: any = null;
				if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null && (<DescriptionTreeNode>action.newValue).type == "EmbeddedRef") {
					value = this.resolveEmbeddedRef(container, <DescriptionTreeNode>action.newValue);
				} else {
					value = this.getCoercedValue(action, container, state);
				}
				if (action.propertyMetadata.knownType == TypeDescriptionWellKnownType.ExportedType && action.propertyMetadata.specificType == "string") {
					value = this.coerceToEnum(action.propertyMetadata.specificExternalType, <string>value, action.propertyMetadata);
				}
				argumentValues.add1(value);
				argumentMetadata.add(action.propertyMetadata);
			}
			let returnMetadata: TypeDescriptionMetadata = null;
			if (builder.hasReturn()) {
				let action1 = this.returnBuilderToAction(tree, builder.getReturn());
				returnMetadata = action1.propertyMetadata;
			}
			this.adapter.executeMethod(target, methodName, argumentValues.toArray(), argumentMetadata.toArray(), (res: any) => {
				let resString = this.serializeReturnValue(res, returnMetadata);
				onResult(resString);
			});
		}
	}
	private resolveEmbeddedRef(container: any, node: DescriptionTreeNode): any {
		if (node.type == "EmbeddedRef") {
			let refType_o: string = "uuid";
			let value: string = null;
			for (let i = 0; i < node.items().count; i++) {
				if (node.items()._inner[i].propertyName == "RefType") {
					refType_o = <string>node.items()._inner[i].value;
				}
				if (node.items()._inner[i].propertyName == "Value") {
					value = <string>node.items()._inner[i].value;
				}
			}
			if (value != null) {
				for (let i1 = 0; i1 < this._lookupHandlers.count; i1++) {
					let retVal = this._lookupHandlers._inner[i1](container, refType_o, value);
					if (retVal != null) {
						return retVal;
					}
				}
				if (refType_o == "uuid") {
					let internalVal = this.resolveInternalId(container, refType_o, value);
					if (internalVal != null) {
						return internalVal;
					}
				}
				if (refType_o == "name") {
					let refName = value;
					if (this._systemValues.containsKey(refName)) {
						return this._systemValues.item(refName);
					}
					if (this._userValues.containsKey(refName)) {
						return this._userValues.item(refName);
					}
				}
				return null;
			}
		}
		return null;
	}
	private resolveInternalId(container: any, refType: string, value: string): any {
		if (this._itemMaps != null) {
			for (let key of fromEnum<string>(this._itemMaps.keys)) {
				let map = this._itemMaps.item(key);
				if (map.containsKey(value)) {
					return map.item(value);
				}
			}
		}
		return null;
	}
	static convertToDouble(x: any): number {
		if (typeCast<Date>(Date_$type, x) !== null) {
			return (<Date>x).getTime();
		}
		if (x == null) {
			return 0;
		}
		return <number>x;
	}
	private resolveToEmbeddedRef(value: any, forceEmbed: boolean): any {
		if (value == null) {
			return null;
		}
		if (this._refNameLookup.containsKey(value)) {
			if (forceEmbed) {
				let embeddedRef = new EmbeddedRefDescription();
				embeddedRef.refType = "name";
				embeddedRef.value = this._refNameLookup.item(value);
				return embeddedRef;
			}
			return this._refNameLookup.item(value);
		}
		for (let i: number = 0; i < this._reverseLookupHandlers.count; i++) {
			let key = this._reverseLookupHandlers._inner[i](value);
			if (key != null) {
				let embeddedRef1 = new EmbeddedRefDescription();
				embeddedRef1.refType = "uuid";
				embeddedRef1.value = key;
				return embeddedRef1;
			}
		}
		for (let key1 of fromEnum<string>(this._reverseItemMaps.keys)) {
			let reverseMap = this._reverseItemMaps.item(key1);
			if (reverseMap.containsKey(value)) {
				let ukey = reverseMap.item(value);
				let embeddedRef2 = new EmbeddedRefDescription();
				embeddedRef2.refType = "uuid";
				embeddedRef2.value = ukey;
				return embeddedRef2;
			}
		}
		return null;
	}
	private convertToDescription(value: any, descriptionType: string, knownType: TypeDescriptionWellKnownType): Description {
		if (value == null) {
			return null;
		}
		if (knownType == TypeDescriptionWellKnownType.ExportedType) {
			let description = this.context.createDescriptionForType(descriptionType);
			if (description == null) {
				let toFind = "Description";
				if (stringEndsWith(descriptionType, toFind)) {
					descriptionType = descriptionType.substr(0, descriptionType.length - toFind.length);
					description = this.context.createDescriptionForType(descriptionType);
				}
			}
			if (description == null) {
				descriptionType = getInstanceType(value).typeName;
				description = this.context.createDescriptionForType(descriptionType);
			}
			if (description == null) {
				return null;
			}
			let properties = this.context.getAllProperties(descriptionType);
			for (let i = 0; i < properties.length; i++) {
				let property = properties[i];
				if (stringStartsWith(property, "__")) {
					continue;
				}
				if (stringEndsWith(property, "@names")) {
					continue;
				}
				if (stringEndsWith(property, "@nameBinding")) {
					continue;
				}
				if (stringEndsWith(property, "@ngQueryList")) {
					continue;
				}
				if (stringEndsWith(property, "@mustSetInCode")) {
					continue;
				}
				if (stringEndsWith(property, "@stringUnion")) {
					continue;
				}
				let propMeta = this.context.getMetadata(descriptionType, property);
				let platformPropertyName = propMeta.getPlatformName(ComponentRenderer.platform);
				let valReflectionHelper = new FastReflectionHelper(false, platformPropertyName);
				let propValue = valReflectionHelper.getPropertyValue(value);
				let descriptionValue: any = null;
				switch (propMeta.knownType) {
					case TypeDescriptionWellKnownType.ExportedType:
					if (propMeta.specificExternalType == "string") {
						descriptionValue = propValue.toString();
					} else {
						descriptionValue = this.convertToDescription(propValue, propMeta.specificExternalType, propMeta.knownType);
					}
					break;

					case TypeDescriptionWellKnownType.Collection:
					descriptionValue = this.convertToDescriptionCollection(propValue, propMeta.collectionElementType);
					break;

					case TypeDescriptionWellKnownType.Array:
					descriptionValue = this.convertToDescriptionCollection(propValue, propMeta.specificExternalType);
					break;

					case TypeDescriptionWellKnownType.BrushCollection: break;
					case TypeDescriptionWellKnownType.ColorCollection: break;
					case TypeDescriptionWellKnownType.boolean1:
					descriptionValue = <boolean>propValue;
					break;

					case TypeDescriptionWellKnownType.Pixel:
					switch (propMeta.specificExternalType) {
						case "int":
						descriptionValue = <number>truncate(this.convertFromPixels(ComponentRenderer.convertToDouble(propValue)));
						break;

						default:
						descriptionValue = this.convertFromPixels(ComponentRenderer.convertToDouble(propValue));
						break;

					}

					break;

					case TypeDescriptionWellKnownType.Number:
					switch (propMeta.specificExternalType) {
						case "int":
						descriptionValue = <number>truncate(ComponentRenderer.convertToDouble(propValue));
						break;

						default:
						descriptionValue = ComponentRenderer.convertToDouble(propValue);
						break;

					}

					break;

					case TypeDescriptionWellKnownType.string1:
					descriptionValue = propValue != null ? propValue.toString() : null;
					break;

					case TypeDescriptionWellKnownType.Date:
					descriptionValue = <Date>propValue;
					break;

					case TypeDescriptionWellKnownType.PixelPoint:
					descriptionValue = this.convertToDescription(propValue, "Point", TypeDescriptionWellKnownType.PixelPoint);
					break;

					case TypeDescriptionWellKnownType.Point:
					descriptionValue = this.convertToDescription(propValue, "Point", TypeDescriptionWellKnownType.Point);
					break;

					case TypeDescriptionWellKnownType.PixelSize:
					descriptionValue = this.convertToDescription(propValue, "Size", TypeDescriptionWellKnownType.PixelSize);
					break;

					case TypeDescriptionWellKnownType.Size:
					descriptionValue = this.convertToDescription(propValue, "Size", TypeDescriptionWellKnownType.Size);
					break;

					case TypeDescriptionWellKnownType.PixelRect:
					descriptionValue = this.convertToDescription(propValue, "Rect", TypeDescriptionWellKnownType.PixelRect);
					break;

					case TypeDescriptionWellKnownType.Rect:
					descriptionValue = this.convertToDescription(propValue, "Rect", TypeDescriptionWellKnownType.Rect);
					break;

					case TypeDescriptionWellKnownType.Brush:
					descriptionValue = CSSColorUtil.brushToString(<Brush>propValue);
					break;

					case TypeDescriptionWellKnownType.Color:
					descriptionValue = CSSColorUtil.colorToString(<Color>propValue);
					break;

					case TypeDescriptionWellKnownType.Unknown:
					if (this.isPrimitiveValue(propValue)) {
						descriptionValue = propValue;
					}
					descriptionValue = this.resolveToEmbeddedRef(propValue, false);
					break;

					case TypeDescriptionWellKnownType.DataRef:
					descriptionValue = this.resolveToEmbeddedRef(propValue, false);
					break;

				}

				if (descriptionValue != null) {
					if (this.shouldForcePascal()) {
						this.context.setDescriptionPropertyPascal(<Description>description, propMeta.propertyName, propMeta, descriptionValue);
					} else {
						this.context.setDescriptionProperty(<Description>description, propMeta.propertyName, propMeta, descriptionValue);
					}
				}
			}
			return <Description>description;
		} else if (knownType == TypeDescriptionWellKnownType.string1) {
			return ((() => {
				let $ret = new StringDescription();
				$ret.value = value != null ? value.toString() : null;
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Number) {
			return ((() => {
				let $ret = new NumberDescription();
				$ret.value = ComponentRenderer.convertToDouble(value);
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Pixel) {
			let v = ComponentRenderer.convertToDouble(value);
			v = this.convertFromPixels(v);
			return ((() => {
				let $ret = new NumberDescription();
				$ret.value = v;
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.PixelPoint) {
			let pt: Point = <Point>PlatformAPIHelper.internalizePointValue(value);
			return ((() => {
				let $ret = new PointDescription();
				$ret.x = this.convertFromPixels(pt.x);
				$ret.y = this.convertFromPixels(pt.y);
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Point) {
			let pt1: Point = <Point>PlatformAPIHelper.internalizePointValue(value);
			return ((() => {
				let $ret = new PointDescription();
				$ret.x = pt1.x;
				$ret.y = pt1.y;
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.PixelSize) {
			let size: Size = <Size>PlatformAPIHelper.internalizeSizeValue(value);
			return ((() => {
				let $ret = new SizeDescription();
				$ret.width = this.convertFromPixels(size.width);
				$ret.height = this.convertFromPixels(size.height);
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Size) {
			let size1: Size = <Size>PlatformAPIHelper.internalizeSizeValue(value);
			return ((() => {
				let $ret = new SizeDescription();
				$ret.width = size1.width;
				$ret.height = size1.height;
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.PixelRect) {
			let rect: Rect = <Rect>PlatformAPIHelper.internalizeRectValue(value);
			return ((() => {
				let $ret = new RectDescription();
				$ret.left = this.convertFromPixels(rect.left);
				$ret.top = this.convertFromPixels(rect.top);
				$ret.width = this.convertFromPixels(rect.width);
				$ret.height = this.convertFromPixels(rect.height);
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Rect) {
			let rect1: Rect = <Rect>PlatformAPIHelper.internalizeRectValue(value);
			return ((() => {
				let $ret = new RectDescription();
				$ret.left = rect1.left;
				$ret.top = rect1.top;
				$ret.width = rect1.width;
				$ret.height = rect1.height;
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Brush) {
			let brush: Brush = <Brush>PlatformAPIHelper.internalizeBrushValue(value);
			return ((() => {
				let $ret = new BrushDescription();
				$ret.value = CSSColorUtil.brushToString(brush);
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Color) {
			let color = <Color>PlatformAPIHelper.internalizeColorValue(value);
			return ((() => {
				let $ret = new ColorDescription();
				$ret.value = CSSColorUtil.colorToString(color);
				return $ret;
			})());
		} else if (knownType == TypeDescriptionWellKnownType.Unknown) {
			let embeddedRef = <EmbeddedRefDescription>this.resolveToEmbeddedRef(value, true);
			return embeddedRef;
		}
		return null;
	}
	private convertFromPixels(x: number): number {
		throw new NotImplementedException(0);
	}
	private convertToDescriptionCollection(value: any, descriptionType: string): any {
		if (value == null) {
			return null;
		}
		let items = this.adapter.publicCollectionAsObjectArray(value);
		let isPrimitive: boolean = this.isPrimitiveType(descriptionType);
		let ret = <any[]>new Array(items.length);
		for (let i: number = 0; i < items.length; i++) {
			if (!isPrimitive) {
				if (descriptionType == "Object") {
					ret[i] = this.convertToDescription(items[i], descriptionType, TypeDescriptionWellKnownType.Unknown);
					if (ret[i] == null) {
						ret[i] = items[i];
					}
				} else {
					ret[i] = this.convertToDescription(items[i], descriptionType, TypeDescriptionWellKnownType.ExportedType);
				}
			} else {
				ret[i] = items[i];
			}
		}
		return ret;
	}
	private serializeReturnValue(value: any, returnMetadata: TypeDescriptionMetadata): string {
		let result: JsonDictionaryObject = new JsonDictionaryObject();
		result.item("knownType", ((() => {
			let $ret = new JsonDictionaryValue();
			$ret.type = JsonDictionaryValueType.StringValue;
			$ret.value = EnumUtil.getName<TypeDescriptionWellKnownType>(TypeDescriptionWellKnownType_$type, returnMetadata.knownType);
			return $ret;
		})()));
		if (returnMetadata.specificType != null) {
			result.item("specificType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = returnMetadata.specificType;
				return $ret;
			})()));
		}
		if (returnMetadata.specificExternalType != null) {
			result.item("specificExternalType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = returnMetadata.specificExternalType;
				return $ret;
			})()));
		}
		if (returnMetadata.collectionElementType != null) {
			result.item("collectionElementType", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = returnMetadata.collectionElementType;
				return $ret;
			})()));
		}
		switch (returnMetadata.knownType) {
			case TypeDescriptionWellKnownType.Void: break;
			case TypeDescriptionWellKnownType.Pixel:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NumberValue;
				$ret.value = this.convertFromPixels(ComponentRenderer.convertToDouble(value));
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Number:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.NumberValue;
				$ret.value = ComponentRenderer.convertToDouble(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.string1:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = value != null ? value.toString() : null;
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Date:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = value != null ? value.toString() : null;
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Brush:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeBrush(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Color:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeColor(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.BrushCollection:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeBrushCollection(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.boolean1:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.BooleanValue;
				$ret.value = <boolean>value;
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.ExportedType: break;
			case TypeDescriptionWellKnownType.Collection: break;
			case TypeDescriptionWellKnownType.Array: break;
			case TypeDescriptionWellKnownType.PixelPoint:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializePixelPoint(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Point:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializePoint(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.PixelSize:
			result.item("result", <JsonDictionaryItem>this.serializePixelSize(value));
			break;

			case TypeDescriptionWellKnownType.Size:
			result.item("result", <JsonDictionaryItem>this.serializeSize(value));
			break;

			case TypeDescriptionWellKnownType.IList: break;
			case TypeDescriptionWellKnownType.PixelRect:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializePixelRect(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Rect:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeRect(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.DataTemplate: break;
			case TypeDescriptionWellKnownType.ColorCollection:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeColorCollection(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.Unknown: break;
			case TypeDescriptionWellKnownType.MethodRef: break;
			case TypeDescriptionWellKnownType.EventRef: break;
			case TypeDescriptionWellKnownType.DataRef: break;
			case TypeDescriptionWellKnownType.TimeSpan:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeTimespan(value);
				return $ret;
			})()));
			break;

			case TypeDescriptionWellKnownType.TemplateRef: break;
			case TypeDescriptionWellKnownType.DoubleCollection:
			result.item("result", ((() => {
				let $ret = new JsonDictionaryValue();
				$ret.type = JsonDictionaryValueType.StringValue;
				$ret.value = this.serializeDoubleCollection(value);
				return $ret;
			})()));
			break;

		}

		return result.toJson();
	}
	private serializeDoubleCollection(value: any): any {
		return this.adapter.serializeDoubleCollection(value);
	}
	private serializeTimespan(value: any): any {
		return this.adapter.serializeTimespan(value);
	}
	private serializeColorCollection(value: any): any {
		return this.adapter.serializeColorCollection(value);
	}
	private serializeRect(value: any): any {
		return this.adapter.serializeRect(value);
	}
	private serializePixelRect(value: any): any {
		return this.adapter.serializePixelRect(value);
	}
	private serializeSize(value: any): any {
		return this.adapter.serializeSize(value);
	}
	private serializePixelSize(value: any): any {
		return this.adapter.serializePixelSize(value);
	}
	private serializePoint(value: any): any {
		return this.adapter.serializePoint(value);
	}
	private serializePixelPoint(value: any): any {
		return this.adapter.serializePixelPoint(value);
	}
	private serializeBrushCollection(value: any): any {
		return this.adapter.serializeBrushCollection(value);
	}
	private serializeColor(value: any): any {
		return this.adapter.serializeColor(value);
	}
	private serializeBrush(value: any): any {
		return this.adapter.serializeBrush(value);
	}
	private jsonArgumentToAction(targetNode: DescriptionTreeNode, arg: JsonDictionaryItem, ser: DescriptionSerializer): DescriptionTreeAction {
		let act = new DescriptionTreeAction();
		act.actionType = DescriptionTreeActionType.UpdateProperty;
		act.targetNode = targetNode;
		act.propertyName = "*argument";
		let meta = new TypeDescriptionMetadata();
		meta.propertyName = "*argument";
		let wkt: TypeDescriptionWellKnownType = TypeDescriptionWellKnownType.Unknown;
		let wktString = this.extractArgString(arg, "knownType");
		let $ret = EnumUtil.tryParse$1<TypeDescriptionWellKnownType>(TypeDescriptionWellKnownType_$type, wktString, true, wkt);
		wkt = $ret.p2;
		meta.knownType = wkt;
		meta.specificExternalType = this.extractArgString(arg, "specificExternalType");
		meta.specificType = this.extractArgString(arg, "specificType");
		meta.collectionElementType = this.extractArgString(arg, "collectionElementType");
		act.propertyMetadata = meta;
		let value: any = null;
		value = this.extractArgValue(meta, arg, ser);
		act.newValue = value;
		return act;
	}
	private argumentBuilderToAction(targetNode: DescriptionTreeNode, argument: ComponentRendererMethodHelperArgumentBuilder, ser: DescriptionSerializer): DescriptionTreeAction {
		let act = new DescriptionTreeAction();
		act.actionType = DescriptionTreeActionType.UpdateProperty;
		act.targetNode = targetNode;
		act.propertyName = "*argument";
		let meta = new TypeDescriptionMetadata();
		meta.propertyName = "*argument";
		let wkt: TypeDescriptionWellKnownType = argument.wellKnownType;
		meta.knownType = wkt;
		meta.specificExternalType = argument.specificExternalType;
		meta.specificType = argument.specificType;
		meta.collectionElementType = argument.collectionElementType;
		act.propertyMetadata = meta;
		let value: any = this.convertArgValue(meta.knownType, meta.specificType, meta.specificExternalType, meta.collectionElementType, argument.value, ser);
		act.newValue = value;
		return act;
	}
	private returnBuilderToAction(targetNode: DescriptionTreeNode, argument: ComponentRendererMethodHelperReturnBuilder): DescriptionTreeAction {
		let act = new DescriptionTreeAction();
		act.actionType = DescriptionTreeActionType.UpdateProperty;
		act.targetNode = targetNode;
		act.propertyName = "*argument";
		let meta = new TypeDescriptionMetadata();
		meta.propertyName = "*argument";
		let wkt: TypeDescriptionWellKnownType = argument.wellKnownType;
		meta.knownType = wkt;
		meta.specificExternalType = argument.specificExternalType;
		meta.specificType = argument.specificType;
		meta.collectionElementType = argument.collectionElementType;
		act.propertyMetadata = meta;
		let value: any = null;
		act.newValue = value;
		return act;
	}
	private extractArgString(arg: JsonDictionaryItem, v: string): string {
		if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, arg) !== null) {
			let obj = <JsonDictionaryObject>arg;
			if (obj.containsKey(v)) {
				return (<JsonDictionaryValue>obj.item(v)).value != null ? (<JsonDictionaryValue>obj.item(v)).value.toString() : null;
			}
		}
		return null;
	}
	private extractArgValue(metadata: TypeDescriptionMetadata, arg: JsonDictionaryItem, ser: DescriptionSerializer): any {
		if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, arg) !== null) {
			let obj = <JsonDictionaryObject>arg;
			if (obj.containsKey("value")) {
				let value = (obj.item("value")) != null ? (obj.item("value")) : null;
				return this.convertArgValue(metadata.knownType, metadata.specificType, metadata.specificExternalType, metadata.collectionElementType, value, ser);
			}
		}
		return null;
	}
	private convertArgValue(knownType: TypeDescriptionWellKnownType, specificType: string, specificExternalType: string, collectionElementType: string, value: any, ser: DescriptionSerializer): any {
		if (typeCast<JsonDictionaryObject>((<any>JsonDictionaryObject).$type, value) !== null) {
			let jsonObj = <JsonDictionaryObject>value;
			if (jsonObj.containsKey("type") || jsonObj.containsKey("refType")) {
				let res = ser.deserialize(this.context, jsonObj.toJson());
				if (res != null) {
					let result = res.result;
					if (result == null) {
						return null;
					}
					return DescriptionTreeBuilder.createTreeWithOptions(this.context, result, this.preserveKeyOrder);
				}
				return null;
			} else if (knownType == TypeDescriptionWellKnownType.Point) {
				let desc = DescriptionSerializer.convertObjToPoint(this.context, jsonObj);
				return desc;
			} else if (knownType == TypeDescriptionWellKnownType.Rect) {
				let desc1 = DescriptionSerializer.convertObjToPoint(this.context, jsonObj);
				return desc1;
			} else if (knownType == TypeDescriptionWellKnownType.Size) {
				let desc2 = DescriptionSerializer.convertObjToPoint(this.context, jsonObj);
				return desc2;
			}
		} else if (typeCast<JsonDictionaryArray>((<any>JsonDictionaryArray).$type, value) !== null) {
			let jArr = <JsonDictionaryArray>value;
			let eleType_o: string = "string";
			let isCollection = false;
			if (knownType == TypeDescriptionWellKnownType.Array) {
				eleType_o = specificExternalType;
			}
			if (knownType == TypeDescriptionWellKnownType.Collection) {
				isCollection = true;
				eleType_o = collectionElementType;
			}
			let ret = <any[]>new Array(jArr.items.length);
			let eleWkt: TypeDescriptionWellKnownType = TypeDescriptionWellKnownType.Unknown;
			let eleSpecificType: string;
			let eleSpecificExternalType: string;
			let eleCollectionElementType: string;
			let $ret = this.expandEleType(isCollection, eleType_o, specificType, specificExternalType, eleWkt, eleSpecificType, eleSpecificExternalType, eleCollectionElementType);
			eleWkt = $ret.p4;
			eleSpecificType = $ret.p5;
			eleSpecificExternalType = $ret.p6;
			eleCollectionElementType = $ret.p7;
			if (jArr.items != null) {
				for (let i = 0; i < jArr.items.length; i++) {
					let item = jArr.items[i];
					let obj = this.convertArgValue(eleWkt, eleSpecificType, eleSpecificExternalType, eleCollectionElementType, item, ser);
					ret[i] = obj;
				}
			}
			return ret;
		} else if (typeCast<JsonDictionaryValue>((<any>JsonDictionaryValue).$type, value) !== null) {
			let dictValue = <JsonDictionaryValue>value;
			if (dictValue.type == JsonDictionaryValueType.NumberValue) {
				switch (specificExternalType) {
					case "int": return <number>truncate(<number>dictValue.value);
					case "short": return <number>truncate(<number>dictValue.value);
					case "long": return <number>truncate(<number>dictValue.value);
				}

			} else if (dictValue.type == JsonDictionaryValueType.StringValue) {
				if (specificExternalType == "DateTime") {
					return dateParse(<string>dictValue.value);
				}
			}
			return dictValue.value;
		}
		return value;
	}
	private expandEleType(isCollection: boolean, eleType: string, specificType: string, specificExternalType: string, eleWkt: TypeDescriptionWellKnownType, eleSpecificType: string, eleSpecificExternalType: string, eleCollectionElementType: string): { p4: TypeDescriptionWellKnownType; p5: string; p6: string; p7: string; } {
		if (isCollection) {
			eleWkt = TypeDescriptionWellKnownType.ExportedType;
			eleSpecificType = eleType;
			eleSpecificExternalType = null;
			eleCollectionElementType = null;
			return {
				p4: eleWkt,
				p5: eleSpecificType,
				p6: eleSpecificExternalType,
				p7: eleCollectionElementType

			};
		}
		let targetType = specificType;
		if (specificExternalType != null) {
			targetType = specificExternalType;
		}
		eleCollectionElementType = null;
		switch (specificExternalType) {
			case "string":
			if (specificType != null && specificType != "string") {
				eleWkt = TypeDescriptionWellKnownType.ExportedType;
				eleSpecificType = "string";
				eleSpecificExternalType = specificExternalType;
			} else {
				eleWkt = TypeDescriptionWellKnownType.string1;
				eleSpecificType = null;
				eleSpecificExternalType = null;
			}
			return {
				p4: eleWkt,
				p5: eleSpecificType,
				p6: eleSpecificExternalType,
				p7: eleCollectionElementType

			};

			case "double":
			eleWkt = TypeDescriptionWellKnownType.Number;
			eleSpecificType = "double";
			eleSpecificExternalType = "double";
			return {
				p4: eleWkt,
				p5: eleSpecificType,
				p6: eleSpecificExternalType,
				p7: eleCollectionElementType

			};

			case "int":
			eleWkt = TypeDescriptionWellKnownType.Number;
			eleSpecificType = "int";
			eleSpecificExternalType = "int";
			return {
				p4: eleWkt,
				p5: eleSpecificType,
				p6: eleSpecificExternalType,
				p7: eleCollectionElementType

			};

			case "long":
			eleWkt = TypeDescriptionWellKnownType.Number;
			eleSpecificType = "double";
			eleSpecificExternalType = "double";
			return {
				p4: eleWkt,
				p5: eleSpecificType,
				p6: eleSpecificExternalType,
				p7: eleCollectionElementType

			};

			case "bool":
			eleWkt = TypeDescriptionWellKnownType.Number;
			eleSpecificType = "double";
			eleSpecificExternalType = "double";
			return {
				p4: eleWkt,
				p5: eleSpecificType,
				p6: eleSpecificExternalType,
				p7: eleCollectionElementType

			};

		}

		eleWkt = TypeDescriptionWellKnownType.ExportedType;
		eleSpecificType = specificType;
		eleSpecificExternalType = specificExternalType;
		return {
			p4: eleWkt,
			p5: eleSpecificType,
			p6: eleSpecificExternalType,
			p7: eleCollectionElementType

		};
	}
	getContainerState(container: any): ContainerState {
		if (!this._states.containsKey(container)) {
			this._states.addItem(container, ((() => {
				let $ret = new ContainerState();
				$ret.container = container;
				return $ret;
			})()));
		}
		let state: ContainerState = this._states.item(container);
		return state;
	}
	private resolveRefValueImmediate(container: any, refName: string): any {
		if (!this._states.containsKey(container)) {
			this._states.addItem(container, ((() => {
				let $ret = new ContainerState();
				$ret.container = container;
				return $ret;
			})()));
		}
		let state: ContainerState = this._states.item(container);
		if (!this.hasRef(refName)) {
			if (this._systemValues.containsKey(refName)) {
				return this._systemValues.item(refName);
			}
			return null;
		}
		let refr = this.getRef(refName);
		let value = this.getRefValue(refr);
		return value;
	}
	resolveRefValue(container: any, refName: string, onResolve: (arg1: any) => void): void {
		this.onUIThread(container, () => {
			if (!this._states.containsKey(container)) {
				this._states.addItem(container, ((() => {
					let $ret = new ContainerState();
					$ret.container = container;
					return $ret;
				})()));
			}
			let state: ContainerState = this._states.item(container);
			if (!this.hasRef(refName)) {
				if (this._systemValues.containsKey(refName)) {
					onResolve(this._systemValues.item(refName));
					return;
				}
				if (!this.isIdle(container)) {
					this.queueForIdle(container, () => this.resolveRefValue(container, refName, onResolve));
					return;
				}
				onResolve(null);
				return;
			}
			let refr = this.getRef(refName);
			let value = this.getRefValue(refr);
			onResolve(value);
		});
	}
	resolveRefName(container: any, refValue: any, onResolve: (arg1: string) => void): void {
		this.onUIThread(container, () => {
			if (!this._states.containsKey(container)) {
				this._states.addItem(container, ((() => {
					let $ret = new ContainerState();
					$ret.container = container;
					return $ret;
				})()));
			}
			let state: ContainerState = this._states.item(container);
			if (refValue != null && this._refNameLookup.containsKey(refValue)) {
				onResolve(this._refNameLookup.item(refValue));
				return;
			}
			onResolve(null);
		});
	}
	provideRefValue(container: any, refName: string, value: any): void {
		this.onUIThread(container, () => {
			if (!this._states.containsKey(container)) {
				this._states.addItem(container, ((() => {
					let $ret = new ContainerState();
					$ret.container = container;
					return $ret;
				})()));
			}
			let state: ContainerState = this._states.item(container);
			this.provideRefValueCore(container, refName, value, true);
		});
	}
	removeRefValue(container: any, refName: string): void {
		this.onUIThread(container, () => {
			if (!this._states.containsKey(container)) {
				this._states.addItem(container, ((() => {
					let $ret = new ContainerState();
					$ret.container = container;
					return $ret;
				})()));
			}
			let state: ContainerState = this._states.item(container);
			this.removeRefValueCore(container, refName, true);
		});
	}
	private removeUnusedRefs(container: any): void {
		let state: ContainerState = this._states.item(container);
		let cleanupList: List$1<string> = new List$1<string>(String_$type, 0);
		for (let key of fromEnum<string>(this._refs.keys)) {
			if (this._refs.item(key).refCount == 0) {
				cleanupList.add(key);
			}
		}
		for (let i = 0; i < cleanupList.count; i++) {
			if (this._systemValues.containsKey(cleanupList._inner[i])) {
				if (this._cleanup == null) {
					continue;
				}
			}
			let old = this.shouldNamespaceSystemRefValues;
			this.shouldNamespaceSystemRefValues = false;
			this.removeRefValueCore(container, cleanupList._inner[i], this._userValues.containsKey(cleanupList._inner[i]));
			this.shouldNamespaceSystemRefValues = old;
			if (this._cleanupHandlers != null && this._cleanupHandlers.count > 0) {
				for (let j = 0; j < this._cleanupHandlers.count; j++) {
					this._cleanupHandlers._inner[j](container, cleanupList._inner[i]);
				}
			}
		}
	}
	clearRefValues(container: any): void {
		this.onUIThread(container, () => {
			if (!this._states.containsKey(container)) {
				this._states.addItem(container, ((() => {
					let $ret = new ContainerState();
					$ret.container = container;
					return $ret;
				})()));
			}
			let state: ContainerState = this._states.item(container);
			this.clearUserRefValues();
		});
	}
	private isIdle(container: any): boolean {
		if (this._actionsRunning.containsKey(container) && this._actionsRunning.item(container)) {
			return false;
		}
		if (this._queuedActions.containsKey(container) && this._queuedActions.item(container).count > 0) {
			return false;
		}
		return true;
	}
	queueForIdle(container: any, action: () => void): void {
		this.onUIThread(container, () => {
			if (this.isIdle(container)) {
				action();
			} else {
				if (!this._idleActions.containsKey(container)) {
					this._idleActions.item(container, new List$1<() => void>(Delegate_$type, 0));
				}
				this._idleActions.item(container).add(action);
			}
		});
	}
	tryFlushIdleActions(container: any): void {
		if (this.isIdle(container)) {
			if (this._idleActions.containsKey(container)) {
				let toFlush = new List$1<() => void>(Delegate_$type, 1, this._idleActions.item(container));
				this._idleActions.item(container).clear();
				for (let i = 0; i < toFlush.count; i++) {
					toFlush._inner[i]();
				}
			}
		}
	}
	private renderDiff(container: any, diff: List$1<DescriptionTreeAction>): void {
		let state: ContainerState = this._states.item(container);
		this.processActions(container, state, diff);
	}
	private resumeActions(container: any, state: ContainerState): void {
		if (this.isProceedOnErrorEnabled) {
			try {
				this.doActions(container, state, true);
			}
			catch (e) {
				this.logError("error running actions: " + e.toString());
				this._actionsRunning.item(container, false);
			}
		} else {
			this.doActions(container, state, true);
		}
	}
	private tryActions(container: any, state: ContainerState): void {
		if (this.isProceedOnErrorEnabled) {
			try {
				this.doActions(container, state, false);
			}
			catch (e) {
				this.logError("error running actions: " + e.toString());
				this._actionsRunning.item(container, false);
			}
		} else {
			this.doActions(container, state, false);
		}
	}
	private doActions(container: any, state: ContainerState, fromSleep: boolean): void {
		if (!fromSleep) {
			if (this._actionsRunning.containsKey(container) && this._actionsRunning.item(container)) {
				return;
			}
		}
		this._actionsRunning.item(container, true);
		let cont: boolean = true;
		while (cont && this._queuedActions.containsKey(container) && this._queuedActions.item(container).count > 0) {
			while (this._queuedActions.item(container).count > 0 && this._queuedActions.item(container).peek().count < 1) {
				this._queuedActions.item(container).dequeue();
			}
			if (this._queuedActions.item(container).count < 1) {
				break;
			}
			let currQueue = this._queuedActions.item(container).peek();
			while (currQueue.count > 0) {
				let curr = currQueue.dequeue();
				cont = this.processAction(container, state, curr);
				if (!cont) {
					break;
				}
			}
		}
		if (cont) {
			this._actionsRunning.item(container, false);
		}
		if (this.isIdle(container)) {
			this.tryFlushIdleActions(container);
		}
	}
	private processActions(container: any, state: ContainerState, actions: List$1<DescriptionTreeAction>): void {
		let actionQueue: Queue$1<DescriptionTreeAction> = new Queue$1<DescriptionTreeAction>((<any>DescriptionTreeAction).$type);
		for (let i = 0; i < actions.count; i++) {
			actionQueue.enqueue(actions._inner[i]);
		}
		if (!this._queuedActions.containsKey(container)) {
			this._queuedActions.addItem(container, new Queue$1<Queue$1<DescriptionTreeAction>>((<any>Queue$1).$type.specialize((<any>DescriptionTreeAction).$type)));
		}
		this._queuedActions.item(container).enqueue(actionQueue);
		this.tryActions(container, state);
	}
	private processActionsImmediate(container: any, state: ContainerState, actions: List$1<DescriptionTreeAction>): void {
		for (let i = 0; i < actions.count; i++) {
			this.processAction(container, state, actions._inner[i]);
		}
	}
	private processAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		switch (action.actionType) {
			case DescriptionTreeActionType.ClearItems: return this.processClearItemsAction(container, state, action);
			case DescriptionTreeActionType.InsertItem: return this.processInsertItemAction(container, state, action);
			case DescriptionTreeActionType.RemoveItem: return this.processRemoveItemAction(container, state, action);
			case DescriptionTreeActionType.ReplaceItem: return this.processReplaceItemAction(container, state, action);
			case DescriptionTreeActionType.ResetProperty: return this.processResetPropertyAction(container, state, action);
			case DescriptionTreeActionType.UpdateProperty: return this.processUpdatePropertyAction(container, state, action);
		}

		return true;
	}
	private processUpdatePropertyAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		let target = this.getTarget(container, state, action);
		this.updatePropertyOnTarget(container, state, action, target);
		return true;
	}
	private updatePropertyOnTarget(container: any, state: ContainerState, action: DescriptionTreeAction, target: any): boolean {
		let value = this.getCoercedValue(action, container, state);
		value = this.transformer.transform(ComponentRenderer.platform, value, action);
		if (typeCast<TypeDescriptionPropretyTransformsMultipleSets>((<any>TypeDescriptionPropretyTransformsMultipleSets).$type, value) !== null) {
			let ms = <TypeDescriptionPropretyTransformsMultipleSets>value;
			for (let act of fromEnum<TypeDescriptionPropretyTransformsMultipleSetsInfo>(ms.actions)) {
				this.updatePropertyOnTargetCore(container, state, action, target, act.propertyName, act.newValue);
			}
			return true;
		}
		let shouldSkip: boolean = false;
		if (this._targetUpdatingHandlers != null && this._targetUpdatingHandlers.count > 0) {
			for (let i = 0; i < this._targetUpdatingHandlers.count; i++) {
				let handler = this._targetUpdatingHandlers._inner[i];
				let path = this.getPropertyPath(container, action);
				if (handler(path, target, value)) {
					shouldSkip = true;
				}
			}
		}
		if (shouldSkip) {
			return true;
		}
		this.updatePropertyOnTargetCore(container, state, action, target, action.getPlatformPropertyName(ComponentRenderer.platform), value);
		return true;
	}
	private updatePropertyOnTargetCore(container: any, state: ContainerState, action: DescriptionTreeAction, target: any, propertyName: string, value: any): boolean {
		let shouldSkip: boolean = false;
		if (propertyName == "Type" || propertyName == "type") {
			let deliberateOverride = false;
			if (action.propertyMetadata != null) {
				if (!Base.equalsStatic(action.propertyName.toLowerCase(), propertyName.toLowerCase())) {
					deliberateOverride = true;
				}
			}
			if (!deliberateOverride) {
				shouldSkip = true;
			}
		}
		if (shouldSkip) {
			return true;
		}
		if (action.propertyMetadata != null && action.propertyMetadata.knownType == TypeDescriptionWellKnownType.Collection) {
			this.setOrUpdateCollectionOnTarget(container, state, propertyName, action.propertyMetadata, value, target);
		} else {
			this.setPropertyOnTarget(container, state, propertyName, action.propertyMetadata, value, action.oldValue, target);
		}
		return true;
	}
	private getPropertyPath(container: any, action: DescriptionTreeAction): string {
		let node = action.targetNode;
		let path_o: string = "";
		if (node != this._currentDescription.item(container)) {
			let pathToNode = this.getPathToNode(container, node);
			path_o = pathToNode;
		}
		if (path_o.length > 0) {
			path_o += ".";
		}
		path_o += action.propertyName;
		return path_o;
	}
	getNode(container: any, id: number): DescriptionTreeNode {
		let ans = this.getNodeHelper(this._currentDescription.item(container), id);
		if (ans.item1) {
			return ans.item2;
		}
		return null;
	}
	getOldNode(container: any, id: number): DescriptionTreeNode {
		if (this._currentOldTree != null) {
			let ans = this.getNodeHelper(this._currentOldTree, id);
			if (ans.item1) {
				return ans.item2;
			}
			return null;
		}
		return null;
	}
	private getPathToNode(container: any, node: DescriptionTreeNode): string {
		if (node == null) {
			return "";
		}
		let ans = this.getPathToNodeHelper("", this._currentDescription.item(container), node);
		if (ans.item1) {
			return ans.item2;
		}
		return "";
	}
	private getPathToNodeHelper(currPath: string, currNode: DescriptionTreeNode, findNode: DescriptionTreeNode): Tuple$2<boolean, string> {
		if (currNode == findNode) {
			return new Tuple$2<boolean, string>(Boolean_$type, String_$type, true, currPath);
		}
		if (currPath.length > 0) {
			currPath += ".";
		}
		let currItems = currNode.items();
		for (let i = 0; i < currItems.count; i++) {
			let item = currItems._inner[i];
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, item.value) !== null) {
				if ((<DescriptionTreeNode>item.value).id == findNode.id) {
					currPath += item.propertyName;
					return new Tuple$2<boolean, string>(Boolean_$type, String_$type, true, currPath);
				} else {
					let subPath = currPath + item.propertyName;
					let ans = this.getPathToNodeHelper(subPath, <DescriptionTreeNode>item.value, findNode);
					if (ans.item1) {
						return ans;
					}
				}
			}
			if (item.metadata != null && item.metadata.knownType == TypeDescriptionWellKnownType.Collection) {
				let arr = <any[]>item.value;
				for (let j = 0; j < arr.length; j++) {
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr[j]) !== null) {
						let testNode = <DescriptionTreeNode>arr[j];
						if ((<DescriptionTreeNode>testNode).id == findNode.id) {
							currPath += item.propertyName + "[" + j + "]";
							return new Tuple$2<boolean, string>(Boolean_$type, String_$type, true, currPath);
						} else {
							let subPath1 = currPath + item.propertyName + "[" + j + "]";
							let ans1 = this.getPathToNodeHelper(subPath1, <DescriptionTreeNode>testNode, findNode);
							if (ans1.item1) {
								return ans1;
							}
						}
					}
				}
			}
		}
		return new Tuple$2<boolean, string>(Boolean_$type, String_$type, false, currPath);
	}
	private getNodeHelper(currNode: DescriptionTreeNode, findID: number): Tuple$2<boolean, DescriptionTreeNode> {
		if (currNode.id == findID) {
			return new Tuple$2<boolean, DescriptionTreeNode>(Boolean_$type, (<any>DescriptionTreeNode).$type, true, currNode);
		}
		let currItems = currNode.items();
		for (let i = 0; i < currItems.count; i++) {
			let item = currItems._inner[i];
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, item.value) !== null) {
				if ((<DescriptionTreeNode>item.value).id == findID) {
					return new Tuple$2<boolean, DescriptionTreeNode>(Boolean_$type, (<any>DescriptionTreeNode).$type, true, (<DescriptionTreeNode>item.value));
				} else {
					let ans = this.getNodeHelper(<DescriptionTreeNode>item.value, findID);
					if (ans.item1) {
						return ans;
					}
				}
			}
			if (item.metadata != null && item.metadata.knownType == TypeDescriptionWellKnownType.Collection) {
				let arr = <any[]>item.value;
				for (let j = 0; j < arr.length; j++) {
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr[j]) !== null) {
						let testNode = <DescriptionTreeNode>arr[j];
						if ((<DescriptionTreeNode>testNode).id == findID) {
							return new Tuple$2<boolean, DescriptionTreeNode>(Boolean_$type, (<any>DescriptionTreeNode).$type, true, (<DescriptionTreeNode>testNode));
						} else {
							let ans1 = this.getNodeHelper(<DescriptionTreeNode>testNode, findID);
							if (ans1.item1) {
								return ans1;
							}
						}
					}
				}
			}
		}
		return new Tuple$2<boolean, DescriptionTreeNode>(Boolean_$type, (<any>DescriptionTreeNode).$type, false, null);
	}
	private setOrUpdateCollectionOnTarget(container: any, state: ContainerState, propertyName: string, propertyMetadata: TypeDescriptionMetadata, value: any, target: any): boolean {
		if (this._updatingHandlers.containsKey(propertyName)) {
			for (let h of fromEnum<(propertyName: string, target: any, newValue: any) => void>(this._updatingHandlers.item(propertyName))) {
				h(propertyName, target, value);
			}
		}
		this.adapter.setOrUpdateCollectionOnTarget(container, propertyName, propertyMetadata, this.context, target, value);
		return true;
	}
	private getCoercedValue(action: DescriptionTreeAction, container: any, state: ContainerState): any {
		if (action.propertyMetadata == null) {
			return action.newValue;
		}
		switch (action.propertyMetadata.knownType) {
			case TypeDescriptionWellKnownType.Array: return this.coerceToArray(action, container, state);
			case TypeDescriptionWellKnownType.boolean1: return this.coerceToBoolean(action);
			case TypeDescriptionWellKnownType.Brush: return this.coerceToBrush(action);
			case TypeDescriptionWellKnownType.BrushCollection: return this.coerceToBrushCollection(action);
			case TypeDescriptionWellKnownType.DoubleCollection: return this.coerceToDoubleCollection(action);
			case TypeDescriptionWellKnownType.Collection: return this.coerceToArray(action, container, state);
			case TypeDescriptionWellKnownType.Color: return this.coerceToColor(action);
			case TypeDescriptionWellKnownType.ColorCollection: return this.coerceToColorCollection(action);
			case TypeDescriptionWellKnownType.DataRef: return this.setUpDataRef(action, state);
			case TypeDescriptionWellKnownType.TemplateRef: return this.setUpTemplateRef(action, state);
			case TypeDescriptionWellKnownType.DataTemplate: return null;
			case TypeDescriptionWellKnownType.Date: return this.coerceToDate(action);
			case TypeDescriptionWellKnownType.EventRef: return this.setUpEventRef(action, state);
			case TypeDescriptionWellKnownType.ExportedType: return this.coerceToExportedType(action, container, state);
			case TypeDescriptionWellKnownType.IList: return null;
			case TypeDescriptionWellKnownType.MethodRef: return this.setUpMethodRef(action, state);
			case TypeDescriptionWellKnownType.Pixel: return this.coerceToPixel(action);
			case TypeDescriptionWellKnownType.Number: return this.coerceToNumber(action);
			case TypeDescriptionWellKnownType.PixelPoint: return this.coerceToPixelPoint(action);
			case TypeDescriptionWellKnownType.Point: return this.coerceToPoint(action);
			case TypeDescriptionWellKnownType.PixelRect: return this.coerceToPixelRect(action);
			case TypeDescriptionWellKnownType.Rect: return this.coerceToRect(action);
			case TypeDescriptionWellKnownType.PixelSize: return this.coerceToPixelSize(action);
			case TypeDescriptionWellKnownType.Size: return this.coerceToSize(action);
			case TypeDescriptionWellKnownType.string1: return this.coerceToString(action);
			case TypeDescriptionWellKnownType.TimeSpan: return this.coerceToTimeSpan(action);
			case TypeDescriptionWellKnownType.Unknown: return this.coerceUnknown(action, container, state);
			case TypeDescriptionWellKnownType.Void: return null;
		}

		return null;
	}
	private coerceUnknown(action: DescriptionTreeAction, container: any, state: ContainerState): any {
		let ret = this.coerceUnknownValue(action.newValue, action, container, state);
		return ret;
	}
	private coerceUnknownValue(value: any, action: DescriptionTreeAction, container: any, state: ContainerState): any {
		if (typeof value === 'string') {
			if (stringStartsWith((<string>value), "@d:")) {
				let v_ = (<string>value).substr(3);
				value = new Date(v_);
			}
		}
		if (typeCast<any[]>(Array_$type, value) !== null) {
			let newValues = <any[]>value;
			let intValues: any[] = <any[]>new Array(newValues.length);
			for (let i = 0; i < newValues.length; i++) {
				let intVal: any = this.coerceUnknownValue(newValues[i], action, container, state);
				intValues[i] = intVal;
			}
			value = intValues;
		}
		if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, value) !== null) {
			let node = <DescriptionTreeNode>value;
			if (node.type == "EmbeddedRef") {
				return this.createObject(node.type, value, container, state, false, action.targetNode.id, action.propertyMetadata);
			}
		}
		return value;
	}
	coerceToTimeSpan(action: DescriptionTreeAction): any {
		return action.newValue;
	}
	private coerceToString(action: DescriptionTreeAction): any {
		return action.newValue != null ? action.newValue.toString() : null;
	}
	coerceToSize(action: DescriptionTreeAction): any {
		let size = <SizeDescription>action.newValue;
		let width_ = size.width;
		let height_ = size.height;
		let s = { width: width_, height: height_ };
		return s;
	}
	coerceToPixelSize(action: DescriptionTreeAction): any {
		let size = <SizeDescription>action.newValue;
		let cln: SizeDescription = null;
		if (size != null) {
			cln = new SizeDescription();
			cln.width = this.convertToPixels(cln.width);
			cln.height = this.convertToPixels(cln.height);
		}
		let width_ = cln.width;
		let height_ = cln.height;
		let s = { width: width_, height: height_ };
		return s;
	}
	coerceToRect(action: DescriptionTreeAction): any {
		let rect = <RectDescription>action.newValue;
		let top_ = rect.top;
		let left_ = rect.left;
		let width_ = rect.width;
		let height_ = rect.height;
		let r = { left: left_, top: top_, width: width_, height: height_ };
		return r;
	}
	coerceToPixelRect(action: DescriptionTreeAction): any {
		let rect = <RectDescription>action.newValue;
		let cln: RectDescription = null;
		if (rect != null) {
			cln = new RectDescription();
			cln.left = this.convertToPixels(cln.left);
			cln.top = this.convertToPixels(cln.top);
			cln.width = this.convertToPixels(cln.width);
			cln.height = this.convertToPixels(cln.height);
		}
		let top_ = cln.top;
		let left_ = cln.left;
		let width_ = cln.width;
		let height_ = cln.height;
		let r = { left: left_, top: top_, width: width_, height: height_ };
		return r;
	}
	coerceToPoint(action: DescriptionTreeAction): any {
		let point = <PointDescription>action.newValue;
		let x_ = point.x;
		let y_ = point.y;
		let p = { x: x_, y: y_ };
		return p;
	}
	coerceToPixelPoint(action: DescriptionTreeAction): any {
		let point = <PointDescription>action.newValue;
		let cln: PointDescription = null;
		if (point != null) {
			cln = new PointDescription();
			cln.x = this.convertToPixels(cln.x);
			cln.y = this.convertToPixels(cln.y);
		}
		let x_ = cln.x;
		let y_ = cln.y;
		let p = { x: x_, y: y_ };
		return p;
	}
	private coerceToPixel(action: DescriptionTreeAction): any {
		if (action.newValue == null) {
			if (action.propertyMetadata != null && ((action.propertyMetadata.specificType == "double" || action.propertyMetadata.specificType == "float") || (action.propertyMetadata.specificExternalType == "double" || action.propertyMetadata.specificExternalType == "float"))) {
				return NaN;
			}
			return 0;
		}
		let val = action.newValue;
		if (typeof val === 'string') {
			let strVal = <string>val;
			if (strVal == "@dbl:INFINITY") {
				return Number.POSITIVE_INFINITY;
			}
			if (strVal == "@dbl:-INFINITY") {
				return Number.NEGATIVE_INFINITY;
			}
		}
		if (typeof val === 'number') {
			val = <number>typeGetValue(val);
		}
		if (action.propertyMetadata != null && action.propertyMetadata.specificExternalType != null) {
			switch (action.propertyMetadata.specificExternalType) {
				case "double":
				val = <number>this.convertToPixels(<number>val);
				break;

				case "int":
				if (typeof val === 'number') {
					val = typeGetValue(val);
				} else {
					val = <number>truncate(<number>val);
				}
				val = <number>truncate(this.convertToPixels(typeGetValue(val)));
				break;

				case "float":
				if (typeof val === 'number') {
					val = <number>val;
				} else {
					val = <number><number>val;
				}
				val = <number>this.convertToPixels(<number>val);
				break;

				case "short":
				if (typeof val === 'number') {
					val = typeGetValue(val);
				} else {
					val = <number>truncate(<number>val);
				}
				val = <number>truncate(this.convertToPixels(typeGetValue(val)));
				break;

				case "long":
				if (typeof val === 'number') {
					val = typeGetValue(val);
				} else {
					val = <number>truncate(<number>val);
				}
				val = <number>truncate(this.convertToPixels(typeGetValue(val)));
				break;

			}

		}
		return val;
	}
	private convertToPixels(val: number): number {
		throw new NotImplementedException(0);
	}
	private coerceToNumber(action: DescriptionTreeAction): any {
		if (action.newValue == null) {
			if (action.propertyMetadata != null && ((action.propertyMetadata.specificType == "double" || action.propertyMetadata.specificType == "float") || (action.propertyMetadata.specificExternalType == "double" || action.propertyMetadata.specificExternalType == "float"))) {
				return NaN;
			}
			return 0;
		}
		let val = action.newValue;
		if (typeof val === 'string') {
			let strVal = <string>val;
			if (strVal == "@dbl:INFINITY") {
				return Number.POSITIVE_INFINITY;
			}
			if (strVal == "@dbl:-INFINITY") {
				return Number.NEGATIVE_INFINITY;
			}
		}
		if (typeof val === 'number') {
			val = <number>typeGetValue(val);
		}
		if (action.propertyMetadata != null && action.propertyMetadata.specificExternalType != null) {
			switch (action.propertyMetadata.specificExternalType) {
				case "int":
				if (typeof val === 'number') {
					val = typeGetValue(val);
				} else {
					val = <number>truncate(<number>val);
				}
				break;

				case "float":
				if (typeof val === 'number') {
					val = <number>val;
				} else {
					val = <number><number>val;
				}
				break;

				case "short":
				if (typeof val === 'number') {
					val = typeGetValue(val);
				} else {
					val = <number>truncate(<number>val);
				}
				break;

				case "long":
				if (typeof val === 'number') {
					val = typeGetValue(val);
				} else {
					val = <number>truncate(<number>val);
				}
				break;

			}

		}
		return val;
	}
	createRef(name: string): DescriptionRef {
		if (this._refs.containsKey(name)) {
			let existing = this._refs.item(name);
			return existing;
		}
		let r = new DescriptionRef(name);
		this._refs.addItem(name, r);
		return r;
	}
	private setUpMethodRef(action: DescriptionTreeAction, state: ContainerState): any {
		return action.newValue == null ? null : this.createRef(<string>action.newValue);
	}
	private coerceToExportedType(action: DescriptionTreeAction, container: any, state: ContainerState): any {
		let t = action.propertyMetadata.specificType;
		if (t == null) {
			t = action.propertyMetadata.specificExternalType;
		}
		if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null) {
			let node = <DescriptionTreeNode>action.newValue;
			if (node.has("Type")) {
				t = <string>node.get("Type").value;
			}
		}
		let obj = this.createObject(t, action.newValue, container, state, false, action.targetNode.id, action.propertyMetadata);
		return obj;
	}
	private setUpEventRef(action: DescriptionTreeAction, state: ContainerState): any {
		return action.newValue == null ? null : this.createRef(<string>action.newValue);
	}
	private coerceToDate(action: DescriptionTreeAction): any {
		if (typeof action.newValue === 'string') {
			let v_ = action.newValue;
			action.newValue = new Date(v_);
		}
		return <Date>action.newValue;
	}
	private setUpDataRef(action: DescriptionTreeAction, state: ContainerState): any {
		return action.newValue == null ? null : this.createRef(<string>action.newValue);
	}
	private setUpTemplateRef(action: DescriptionTreeAction, state: ContainerState): any {
		return action.newValue == null ? null : this.createRef(<string>action.newValue);
	}
	protected toColor(color: string): any {
		return color;
	}
	private coerceToColorCollection(action: DescriptionTreeAction): any {
		let arr = <any[]>action.newValue;
		if (arr == null) {
			return null;
		}
		let colors: any[] = <any[]>new Array(arr.length);
		for (let i = 0; i < arr.length; i++) {
			colors[i] = this.toColor(<string>arr[i]);
		}
		return colors;
	}
	private coerceToColor(action: DescriptionTreeAction): any {
		return this.toColor(<string>action.newValue);
	}
	private coerceToBrushCollection(action: DescriptionTreeAction): any {
		let arr = <any[]>action.newValue;
		if (arr == null) {
			return null;
		}
		let colors: any[] = <any[]>new Array(arr.length);
		for (let i = 0; i < arr.length; i++) {
			colors[i] = this.toBrush(<string>arr[i]);
		}
		return colors;
	}
	private coerceToDoubleCollection(action: DescriptionTreeAction): any {
		let arr = <any[]>action.newValue;
		if (arr == null) {
			return null;
		}
		let lengths: any[] = <any[]>new Array(arr.length);
		for (let i = 0; i < arr.length; i++) {
			lengths[i] = (<number>arr[i]);
		}
		return lengths;
	}
	protected toBrush(v: string): any {
		return v;
	}
	private coerceToBrush(action: DescriptionTreeAction): any {
		return this.toBrush(<string>action.newValue);
	}
	private coerceToBoolean(action: DescriptionTreeAction): any {
		return <boolean>action.newValue;
	}
	private _targetUpdatingHandlers: List$1<(propertyPath: string, target: any, newValue: any) => boolean> = new List$1<(propertyPath: string, target: any, newValue: any) => boolean>(Delegate_$type, 0);
	addTargetPropertyUpdatingListener(handler: (propertyPath: string, target: any, newValue: any) => boolean): void {
		this._targetUpdatingHandlers.add(handler);
	}
	removeTargetPropertyUpdatingListener(handler: (propertyPath: string, target: any, newValue: any) => boolean): void {
		this._targetUpdatingHandlers.remove(handler);
	}
	private _updatingHandlers: Dictionary$2<string, List$1<(propertyName: string, target: any, newValue: any) => void>> = new Dictionary$2<string, List$1<(propertyName: string, target: any, newValue: any) => void>>(String_$type, (<any>List$1).$type.specialize(Delegate_$type), 0);
	addPropertyUpdatingListener(propertyName: string, handler: (propertyName: string, target: any, newValue: any) => void): void {
		if (!this._updatingHandlers.containsKey(propertyName)) {
			this._updatingHandlers.addItem(propertyName, new List$1<(propertyName: string, target: any, newValue: any) => void>(Delegate_$type, 0));
		}
		this._updatingHandlers.item(propertyName).add(handler);
	}
	private _lookupHandlers: List$1<(container: any, refType: string, id: string) => any> = new List$1<(container: any, refType: string, id: string) => any>(Delegate_$type, 0);
	addReferenceLookupListener(handler: (container: any, refType: string, id: string) => any): void {
		this._lookupHandlers.add(handler);
	}
	removeReferenceLookupListener(handler: (container: any, refType: string, id: string) => any): void {
		this._lookupHandlers.remove(handler);
	}
	private _reverseLookupHandlers: List$1<(obj: any) => string> = new List$1<(obj: any) => string>(Delegate_$type, 0);
	addReferenceReverseLookupListener(handler: (obj: any) => string): void {
		this._reverseLookupHandlers.add(handler);
	}
	removeReferenceReverseLookupListener(handler: (obj: any) => string): void {
		this._reverseLookupHandlers.remove(handler);
	}
	removePropertyUpdatingListener(propertyName: string, handler: (propertyName: string, target: any, newValue: any) => void): void {
		if (this._updatingHandlers.containsKey(propertyName)) {
			this._updatingHandlers.item(propertyName).remove(handler);
			if (this._updatingHandlers.item(propertyName).count == 0) {
				this._updatingHandlers.removeItem(propertyName);
			}
		}
	}
	private _namespaceLookupHandlers: List$1<(container: any) => string> = new List$1<(container: any) => string>(Delegate_$type, 0);
	addNamespaceLookupListener(handler: (container: any) => string): void {
		this._namespaceLookupHandlers.add(handler);
	}
	removeNamespaceLookupListener(handler: (container: any) => string): void {
		this._namespaceLookupHandlers.remove(handler);
	}
	private _cleanupHandlers: List$1<(container: any, id: string) => void> = null;
	private _currentOldTree: DescriptionTreeNode = null;
	addCleanupListener(handler: (container: any, id: string) => void): void {
		if (this._cleanupHandlers == null) {
			this._cleanupHandlers = new List$1<(container: any, id: string) => void>(Delegate_$type, 0);
		}
		this._cleanupHandlers.add(handler);
	}
	removeCleanupListener(handler: (container: any, id: string) => void): void {
		if (this._cleanupHandlers != null) {
			this._cleanupHandlers.remove(handler);
		}
	}
	private coerceToArray(action: DescriptionTreeAction, container: any, state: ContainerState): any {
		if (action.newValue == null) {
			return null;
		}
		let arr: IList = <IList><any>action.newValue;
		let list: List$1<any> = new List$1<any>((<any>Base).$type, 0);
		let t = action.propertyMetadata.specificType;
		if (t == null) {
			t = action.propertyMetadata.specificExternalType;
		}
		if (action.propertyMetadata.knownType == TypeDescriptionWellKnownType.Collection) {
			t = action.propertyMetadata.collectionElementType;
			if (t == null) {
				t = action.propertyMetadata.specificExternalType;
			}
		}
		for (let element of fromEn<any>(arr)) {
			let itemT = t;
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, element) !== null) {
				let node = <DescriptionTreeNode>element;
				if (node.has("Type")) {
					itemT = <string>node.get("Type").value;
				}
			}
			let item = this.createObject(itemT, element, container, state, false, action.targetNode.id, action.propertyMetadata);
			list.add1(item);
		}
		return list.toArray();
	}
	setPropertyOnTarget(container: any, state: ContainerState, propertyName: string, metadata: TypeDescriptionMetadata, value: any, oldValue: any, target: any): void {
		if (value == null && oldValue != null) {
			if (metadata.knownType == TypeDescriptionWellKnownType.DataRef || metadata.knownType == TypeDescriptionWellKnownType.EventRef || metadata.knownType == TypeDescriptionWellKnownType.MethodRef || metadata.knownType == TypeDescriptionWellKnownType.TemplateRef) {
				if (oldValue != null && typeof oldValue === 'string') {
					if (this.hasRef(<string>oldValue)) {
						let oldRef = this.getRef(<string>oldValue);
						oldRef.unregisterValueChanged(container, target, propertyName);
					}
				}
			}
		}
		if (typeCast<DescriptionRef>((<any>DescriptionRef).$type, value) !== null) {
			let r = <DescriptionRef>value;
			let refUnchanged: boolean = false;
			if (oldValue != null && typeof oldValue === 'string') {
				if (this.hasRef(<string>oldValue)) {
					let oldRef1 = this.getRef(<string>oldValue);
					if (Base.equalsStatic(oldRef1, value)) {
						refUnchanged = true;
					} else {
						oldRef1.unregisterValueChanged(container, target, propertyName);
					}
				}
			}
			if (this.hasRefValue(<DescriptionRef>value)) {
				let reff = <DescriptionRef>value;
				if (reff.name != null && reff.name.length > 0 && this._systemValues.containsKey(reff.name)) {
					reff.hasSystemValue = true;
				}
				value = this.getRefValue(<DescriptionRef>value);
				this.setPropertyValue(target, propertyName, metadata, value, oldValue, reff);
			} else {
				this.adapter.onPendingRef(target, propertyName, metadata, <DescriptionRef>value);
			}
			let ev: (sender: any, args: DescriptionRefValueChangedEventArgs) => void = null;
			ev = (rf: any, args: DescriptionRefValueChangedEventArgs) => {
				let reff = <DescriptionRef>rf;
				if (reff.name != null && reff.name.length > 0 && this._systemValues.containsKey(reff.name)) {
					reff.hasSystemValue = true;
				}
				let v = this.getRefValue(<DescriptionRef>rf);
				this.setPropertyValue(target, propertyName, metadata, v, args.oldValue, reff);
			};
			if (!refUnchanged) {
				r.registerValueChanged(container, target, propertyName, ev);
				if (typeCast<DescriptionRef>((<any>DescriptionRef).$type, value) !== null) {
					let reff1 = <DescriptionRef>value;
					let found: boolean = false;
					if (reff1.name != null && reff1.name.length > 0) {
						let args = this.tryResolveReference(reff1.name);
						if (args != null && args.found) {
							found = true;
							let v = args.referenceValue;
							this.provideRefValue(container, reff1.name, v);
						}
					}
				}
			}
			return;
		}
		this.setPropertyValue(target, propertyName, metadata, value, oldValue, null);
	}
	hasUserRef(name: string): boolean {
		return this._userValues.containsKey(name);
	}
	private _shouldNamespaceSystemRefValues: boolean = false;
	get shouldNamespaceSystemRefValues(): boolean {
		return this._shouldNamespaceSystemRefValues;
	}
	set shouldNamespaceSystemRefValues(value: boolean) {
		this._shouldNamespaceSystemRefValues = value;
	}
	private _skipRemoveItemMaps: boolean = false;
	provideRefValueCore(container: any, name: string, value: any, isUserValue: boolean): void {
		let oldValue: any = null;
		if (isUserValue) {
			if (this._userValues.containsKey(name)) {
				oldValue = this._userValues.item(name);
			}
			this._userValues.item(name, value);
			if (value != null) {
				if (oldValue != null && this._refNameLookup.containsKey(oldValue)) {
					this._refNameLookup.removeItem(oldValue);
				}
				if (oldValue != null && !this._skipRemoveItemMaps && this._itemMaps.containsKey(name)) {
					this._itemMaps.removeItem(name);
				}
				if (oldValue != null && !this._skipRemoveItemMaps && this._reverseItemMaps.containsKey(name)) {
					this._reverseItemMaps.removeItem(name);
				}
				if (oldValue != null && !this._skipRemoveItemMaps && this._dateCacheInfo.containsKey(name)) {
					this._dateCacheInfo.removeItem(name);
				}
				this._refNameLookup.item(value, name);
			}
		} else {
			if (this.shouldNamespaceSystemRefValues) {
				let containerId_o: string = "";
				if (this._namespaceLookupHandlers != null && this._namespaceLookupHandlers.count > 0) {
					for (let i = 0; i < this._namespaceLookupHandlers.count; i++) {
						containerId_o = this._namespaceLookupHandlers._inner[i](container);
					}
				}
				if (!stringIsNullOrEmpty(containerId_o)) {
					name = containerId_o + "/" + name;
				}
			}
			if (this._systemValues.containsKey(name)) {
				oldValue = this._systemValues.item(name);
			}
			this._systemValues.item(name, value);
			if (value != null) {
				this._refNameLookup.item(value, name);
			}
			if (this._refs.containsKey(name)) {
				this._refs.item(name).hasSystemValue = true;
			}
		}
		if (this._refs.containsKey(name)) {
			this._refs.item(name).notifyChanged(oldValue, value);
		}
	}
	removeRefValueCore(container: any, name: string, isUserValue: boolean): void {
		let oldValue: any = null;
		if (isUserValue) {
			if (this._userValues.containsKey(name)) {
				oldValue = this._userValues.item(name);
			}
			this._userValues.removeItem(name);
			if (oldValue != null && this._refNameLookup.containsKey(oldValue)) {
				this._refNameLookup.removeItem(oldValue);
			}
			if (oldValue != null && this._itemMaps.containsKey(name)) {
				this._itemMaps.removeItem(name);
			}
			if (oldValue != null && this._reverseItemMaps.containsKey(name)) {
				this._reverseItemMaps.removeItem(name);
			}
			if (oldValue != null && this._dateCacheInfo.containsKey(name)) {
				this._dateCacheInfo.removeItem(name);
			}
		} else {
			if (this.shouldNamespaceSystemRefValues) {
				let containerId_o: string = "";
				if (this._namespaceLookupHandlers != null && this._namespaceLookupHandlers.count > 0) {
					for (let i = 0; i < this._namespaceLookupHandlers.count; i++) {
						containerId_o = this._namespaceLookupHandlers._inner[i](container);
					}
				}
				if (!stringIsNullOrEmpty(containerId_o)) {
					name = containerId_o + "/" + name;
				}
			}
			if (this._systemValues.containsKey(name)) {
				oldValue = this._systemValues.item(name);
			}
			this._systemValues.removeItem(name);
			if (oldValue != null && this._refNameLookup.containsKey(oldValue)) {
				this._refNameLookup.removeItem(oldValue);
			}
			if (oldValue != null && this._itemMaps.containsKey(name)) {
				this._itemMaps.removeItem(name);
			}
			if (oldValue != null && this._reverseItemMaps.containsKey(name)) {
				this._reverseItemMaps.removeItem(name);
			}
			if (oldValue != null && this._dateCacheInfo.containsKey(name)) {
				this._dateCacheInfo.removeItem(name);
			}
		}
		if (this._refs.containsKey(name)) {
			this._refs.item(name).notifyChanged(oldValue, this.getRefValue(this._refs.item(name)));
			if (this._refs.item(name).refCount <= 0) {
				this._refs.item(name).unregisterAll();
				this._refs.removeItem(name);
			}
		}
	}
	clearUserRefValues(): void {
		let keys = new List$1<string>(String_$type, 0);
		for (let key of fromEnum<string>(this._userValues.keys)) {
			keys.add(key);
		}
		for (let key1 of fromEnum<string>(keys)) {
			this.removeRefValueCore(null, key1, true);
		}
	}
	hasRefValue(r: DescriptionRef): boolean {
		return this._userValues.containsKey(r.name) || this._systemValues.containsKey(r.name);
	}
	getRefValue(r: DescriptionRef): any {
		if (this._userValues.containsKey(r.name)) {
			return this._userValues.item(r.name);
		}
		if (this._systemValues.containsKey(r.name)) {
			return this._systemValues.item(r.name);
		}
		return null;
	}
	hasRef(refName: string): boolean {
		return this._refs.containsKey(refName);
	}
	getRef(refName: string): DescriptionRef {
		return this._refs.item(refName);
	}
	getMissingRefs(): string[] {
		let missing: List$1<string> = new List$1<string>(String_$type, 0);
		for (let key of fromEnum<string>(this._refs.keys)) {
			if (!this.hasRefValue(this._refs.item(key))) {
				missing.add(key);
			}
		}
		return missing.toArray();
	}
	getRefChangeInfos(refName: string): DescriptionRefTargetInfo[] {
		let infos: List$1<DescriptionRefTargetInfo> = new List$1<DescriptionRefTargetInfo>((<any>DescriptionRefTargetInfo).$type, 0);
		if (this._refs.containsKey(refName)) {
			for (let val of fromEnum<RefValueChangedTarget>(this._refs.item(refName).valueChangedTargets)) {
				infos.add(((() => {
					let $ret = new DescriptionRefTargetInfo();
					$ret.container = val.container;
					$ret.propertyName = val.propertyName;
					$ret.target = val.target;
					return $ret;
				})()));
			}
		}
		return infos.toArray();
	}
	private getTarget(container: any, state: ContainerState, action: DescriptionTreeAction): any {
		return state.getComponent(action.targetNode.id);
	}
	private processResetPropertyAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		let target = this.getTarget(container, state, action);
		this.resetPropertyOnTarget(container, state, action, target);
		return true;
	}
	private resetPropertyOnTarget(container: any, state: ContainerState, action: DescriptionTreeAction, target: any): boolean {
		this.adapter.resetPropertyOnTarget(container, action.getPlatformPropertyName(ComponentRenderer.platform), action.propertyMetadata, target);
		return true;
	}
	private processReplaceItemAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		if (action.targetNode == null) {
			return this.replaceRootItem(container, state, action);
		} else {
			let target = this.getTarget(container, state, action);
			this.replaceItemInCollection(container, state, action, target);
		}
		return true;
	}
	private replaceItemInCollection(container: any, state: ContainerState, action: DescriptionTreeAction, target: any): void {
		let propertyName = action.getPlatformPropertyName(ComponentRenderer.platform);
		let eleType = action.propertyMetadata.collectionElementType;
		if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null) {
			let node = <DescriptionTreeNode>action.newValue;
			if (node.has("Type")) {
				eleType = <string>node.get("Type").value;
			}
		}
		if (eleType == null) {
			eleType = action.propertyMetadata.specificExternalType;
		}
		let item = this.createObject(eleType, action.newValue, container, state, false, action.targetNode.id, action.propertyMetadata);
		this.adapter.replaceItemInCollection(propertyName, action.propertyMetadata, target, action.newIndex, item);
		if (action.oldValue != null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.oldValue) !== null) {
			let id = (<DescriptionTreeNode>action.oldValue).id;
			if (id >= 0) {
				let subTarget = state.getComponent(id);
				if (subTarget != null) {
					this.destroyObject(container, subTarget, state);
				}
			}
		}
	}
	private replaceRootItem(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		let current = this.adapter.getRootObject(container);
		if (current != null) {
			this.destroyObject(container, current, state);
		}
		if (!(typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null)) {
			throw new NotImplementedException(0);
		}
		let node = <DescriptionTreeNode>action.newValue;
		let finished: boolean = false;
		this.adapter.replaceRootItem(container, node.type, this.context, (resumeRequired: boolean) => {
			if (this.isProceedOnErrorEnabled) {
				try {
					let obj = this.adapter.getRootObject(container);
					state.addComponent(container, node.id, obj, node, runOn(this, this.provideRefValueCore), -1);
					let actions = this.toActions(node);
					this.processActionsImmediate(container, state, actions);
				}
				catch (e) {
					this.logError("error processing actions on resume: " + e.toString());
					this._actionsRunning.item(container, false);
					finished = true;
					return;
				}
			} else {
				let obj1 = this.adapter.getRootObject(container);
				state.addComponent(container, node.id, obj1, node, runOn(this, this.provideRefValueCore), -1);
				let actions1 = this.toActions(node);
				this.processActionsImmediate(container, state, actions1);
			}
			finished = true;
			if (resumeRequired) {
				this.resumeActions(container, state);
			}
		});
		return finished;
	}
	destroyObject(container: any, current: any, state: ContainerState): void {
		if (this.isProceedOnErrorEnabled) {
			try {
				this.destroyObjectHelper(container, current, state);
			}
			catch (e) {
				this.logError("error destroying object: " + e.toString());
				return;
			}
		} else {
			this.destroyObjectHelper(container, current, state);
		}
	}
	destroyObjectHelper(container: any, current: any, state: ContainerState): void {
		let id = this.getComponentId(container, current, state);
		if (this.isProceedOnErrorEnabled) {
			try {
				this.cleanupMethods.cleanup(container, ComponentRenderer.platform, current);
			}
			catch (e) {
				this.logError("error cleaning up object object: " + e.toString());
				return;
			}
		} else {
			this.cleanupMethods.cleanup(container, ComponentRenderer.platform, current);
		}
		for (let r of fromEnum<DescriptionRef>(this._refs.values)) {
			r.unregisterAllValueChanged(current);
		}
		if (id != -1) {
			state.removeComponent(this, container, id, runOn(this, this.removeRefValueCore));
		}
	}
	getComponentId(container: any, current: any, state: ContainerState): number {
		return state.getComponentId(current);
	}
	private processRemoveItemAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		if (action.targetNode == null) {
			return this.removeRootItem(container, state, action);
		} else {
			let target = this.getTarget(container, state, action);
			this.removeItemFromCollection(container, state, action, target);
		}
		return true;
	}
	private removeItemFromCollection(container: any, state: ContainerState, action: DescriptionTreeAction, target: any): void {
		let propertyName = action.getPlatformPropertyName(ComponentRenderer.platform);
		this.adapter.removeItemFromCollection(propertyName, action.propertyMetadata, target, action.oldIndex);
		if (action.oldValue != null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.oldValue) !== null) {
			let id = (<DescriptionTreeNode>action.oldValue).id;
			if (id >= 0) {
				let subTarget = state.getComponent(id);
				if (subTarget != null) {
					this.destroyObject(container, subTarget, state);
				}
			}
		}
	}
	private removeRootItem(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		let current = this.adapter.getRootObject(container);
		if (current != null) {
			this.destroyObject(container, current, state);
		}
		let finished: boolean = false;
		this.adapter.removeRootItem(container, this.context, (resumeRequired: boolean) => {
			finished = true;
			if (resumeRequired) {
				this.resumeActions(container, state);
			}
		});
		return finished;
	}
	private processInsertItemAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		if (action.targetNode == null) {
			return this.addRootItem(container, state, action);
		} else {
			let target = this.getTarget(container, state, action);
			this.addItemToCollection(container, state, action, target);
		}
		return true;
	}
	private addRootItem(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		if (!(typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null)) {
			throw new NotImplementedException(0);
		}
		let node = <DescriptionTreeNode>action.newValue;
		let finished: boolean = false;
		this.adapter.replaceRootItem(container, node.type, this.context, (resumeRequired: boolean) => {
			if (this.isProceedOnErrorEnabled) {
				try {
					let obj = this.adapter.getRootObject(container);
					state.addComponent(container, node.id, obj, node, runOn(this, this.provideRefValueCore), -1);
					let actions = this.toActions(node);
					this.processActionsImmediate(container, state, actions);
				}
				catch (ex) {
					this.logError("error resuming actions" + ex.toString());
					this._actionsRunning.item(container, false);
					finished = true;
					return;
				}
			} else {
				let obj1 = this.adapter.getRootObject(container);
				state.addComponent(container, node.id, obj1, node, runOn(this, this.provideRefValueCore), -1);
				let actions1 = this.toActions(node);
				this.processActionsImmediate(container, state, actions1);
			}
			finished = true;
			if (resumeRequired) {
				this.resumeActions(container, state);
			}
		});
		return finished;
	}
	private addItemToCollection(container: any, state: ContainerState, action: DescriptionTreeAction, target: any): void {
		let propertyName = action.getPlatformPropertyName(ComponentRenderer.platform);
		let eType = action.propertyMetadata.collectionElementType;
		if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, action.newValue) !== null) {
			let node = <DescriptionTreeNode>action.newValue;
			if (node.has("Type")) {
				eType = <string>node.get("Type").value;
			}
		}
		if (eType == null) {
			eType = action.propertyMetadata.specificExternalType;
		}
		let item = this.createObject(eType, action.newValue, container, state, false, action.targetNode.id, action.propertyMetadata);
		this.adapter.addItemToCollection(propertyName, action.propertyMetadata, target, action.newIndex, item);
	}
	private createObject(type: string, newValue: any, container: any, state: ContainerState, skipTracking: boolean, parentId: number, metadata: TypeDescriptionMetadata): any {
		if (this.isPrimitiveType(type)) {
			return newValue;
		}
		let obj: any = null;
		if (newValue != null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newValue) !== null) {
			let newNode = <DescriptionTreeNode>newValue;
			let name: string = null;
			if (newNode.has("Name")) {
				name = <string>newNode.get("Name").value;
				if (!stringIsNullOrEmpty(name)) {
					if (this.shouldNamespaceSystemRefValues) {
						let containerId_o: string = "";
						if (this._namespaceLookupHandlers != null && this._namespaceLookupHandlers.count > 0) {
							for (let i = 0; i < this._namespaceLookupHandlers.count; i++) {
								containerId_o = this._namespaceLookupHandlers._inner[i](container);
							}
						}
						if (!stringIsNullOrEmpty(containerId_o)) {
							name = containerId_o + "/" + name;
						}
					}
					if (this._systemValues.containsKey(name)) {
						let existingValue = this._systemValues.item(name);
						obj = existingValue;
					}
				}
			}
			if (obj == null) {
				obj = this.context.createObject(type, container, name);
			}
		} else if (newValue != null) {
			obj = this.context.createObject(type, container, null);
		}
		if (newValue == null) {
			return obj;
		}
		if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, newValue) !== null) {
			let node = <DescriptionTreeNode>newValue;
			if (node.type == "EmbeddedRef") {
				let refType_o: string = "uuid";
				let value: string = null;
				for (let i1 = 0; i1 < node.items().count; i1++) {
					if (node.items()._inner[i1].propertyName == "RefType") {
						refType_o = <string>node.items()._inner[i1].value;
					}
					if (node.items()._inner[i1].propertyName == "Value") {
						value = <string>node.items()._inner[i1].value;
					}
				}
				if (value != null) {
					for (let i2 = 0; i2 < this._lookupHandlers.count; i2++) {
						let retVal = this._lookupHandlers._inner[i2](container, refType_o, value);
						if (retVal != null) {
							return retVal;
						}
					}
					if (refType_o == "uuid") {
						for (let key of fromEnum<string>(this._itemMaps.keys)) {
							if (this._itemMaps.item(key).containsKey(value)) {
								return this._itemMaps.item(key).item(value);
							}
						}
					}
					if (refType_o == "name") {
						let refName = value;
						if (this._systemValues.containsKey(refName)) {
							return this._systemValues.item(refName);
						}
					}
					return null;
				}
			} else {
				if (!this._states.containsKey(container)) {
					this._states.addItem(container, ((() => {
						let $ret = new ContainerState();
						$ret.container = container;
						return $ret;
					})()));
					state = this._states.item(container);
				}
				state.addComponent(container, node.id, obj, node, runOn(this, this.provideRefValueCore), parentId);
				let actions = this.toActions(node);
				this.processActionsImmediate(container, state, actions);
				if (skipTracking) {
					state.removeComponent(this, container, node.id, runOn(this, this.removeRefValueCore));
				}
			}
		}
		let isObject = (type == "object" || type == "Object");
		let isArrayObject = isObject && typeof newValue === 'string' && stringStartsWith(newValue.toString(), "[") && stringEndsWith(newValue.toString(), "]");
		if (isArrayObject) {
			let newVal_ = newValue;
			let res = JSON.parse(newVal_);
			return res;
		}
		if (!isObject && typeof newValue === 'string' && type.toLowerCase() != "string") {
			return this.coerceToEnum(type, <string>newValue, metadata);
		}
		if (isObject) {
			return newValue;
		}
		return obj;
	}
	private isPrimitiveType(type: string): boolean {
		return type == "int" || type == "Int32" || type == "short" || type == "Int16" || type == "double" || type == "Double" || type == "float" || type == "Float" || type == "Single" || type == "single" || type == "DateTime" || type == "decimal" || type == "Decimal" || type == "long" || type == "Int64" || type == "byte" || type == "bool" || type == "string" || type == "String";
	}
	private coerceToEnum(type: string, newValue: string, metadata: TypeDescriptionMetadata): any {
		return this.context.coerceToEnum(type, newValue, metadata, this.allowEnumIntCoerce());
	}
	protected allowEnumIntCoerce(): boolean {
		return true;
	}
	private toActions(newValue: DescriptionTreeNode): List$1<DescriptionTreeAction> {
		let items = newValue.items();
		let ret = new List$1<DescriptionTreeAction>((<any>DescriptionTreeAction).$type, 0);
		for (let i = 0; i < items.count; i++) {
			let act = new DescriptionTreeAction();
			act.actionType = DescriptionTreeActionType.UpdateProperty;
			act.propertyName = items._inner[i].propertyName;
			act.propertyMetadata = items._inner[i].metadata;
			act.targetNode = newValue;
			act.currentNode = newValue;
			act.newValue = items._inner[i].value;
			if (act.propertyMetadata != null && act.propertyMetadata.mustBeFirst) {
				ret.insert(0, act);
			} else {
				ret.add(act);
			}
		}
		return ret;
	}
	private processClearItemsAction(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		if (action.targetNode == null) {
			return this.clearRootItems(container, state, action);
		} else {
			let target = this.getTarget(container, state, action);
			this.clearCollection(container, state, action, target);
		}
		return true;
	}
	private clearCollection(container: any, state: ContainerState, action: DescriptionTreeAction, target: any): void {
		let propertyName = action.getPlatformPropertyName(ComponentRenderer.platform);
		this.adapter.clearCollection(target, propertyName, action.propertyMetadata);
		if (action.oldValue != null && typeCast<any[]>(Array_$type, action.oldValue) !== null) {
			let arr = <any[]>action.oldValue;
			for (let i = 0; i < arr.length; i++) {
				let item = arr[i];
				if (item != null && typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, item) !== null) {
					let id = (<DescriptionTreeNode>item).id;
					if (id >= 0) {
						let subTarget = state.getComponent(id);
						if (subTarget != null) {
							this.destroyObject(container, subTarget, state);
						}
					}
				}
			}
		}
	}
	private setPropertyValue(target: any, propertyName: string, metadata: TypeDescriptionMetadata, value: any, oldValue: any, sourceRef: DescriptionRef): void {
		if (this._updatingHandlers.containsKey(propertyName)) {
			for (let h of fromEnum<(propertyName: string, target: any, newValue: any) => void>(this._updatingHandlers.item(propertyName))) {
				h(propertyName, target, value);
			}
		}
		this.adapter.setPropertyValue(target, propertyName, metadata, value, oldValue, sourceRef);
	}
	private getPropertyValue(target: any, propertyName: string): any {
		return this.adapter.getPropertyValue(target, propertyName);
	}
	private clearRootItems(container: any, state: ContainerState, action: DescriptionTreeAction): boolean {
		return this.clearContainer(container, state);
	}
	private clearContainer(container: any, state: ContainerState): boolean {
		let current = this.adapter.getRootObject(container);
		if (current != null) {
			this.destroyObject(container, current, state);
		}
		let finished: boolean = false;
		this.adapter.clearContainer(container, this.context, (resumeRequired: boolean) => {
			finished = true;
			if (resumeRequired) {
				this.resumeActions(container, state);
			}
		});
		return finished;
	}
	waitForFlush(cont: any, onFlush: () => void): void {
		if (this.isProceedOnErrorEnabled) {
			try {
				let call = ComponentRendererMethodHelper.call("flush").$return().asVoid();
				this.executeMethod(cont, call.build(), (o: string) => onFlush());
			}
			catch (e) {
				this.logError("error flushing container: " + e.toString());
				onFlush();
			}
		} else {
			let call1 = ComponentRendererMethodHelper.call("flush").$return().asVoid();
			this.executeMethod(cont, call1.build(), (o: string) => onFlush());
		}
	}
	registerFont(key: string, font: any): void {
		FontRegistry.instance.registerFont(key, font);
	}
	removeFont(key: string): void {
		FontRegistry.instance.removeFont(key);
	}
}

/**
 * @hidden 
 */
export class TypeDescriptionCleanups extends Base {
	static $t: Type = markType(TypeDescriptionCleanups, 'TypeDescriptionCleanups');
	private _cleanups: Dictionary$2<string, (arg1: any, arg2: any) => void> = new Dictionary$2<string, (arg1: any, arg2: any) => void>(String_$type, Delegate_$type, 0);
	private _renderer: ComponentRenderer = null;
	constructor(renderer: ComponentRenderer) {
		super();
		this._renderer = renderer;
		this._cleanups.item("Series", (c: any, s: any) => {
			let s_ = s;
			if (s_.removeAxes) s_.removeAxes();
		});
		this._cleanups.item("Axis", (c: any, a: any) => {
			let typeName = this.getTypeName(a);
			if (typeName != null) {
				let meta = this._renderer.context.getMetadata(typeName, "CrossingAxisRef");
				let id = this._renderer.getComponentId(c, a, this._renderer.getContainerState(c));
				if (meta != null && id >= 0) {
					let node = this._renderer.getOldNode(c, id);
					if (node != null && node.has("CrossingAxisRef")) {
						let oldValue = node.get("CrossingAxisRef").value;
						let prop = meta.getPlatformName(ComponentRenderer.platform);
						this._renderer.setPropertyOnTarget(c, this._renderer.getContainerState(c), "CrossingAxis", meta, null, oldValue, a);
					}
				}
			}
		});
		this._cleanups.item("DomainChart", (c: any, s: any) => {
			let s_ = s;
			if (s_.onDetach) s_.onDetach();
		});
		this._cleanups.item("DataChart", (c: any, s: any) => {
			let s_ = s;
			if (s_.hideToolTip) s_.hideToolTip();
		});
		this._cleanups.item("GeographicMap", (c: any, s: any) => {
			let s_ = s;
			if (s_.hideToolTip) s_.hideToolTip();
		});
	}
	cleanup(container: any, platform: TypeDescriptionPlatform, obj: any): void {
		if (obj == null) {
			return;
		}
		let typeName_o: string = "";
		let type_o: Type;
		let obj_ = obj;
		let impl_ = obj_.i;
		let hasImpl = <boolean>(impl_);
		if (hasImpl) {
			type_o = getInstanceType(impl_);
		} else {
			impl_ = obj_._chart || obj_._map || obj_._gauge;
			hasImpl = <boolean>(impl_);
			if (hasImpl) {
				type_o = getInstanceType(impl_);
			} else {
				return;
			}
		}
		while (type_o != null) {
			typeName_o = type_o.typeName;
			typeName_o = stringReplace(stringReplace(typeName_o, "Ultra", ""), "Xam", "");
			if (this._cleanups.containsKey(typeName_o)) {
				this._cleanups.item(typeName_o)(container, obj);
			}
			type_o = type_o.baseType;
			if (type_o != null && (type_o.typeName == "Object" || type_o.typeName == "Base")) {
				break;
			}
		}
		if (<boolean>(obj_.destroy)) {
			obj_.destroy();
		}
	}
	private getTypeName(obj: any): string {
		let type: Type;
		let obj_ = obj;
		let impl_ = obj_.i;
		let hasImpl = <boolean>(impl_);
		if (hasImpl) {
			type = getInstanceType(impl_);
		} else {
			impl_ = obj_._chart || obj_._map || obj_._gauge;
			hasImpl = <boolean>(impl_);
			if (hasImpl) {
				type = getInstanceType(impl_);
			} else {
				return null;
			}
		}
		let typeName = type.typeName;
		typeName = stringReplace(stringReplace(typeName, "Ultra", ""), "Xam", "");
		return typeName;
	}
}

/**
 * @hidden 
 */
export class ContainerState extends Base {
	static $t: Type = markType(ContainerState, 'ContainerState');
	private _container: any = null;
	get container(): any {
		return this._container;
	}
	set container(value: any) {
		this._container = value;
	}
	private _components: Dictionary$2<number, any> = new Dictionary$2<number, any>(Number_$type, (<any>Base).$type, 0);
	private _ids: Dictionary$2<any, number> = new Dictionary$2<any, number>((<any>Base).$type, Number_$type, 0);
	private _componentNames: Dictionary$2<number, string> = new Dictionary$2<number, string>(Number_$type, String_$type, 0);
	private _parentIds: Dictionary$2<number, HashSet$1<number>> = new Dictionary$2<number, HashSet$1<number>>(Number_$type, (<any>HashSet$1).$type.specialize(Number_$type), 0);
	private _children: Dictionary$2<number, List$1<number>> = new Dictionary$2<number, List$1<number>>(Number_$type, (<any>List$1).$type.specialize(Number_$type), 0);
	addComponent(container: any, id: number, component: any, fromNode: DescriptionTreeNode, provideRefValue: (arg1: any, arg2: string, arg3: any, arg4: boolean) => void, parentId: number): void {
		if (fromNode.has("Name")) {
			this._componentNames.item(id, <string>fromNode.get("Name").value);
			provideRefValue(container, this._componentNames.item(id), component, false);
		}
		this._components.item(id, component);
		this._ids.item(component, id);
		if (!this._parentIds.containsKey(id)) {
			this._parentIds.item(id, new HashSet$1<number>(Number_$type, 0));
		}
		this._parentIds.item(id).add_1(parentId);
		if (parentId >= 0) {
			if (!this._children.containsKey(parentId)) {
				this._children.item(parentId, new List$1<number>(Number_$type, 0));
			}
			this._children.item(parentId).add(id);
		}
	}
	hasComponent(id: number): boolean {
		return this._components.containsKey(id);
	}
	hasComponentObject(component: any): boolean {
		return this._ids.containsKey(component);
	}
	getComponent(id: number): any {
		return this._components.item(id);
	}
	removeComponent(renderer: ComponentRenderer, container: any, id: number, removeRefValue: (arg1: any, arg2: string, arg3: boolean) => void): void {
		if (this._children.containsKey(id)) {
			let children = this._children.item(id);
			for (let i = 0; i < children.count; i++) {
				if (!this._parentIds.containsKey(children._inner[i]) || this._parentIds.item(children._inner[i]).count < 1 || (this._parentIds.item(children._inner[i]).count == 1 && this._parentIds.item(children._inner[i]).contains(id))) {
					if (this._components.containsKey(children._inner[i])) {
						renderer.destroyObject(container, this._components.item(children._inner[i]), this);
					}
				}
			}
			this._children.removeItem(id);
		}
		let component = this._components.item(id);
		this._ids.removeItem(component);
		this._components.removeItem(id);
		if (this._parentIds.containsKey(id)) {
			this._parentIds.removeItem(id);
		}
		if (this._componentNames.containsKey(id)) {
			let name = this._componentNames.item(id);
			this._componentNames.removeItem(id);
			removeRefValue(container, name, false);
		}
	}
	getComponentId(current: any): number {
		if (this._ids.containsKey(current)) {
			return this._ids.item(current);
		}
		return -1;
	}
}


