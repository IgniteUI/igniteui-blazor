import './public_path';

import { ComponentRenderer } from 'igniteui-core/ComponentRenderer';
import { TypeRegistrar, createMutationObserver } from 'igniteui-core/type';
import { fromSpinal, getAllPropertyNames } from 'igniteui-core/componentUtil';
import { dateMinValue } from 'igniteui-core/date';
import { Loader } from './Loader';
import { html, noChange } from 'lit-html';
import { IgcPortalModule } from 'igniteui-core/igc-portal';
import { refValues, itemMaps } from './refs-state';

IgcPortalModule.register();

(window as any).igTemplating = {
  html: html
}

let cr = new ComponentRenderer()
ComponentRenderer.defaultInstance = cr;
(cr.adapter as any).isBlazorRenderer = true;

const DATA_IG_ID_ATTRIBUTE = "data-ig-id";
let containers: Map<string, HTMLElement> = new Map<string, HTMLElement>();
let containersDirect: Map<string, boolean> = new Map<string, boolean>();
let containersPendingRefs: Map<string, (() => void)[]> = new Map<string, (() => void)[]>();
let containersPendingDataRefs: Map<string, (() => void)[]> = new Map<string, (() => void)[]>();
function getContainer(id: string): HTMLElement {
  let cont = containers.get(id);
    if (!cont) {
        return null;
    }
  if (!containersDirect.has(id)) {
    if (cont.tagName.toUpperCase() == "IGC-COMPONENT-RENDERER-CONTAINER") {
      containersDirect.set(id, false);
    } else {
      containersDirect.set(id, true);
    }
  }
  return cont;
}
function getContainerId(container) {
  // return container.id;
  return getContainerIgIdAttribute(container);
}
function getContainerByIgIdAttribute(id) {
  return document.querySelector(`[${DATA_IG_ID_ATTRIBUTE}='${id}']`);
}
cr.addNamespaceLookupListener(getContainerId);
cr.shouldNamespaceSystemRefValues = true;


cr.addReferenceLookupListener((container, refType, value) => {
  if (refType == "uuid") {
    var retVal = null;
    itemMaps.forEach((v, k, map) => {
      if (v.has(value)) {
        retVal = v.get(value);
        return false;
      }
    });
    return retVal;
  } else {
    findByName(container.children[0], value);
  }
});

cr.addCleanupListener((container, refId) => {
  if (refValues && refValues.has(refId)) {
    refValues.delete(refId);
  }
  if (itemMaps && itemMaps.has(refId)) {
    itemMaps.delete(refId);
  }
});

let refDataIntents: Map<string, any[]> = new Map<string, any[]>();
let directEvents: Map<string, any[]> = new Map<string, any[]>();
let currentContainerName: string = null;
let eventBehaviors: Map<string, string> = new Map<string, string>();
let isSyncMethodInvoke: boolean = false;

var isDotNet = true;
async function callDotNet(webCallback, methodName: string, ...args: any[]): Promise<any> {
    if (isDotNet) {
        return await webCallback.invokeMethodAsync(methodName, currentContainerName, ...args);
    }
}

(window as any).igWaitForLoaded = async function waitForLoaded() {
  if (Loader.instance.isLoading) {
    await Loader.instance.loadingPromise;
  } else {
    return;
  }
};

(window as any).igRequestLoad = function requestLoad(module: string) {
  Loader.instance.request(module, cr);
};

(window as any).igSetResourceString = function setResourceString(type: string, grouping: string, id: string, value: string) {
  switch (type) {
    case "set":
      Loader.instance.setResourceString(grouping, id, value);
      break;
    case "register":
      Loader.instance.registerResource(grouping, JSON.parse(value));
      break;
  }
};

function currentContainer() {
  return getContainer(currentContainerName);
}

function isContainerDirectRender(): boolean {
  if (!containersDirect.has(currentContainerName)) {
    return false;
  }
  return containersDirect.get(currentContainerName);
}

function getMainTarget() {
  let cont = currentContainer();
  
  if (!containersDirect.get(currentContainerName)) {
    return cont.children[0];
  } else {
    return cont;
  }
}

function copyProperties(target: any, source: any) {
  if (target !== source) {
    let keys = Object.keys(source);
    for (let i = 0; i < keys.length; i++) {
      let key = keys[i];
      if (target[key] !== undefined) {
        target[key] = source[key];
      }
    }
  }
}

function findByName(node: any, name: string) {
  let root = node;
  if (name == "mainControl") {
    return root;
  }

  root = ensureExternalObject(root);

  //TODO: this should be more general
  if (root.series) {
    for (let i = 0; i < root.series.count; i++) {
      let child = root.series.item(i);
      if ((child as any).name == name) {
        return child;
      }
      var sub = findByName(child, name);
      if (sub) {
        return sub;
      }
    }
  }
  if (root.axes) {
    for (let i = 0; i < root.axes.count; i++) {
      let child = root.axes.item(i);
      if ((child as any).name == name) {
        return child;
      }
      var sub = findByName(child, name);
      if (sub) {
        return sub;
      }
    }
  }
  if (root.dataSource && root.dataSource.name) {
    if (root.dataSource.name == name) {
      return root.dataSource;
    }
  }
  if (root.shapefileDataSource && root.shapefileDataSource.name) {
    if (root.shapefileDataSource.name == name) {
      return root.shapefileDataSource;
    }
  }
  if (root.findByName && root.findByName(name)) {
    return root.findByName(name);
  }
  if (root.children && root.children.length) {
    for (let i = 0; i < root.children.length; i++) {
      let child = root.children[i];
      if ((child as any).name == name) {
        return child;
      }
      findByName(child, name);
    }
  }
  return null;
}

let convertReturnValue = function(retVal: any): any {
  // if (Loader.isMarshalByValue(retVal)) {
  //   retVal = Loader.stringify(retVal);
  // } else {
  //   if (retVal && retVal.___id) {
  //     retVal = { refType: "uuid", id: retVal.___id };
  //   } else if (retVal == currentContainer().children[0]) {
  //     retVal = { refType: "name", id: "mainControl" };
  //   } else if (retVal && (retVal._styling || retVal._implementation) && retVal.name) {
  //     retVal = { refType: "name", id: retVal.name };
  //   } else {
  //     retVal = {
  //       retType: typeof(retVal),
  //       value: retVal
  //     }
  //     retVal = JSON.stringify(retVal);
  //   }
    // }

  if (typeof(retVal) == "number") {
    return JSON.stringify({ retType: "number", value: retVal});
  } else if (typeof(retVal) == "string") {
    return JSON.stringify({ retType: "string", value: retVal});
  } else if (typeof(retVal) == "boolean") {
    return JSON.stringify({ retType: "boolean", value: retVal});
  } else if (retVal instanceof Date) {
    if (retVal.getTime() === dateMinValue().getTime()) {
      retVal = "0001-01-01T00:00:00.000Z";
    }
    return JSON.stringify({ retType: "date", value: retVal});
  } 

  if (retVal == null) {
    return JSON.stringify({ retType: "object", type: "", value: null});
  }

  return Loader.stringify(retVal, getMainTarget());
};

let toReturn = function(retVal: any): any {
  if (typeof(retVal) == "number") {
    return { retType: "number", value: retVal};
  } else if (typeof(retVal) == "string") {
    return { retType: "string", value: retVal};
  } else if (typeof(retVal) == "boolean") {
    return { retType: "boolean", value: retVal};
  } else if (retVal instanceof Date) {
    return { retType: "date", value: retVal};
  }

  if (retVal == null) {
    return { retType: "object", type: "", value: null};
  }

  return JSON.parse(Loader.stringify(retVal, getMainTarget()));
};

(window as any).igConvertReturnValue = convertReturnValue;

function toSimpleArgs(args: CustomEvent) {
  let orig = args;

  class SimpleCustomEvent {
    detail: any = {};
  };
  
  let newArgs = new SimpleCustomEvent();

  //HACK: we need to fix the stringify to include non prototypal keys. But I'm not doing that right now.
  if (typeof(args.detail) == "object" && args.detail !== null && !args.detail.tagName) {
    args.detail.___cloned = true;
  }
  newArgs.detail = typeof(args.detail) == "object" ? toReturn(args.detail) : args.detail;

  return newArgs;
}

function markupCustomArgs(origArgs, args) {
  if (origArgs instanceof CustomEvent) {
    if (origArgs.target && origArgs.target instanceof HTMLElement) {
      let ele = origArgs.target as HTMLElement;
      if (ele.tagName.toLowerCase() == "igc-combo") {
        if (origArgs.type == "igcChange") {
          args.detail.type = "WebComboChangeEventArgsDetail"
        }
      }
    }
  }
}

function flattenArgs(args, forceSimple?) {
  if (args === null || args === undefined) {
    return null;
  }
  if (!(typeof(args) == "object")) {
    args = toReturn(args);
    return args;
  }

  if (args instanceof CustomEvent || forceSimple) {
    let origArgs = args;
    args = toSimpleArgs(args);
    markupCustomArgs(origArgs, args);
    return args;
  }

  let outerArgs = convertReturnValue(args);
  return outerArgs;
  // let outerArgs = {};
  // for (let name of getAllPropertyNames(args)) {
  //   if (name == "constructor" || name == "i" || name.indexOf("_") == 0 ||
  //   typeof args[name] == "function") {
  //     continue;
  //   }
  //   outerArgs[name] = convertReturnValue(args[name])
  // }
  // return outerArgs;
}

function expandDateColumns(item: any, source: any, itemIndex: number = 0, startIndex: number = 0) {
  if (Array.isArray(item)) {
      if (source.__dateColumnsCache) {
        let cache = source.__dateColumnsCache.columns[itemIndex];
        (item as any).__dateColumnsCache = {};
        (item as any).__dateColumnsCache.columns = cache;
      }
      
      for (let i = 0; i < item.length; i++) {
        expandDateColumns(item[i], item);
      }
      return;
  }

  if (source.__dateColumnsCache) {
      for (let i = startIndex; i < source.__dateColumnsCache.columns.length; i++) {
          let propPath = source.__dateColumnsCache.columns[i];
          expandDateColumn(item, propPath);
      }
  }
}

function expandDateColumn(item: any, path: string) {
  if (Array.isArray(item)) {
    for (let i = 0; i < item.length; i++) {
      expandDateColumn(item[i], path);
    }
  } else {
    if (path.includes(".")) {
      let propName = path.split(".")[0];
      if (propName.includes("[]")) {
        let arr = item[propName.replace("[]", "")];
        expandDateColumn(arr, path.replace(`${propName}.`, ""));
      } else {
        expandDateColumn(item[propName], path.replace(`${propName}.`, ""));
      }
    } else {
      if (item[path]) {
        item[path] = new Date(item[path].substring(3));
      }
    }
  }
}

let dynamicContentBatch: any[] = [];
let dynamicContentBatchPending = false;
var adjustDynamicContent = function (containerId: string, contentType: string, templateId: string, contentId: string, actionType: string, webCallback, args: any) {
  updateContainer(containerId);
  dynamicContentBatch.push({
    containerId: containerId,
    contentType: contentType,
    templateId: templateId,
    contentId: contentId,
    actionType: actionType,
    args: flattenArgs(args)
  });

  if (dynamicContentBatchPending) {
    return;
  }
  dynamicContentBatchPending = true;
  
  window.setTimeout(() => {
    dynamicContentBatchPending = false;
    var arg = JSON.stringify(dynamicContentBatch);
    dynamicContentBatch.length = 0;
    currentContainerName = containerId;
    callDotNet(webCallback, "AdjustDynamicContentBatch", arg);
  }, 0);
};

function getContainerIgIdAttribute(container: HTMLElement) {
  if (!container.getAttribute) {
    return null;
  }
  const igKey = container.getAttribute(DATA_IG_ID_ATTRIBUTE);
  if (!igKey) {
    return null;
  }
  if (igKey.trim().length === 0) {
    return null;
  }
  return igKey;
}

function raiseEventImpl(propertyName: string, sender: any, args: any, containerName: any, webCallback) {
  let name: string = "mainControl";
  let cont = sender;

  if (!args && (sender instanceof CustomEvent || sender instanceof UIEvent)) {
     args = sender;
     updateContainer(containerName);
     sender = getMainTarget();
     cont = getContainerByIgIdAttribute(containerName);
  }

  let isWrapper = !(sender instanceof HTMLElement) && sender.i && sender.i.nativeElement;
  if (isWrapper) {
    updateContainer(containerName);
    var mainTarget = getMainTarget();
    if (mainTarget !== isWrapper && mainTarget.contains && mainTarget.contains(isWrapper)) {
      sender = isWrapper;
      cont = null;
    } else {
      sender = mainTarget;
      cont = getContainerByIgIdAttribute(containerName);
    }
  }

  //while (cont != null && (!cont.id || !containers.has(cont.id))) {
  while (cont != null && (!getContainerIgIdAttribute(cont) || !containers.has(getContainerIgIdAttribute(cont)))) {
    if (cont.parentElement) {
      cont = cont.parentElement;
    } else if (cont.___parent) {
      cont = cont.___parent;
    } else {
      cont = null;
    }
  }
  if (cont) {
    // currentContainerName = cont.id;
    currentContainerName = getContainerIgIdAttribute(cont);
  }
  if (sender != getMainTarget()) {
    name = sender.name;
  }
  sender = convertReturnValue(sender);
  let outerArgs: any;

  if (isWrapper && args.detail) {
    outerArgs = { detail: toReturn(args.detail) };
  } else {
    outerArgs = flattenArgs(args, args instanceof UIEvent);
  }
  
  Loader.clearMarshalIdByValueOnceByEvent(propertyName);

    try
    {
      (window as any).raisingEvent = true;

      callDotNet(webCallback, "OnRaiseEvent", name, propertyName, JSON.stringify({ sender: sender, args: outerArgs }));
      if ((window as any).webViewCallback) {
        ((window as any).webViewCallback).onRaiseEvent(name, propertyName, JSON.stringify({ sender: sender, args: outerArgs }));
      }
      if ((window as any).webkit && (window as any).webkit.messageHandlers && (window as any).webkit.messageHandlers.raiseEvent) {
        (window as any).webkit.messageHandlers.raiseEvent.postMessage({ name: name, propertyName: propertyName, sender: sender, args: outerArgs });
      }
    } finally {
      (window as any).raisingEvent = false;
  }
}

var raiseEvent = function (propertyName: string, sender: any, args: any, containerName: any, webCallback) {
  let resolvedBehavior = "queued";
  if (eventBehaviors.has(containerName)) {
    resolvedBehavior = eventBehaviors.get(containerName);
  }
  
  if (isSyncMethodInvoke || resolvedBehavior === "queued") {
    window.setTimeout(() => {
      raiseEventImpl(propertyName, sender, args, containerName, webCallback);
    }, 0);
  } else {
    raiseEventImpl(propertyName, sender, args, containerName, webCallback);
  }
};

let igScripts: Map<string, { shouldCall: boolean, func: Function }> = new Map<string, { shouldCall: boolean, func: Function }>();
(window as any).igRegisterScript = function (scriptId: string, script: Function, shouldCall: boolean = true) {
  igScripts.set(scriptId, { shouldCall: shouldCall, func: script });
};
(window as any).igRemoveScript = function (scriptId: string) {
  igScripts.delete(scriptId);
};

(window as any).igCheckReady = function (containerId: string) {
  // var cont = document.getElementById(containerId);
  var cont = getContainerByIgIdAttribute(containerId);
  return (cont !== null && cont !== undefined);
};

(window as any).igSendMessages = function(json: string) {
  let m = JSON.parse(json);
  for (let i = 0; i < m.length; i++) {
    (window as any).sendMessage(JSON.stringify(m[i]));
  }
};

var propMaps = new WeakMap<any, Set<string>>();
function getPropMap(target: any) {
  if (!target) {
    return null;
  }
  var cons = target.constructor;
  if (!cons) {
    return null;
  }
  if (!propMaps.has(cons)) {
    var propSet = new Set<string>();
    var props = getAllPropertyNames(target);
    for (var i = 0; i < props.length; i++) {
      propSet.add(props[i]);
    }
    propMaps.set(cons, propSet);
    return propSet;
  } else {
    return propMaps.get(cons);
  }
}

function hasProp(target: any, prop: string) {
  var map = getPropMap(target);
  if (!map) {
    return false;
  }
  return map.has(prop);
}

function updateContainer(containerId: string) {
  currentContainerName = containerId;
  if (!containers.has(containerId) || containers.get(containerId) == null) {
    // let cont = document.getElementById(containerId);
    let cont = getContainerByIgIdAttribute(containerId) as HTMLElement;

    if (cont && !(<any>cont).__disposed) {
      containers.set(containerId, cont);

      if (containersPendingDataRefs.has(containerId)) {
        let arr = containersPendingDataRefs.get(containerId);
        containersPendingDataRefs.delete(containerId);
        for (let j = 0; j < arr.length; j++) {
          arr[j]();
        }
      }
    }
  }
}

function processDataIntentsHelper(refValue: any, dataIntents: any) {
  if (dataIntents && dataIntents.subProps) {
    if (refValue.length > 0 && refValue[0]) {
      if (Array.isArray(refValue[0])) {
        for (var i = 0; i < refValue.length; i++) {
          processDataIntentsHelper(refValue[i], dataIntents.subIntents);
        }
      } else {
        processDataIntentsHelper(refValue[0], dataIntents.subIntents);
      }
    }
    return;
  }

  let intent = {};
  for (var key of Object.keys(dataIntents)) {
    if (dataIntents[key] && dataIntents[key].subProps) {
        if (refValue.length > 0 && refValue[0][key]) {
          processDataIntentsHelper(refValue[0][key], dataIntents[key].subIntents);
        }
    } else {
      intent[key] = dataIntents[key];
    }
  }
  refValue.__dataIntents = intent;
}

function processDataIntents(refName: string, refValue: any) {
  if (refDataIntents.has(refName)) {
    let dataIntents = refDataIntents.get(refName);
    if (refValue) {
      processDataIntentsHelper(refValue, dataIntents);
      refDataIntents.delete(refName);
    }
  }
}

function ensureExternalObject(target: any) {
  if (target && target.externalObject) {
    let ext = target.externalObject;
    //for ng grid, we may need to step out 2 levels to reach the outer interface.
    if (ext.externalObject) {
      ext = ext.externalObject;
    }
    return ext;
  }

  if (target && target._implementation) {
      // we are already an external type.
      return target;
  }

  if (!target || !target.tagName) {
    return target;
  }

  let typeNameWithoutComponent = fromSpinal(target.tagName.toLowerCase());
  let typeName = typeNameWithoutComponent + "Component";
  
  if (TypeRegistrar.isRegistered(typeName)) {
      let ret = TypeRegistrar.create(typeName);
      target.externlObject = ret;
      if (ret.setNativeElement) {
        ret.setNativeElement(target);
      }
      if (ret.provideImplementation) {
        ret.provideImplementation(target);
      } else {
        ret._implementation = target;
      }
      return ret;
  }

  typeName = typeNameWithoutComponent;
  
  if (TypeRegistrar.isRegistered(typeName)) {
      let ret = TypeRegistrar.create(typeName);
      target.externlObject = ret;
      if (ret.setNativeElement) {
        ret.setNativeElement(target);
      }
      if (ret.provideImplementation) {
        ret.provideImplementation(target);
      }
      return ret;
  }

  return target;
}

(window as any).igSendMessage = function (containerId: string, json: string, webCallback: any, nativeElements: any[]) {
  updateContainer(containerId);
  let m = JSON.parse(json);
  
  switch (m.type) {
    case "description":
      let desc = m.description;
      let ms = { descriptions: {"root": desc } };
      cr.loadJson(JSON.stringify(ms), (c) => currentContainer());
      if (cr.hasErrors()) {
        let errors = cr.getErrors();
        cr.clearErrors();
        for (let ei = 0; ei < errors.length; ei++) {
          let error = errors[ei];
          console.error(error);
        }
      }
      if (containersPendingRefs.has(containerId)) {
        let arr = containersPendingRefs.get(containerId);
        containersPendingRefs.delete(containerId);
        for (let j = 0; j < arr.length; j++) {
          arr[j]();
        }
      }
      break;
    case "cleanup":
      var container = currentContainer();
      if (container != null) {
        if (isContainerDirectRender()) {
          let toRemove = [];
          for (const [key, value] of directEvents) {
            if (key.startsWith(containerId)) {
              toRemove.push(key);

              container.removeEventListener(value[0], value[1]);
              cr.removeRefValue(container, key);
            }
          }
          for (const key of toRemove) {
            directEvents.delete(key);
            refValues.delete(key);
          }
          toRemove = null;
        }

        cr.cleanup(currentContainer(), true);
        if (cr.hasErrors()) {
          let errors = cr.getErrors();
          cr.clearErrors();
          for (let ei = 0; ei < errors.length; ei++) {
            let error = errors[ei];
            console.error(error);
          }
        }

        if (containers.has(containerId)) {
          (<any>container).__disposed = true;
          containers.delete(containerId);
        }

        if (eventBehaviors.has(containerId)) {
          eventBehaviors.delete(containerId);
        }
      }
      break;
    case "descriptionDelta":
        let descDelta = m.description;
        let skipApply = m.skipApply;
        let msDelta = { descriptions: {"root": descDelta } };

        cr.loadJsonDelta(JSON.stringify(msDelta), (c) => currentContainer(), skipApply);
        if (cr.hasErrors()) {
          let errors = cr.getErrors();
          cr.clearErrors();
          for (let ei = 0; ei < errors.length; ei++) {
            let error = errors[ei];
            console.error(error);
          }
        }

        if (containersPendingRefs.has(containerId)) {
          let arr = containersPendingRefs.get(containerId);
          containersPendingRefs.delete(containerId);
          for (let j = 0; j < arr.length; j++) {
            arr[j]();
          }
        }
        break;
    case "refChanged":
      {
        let refName = m.refName;
        const originalRefVal = m.refValue;
        let refValue = m.refValue;
        
        if (typeof(refValue) == "string" &&
            refValue.indexOf("json:::") == 0) {
            refValue = refValue.substring("json:::".length);
            
            fetch(refValue).then((d) => {
              d.json().then(dj => {
                refValue = dj;
                cr.provideRefValue(currentContainer(), refName, refValue);
                refValues.set(refName, refValue);

                for (let i = 0; i < refValue.length; i++) {
                  refValue[i].___localJson = true;
                }
              });
            })
        } else {
          if (typeof(refValue) == "string" &&
            refValue.indexOf("containerId:::") == 0) {
              refValue = refValue.substring("containerId:::".length);
              if (containers.has(refValue) && containers.get(refValue).children.length > 0) {
                refValue = containers.get(refValue).children[0];
              } else {
                if (!containersPendingRefs.has(refValue)) {
                  containersPendingRefs.set(refValue, []);
                }
                let arr = containersPendingRefs.get(refValue);
                let cc = currentContainer();
                arr.push(() => {
                  refValue = containers.get(refValue).children[0];
                  cr.provideRefValue(cc, refName, refValue);
                  refValues.set(refName, refValue);
                });
                return;
              }
          }
          if (typeof(refValue) == "string" &&
              refValue.indexOf("literalJson:::") == 0) {
              refValue = refValue.substring("literalJson:::".length);
              refValue = JSON.parse(refValue);
          }
          if (typeof(refValue) == "string" &&
              refValue.indexOf("localJson:::") == 0) {
              refValue = refValue.substring("localJson:::".length);
              refValue = JSON.parse(refValue);

              for (let i = 0; i < refValue.length; i++) {
                refValue[i].___localJson = true;
              }
          }
          if (typeof(refValue) == "string" &&
              refValue.indexOf("script:::") == 0) {
              refValue = refValue.substring("script:::".length);
              let scriptRef = refValue;
              if (!igScripts.has(refValue)) {
                return;
              }
              var f = igScripts.get(refValue);
              if (f.shouldCall && typeof(f.func) == "function") {
                refValue = f.func();
              } else {
                refValue = f.func;
              }
              if (refValue &&
                typeof(refValue) == "function") {
                  (refValue as any).___fromScript = true;
                  (refValue as any).___fromScriptId = scriptRef;

                  if (getMainTarget() != null &&
                      isContainerDirectRender()) {
                      // attached and raises client-side event for direct render components
                      let target = getMainTarget();
                      let isNativeEvent = originalRefVal.indexOf("nativeEvent:::") == 0;
                      const refs = refName.split("/");
                      let actualEvent = refs[refs.length - 1];
                      if (actualEvent.endsWith("Ocurred")) {
                          actualEvent = actualEvent.replace("Ocurred", "");
                      }
                      if (actualEvent.toLowerCase() == "selectionchanged") {
                          actualEvent = "Selection";
                      }
                      let directEvent = isNativeEvent ? actualEvent.toLowerCase() : "igc" + actualEvent;
                      if (directEvents.has(refName)) {
                          target.removeEventListener(directEvent, directEvents.get(refName)[1])
                      }
                      target.addEventListener(directEvent, refValue);
                      directEvents.set(refName, [directEvent, refValue]);
                  }
              }
          }
          if (
            refValue == null &&
            getMainTarget() != null &&
            isContainerDirectRender()) {
            if (directEvents.has(refName)) {
              let target = getMainTarget();
              target.removeEventListener(directEvents.get(refName)[0], directEvents.get(refName)[1]);
              directEvents.delete(refName);
            }
            }

          if (typeof(refValue) == "string" &&
              (refValue.indexOf("event:::") == 0 ||
              refValue.indexOf("nativeEvent:::") == 0)) {
              let isNativeEvent = refValue.indexOf("nativeEvent:::") == 0;
              refValue = isNativeEvent ?
                refValue.substring("nativeEvent:::".length):
                refValue.substring("event:::".length);
              var eventName = refValue;
              if (getMainTarget() != null &&
                  isContainerDirectRender()) {
                // attached and raises server-side event for direct render components
                let target = getMainTarget();
                refValue = function (args) {
                  raiseEvent(eventName, target, isNativeEvent ? null : args, containerId, webCallback);
                }
              } else {
                let target = getMainTarget();
                refValue = function (sender, args) {
                    raiseEvent(eventName, sender, isNativeEvent ? null : args, containerId, webCallback);
                }
              }

              if (getMainTarget() != null &&
                isContainerDirectRender()) {
                let actualEvent = eventName;
                if (eventName.endsWith("Ocurred")) {
                  actualEvent = actualEvent.replace("Ocurred", "");
                  //TODO: this must come from metadata.
                }
                if (eventName.toLowerCase() == "selectionchanged") {
                  actualEvent = "Selection";
                }
                let directEvent = isNativeEvent ? actualEvent.toLowerCase() : "igc" + actualEvent;
                let target = getMainTarget();

                // 28717 - .net8 exposed a bug we had where if you assign a handler in Blazor and then assign a different
                // handler we don't clean up the first handler. Our Blazor API doesn't allow multiple event handlers attached
                // to a single event so we need to cleanup the previous handler if exists.
                if (directEvents.has(refName)) {
                  target.removeEventListener(directEvent, directEvents.get(refName)[1])
                }

                target.addEventListener(directEvent, refValue);
                directEvents.set(refName, [directEvent, refValue]);
              }
              if (m.eventBehavior) {
                eventBehaviors.set(containerId, m.eventBehavior);
              }
          }
          if (typeof(refValue) == "string" &&
            refValue.indexOf("template:::") == 0) {
              refValue = refValue.substring("template:::".length);
              if (refValues.has(refName) && refValues.get(refName).__templateId == refValue) {
                refValue = refValues.get(refName);
              } else {

                var currTemplate = null;
                var templateId = refValue;
                refValue = function (context) {
                  if (currTemplate.___container.__disposed) {
                    return noChange;
                  }
                  if (!context) {
                    return html`<div></div>`;
                  }
                  let contentId = context.___contentId;
                  if (context.___immediate || 
                    (context.i && context.i.___immediate) ||
                    (context.i && context.i.nativeElement && context.i.nativeElement.___immediate)) {
                      let innerContext = context;
                      if (!contentId && context.i && context.i.___contentId) {
                        contentId = context.i.___contentId;
                        innerContext = context.i;
                      }
                      if (!contentId && context.i && context.i.nativeElement && context.i.nativeElement.___contentId) {
                        contentId = context.i.nativeElement.___contentId;
                        innerContext = context.i.nativeElement;
                      }
                      var template = currTemplate;
                      if (!template.___currentContextMap) {
                        template.___currentContextMap = new Map<string, any>();
                      }
                      let currentContext: any = null;
                      if (template.___currentContextMap.has(contentId)) {
                        currentContext = template.___currentContextMap.get(contentId);
                      }
                      const hasImplicit = Object.getPrototypeOf(context).hasOwnProperty('implicit');
                      if (hasImplicit || currentContext !== innerContext) {
                        template.___currentContextMap.set(contentId, innerContext);
                        adjustDynamicContent(template.___containerId,
                          "TemplateContent",
                          template.___templateId,
                          contentId,
                          "Update",
                          webCallback,
                          context);
                      }
                      if (innerContext.___root) {
                        let root = innerContext.___root;
                        if (!root.___host) {
                          var host = root.querySelector("#" + 'host-' + contentId);
                          if (host) {
                            root.___host = host;
                            template.___checkHost(template, root, root.___host);
                          }
                        } else {
                          if (root.___host) {
                            template.___checkHost(template, root, root.___host);
                          }
                        }
                      }
                  }
                  return html`<div id="${'host-' + contentId}">
                  </div>`;
                };
                currTemplate = refValue;
                refValue.___isBridged = true;
                refValue.___templateId = templateId;
                refValue.___containerId = currentContainerName;
                refValue.___container = currentContainer();
                refValue.___onTemplateInit = (template, templateContent) => {
                  let mut = createMutationObserver((list) => {
                      for (var mutation of list) {
                          if (mutation.type == 'childList') {
                              var host = templateContent.querySelector("#" + 'host-' + templateContent._id);
                              templateContent.___host = host;
                              if (template.___checkHost(template, templateContent, templateContent.___host)) {
                                mut.disconnect();
                                mut = null;
                                if (mut2) {
                                  mut2.disconnect();
                                  mut2 = null;
                                }
                              }
                              break;
                          }
                      }
                  });
                  mut.observe(templateContent, {
                      childList: true
                  });
                  var dynCont = template.___container.parentElement.querySelector(".ig-dynamic-content-holder");
                  let mut2 = createMutationObserver((list) => {
                      for (var mutation of list) {
                          if (mutation.type == 'childList') {
                              var host = templateContent.querySelector("#" + 'host-' + templateContent._id);
                              templateContent.___host = host;
                              if (template.___checkHost(template, templateContent, templateContent.___host)) {
                                mut2.disconnect();
                                mut2 = null;
                                if (mut) {
                                  mut.disconnect();
                                  mut = null;
                                }
                              }
                              break;
                          }
                      }
                  });
                  mut2.observe(dynCont, {
                      childList: true
                  });
                  adjustDynamicContent(template.___containerId,
                    "TemplateContent",
                    template.___templateId,
                    templateContent._id,
                      "Add",
                      webCallback,
                      null);
                };
                refValue.___onTemplateTeardown = (template, templateContent) => {
                  adjustDynamicContent(template.___containerId,
                    "TemplateContent",
                    template.___templateId,
                    templateContent._id,
                      "Remove",
                      webCallback,
                      null);
                };
                refValue.___checkHost = (template, templateContent, host) => {
                  var content = document.getElementById(templateContent._id);
                  if (content && host) {
                    if (content.parentElement != host) {
                      if (host.id.replace("host-", "") != content.id) {
                        console.log("error!");
                      }
                      host.appendChild(content);
                      return true;
                    }
                  }
                  return false;
                }
                refValue.___onTemplateContextChanged = (template, templateContent, context) => {
                  if (templateContent.___host) {
                    template.___checkHost(template, templateContent, templateContent.___host);
                  }
                  adjustDynamicContent(template.___containerId,
                    "TemplateContent",
                    template.___templateId,
                    templateContent._id,
                      "Update",
                      webCallback,
                      context);
                };
            }
          }
          if (Array.isArray(refValue)) {
            itemMaps.set(refName, new Map<string, any>());
            let map = itemMaps.get(refName);

            if (m.dateCache && !(refValue as any).__dateColumnsCache) {
              (refValue as any).__dateColumnsCache = {};
              (refValue as any).__dateColumnsCache.columns = m.dateCache;
            }
            
            for (let i = 0; i < refValue.length; i++) {
              let item = refValue[i];
              expandDateColumns(item, refValue, i);
              if (item.___id) {
                map.set(item.___id, item);
              }
            }
          }

          if (m.dataIntents) {
            refDataIntents.set(refName, m.dataIntents);
            processDataIntents(refName, refValue);            
          }

          cr.provideRefValue(currentContainer(), refName, refValue);
          refValues.set(refName, refValue);
        }
      }
      break;
    case "invokeMethod":
      {
        try
        {
          
        let methodName = m.methodName;
        let target = m.target;
        let invokeId = m.invokeId;
        let args = m.arguments;
        let types = m.types;

        for (let i = 0; i < args.length; i++) {
          let t = types[i];
          if (t == "Date") {
            args[i] = new Date(args[i]);
          }
          if (t == "DateArray") {
            let dates = [];
            for (let j = 0; j < args[i].length; j++) {
              dates.push(new Date(args[i][j]));
            }
            args[i] = dates;
          }
          if (t == "Number") {
            if (args[i] == null) {
              args[i] = NaN;
            }
          }
          if (t == "NumberArray") {
            for (let j = 0; j < args[i].length; j++) {
              if (args[i][j] == null) {
                args[i][j] = NaN;
              }
            }
          }
          if (t == "Json") {
            let currArr = args[i];
            if (Array.isArray(currArr) && currArr.length > 0 && currArr[0].___byValue) {
              for (let x = 0; x < currArr.length; x++) {
                  currArr[x] = cr.createObjectFromJson(JSON.stringify(currArr[x]), currentContainer());
              }
            }
            else if (args[i] && args[i].___byValue) {
              
                args[i] = cr.createObjectFromJson(JSON.stringify(args[i]), currentContainer());

                if (!args[i].i) {
                  args[i].i = args[i];
                }
            } else if (args[i] && args[i].refType) {
              if (args[i].refType == "uuid") {
                itemMaps.forEach((value: any, key: string) => {
                  if (value.has(args[i].id)) {
                    args[i] = value.get(args[i].id);
                  }
                })
              } else if (args[i].refType == "name") {
                args[i] = findByName(getMainTarget(), args[i].id);
              }
            }
          }
          if (t == "Component") {
            if (args[i] != null) {
              if (args[i].indexOf("containerId:::") == 0) {
                args[i] = args[i].substring("containerId:::".length);
                if (containers.has(args[i])) {
                  if (containersDirect.has(args[i])) {
                    args[i] = containers.get(args[i]);
                  } else {
                    args[i] = containers.get(args[i]).children[0];
                  }
                } else {
                  var ele = getContainerByIgIdAttribute(args[i]);
                  if (ele) {
                    args[i] = ele;
                  } 
                }
              } else {
                if (args[i].indexOf("elementIndex:::") == 0) {
                  args[i] = args[i].substring("elementIndex:::".length);
                  args[i] = parseInt(args[i]);
                  args[i] = nativeElements[i];
                }
              }
            }
          }
        }

        let child: any = getMainTarget();
        if (target) {
          child = findByName(child, target);
        }
        let retVal: any = null;

        child = ensureExternalObject(child);

        var isPropGet = false;
        if (methodName.indexOf("p:") == 0) {
          isPropGet = true;
          methodName = methodName.substring(2);
          methodName = methodName.substr(0, 1).toLowerCase() + (methodName).substring(1);
        }

        if (!hasProp(child, methodName)) {
          if (methodName.endsWith("Component")) {
            methodName = methodName.substr(0, methodName.length - "Component".length);
          } else if (methodName.startsWith("perform")) {
            methodName = methodName.substr(7);
          }

          if (!hasProp(child, methodName)) {
            let error = "error: target doesn't have prop: " + methodName;
            console.error(error)
            return error;
          }
        }

        if (isPropGet) {
            retVal = child[methodName];
        } else {
          if (child[methodName]) {
            if (m.isSync) {
              isSyncMethodInvoke = true;
            }
            retVal = child[methodName].apply(child, args);
            isSyncMethodInvoke = false;
          }
            }

        if (Object.prototype.toString.call(retVal) === '[object Promise]') {
            (retVal as any).then((value) => {
              window.setTimeout(() => {
                  //reset container id, because this is async and something else might have send a message in the meantime
                  updateContainer(containerId);
                callDotNet(webCallback, "OnInvokeReturn", invokeId, convertReturnValue(value));
              }, 0);
            });
            return JSON.stringify({ retType: "promise" });
        }
        retVal = convertReturnValue(retVal);

        //callDotNet("OnInvokeReturn", invokeId, retVal);
        if ((window as any).webViewCallback) {
          ((window as any).webViewCallback).onInvokeReturn(invokeId, retVal);
        }
        if ((window as any).webkit && (window as any).webkit.messageHandlers && (window as any).webkit.messageHandlers.onInvokeReturn) {
          (window as any).webkit.messageHandlers.onInvokeReturn.postMessage({ invokeId: invokeId, retVal: retVal });
        }
          return retVal;
        } catch (error) {
          console.error(error);

          //callDotNet("OnInvokeReturn", m.invokeId, "error: " + error);
          if ((window as any).webViewCallback) {
            ((window as any).webViewCallback).onInvokeReturn(m.invokeId, "error: " + error);
          }
          if ((window as any).webkit && (window as any).webkit.messageHandlers && (window as any).webkit.messageHandlers.onInvokeReturn) {
            (window as any).webkit.messageHandlers.onInvokeReturn.postMessage({ invokeId: m.invokeId, retVal: "error: " + error });
          }
          return "error: " + error;
        }
      }
    case "refNotifyInsertItem":
      {
        let refName = m.refName;
        let index: number = m.index;
        let newItem = m.newItem;
        let refValue: any[] = refValues.get(refName);
        refValue.splice(index, 0, newItem);
        let child: any = getMainTarget();

        if (m.dateCache && !(refValue as any).__dateColumnsCache) {
          (refValue as any).__dateColumnsCache = {};
          (refValue as any).__dateColumnsCache.columns = m.dateCache;
        }

        expandDateColumns(newItem, refValue);
        if (itemMaps.has(refName)) {
          let map = itemMaps.get(refName);
          if (newItem.___id) {
            map.set(newItem.___id, newItem);
          }
        }
        if (child.notifyInsertItem) {
          if (child.tagName && child.tagName == "IGC-DATA-GRID") {
            child.notifyInsertItem(index, newItem);
          } else {
            child.notifyInsertItem(refValue, index, newItem);
          }
        } else if (child.markForCheck) {
          child.markForCheck();
        }
      }
      break;
    case "refNotifyRemoveItem":
        {
          let refName = m.refName;
          let index: number = m.index;
          
          let refValue: any[] = refValues.get(refName);
          let oldItem = refValue[index];
          refValue.splice(index, 1);
          let child: any = getMainTarget();
          if (itemMaps.has(refName)) {
            let map = itemMaps.get(refName);
            if (oldItem.___id) {
              map.delete(oldItem.___id);
            }
          }
          if (child.notifyRemoveItem) {
            if (child.tagName && child.tagName == "IGC-DATA-GRID") {
              child.notifyRemoveItem(index, oldItem);
            } else {
              child.notifyRemoveItem(refValue, index, oldItem);
            }
          } else if (child.markForCheck) {
            child.markForCheck();
          }
        }
        break;
    case "refClearItems":
        {
          let refName = m.refName;
          let index: number = m.index;
          let newItem = m.newItem;
          let newRefValue = m.refValue;
          
          let refValue: any[] = refValues.get(refName);
          if (itemMaps.has(refName)) {
            let map = itemMaps.get(refName);
            for (let i = 0; i < refValue.length; i++) {
              let child = refValue[i];
              if (child.___id) {
                map.delete(child.___id);
              }
            }
          }
          refValue.length = 0;

          if (m.dateCache && !(refValue as any).__dateColumnsCache) {
            (refValue as any).__dateColumnsCache = {};
            (refValue as any).__dateColumnsCache.columns = m.dateCache;
          }

          for (let i = 0; i < newRefValue.length; i++) {
            refValue[i] = newRefValue[i];
            expandDateColumns(newRefValue[i], refValue);
            if (itemMaps.has(refName)) {
              let map = itemMaps.get(refName);
              if (refValue[i].___id) {
                map.set(refValue[i].___id, refValue[i]);
              }
            }
          }
          let child: any = getMainTarget();
          if (child.notifyClearItems) {
            if (child.tagName && child.tagName == "IGC-DATA-GRID") {
              child.notifyClearItems();
            } else {
              child.notifyClearItems(refValue);
            }
          } else if (child.markForCheck) {
            child.markForCheck();
          }
        }
        break;
    case "refNotifySetItem":
        {
          let refName = m.refName;
          let index: number = m.index;
          let oldItem = m.oldItem;
          if (itemMaps.has(refName)) {
            let map = itemMaps.get(refName);
            if (oldItem.___id) {
              map.delete(oldItem.___id);
            }
          }
          let newItem = m.newItem;
          let refValue: any[] = refValues.get(refName);

          if (m.dateCache && !(refValue as any).__dateColumnsCache) {
            (refValue as any).__dateColumnsCache = {};
            (refValue as any).__dateColumnsCache.columns = m.dateCache;
          }

          expandDateColumns(newItem, refValue);
          refValue[index] = newItem;
          let child: any = getMainTarget();
          if (itemMaps.has(refName)) {
            let map = itemMaps.get(refName);
            if (child.___id) {
              map.set(child.___id, child);
            }
          }
          if (child.notifySetItem) {
            if (child.tagName && child.tagName == "IGC-DATA-GRID") {
              child.notifySetItem(index, oldItem, newItem);
            } else {
              child.notifySetItem(refValue, index, oldItem, newItem);
            }
          } else if (child.markForCheck) {
            child.markForCheck();
          }
        }
        break;
    case "refNotifyUpdateItem":
        {
          let refName = m.refName;
          let index: number = m.index;
          let syncDataonly = m.syncDataonly;
          
          let newItem = m.item;
          
          let refValue: any[] = refValues.get(refName);
          let oldItem = refValue[index];

          if (m.dateCache && !(refValue as any).__dateColumnsCache) {
            (refValue as any).__dateColumnsCache = {};
            (refValue as any).__dateColumnsCache.columns = m.dateCache;
          }

          expandDateColumns(newItem, refValue);
          copyProperties(oldItem, newItem);
          
          let child: any = getMainTarget();
          if (child.notifySetItem && !syncDataonly) {
            if (child.tagName && child.tagName == "IGC-DATA-GRID") {
              child.notifySetItem(index, oldItem, oldItem);
            } else {
              child.notifySetItem(refValue, index, oldItem, oldItem);
            }
          } else if (child.markForCheck) {
            child.markForCheck();
          }
        }
        break;
  }
};

declare var Blazor: any;
declare var BINDING: any;
declare var getValue: any;
declare var Module: any;

let getValueActual: any = null;
let getArrayLengthActual: any = null;

enum UnmarshalledColumnType {
  DoubleValue,
  IntValue,
  LongValue,
  StringValue,
  CalendarValue,
  DateTimeValue,
  BooleanValue,
  ObjectValue,
  DecimalValue,
  ByteValue,
  ShortValue,
  SingleValue,
  
  NullableDoubleValue,
  NullableIntValue,
  NullableLongValue,
  NullableCalendarValue,
  NullableDateTimeValue,
  NullableBooleanValue,
  NullableDecimalValue,
  NullableByteValue,
  NullableShortValue,
  NullableSingleValue,

  DoubleArrayValue,
  IntArrayValue,
  LongArrayValue,
  StringArrayValue,
  CalendarArrayValue,
  DateTimeArrayValue,
  BooleanArrayValue,
  DecimalArrayValue,
  ByteArrayValue,
  ShortArrayValue,
  SingleArrayValue
}

class UnmarshalledColumn {
  public constructor(ref: any) {
    this.actualCount = Blazor.platform.readInt32Field(ref, 0);
    this.dataSourceId = Blazor.platform.readStringField(ref, 8);
    this.type = Blazor.platform.readInt32Field(ref, 16);
    this.propertyPath = Blazor.platform.readStringField(ref, 24);
    var propertyPathParts = this.propertyPath.split(".");
    this.propertyPathParts = propertyPathParts;
    this.propertyName = propertyPathParts[propertyPathParts.length - 1];
    if (this.propertyName == "___self") {
      this.isSelf = true;
    }
    this.isSubDataSource = Blazor.platform.readInt32Field(ref, 32) != 0;

    var arrStart = Blazor.platform.readObjectField(ref, 40);

    var nullArrayStart = null;
    if (this.type > UnmarshalledColumnType.SingleValue &&
        this.type !== UnmarshalledColumnType.StringValue &&
        this.type !== UnmarshalledColumnType.DateTimeValue &&
        this.type !== UnmarshalledColumnType.CalendarValue) {
      nullArrayStart = Blazor.platform.readObjectField(ref, 48);
    }

    switch (this.type) {
      case UnmarshalledColumnType.DoubleValue:
      case UnmarshalledColumnType.SingleValue:     
      case UnmarshalledColumnType.DecimalValue:
        if (!this.numberValues) {
          this.numberValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 8);
          this.numberValues[i] = getValueActual(ele, "double");
        }
        if (this.propertyName === "___primitiveValueCollection") {
          // item is the array of primitive values
          this.setValue = (item, index) => {
            item.push(...this.numberValues);
          };
        } else {
          this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = this.numberValues[index];
        }       
        break;
      case UnmarshalledColumnType.BooleanValue:
        if (!this.booleanValues) {
          this.booleanValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
          var val = Blazor.platform.readInt32Field(ele, 0);
          if (val) {
            this.booleanValues[i] = true;
          } else {
            this.booleanValues[i] = false;
          }
        }
        if (this.propertyName === "___primitiveValueCollection") {
          // item is the array of primitive values
          this.setValue = (item, index) => {
            item.push(...this.booleanValues);
          };
        } else {
          this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = this.booleanValues[index];  
        } 
        break;      
      case UnmarshalledColumnType.ByteValue:         
      case UnmarshalledColumnType.IntValue:        
      case UnmarshalledColumnType.ShortValue:
        if (!this.numberValues) {
          this.numberValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
          this.numberValues[i] = Blazor.platform.readInt32Field(ele, 0);
        }
        if (this.propertyName === "___primitiveValueCollection") {
          // item is the array of primitive values
          this.setValue = (item, index) => {
            item.push(...this.numberValues);
          };
        } else {
          this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = this.numberValues[index];  
        } 
        break;         
      case UnmarshalledColumnType.LongValue:   
        if (!this.numberValues) {
          this.numberValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 8);
          //this.numberValues[i] = Blazor.platform.readInt64Field(ele, 0);
          this.numberValues[i] = getValueActual(ele, "i64");
        }
        if (this.propertyName === "___primitiveValueCollection") {
          // item is the array of primitive values
          this.setValue = (item, index) => {
            item.push(...this.numberValues);
          };
        } else {
          this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = this.numberValues[index];   
        }                
        break;
      case UnmarshalledColumnType.StringValue: 
        if (!this.stringValues) {
          this.stringValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
          this.stringValues[i] = Blazor.platform.readStringField(ele, 0);
        }
        if (this.propertyName === "___primitiveValueCollection") {
          // item is the array of primitive values
          this.setValue = (item, index) => {
            item.push(...this.stringValues);
          };
        } else {
          this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = this.stringValues[index];  
        }        
        break;
      case UnmarshalledColumnType.CalendarValue:
      case UnmarshalledColumnType.DateTimeValue:
      case UnmarshalledColumnType.NullableCalendarValue:
      case UnmarshalledColumnType.NullableDateTimeValue:
        if (!this.dateValues) {
          this.dateValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
          this.dateValues[i] = null;
          var stringValue = Blazor.platform.readStringField(ele, 0);
          if (stringValue) {
            this.dateValues[i] = new Date(stringValue);
          }
        }
        if (this.propertyName === "___primitiveValueCollection") {
          // item is the array of primitive values
          this.setValue = (item, index) => {
            item.push(...this.dateValues);
          };
        } else {
          this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = this.dateValues[index];    
        } 
        break;
      case UnmarshalledColumnType.ObjectValue:
          if (this.isSubDataSource) {
            if (!this.subDataSourceValues) {
              this.subDataSourceValues = new Array(this.actualCount);
            }
            for (var i = 0; i < this.actualCount; i++) {
              var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
              this.subDataSourceValues[i] = null;
              var ele = Blazor.platform.readObjectField(ele, 0);
              var subDataSourceColumns = getUnmarshalledColumns(ele);
              if (subDataSourceColumns) {
                this.subDataSourceValues[i] = createOrUpdateUnmarshalledDataSource(null, null, subDataSourceColumns);
              }
            }

            this.setValue = (item, index) => {
              if (!this.isSelf) {
                this.getTarget(item)[this.propertyName] = this.subDataSourceValues[index];  
              }
            }
          }
          break;
      case UnmarshalledColumnType.BooleanArrayValue:
      case UnmarshalledColumnType.ByteArrayValue:
      case UnmarshalledColumnType.CalendarArrayValue:
      case UnmarshalledColumnType.DateTimeArrayValue:
      case UnmarshalledColumnType.DecimalArrayValue:
      case UnmarshalledColumnType.DoubleArrayValue:
      case UnmarshalledColumnType.IntArrayValue:
      case UnmarshalledColumnType.LongArrayValue:
      case UnmarshalledColumnType.ShortArrayValue:
      case UnmarshalledColumnType.SingleArrayValue:
      case UnmarshalledColumnType.StringArrayValue:
          if (this.isSubDataSource) {
            if (!this.subDataSourceValues) {
              this.subDataSourceValues = new Array(this.actualCount);
            }
            for (var i = 0; i < this.actualCount; i++) {
              var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
              this.subDataSourceValues[i] = null;
              var ele = Blazor.platform.readObjectField(ele, 0);
              var subDataSourceColumns = getUnmarshalledColumns(ele);
              if (subDataSourceColumns) {
                for (var j = 0; j < subDataSourceColumns.length; j++) {
                  if (subDataSourceColumns[j].propertyPath === "___primitiveVal") {
                    let vals = [];
                    for (var k = 0; k < subDataSourceColumns[j].actualCount; k++) {
                      switch (this.type) {
                        case UnmarshalledColumnType.BooleanArrayValue:
                          vals.push(subDataSourceColumns[j].booleanValues[k]); break;
                        case UnmarshalledColumnType.ByteArrayValue:
                        case UnmarshalledColumnType.IntArrayValue:
                        case UnmarshalledColumnType.ShortArrayValue:
                        case UnmarshalledColumnType.DoubleArrayValue:
                        case UnmarshalledColumnType.SingleArrayValue:
                        case UnmarshalledColumnType.DecimalArrayValue:
                        case UnmarshalledColumnType.LongArrayValue:
                          vals.push(subDataSourceColumns[j].numberValues[k]); break;
                        case UnmarshalledColumnType.StringArrayValue:
                          vals.push(subDataSourceColumns[j].stringValues[k]); break;
                        case UnmarshalledColumnType.DateTimeArrayValue:
                        case UnmarshalledColumnType.CalendarArrayValue:
                          vals.push(subDataSourceColumns[j].dateValues[k]); break;
                      }
                    }
                    this.subDataSourceValues[i] = vals;
                  }
                }
              }
            }

            this.setValue = (item, index) => {
              if (!this.isSelf) {
                this.getTarget(item)[this.propertyName] = this.subDataSourceValues[index];  
              }
            }
          }
          break;
          
      case UnmarshalledColumnType.NullableDoubleValue:
      case UnmarshalledColumnType.NullableSingleValue:     
      case UnmarshalledColumnType.NullableDecimalValue:
        if (!this.numberValues) {
          this.numberValues = new Array(this.actualCount);
        }
        if (!this.nullValues) {
          this.nullValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 8);
          this.numberValues[i] = getValueActual(ele, "double");

          var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, i, 1);
          this.nullValues[i] = getValueActual(nullEle, "i8");
        }
        this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = !this.nullValues[index] ? this.numberValues[index] : null;
        break;
      case UnmarshalledColumnType.NullableBooleanValue:
        if (!this.booleanValues) {
          this.booleanValues = new Array(this.actualCount);
        }
        if (!this.nullValues) {
          this.nullValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
          var val = Blazor.platform.readInt32Field(ele, 0);
          if (val) {
            this.booleanValues[i] = true;
          } else {
            this.booleanValues[i] = false;
          }

          var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, i, 1);
          this.nullValues[i] = getValueActual(nullEle, "i8");
        }
        this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = !this.nullValues[index] ? this.booleanValues[index] : null;
        break;
      case UnmarshalledColumnType.NullableByteValue:         
      case UnmarshalledColumnType.NullableIntValue:        
      case UnmarshalledColumnType.NullableShortValue:
        if (!this.numberValues) {
          this.numberValues = new Array(this.actualCount);
        }
        if (!this.nullValues) {
          this.nullValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 4);
          this.numberValues[i] = Blazor.platform.readInt32Field(ele, 0);

          var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, i, 1);
          this.nullValues[i] = getValueActual(nullEle, "i8");
        }
        this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = !this.nullValues[index] ? this.numberValues[index] : null;
        break;
      case UnmarshalledColumnType.NullableLongValue:   
        if (!this.numberValues) {
          this.numberValues = new Array(this.actualCount);
        }
        if (!this.nullValues) {
          this.nullValues = new Array(this.actualCount);
        }
        for (var i = 0; i < this.actualCount; i++) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, i, 8);
          //this.numberValues[i] = Blazor.platform.readInt64Field(ele, 0);
          this.numberValues[i] = getValueActual(ele, "i64");

          var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, i, 1);
          this.nullValues[i] = getValueActual(nullEle, "i8");
        }               
        this.setValue = (item, index) => this.getTarget(item)[this.propertyName] = !this.nullValues[index] ? this.numberValues[index] : null;
        break;
    }
  }

  public getTarget(item: any) {
    if (this.propertyPathParts.length == 1) {
      return item;
    }
    var currTarget = item;
    for (var i = 0; i < this.propertyPathParts.length - 1; i++) {
      if (currTarget[this.propertyPathParts[i]]) {
        currTarget = currTarget[this.propertyPathParts[i]];
      } else {
        currTarget[this.propertyPathParts[i]] = {};
        currTarget = currTarget[this.propertyPathParts[i]];
      }
    }
    return currTarget;
  }

  actualCount: number;
  dataSourceId: string;
  type: UnmarshalledColumnType;
  propertyPath: string;
  propertyPathParts: string[];
  propertyName: string;
  numberValues: number[];
  stringValues: string[];
  dateValues: Date[];
  booleanValues: boolean[];
  nullValues: boolean[];
  subDataSourceValues: any[][];
  isSubDataSource: boolean;
  isSelf: boolean;
  setValue: (item: any, index: number) => void = null;
}

class UnmarshalledColumnItem {
  public constructor(ref: any, index: number) {
    this.actualCount = Blazor.platform.readInt32Field(ref, 0);
    this.dataSourceId = Blazor.platform.readStringField(ref, 8);
    this.type = Blazor.platform.readInt32Field(ref, 16);
    this.propertyPath = Blazor.platform.readStringField(ref, 24);
    var propertyPathParts = this.propertyPath.split(".");
    this.propertyPathParts = propertyPathParts;
    this.propertyName = propertyPathParts[propertyPathParts.length - 1];
    if (this.propertyName == "___self") {
      this.isSelf = true;
    }
    this.isSubDataSource = Blazor.platform.readInt32Field(ref, 32) != 0;
    this.index = index;

    var arrStart = Blazor.platform.readObjectField(ref, 40);

    var nullArrayStart = null;
    if (this.type > UnmarshalledColumnType.SingleValue &&
        this.type !== UnmarshalledColumnType.StringValue &&
        this.type !== UnmarshalledColumnType.DateTimeValue &&
        this.type !== UnmarshalledColumnType.CalendarValue) {
      nullArrayStart = Blazor.platform.readObjectField(ref, 48);
    }

    switch (this.type) {
      case UnmarshalledColumnType.DoubleValue:
      case UnmarshalledColumnType.SingleValue:     
      case UnmarshalledColumnType.DecimalValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 8);
        this.numberValue = getValueActual(ele, "double");
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = this.numberValue;
        break;
      case UnmarshalledColumnType.NullableDoubleValue:
      case UnmarshalledColumnType.NullableSingleValue:     
      case UnmarshalledColumnType.NullableDecimalValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 8);
        this.numberValue = getValueActual(ele, "double");
        var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, index, 1);
        this.nullValue = getValueActual(nullEle, "i8");
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = !this.nullValue ? this.numberValue : null; 
        break;
      case UnmarshalledColumnType.BooleanValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
        var val = Blazor.platform.readInt32Field(ele, 0);
        if (val) {
          this.booleanValue = true;
        } else {
          this.booleanValue = false;
        }
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = this.booleanValue;  
        break;
      case UnmarshalledColumnType.NullableBooleanValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
        var val = Blazor.platform.readInt32Field(ele, 0);
        if (val) {
          this.booleanValue = true;
        } else {
          this.booleanValue = false;
        }
        var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, index, 1);
        this.nullValue = getValueActual(nullEle, "i8"); 
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = !this.nullValue ? this.booleanValue : null;
        break;
      case UnmarshalledColumnType.ByteValue:         
      case UnmarshalledColumnType.IntValue:        
      case UnmarshalledColumnType.ShortValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
        this.numberValue = Blazor.platform.readInt32Field(ele, 0);
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = this.numberValue;  
        break;
      case UnmarshalledColumnType.NullableByteValue:         
      case UnmarshalledColumnType.NullableIntValue:        
      case UnmarshalledColumnType.NullableShortValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
        this.numberValue = Blazor.platform.readInt32Field(ele, 0);
        var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, index, 1);
        this.nullValue = getValueActual(nullEle, "i8"); 
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = !this.nullValue ? this.numberValue : null; 
        break;  
      case UnmarshalledColumnType.LongValue:   
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 8);
        //this.numberValue = Blazor.platform.readInt64Field(ele, 0);
        this.numberValue = getValueActual(ele, "i64");
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = this.numberValue;  
        break;
      case UnmarshalledColumnType.LongValue:   
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 8);
        //this.numberValue = Blazor.platform.readInt64Field(ele, 0);
        this.numberValue = getValueActual(ele, "i64");
        var nullEle = Blazor.platform.getArrayEntryPtr(nullArrayStart, index, 1);
        this.nullValue = getValueActual(nullEle, "i8"); 
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = !this.nullValue ? this.numberValue : null;
        break;          
      case UnmarshalledColumnType.StringValue: 
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
        this.stringValue = Blazor.platform.readStringField(ele, 0);
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = this.stringValue;  
        break;        
      case UnmarshalledColumnType.CalendarValue:
      case UnmarshalledColumnType.DateTimeValue:
      case UnmarshalledColumnType.NullableCalendarValue:
      case UnmarshalledColumnType.NullableDateTimeValue:
        var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
        this.dateValue = null;
        var stringValue = Blazor.platform.readStringField(ele, 0);
        if (stringValue) {
          this.dateValue = new Date(stringValue);
        }                   
        this.setValue = (item) => this.getTarget(item)[this.propertyName] = this.dateValue;  
        break;
      case UnmarshalledColumnType.ObjectValue:
        if (this.isSubDataSource) {
          var ele = Blazor.platform.getArrayEntryPtr(arrStart, index, 4);
          ele = Blazor.platform.readObjectField(ele, 0);
          var subDataSourceColumns = getUnmarshalledColumns(ele);
          this.subDataSource = createOrUpdateUnmarshalledDataSource(null, this.subDataSource, subDataSourceColumns);
          this.setValue = (item) => {
            if (!this.isSelf) {
             this.getTarget(item)[this.propertyName] = this.subDataSource;
            }
          }
        }
        break;
    }
  }

  index: number;
  actualCount: number;
  dataSourceId: string;
  type: UnmarshalledColumnType;
  propertyPath: string;
  propertyPathParts: string[];
  propertyName: string;
  numberValue: number;
  nullValue: boolean;
  stringValue: string;
  dateValue: Date;
  isSubDataSource: boolean;
  booleanValue: boolean;
  isSelf: boolean;
  subDataSource: any[];

  public getTarget(item: any) {
    if (this.propertyPathParts.length == 1) {
      return item;
    }
    var currTarget = item;
    for (var i = 0; i < this.propertyPathParts.length - 1; i++) {
      if (currTarget[this.propertyPathParts[i]]) {
        currTarget = currTarget[this.propertyPathParts[i]];
      } else {
        currTarget[this.propertyPathParts[i]] = {};
        currTarget = currTarget[this.propertyPathParts[i]];
      }
    }
    return currTarget;
  }

  setValue: (item: any) => void = null;
}



function getUnmarshalledColumns(columns: any): UnmarshalledColumn[] {
  if (!columns) {
    return null;
  }
  var arrStart = columns; //Blazor.platform.readObjectField(columns, 0);
  var arrLen = getArrayLengthActual(columns);
  let ret: UnmarshalledColumn[] = [];
  for (var i = 0; i < arrLen; i++) {
    var ptr = Blazor.platform.getArrayEntryPtr(arrStart, i, 56); 
    ret.push(new UnmarshalledColumn(ptr));
  }
  return ret;
}
function getUnmarshalledColumnItems(columns: any, index: number): UnmarshalledColumnItem[] {
  if (!columns) {
    return null;
  }
  var arrStart = columns; //Blazor.platform.readObjectField(columns, 0);
  var arrLen = getArrayLengthActual(columns);
  let ret: UnmarshalledColumnItem[] = [];
  for (var i = 0; i < arrLen; i++) {
    var ptr = Blazor.platform.getArrayEntryPtr(arrStart, i, 56); 
    ret.push(new UnmarshalledColumnItem(ptr, index));
  }
  return ret;
}

function createOrUpdateUnmarshalledDataSource(refName: string, data: any[], columns: UnmarshalledColumn[]): any[] {
  var newMap = new Map<string, any>();
  var oldData: any[] = null;
  if (data) {
    oldData = data.slice(0);
  }
  var itemMap: Map<string, any> = new Map<string, any>();
  if (refName) {
    if (itemMaps.has(refName)) {
      itemMap = itemMaps.get(refName);
    } else {
      itemMap = new Map<string, any>();
      itemMaps.set(refName, itemMap);
    }
  }

  var idColumn: UnmarshalledColumn = null;
  var selfColumn: UnmarshalledColumn = null;
  var primColumn: UnmarshalledColumn = null;
  var primitiveCollectionValueColumn: UnmarshalledColumn = null;

  if (!columns) {
    data = null;
  }

  if (columns) {
    for (var i = 0; i < columns.length; i++) {
      if (columns[i].propertyName == "___id") {
        idColumn = columns[i];
      }
      if (columns[i].propertyName == "___self") {
        selfColumn = columns[i];
      }
      if (columns[i].propertyName == "___primitiveVal") {
        primColumn = columns[i];
      }
      if (columns[i].propertyName == "___primitiveValueCollection") {
        primitiveCollectionValueColumn = columns[i];
      }
    }

    if (!data) {
      if (!idColumn) {
        data = new Array(0);
      } else {
        data = new Array(idColumn.actualCount);
      }
    } else {
      data.length = idColumn ? idColumn.actualCount : 0;
    }

    if (idColumn) {
      for (var i = 0; i < idColumn.actualCount; i++) {
        var target: any = null;
        var currId = idColumn.stringValues[i];

        if (!currId) {
          data[i] = null;
          return;
        }

        if (itemMap.has(currId)) {
          target = itemMap.get(currId);
        } else {
          if (selfColumn) {
            target = selfColumn.subDataSourceValues[i];
          } else {
            target = { };
          }
          itemMap.set(currId, target);
        }
        newMap.set(currId, target);

        for (var j = 0; j < columns.length; j++) {
          var currCol = columns[j];
          if (!currCol.setValue) {
            continue;
          }
          currCol.setValue(target, i);
        }

        data[i] = target;
      }
    } else if (primitiveCollectionValueColumn) {
      target = [];
      primitiveCollectionValueColumn.setValue(target, 0);
      data = target;
    }
  }

  if (oldData) {
    for (var i = 0; i < oldData.length; i++) {
      if (oldData[i].___id &&
        !newMap.has(oldData[i].___id)) {
        itemMap.delete(oldData[i].___id);
      }
    }
  }

  return data;
}

function createOrUpdateUnmarshalledItem(refName: string, item: any, columns: UnmarshalledColumnItem[]): any {
  if (!item) {
    item = {};
  }

  if (!columns) {
    return null;
  }

  var itemMap: Map<string, any> = null;
  if (itemMaps.has(refName)) {
    itemMap = itemMaps.get(refName);
  } else {
    itemMap = new Map<string, any>();
    itemMaps.set(refName, itemMap);
  }

  var idColumn: UnmarshalledColumnItem = null;
  var selfColumn: UnmarshalledColumnItem = null;
  for (var i = 0; i < columns.length; i++) {
    if (columns[i].propertyName == "___id") {
      idColumn = columns[i];
    }
    if (columns[i].propertyName == "___self") {
      selfColumn = columns[i];
    }
  }
  if (item.___id && item.___id != idColumn.stringValue) {
    itemMap.delete(item.___id);
    item = {};
  }
  if (selfColumn) {
    item = selfColumn.subDataSource;
  }

  if (!item.___id) {
    item.___id = idColumn.stringValue;
    itemMap.set(item.___id, item);
  }


  for (var j = 0; j < columns.length; j++) {
    var currCol = columns[j];
    if (!currCol.setValue) {
      continue;
    }
    currCol.setValue(item);
  }
  return item;
}

function getValueNinePlus(ptr, type) {
  switch (type) {
    case "double":
      return Blazor.runtime.getHeapF64(ptr);
    case "i64":
      return Blazor.runtime.getHeapI52(ptr);
    case "i32":
      return Blazor.runtime.getHeapI32(ptr);
    case "i8":
      return Blazor.runtime.getHeapI8(ptr);
  }
}

let isDotnetNinePlus = false;

function getArrayDataPtr(value: any): any {
  return value + 12;
}

(window as any).igUnmarshalledDataSourceCreate = function(refName: string, index: number, columns: any) {
  if ((window as any).getValue) {
    getValueActual = (window as any).getValue;
  }
  if ((global as any).getValue) {
    getValueActual = (global as any).getValue;
  }
  if ((self as any).getValue) {
    getValueActual = (self as any).getValue;
  }
  if (!getValueActual && (window as any).Module !== undefined) {
    getValueActual = Module.getValue;
    
  }
  if (!getValueActual) {
    getValueActual = getValueNinePlus;
    isDotnetNinePlus = true;
  }

  if (Blazor.platform.getArrayLength) {
    getArrayLengthActual = Blazor.platform.getArrayLength;
  }
  if (!getArrayLengthActual) {
    getArrayLengthActual = (arr: any) => {
      return getValueActual(getArrayDataPtr(arr), "i32");
    } 
  }

  if (isDotnetNinePlus)
     columns = Blazor.runtime.getHeapU32(columns);

  if (!isDotnetNinePlus) {
    refName = BINDING.conv_string(refName);
  }
  var ind = refName.indexOf(":");
  var containerId = refName.substr(0, ind);
  updateContainer(containerId);
  var refName = refName.substr(ind + 1);

  if (!columns) {
    var oldData = null;
    if (refValues.has(refName)) {
      oldData = refValues.get(refName);
      refValues.delete(refName);
    }
    if (itemMaps.has(refName) && oldData) {
      let map = itemMaps.get(refName);
      for (let i = 0; i < oldData.length; i++) {
        let child = oldData[i];
        if (child.___id) {
          map.delete(child.___id);
        }
      }
    }
    if (currentContainer()) {
      cr.provideRefValue(currentContainer(), refName, null);
    } else {
      if (!containersPendingDataRefs.has(containerId)) {
        containersPendingDataRefs.set(containerId, []);
      }
      let arr = containersPendingDataRefs.get(containerId);
      arr.push(() => {
        let cc = getContainer(containerId);
        cr.provideRefValue(cc, refName, null);        
      });
    }
    return;
  }

  var cols = getUnmarshalledColumns(columns);
  var data = createOrUpdateUnmarshalledDataSource(refName, null, cols);
  processDataIntents(refName, data);

  if (currentContainer()) {
    cr.provideRefValue(currentContainer(), refName, data);
  } else {
    if (!containersPendingDataRefs.has(containerId)) {
      containersPendingDataRefs.set(containerId, []);
    }
    let arr = containersPendingDataRefs.get(containerId);
    arr.push(() => {
      let cc = getContainer(containerId);
      cr.provideRefValue(cc, refName, data);        
    });
  }
  refValues.set(refName, data);
};

(window as any).igUnmarshalledDataSourceCreateDataIntents = function(refName: string, intents: string) {
  if (!isDotnetNinePlus) {
    refName = BINDING.conv_string(refName);
    intents = BINDING.conv_string(intents);
  }
  var ind = refName.indexOf(":");
  var containerId = refName.substr(0, ind);
  updateContainer(containerId);
  var refName = refName.substr(ind + 1);

  let dataIntents: any[] = null;
  if (intents) {
    dataIntents = JSON.parse(intents);
  }

  refDataIntents.set(refName, dataIntents);
};

(window as any).igUnmarshalledDataSourceInsert = function(refName: string, index: number, columns: any) {
  if (!isDotnetNinePlus) {
    refName = BINDING.conv_string(refName);
  }
  var ind = refName.indexOf(":");
  var containerId = refName.substr(0, ind);
  updateContainer(containerId);
  var refName = refName.substr(ind + 1);

  if (isDotnetNinePlus)
     columns = Blazor.runtime.getHeapU32(columns);

  var colItems = getUnmarshalledColumnItems(columns, index);
  var item = createOrUpdateUnmarshalledItem(refName, null, colItems);
  if (refValues.has(refName)) {
    var refValue: any[] = refValues.get(refName);

    refValue.splice(index, 0, item);

    if (currentContainer()) {
      let child: any = getMainTarget();
      if (child) {
        isSyncMethodInvoke = true;
        if (child.notifyInsertItem) {
          if (child.tagName && child.tagName == "IGC-DATA-GRID") {
            child.notifyInsertItem(index, item);
          } else {
            child.notifyInsertItem(refValue, index, item);
          }
        } else if (child.markForCheck) {
          child.markForCheck();
        }
        isSyncMethodInvoke = false;
      }
    }
  }
};
(window as any).igUnmarshalledDataSourceUpdate = function(refName: string, index: number, columns: any) {
  if (!isDotnetNinePlus) {
    refName = BINDING.conv_string(refName);
  }
  var ind = refName.indexOf(":");
  var containerId = refName.substr(0, ind);
  updateContainer(containerId);
  var refName = refName.substr(ind + 1);
  ind = refName.indexOf(":");
  var n = refName.substr(0, ind);
  var syncDataOnly = refName.substr(ind + 1) == "true";
  refName = n;

  if (isDotnetNinePlus)  
     columns = Blazor.runtime.getHeapU32(columns);

  var colItems = getUnmarshalledColumnItems(columns, index);

  if (refValues.has(refName)) {
    var refValue: any[] = refValues.get(refName);

    var oldVal = refValue[index];
    var currVal = createOrUpdateUnmarshalledItem(refName, oldVal, colItems);
    refValue[index] = currVal;

    if (currentContainer()) {
      let child: any = getMainTarget();
      isSyncMethodInvoke = true;
      if (child.notifySetItem && !syncDataOnly) {
        if (child.tagName && child.tagName == "IGC-DATA-GRID") {
          child.notifySetItem(index, oldVal, currVal);
        } else {
          child.notifySetItem(refValue, index, oldVal, currVal);
        }
      } else if (child.markForCheck) {
        child.markForCheck();
      }
      isSyncMethodInvoke = false;
    }
  }
};
(window as any).igUnmarshalledDataSourceRemove = function(refName: string, index: number, columns: any) {
  if (!isDotnetNinePlus) {
    refName = BINDING.conv_string(refName);
  }

  if (isDotnetNinePlus)
     columns = Blazor.runtime.getHeapU32(columns);

  var ind = refName.indexOf(":");
  var containerId = refName.substr(0, ind);
  updateContainer(containerId);
  var refName = refName.substr(ind + 1);
  
  if (refValues.has(refName)) {
    var refValue: any[] = refValues.get(refName);

    var oldVAl = refValue[index];
    refValue.splice(index, 1);

    if (currentContainer()) {
      let child: any = getMainTarget();
      isSyncMethodInvoke = true;
      if (child.notifyRemoveItem) {
        if (child.tagName && child.tagName == "IGC-DATA-GRID") {
          child.notifyRemoveItem(index, oldVAl);
        } else {
          child.notifyRemoveItem(refValue, index, oldVAl);
        }
      } else if (child.markForCheck) {
        child.markForCheck();
      }
      isSyncMethodInvoke = false;
    }
  }
};
(window as any).igUnmarshalledDataSourceClear = function(refName: string, index: number, columns: any) {
  if (!isDotnetNinePlus) {
    refName = BINDING.conv_string(refName);
  }
  var ind = refName.indexOf(":");
  var containerId = refName.substr(0, ind);
  updateContainer(containerId);
  var refName = refName.substr(ind + 1);

  if (isDotnetNinePlus)
     columns = Blazor.runtime.getHeapU32(columns);

  var cols = getUnmarshalledColumns(columns);

  
  if (refValues.has(refName)) {
    var refValue: any[] = refValues.get(refName);

    var refValue = createOrUpdateUnmarshalledDataSource(refName, refValue, cols);

    if (currentContainer()) {
      let child: any = getMainTarget();
      isSyncMethodInvoke = true;
      if (child.notifyClearItems) {
        if (child.tagName && child.tagName == "IGC-DATA-GRID") {
          child.notifyClearItems();
        } else {
          child.notifyClearItems(refValue);
        }
      } else if (child.markForCheck) {
        child.markForCheck();
      }
      isSyncMethodInvoke = false;
    }
  }
};


//callDotNet("OnReady");
if ((window as any).webViewCallback) {
  ((window as any).webViewCallback).onReady();
}
if ((window as any).webkit && (window as any).webkit.messageHandlers && (window as any).webkit.messageHandlers.onReady) {
  (window as any).webkit.messageHandlers.onReady.postMessage({ });
}