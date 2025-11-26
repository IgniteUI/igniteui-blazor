import { IComponentRendererAdapter } from "./IComponentRendererAdapter";
import { TypeDescriptionContext } from './TypeDescriptionContext';
import { TypeDescriptionMetadata } from './TypeDescriptionMetadata';
import { TypeDescriptionWellKnownType } from './TypeDescriptionWellKnownType';
import { IgCollection } from './IgCollection';
import { TypeRegistrar } from './type';
import { DescriptionRef } from './DescriptionRef';
import { Description } from './Description';
import { ComponentRendererSerializationHelper } from './ComponentRendererSerializationHelper';
import { IgcComponentRendererContainerComponent } from './igc-component-renderer-container-component';


export class ComponentRendererAdapter implements IComponentRendererAdapter {
    isBlazorRenderer: boolean = false;

    private _eventCache: Map<any, Map<string, any>>;

    createObject(type: string, container: any, context: TypeDescriptionContext, nameContext: string) {
        var isPlain = context.getMetadata(type, "__isPlainObject");
        if (isPlain) {
            return {};
        }

        var origName = type;
        var meta = context.getMetadata(origName, "__qualifiedNameTS");
        if (meta) {
            type = meta.specificExternalType;
        }

        var meta = context.getMetadata(origName, "__qualifiedNameBlazorTS");
        if (meta && this.isBlazorRenderer) {
            type = meta.specificExternalType;
        }

        var tagName: string = null;
        var meta = context.getMetadata(origName, "__tagNameWC");
        if (meta && this._platform === "Igc") {
            tagName = meta.specificExternalType;
        }

        var portalChildren = false;
        var meta = context.getMetadata(origName, "__portalChildrenWC");
        if (meta && this._platform === "Igc") {
            portalChildren = true;
        }

        if (tagName) {
            if (this._platform === "Igc") {
                let crc = (IgcComponentRendererContainerComponent as any).fromElement(container);
                return crc.createObject(tagName, container, context, portalChildren, nameContext);
            }
        }

        var mustManageInMarkup: boolean = false;
        var meta = context.getMetadata(origName, "__manageCollectionInMarkup");
        if (meta) {
            mustManageInMarkup = true;
        }
        var meta = context.getMetadata(origName, "__manageItemInMarkup");
        if (meta) {
            mustManageInMarkup = true;
        }
        var preferUnsuffixed = false
        var meta = context.getMetadata(origName, "__preferUnsuffixed");
        if (meta) {
            preferUnsuffixed = true;
        }

        if (mustManageInMarkup && this._platform == "Igx") {
            let crc = container as IgcComponentRendererContainerComponent;
            return crc.createObject(origName, container, context, false, nameContext);
        }


        let typeName = "Igc" + type + "Component";
        let shortTypeName = "Igc" + type;
        if (preferUnsuffixed) {
            if (TypeRegistrar.isRegistered(shortTypeName)) {
                return TypeRegistrar.create(shortTypeName);
            }
        }
        if (TypeRegistrar.isRegistered(typeName)) {
            return TypeRegistrar.create(typeName);
        }
        if (TypeRegistrar.isRegistered(shortTypeName)) {
            return TypeRegistrar.create(shortTypeName);
        }
        if (TypeRegistrar.isRegistered(type)) {
            return TypeRegistrar.create(type);
        }
    }

    private _platform = "Igc";

    //createRootObject(type: string, container: any, context: TypeDescriptionContext, continueActions: (resumeRequired: boolean) => void) {
    //    let typeName = "Igr" + type;
    //    if (TypeRegistrar.isRegistered(typeName)) {
    //        let t = TypeRegistrar.get(typeName);

    //        let crc = container as IgrComponentRendererContainer;
    //        crc.createRootObject(t, continueActions);
    //    }
    //}

    createColorCollection(colors: any[]) {
        return colors;
    }
    createBrushCollection(brushes: any[]) {
        return brushes;
    }
    createDoubleCollection(lengths: any[]) {
        return lengths;
    }
    coerceToEnum(type: string, context: TypeDescriptionContext, value: string) {
        return TypeDescriptionMetadata.camelize(value);
    }
    onUIThread(container: any, action: () => void): void {
        action();
    }
    setOrUpdateCollectionOnTarget(container: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, context: TypeDescriptionContext, target: any, value: any): void {
        let coll: any;
        if (this.mustManageInMarkup(target, propertyName, propertyMetadata, true)) {
            coll = this.getMarkupCollection(target, propertyName, propertyMetadata, true);
        } else {
            coll = this.getPropertyValue(target, propertyName);
        }
        if (coll instanceof IgCollection) {
            for (let i = 0; i < coll.count; i++) {
                if (coll.item(i) && coll.item(i)._implementation) {
                    coll.item(i).___parent = null;
                }
            }
            coll.clear();
            let newArr: any[] = value;
            for (let i = 0; i < newArr.length; i++) {
                if (newArr[i] && newArr[i]._implementation) {
                    newArr[i].___parent = target;
                }
            }
            for (let i = 0; i < newArr.length; i++) {
                coll.add(newArr[i]);
            }
        } else if (Array.isArray(coll) || coll == null || coll == undefined) {
            if (coll) {
                for (let i = 0; i < coll.length; i++) {
                    if (coll[i] && coll[i]._implementation) {
                        coll[i].___parent = null;
                    }
                }
            }
            let newArr: any[] = value;
            for (let i = 0; i < newArr.length; i++) {
                if (newArr[i] && newArr[i]._implementation) {
                    newArr[i].___parent = target;
                }
            }
            this.setPropertyValue(target, propertyName, propertyMetadata, value, coll, null)
        } else {
            if (coll.clear !== undefined) {
                if (coll.count && coll.item) {
                    for (let i = 0; i < coll.count; i++) {
                        if (coll.item(i) && coll.item(i)._implementation) {
                            coll.item(i).___parent = null;
                        }
                    }
                }
                coll.clear();
            }
            if (coll.add !== undefined) {
                let newArr: any[] = value;
                for (let i = 0; i < newArr.length; i++) {
                    if (newArr[i] && newArr[i]._implementation) {
                        newArr[i].___parent = target;
                    }
                }
                for (let i = 0; i < newArr.length; i++) {
                    coll.add(newArr[i]);
                }
            }
        }
    }
    onPendingRef(target: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, sourceRef: DescriptionRef): void {

    }

    ensureExternalObject(target: any, propertyMetadata: TypeDescriptionMetadata) {
        if (target && target._implementation) {
            // we are already an external type.
            return target;
        }

        let owningType = propertyMetadata.owningType;
        let context = propertyMetadata.owningContext;

        let typeName = "Igc" + owningType + "Component";
        var meta = context.getMetadata(owningType, "__qualifiedNameTS");
        if (meta) {
            typeName = "Igc" + meta.specificExternalType + "Component";
        }

        if (TypeRegistrar.isRegistered(typeName)) {
            return TypeRegistrar.create(typeName);
        }

        typeName = "Igc" + owningType;
        if (meta) {
            typeName = "Igc" + meta.specificExternalType;
        }


        if (TypeRegistrar.isRegistered(typeName)) {
            return TypeRegistrar.create(typeName);
        }

        return null;
    }

    setPropertyValue(target: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, value: any, oldValue: any, sourceRef: DescriptionRef): void {
        if (this._platform == "Igx" &&
            (IgcComponentRendererContainerComponent as any).isEvent &&
            (IgcComponentRendererContainerComponent as any).isEvent(target[propertyName])) {
            if (target["_" + propertyName + "Subscription"]) {
                if (target["_" + propertyName + "Subscription"].unsubscribe) {
                    target["_" + propertyName + "Subscription"].unsubscribe();
                } else {
                    target["_" + propertyName + "Subscription"]();
                }
                target["_" + propertyName + "Subscription"] = null;
            }

            if (value != null) {
                var fun = function (ev: any) {
                    if (ev.sender && ev.args) {
                        value(ev.sender, ev.args);
                    } else {
                        value(ev);
                    }
                }
                var sub = target[propertyName].subscribe(fun);
                target["_" + propertyName + "Subscription"] = sub;
            }
            return;
        }
        var fromScript = value && value.___fromScript;
        
        if (this._platform == "Igc" &&
            target.addEventListener &&
            propertyMetadata &&
            propertyMetadata.knownType == TypeDescriptionWellKnownType.EventRef &&
            propertyMetadata.isCustomEvent) {

            if (fromScript && this.isBlazorRenderer) {
                if (!target.externalObject) {
                    var ext = this.ensureExternalObject(target, propertyMetadata);
                    if (!ext) {
                        if (value) {
                            target.addEventListener(propertyName, value);
                        } else {
                            target.removeEventListener(propertyName, oldValue);
                        }
                        return;
                    }
                    if (ext !== target) {
                        target.externalObject = ext.i;
                        if (ext.i && !ext.i.nativeElement) {
                            ext.i.nativeElement = target;
                        }
                    }
                }
                let extObj = target.externalObject;
                if (target.externalObject.externalObject) {
                    extObj = target.externalObject.externalObject;
                }
                //extObj[propertyName] = value;
                extObj[propertyName + "Script"] = value.___fromScriptId;
            }
            
            if (propertyMetadata &&
                propertyMetadata.knownType == TypeDescriptionWellKnownType.EventRef &&
                propertyMetadata.isCustomEvent) {
                
                if (this.isBlazorRenderer && !fromScript) {
                    if (!target.externalObject) {
                        var ext = this.ensureExternalObject(target, propertyMetadata);
                        if (!ext) {
                            if (value) {
                                target.addEventListener(propertyName, value);
                            } else {
                                target.removeEventListener(propertyName, oldValue);
                            }
                            return;
                        }
                        if (ext !== target) {
                            target.externalObject = ext.i;
                            if (ext.i && !ext.i.nativeElement) {
                                ext.i.nativeElement = target;
                            }
                        }
                    }
                    let extObj = target.externalObject;
                    if (target.externalObject.externalObject) {
                        extObj = target.externalObject.externalObject;
                    }
                    extObj[propertyName] = value;
                } else {
                    if (oldValue) {
                        target.removeEventListener(propertyName, oldValue);
                    }
                    if (value) {
                        target.addEventListener(propertyName, value);
                    }
                }
                return;
            }
        }
        if (this._platform == "Igc" &&
            target.emitEvent &&
            target.addEventListener) {
            if (propertyMetadata &&
                propertyMetadata.knownType == TypeDescriptionWellKnownType.EventRef) {
                //TODO: this needs to come from the metadata.
                if (propertyName.endsWith("Ocurred")) {
                    propertyName = propertyName.replace("Ocurred", "");
                }
                if (propertyName.toLowerCase() == "selectionchanged") {
                    propertyName = "selection";
                }
                var eventId = propertyName;
                if (!propertyMetadata.skipWCEventPrefix) {
                    eventId = "igc" + TypeDescriptionMetadata.toPascal(propertyName);
                }
                if (oldValue) {
                    target.removeEventListener(eventId, oldValue);
                }
                if (value) {
                    target.addEventListener(eventId, value);
                }
                return;
            }
        }

        if (this._platform == "Igc" &&
            target.addEventListener &&
            this.isBlazorRenderer &&
            propertyMetadata &&
            propertyMetadata.knownType == TypeDescriptionWellKnownType.TemplateRef) {
            
            var fromScript = value && value.___fromScript;

            if (!fromScript) {
                if (!target.externalObject) {
                    var ext = this.ensureExternalObject(target, propertyMetadata);
                    if (!ext) {
                        target[propertyName] = value;
                        return;
                    }

                    if (ext !== target) {
                        target.externalObject = ext.i;

                        if (ext.i && !ext.i.nativeElement) {
                            ext.i.nativeElement = target;
                        }
                    }
                }
                let extObj = target.externalObject;
                if (target.externalObject.externalObject) {
                    extObj = target.externalObject.externalObject;
                }
                extObj[propertyName] = value;
                return;
            }
        }

        if (oldValue && oldValue._implementation) {
            oldValue.___parent = null;
        }
        if (value && value._implementation) {
            value.___parent = target;
        }

        if (propertyMetadata != null &&            
            !(target instanceof Description) &&
            this.mustManageInMarkup(target, propertyName, propertyMetadata, false)) {
            let coll = this.getMarkupCollection(target, propertyName, propertyMetadata, false);
            if (value) {
                coll.add(value);
            } else {
                coll.clear();
            }
            return;
        }

        target[propertyName] = value;
    }
    getPropertyValue(target: any, propertyName: string) {
        return target[propertyName];
    }
    clearContainer(container: any, context: TypeDescriptionContext, continueActions: (resumeRequired: boolean) => void): void {
        if (this._platform === "Igc") {
            let crc = (IgcComponentRendererContainerComponent as any).fromElement(container);
            crc.clearContainer(continueActions);
            return;
        }
        let crc = container as IgcComponentRendererContainerComponent;
        crc.clearContainer(continueActions);
    }
    getRootObject(container: any) {
        if (this._platform === "Igc") {
            let crc = (IgcComponentRendererContainerComponent as any).fromElement(container);
            return crc.getRootObject();
        }
        let crc = container as IgcComponentRendererContainerComponent;

        return crc.getRootObject();
    }
    forPropertyValueItem(target: any, propertyName: string, forItem: (item) => void) {
        let coll = this.getPropertyValue(target, propertyName);
        if (coll instanceof IgCollection) {
            for (let i = 0; i < coll.count; i++) {
                forItem(coll.item(i));
            }
        } else if (Array.isArray(coll)) {
            for (let i = 0; i < coll.length; i++) {
                forItem(coll[i]);
            }
        } else {
            if (coll.clear !== undefined) {
                for (let i = 0; i < coll.count; i++) {
                    forItem(coll.item(i));
                }
            }
        }
    }
    getMarkupTypeMatcher(target: any, propertyName: string, metadata: TypeDescriptionMetadata, isCollection: boolean): ((f: any) => boolean) | null {
        if (this._platform === "Igc" || this._platform === "Igx") {
            if ((isCollection && metadata.knownType !== TypeDescriptionWellKnownType.Collection) ||
                (!isCollection && metadata.knownType !== TypeDescriptionWellKnownType.ExportedType)) {
                return null;
            }
            if (isCollection) {
                let type = metadata.collectionElementType;
                let origName = type;
                var meta = metadata.owningContext.getMetadata(type, "__qualifiedNameTS");
                if (meta) {
                    type = meta.specificExternalType;
                }
                var tagName: string = null;
                meta = metadata.owningContext.getMetadata(origName, "__tagNameWC");
                if (meta && this._platform === "Igc") {
                    tagName = meta.specificExternalType;

                    if (tagName) {
                        if (window && window.customElements && window.customElements.get) {
                            let tags = [];
                            let tt = window.customElements.get(tagName);
                            if (tt) {
                                tags.push(tt);
                            }
                            var subMeta = metadata.owningContext.getMetadata(origName, "__tagNameWCSubstitutable");
                            if (subMeta) {
                                let subs = subMeta.specificExternalType;
                                let subParts = subs.split(',');
                                for (var j = 0; j < subParts.length; j++) {
                                    let subPart = subParts[j];
                                    let subT = window.customElements.get(subPart);
                                    if (subT) {
                                        tags.push(subT)
                                    }
                                }
                            }

                            return (o) => {
                                if (tags.length == 0) {
                                    return false;
                                }
                                for (let i = 0; i < tags.length; i++) {
                                    if (o instanceof tags[i]) {
                                        return true;
                                    }
                                }
                                return false;
                            }
                        }
                    }
                }
                
                let typeName = "Igc" + type + "Component";
                if (TypeRegistrar.isRegistered(typeName)) {
                    let t = TypeRegistrar.get(typeName);
                    return (o) => o instanceof t;
                }
                let shortTypeName = "Igc" + type;
                if (TypeRegistrar.isRegistered(shortTypeName)) {
                    let t = TypeRegistrar.get(shortTypeName);
                    return (o) => o instanceof t;
                }
                if (TypeRegistrar.isRegistered(type)) {
                    let t = TypeRegistrar.get(type);
                    return (o) => o instanceof t;
                }
            } else {
                var type = metadata.specificExternalType;
                let origName = type;
                var meta = metadata.owningContext.getMetadata(type, "__qualifiedNameTS");
                if (meta) {
                    type = meta.specificExternalType;
                }
                var tagName: string = null;
                meta = metadata.owningContext.getMetadata(origName, "__tagNameWC");
                if (meta && this._platform === "Igc") {
                    tagName = meta.specificExternalType;

                    if (tagName) {
                        if (window && window.customElements && window.customElements.get) {
                            let tags = [];
                            let tt = window.customElements.get(tagName);
                            if (tt) {
                                tags.push(tt);
                            }
                            var subMeta = metadata.owningContext.getMetadata(origName, "__tagNameWCSubstitutable");
                            if (subMeta) {
                                let subs = subMeta.specificExternalType;
                                let subParts = subs.split(',');
                                for (var j = 0; j < subParts.length; j++) {
                                    let subPart = subParts[j];
                                    let subT = window.customElements.get(subPart);
                                    if (subT) {
                                        tags.push(subT)
                                    }
                                }
                            }

                            return (o) => {
                                if (tags.length == 0) {
                                    return false;
                                }
                                for (let i = 0; i < tags.length; i++) {
                                    if (o instanceof tags[i]) {
                                        return true;
                                    }
                                }
                                return false;
                            }
                        }
                    }
                }

                let typeName = "Igc" + type + "Component";
                if (TypeRegistrar.isRegistered(typeName)) {
                    let t = TypeRegistrar.get(typeName);
                    return (o) => o instanceof t;
                }
                let shortTypeName = "Igc" + type;
                if (TypeRegistrar.isRegistered(shortTypeName)) {
                    let t = TypeRegistrar.get(shortTypeName);
                    return (o) => o instanceof t;
                }
                if (TypeRegistrar.isRegistered(type)) {
                    let t = TypeRegistrar.get(type);
                    return (o) => o instanceof t;
                }
            }
        }
        return null;
    }
    mustManageInMarkup(target: any, propertyName: string, metadata: TypeDescriptionMetadata, isCollection: boolean) {
        if (this._platform === "Igc" || this._platform === "Igx") {
            if ((isCollection && metadata.knownType !== TypeDescriptionWellKnownType.Collection) ||
                (!isCollection && metadata.knownType !== TypeDescriptionWellKnownType.ExportedType)) {
                return false;
            }
            if (isCollection) {
                var ele = metadata.collectionElementType;
                var needsMarkup = metadata.owningContext.getMetadata(ele, "__manageCollectionInMarkup");
                if (needsMarkup) {
                    return true;
                }
            } else {
                var ele = metadata.specificExternalType;
                if (ele) {
                    var needsMarkup = metadata.owningContext.getMetadata(ele, "__manageItemInMarkup");
                    if (needsMarkup) {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    getMarkupCollection(target: any, propertyName: string, metadata: TypeDescriptionMetadata, isCollection: boolean): MarkupCollection {
        if (this._platform == "Igx") {
            return new ngMarkupCollection(target, this._platform, this.getMarkupTypeMatcher(target, propertyName, metadata, isCollection), metadata);
        } else {
            return new HtmlMarkupCollection(target, this._platform, this.getMarkupTypeMatcher(target, propertyName, metadata, isCollection));
        }
    }
    clearCollection(target: any, propertyName: string, metadata: TypeDescriptionMetadata): void {
        let coll: any;
        if (this.mustManageInMarkup(target, propertyName, metadata, true)) {
            coll = this.getMarkupCollection(target, propertyName, metadata, true);
        } else {
            coll = this.getPropertyValue(target, propertyName);
        }
        if (coll instanceof IgCollection) {
            for (let i = 0; i < coll.count; i++) {
                if (coll.item(i) && coll.item(i)._implementation) {
                    coll.item(i).___parent = null;
                }
            }
            coll.clear();
        } else if (Array.isArray(coll)) {
            for (let i = 0; i < coll.length; i++) {
                if (coll[i] && coll[i]._implementation) {
                    coll[i].___parent = null;
                }
            }
            this.setPropertyValue(target, propertyName, metadata, [], coll, null)
        } else {
            if (coll.clear !== undefined) {
                for (let i = 0; i < coll.count; i++) {
                    if (coll.item(i) && coll.item(i)._implementation) {
                        coll.item(i).___parent = null;
                    }
                }
                coll.clear();
            }
        }
    }
    addItemToCollection(propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any, newIndex: number, item: any): void {
        let coll: any;
        if (this.mustManageInMarkup(target, propertyName, propertyMetadata, true)) {
            coll = this.getMarkupCollection(target, propertyName, propertyMetadata, true);
        } else {
            coll = this.getPropertyValue(target, propertyName);
        }

        if (item && item._implementation) {
            item.___parent = target;
        }
        if (coll instanceof IgCollection) {
            coll.insert(newIndex, item);
        } else if (Array.isArray(coll)) {
            let newArr = [];
            for (let i = 0; i < coll.length; i++) {
                newArr[i] = coll[i];
            }
            newArr.splice(newIndex, 0, item);
            this.setPropertyValue(target, propertyName, propertyMetadata, newArr, coll, null)
        } else {
            if (coll.insert !== undefined) {
                coll.insert(newIndex, item);
            }
        }
    }
    resetPropertyOnTarget(container: any, propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any): void {
        //TODO: anything we can do here? store default?
    }
    replaceItemInCollection(propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any, newIndex: number, item: any): void {
        let coll: any;
        if (this.mustManageInMarkup(target, propertyName, propertyMetadata, true)) {
            coll = this.getMarkupCollection(target, propertyName, propertyMetadata, true);
        } else {
            coll = this.getPropertyValue(target, propertyName);
        }

        if (item && item._implementation) {
            item.___parent = target;
        }
        if (coll instanceof IgCollection) {
            let oldValue = coll.item(newIndex);
            if (oldValue && oldValue._implementation) {
                oldValue.___parent = null;
            }
            coll.item(newIndex, item);
        } else if (Array.isArray(coll)) {
            let newArr = [];
            for (let i = 0; i < coll.length; i++) {
                newArr[i] = coll[i];
            }
            let oldValue = newArr[newIndex];
            if (oldValue && oldValue._implementation) {
                oldValue.___parent = null;
            }
            newArr[newIndex] = item;
            this.setPropertyValue(target, propertyName, propertyMetadata, newArr, coll, null)
        } else {
            if (coll.item !== undefined) {
                let oldValue = coll.item(newIndex);
                if (oldValue && oldValue._implementation) {
                    oldValue.___parent = null;
                }
                coll.item(newIndex, item);
            }
        }
    }
    removeItemFromCollection(propertyName: string, propertyMetadata: TypeDescriptionMetadata, target: any, oldIndex: number): void {
        let coll: any;
        if (this.mustManageInMarkup(target, propertyName, propertyMetadata, true)) {
            coll = this.getMarkupCollection(target, propertyName, propertyMetadata, true);
        } else {
            coll = this.getPropertyValue(target, propertyName);
        }

        if (coll instanceof IgCollection) {
            let oldValue = coll.item(oldIndex);
            if (oldValue && oldValue._implementation) {
                oldValue.___parent = null;
            }
            coll.removeAt(oldIndex);
        } else if (Array.isArray(coll)) {
            let newArr = [];
            for (let i = 0; i < coll.length; i++) {
                newArr[i] = coll[i];
            }
            let oldValue = newArr[oldIndex];
            if (oldValue && oldValue._implementation) {
                oldValue.___parent = null;
            }
            newArr.splice(oldIndex, 1);
            this.setPropertyValue(target, propertyName, propertyMetadata, newArr, coll, null)
        } else {
            if (coll.removeAt !== undefined) {
                let oldValue = coll.item(oldIndex);
                if (oldValue && oldValue._implementation) {
                    oldValue.___parent = null;
                }
                coll.removeAt(oldIndex);
            }
        }
    }
    replaceRootItem(container: any, type: string, context: TypeDescriptionContext, continueActions: (resumeRequired: boolean) => void): void {
        let typeName = "Igc" + type + "Component";
        let deferAttach = false;
        var meta = context.getMetadata(type, "__qualifiedNameTS");
        if (meta) {
            typeName = "Igc" + meta.specificExternalType + "Component";
        }
        meta = context.getMetadata(type, "__deferAttachWC");
        if (meta && this._platform === "Igc") {
            deferAttach = true;
        }


        var tagName: string = null;
        var meta = context.getMetadata(type, "__tagNameWC");
        if (meta && this._platform === "Igc") {
            tagName = meta.specificExternalType;
        }

        if (tagName) {
            if (this._platform === "Igc") {
                let crc = (IgcComponentRendererContainerComponent as any).fromElement(container);
                crc.replaceRootItem(tagName, deferAttach, continueActions);
                return;
            }
        }

        if (TypeRegistrar.isRegistered(typeName)) {
            let t = TypeRegistrar.get(typeName);

            if (this._platform === "Igc") {
                let crc = (IgcComponentRendererContainerComponent as any).fromElement(container);
                crc.replaceRootItem(t, deferAttach, continueActions);
                return;
            }

            let crc = container as IgcComponentRendererContainerComponent;
            crc.replaceRootItem(t, deferAttach, continueActions);
        } else {

            if (this._platform === "Igc") {
                let crc = (IgcComponentRendererContainerComponent as any).fromElement(container);
                crc.clearContainer(continueActions);
                return;
            }

            let crc = container as IgcComponentRendererContainerComponent;
            crc.clearContainer(continueActions);
        }
    }
    removeRootItem(container: any, context: TypeDescriptionContext, continueActions: (resumeRequired: boolean) => void): void {
        this.clearContainer(container, context, continueActions);
    }
    flushChanges(container: any): void {

    }

    publicCollectionAsObjectArray(coll: any): any[] {
        if (coll instanceof IgCollection) {
            let ret = [];
            for (let i = 0; i < coll.count; i++) {
                ret.push(coll.item(i));
            }
            return ret;
        } else if (Array.isArray(coll)) {
            let ret = []
            for (let i = 0; i < coll.length; i++) {
                ret[i] = coll[i];
            }
            return ret;
        } else {
            if (coll.clear !== undefined) {
                let ret = [];
                for (let i = 0; i < coll.count; i++) {
                    ret[i] = coll.item(i);
                }
                return ret;
            }
        }
        return null;
    }

    setHandler(target: any, eventName: string, propertyMetadata: TypeDescriptionMetadata, argsType: string, context: TypeDescriptionContext, callback: (args: any) => void): void {
        var evtName = TypeDescriptionMetadata.camelize(eventName);
        let oldValue = null;
        if (!this._eventCache) {
            this._eventCache = new Map<any, Map<string, any>>();
        }
        if (this._eventCache.has(target) &&
            this._eventCache.get(target).has(evtName)) {
            oldValue = this._eventCache.get(target).get(evtName);
            this._eventCache.get(target).delete(evtName);
            if (this._eventCache.get(target).size == 0) {
                this._eventCache.delete(target);
            }
        }

        if (callback) {
            var handler = (sender, args) => {
                callback(args);
            };
            if (!this._eventCache.has(target)) {
                this._eventCache.set(target, new Map<string, any>());
            }
            this._eventCache.get(target).set(evtName, handler);
            this.setPropertyValue(target, evtName, propertyMetadata, handler, oldValue, null);
        } else {
            this.setPropertyValue(target, evtName, propertyMetadata, null, oldValue, null);
        }
    }

    executeMethod(target: any, methodName: string, argumentValues_: any[], argumentMetadata: TypeDescriptionMetadata[], onFinished: (res: any) => void)
    {
        var methodName = TypeDescriptionMetadata.camelize(methodName);
        var methInfo_ = (target as any)[methodName];

        let ret: any = null;
        if (methInfo_ != null)
        {
            ret = methInfo_.apply(target, argumentValues_);
        }

        onFinished(ret);
    }

    serializeBrush(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeBrush(value);
    }

    serializeColor(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeColor(value);
    }

    serializeBrushCollection(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeBrushCollection(value);
    }

    serializePoint(value: any): any
    {
        return ComponentRendererSerializationHelper.serializePoint(value);
    }

    serializePixelPoint(value: any): any {
        return ComponentRendererSerializationHelper.serializePoint(value);
    }

    serializeSize(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeSize(value);
    }

    serializePixelSize(value: any): any {
        return ComponentRendererSerializationHelper.serializeSize(value);
    }

    serializeRect(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeRect(value);
    }
    serializePixelRect(value: any): any {
        return ComponentRendererSerializationHelper.serializeRect(value);
    }

    serializeColorCollection(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeColorCollection(value);
    }

    serializeTimespan(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeTimespan(value);
    }

    serializeDoubleCollection(value: any): any
    {
        return ComponentRendererSerializationHelper.serializeDoubleCollection(value);
    }

    createHandler(eventName: string, eventHandlerTypeName: string, eventArgsTypeName: string, context: TypeDescriptionContext, callback: (arg1: any, arg2: any) => void): any {
        return callback;
    }
    disposeHandler(callback: (arg1: any, arg2: any) => void) {

    }

}



abstract class MarkupCollection {
    public constructor(target: any, platform: string) {

    }
    public abstract clear();
    public abstract item(index: number, value?: any | undefined): any;
    public abstract removeAt(index: number): void;
    public abstract insert(index: number, item: any): void;
    public abstract add(item: any): void;
}

//TODO: this could be more efficient if it tracked the child list changes and maintained a current view rather than repolling
class ngMarkupCollection extends MarkupCollection {
    private getItems(): any[] {
        return this._target["___listContent" + this._propertyName];
    }
    public get count() {
        return this.getItems().length;
    }
    public clear() {
        let items = this.getItems();
        items.length = 0;
        this.notifyList();
    }
    public item(index: number, value?: any) {
        let items = this.getItems();
        items[index] = value;
        this.notifyList();
    }
    public removeAt(index: number): void {
        let items = this.getItems();
        items.splice(index, 1);
        this.notifyList();
    }
    public insert(index: number, item: any): void {
        let items = this.getItems();
        items.splice(index, 0, item);
        this.notifyList();
    }
    public add(item: any): void {
        let items = this.getItems();
        items.push(item);
        this.notifyList();
    }
    private notifyList() {
        let items = this.getItems();
        let qList = this._target[this._queryListName];
        qList.reset([...items]);
        qList.notifyOnChanges();
    }
    public constructor(target: any, platform: string, typeMatcher: (t: any) => boolean, metadata: TypeDescriptionMetadata) {
        super(target, platform);

        this._target = target;

        if (metadata.owningContext.hasQueryListName(metadata.owningType, metadata.propertyName)) {
            let queryListName = (metadata.owningContext.getQueryListName(metadata.owningType, metadata.propertyName));
            this._queryListName = queryListName;
        }
        this._componentRef = target.___owningRef;
        this._viewContainerRef = target.___viewContainerRef;
        this._propertyName = metadata.propertyName;

        if (!this._target["___listContent" + metadata.propertyName]) {
            this._target["___listContent" + metadata.propertyName] = [];
        }
    }

    private _target: any;
    private _propertyName: string;
    private _queryListName: string;
    private _componentRef: any;
    private _viewContainerRef: any;
}

//TODO: this could be more efficient if it tracked the child list changes and maintained a current view rather than repolling
class HtmlMarkupCollection extends MarkupCollection {
    public get count() {
        let items = Array.from(this._target.children).map((v, i) => { return { item: v, index: i } }).filter((i) => this._typeMatcher(i.item));
        return items.length;
    }
    public clear() {
        let items = Array.from(this._target.children).map((v, i) => { return { item: v, index: i } }).filter((i) => this._typeMatcher(i.item));
        for (var i = 0; i < items.length; i++) {
            items[i].item.remove();
        }
    }
    public item(index: number, value?: any): any | null {
        let items = Array.from(this._target.children).map((v, i) => { return { item: v, index: i } }).filter((i) => this._typeMatcher(i.item));
        if (value) {
            if (index > items.length - 1 || index < 0) {
                return value;
            }
            items[index].item.replaceWith(value);
            return value;
        } else {
            if (index > items.length - 1 || index < 0) {
                return null;
            }
            return items[index].item;
        }
    }
    public removeAt(index: number): void {
        let items = Array.from(this._target.children).map((v, i) => { return { item: v, index: i } }).filter((i) => this._typeMatcher(i.item));
        if (index > items.length - 1 || index < 0) {
            return;
        }
        let removeItem = items[index];
        removeItem.item.remove();
    }
    public insert(index: number, item: any): void {
        let items = Array.from(this._target.children).map((v, i) => { return { item: v, index: i } }).filter((i) => this._typeMatcher(i.item));
        if (items.length == 0) {
            this._target.append(item);
        } else {
            if (items.length - 1 < index) {
                let insertAfter2 = items[items.length - 1].item as HTMLElement;
                insertAfter2.after(item);
                return;
            }
            let insertAfter = items[index].item as HTMLElement;
            insertAfter.after(item);
        }
    }
    public add(item: any): void {
        let items = Array.from(this._target.children).map((v, i) => { return { item: v, index: i } }).filter((i) => this._typeMatcher(i.item));
        if (items.length == 0) {
            this._target.append(item);
        } else {
            let insertAfter = items[items.length - 1].item as HTMLElement;
            insertAfter.after(item);
        }
    }
    public constructor(target: any, platform: string, typeMatcher: (t: any) => boolean) {
        super(target, platform);

        this._target = target as HTMLElement;
        this._typeMatcher = typeMatcher;
    }
    private _target: HTMLElement;
    private _typeMatcher: (t: any) => boolean;
}