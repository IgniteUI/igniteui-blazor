import { IgEvent } from 'igniteui-core/IgEvent';
import { shadowWrap } from 'igniteui-core/igc-html-element';
import { TypeDescriptionContext } from './IComponentRendererAdapter_combined';
import { IgcPortalExitComponent } from 'igniteui-core/igc-portal';

export class IgcComponentRendererContainerComponent {

    private _attrIgnoreList = ['class', 'style'];
    private _observer: MutationObserver;

    static fromElement(ele: Element) {
        if ((ele as any).$$container) {
            return (ele as any).$$container;
        }

        let c = new IgcComponentRendererContainerComponent();
        c.element = ele;
        if (ele.hasAttribute("shadow-dom-mode")) {
            let shadowRoot = (shadowWrap(ele) as Element).attachShadow({ mode: "open" });
            c.element = (shadowRoot as any as Element);
            c.shadowMode = true;
        }
        if (ele.hasAttribute("child-content-mode")) {
            c.childContentMode = true;
            c.collectExtraSlots(ele);
        }

        if (ele.hasAttribute("portal-mode")) {
            c.portalMode = true;
            c.portalId = "portal-" + ele.getAttribute("data-ig-id");
        }

        (ele as any).$$container = c;
        return c;
    }

    private collectExtraSlots(ele: Element) {
        for (var i = 0; i < ele.children.length; i++) {
            let child = ele.children[i];
            if (child.hasAttribute("slot")) {
                if (!this.extraSlotNamesMap.has(child.getAttribute("slot"))) {
                    this.extraSlotNames.push(child.getAttribute("slot"));
                    this.extraSlotNamesMap.add(child.getAttribute("slot"));
                }
            }
        }
    }

    private ensureExtraSlots(ele: Element) {
        let exist = new Set<string>();
        for (var i = 0; i < ele.children.length; i++) {
            if (ele.children[i].tagName == "SLOT") {
                if (ele.hasAttribute("name")) {
                    exist.add(ele.getAttribute("name"));
                }
            }
        }
        for (var i = 0; i < this.extraSlotNames.length; i++) {
            if (!exist.has(this.extraSlotNames[i])) {
                let s = document.createElement("slot");
                s.setAttribute("name", this.extraSlotNames[i]);
                s.setAttribute("slot", this.extraSlotNames[i]);
                ele.append(s);
            }
        }
    }

    private extraSlotNames: string[] = [];
    private extraSlotNamesMap: Set<string> = new Set<string>();

    public element: Element;
    private shadowMode: boolean = false;
    private childContentMode: boolean = false;
    private portalMode: boolean = false;
    private portalId: string | null = null;

    private listeners: ((r: any) => void)[] = [];
    private _currentRoot: any = null;

    createObject(t: any, container: any, context: TypeDescriptionContext, portalChildren: boolean, nameContext: string) {
        if (typeof t == "string") {
            t = document.createElement(t);
        }
        else {
            let C = t;
            if (C.htmlTagName) {
                t = document.createElement(t.htmlTagName);
            } else {
                t = new C();
            }
        }

        if (portalChildren && nameContext) {
            let portalExit = document.createElement("igc-portal-exit") as IgcPortalExitComponent;
            let portalId = "portal-" + nameContext;
            portalExit.portalId = portalId;
            portalExit.useParentAsTarget = true;
            portalExit.moveOnceMode = true;

            t.appendChild(portalExit);
        }

        return t;
    }

    replaceRootItem(t: any, deferAttach: boolean, continueActions: (resumeRequired: boolean) => void) {
        for (let i = this.element.children.length - 1; i >= 0; i--) {
            this.element.children[i].remove();
        }

        if (typeof t == "string") {
            t = document.createElement(t);
        }
        else {
            let C = t;
            if (C.htmlTagName) {
                t = document.createElement(t.htmlTagName);
            } else {
                t = new C();
            }
        }

        this._currentRoot = t;

        if (!deferAttach) {
            this.element.appendChild(this._currentRoot);
        }
        (this._currentRoot as any).width = "100%";
        (this._currentRoot as any).height = "100%";

        if (this.childContentMode) {
            let slot = document.createElement("slot");
            this._currentRoot.appendChild(slot);
            this.ensureExtraSlots(this._currentRoot);
        }

        if (this.portalMode) {
            let portalExit = document.createElement("igc-portal-exit") as IgcPortalExitComponent;
            portalExit.portalId = this.portalId;
            portalExit.useParentAsTarget = true;
            portalExit.moveOnceMode = true;

            this._currentRoot.appendChild(portalExit);
        }

        
        let parent = this.shadowMode ? (this.element as any as ShadowRoot).host : this.element.parentElement;
        if (parent) {
            // sync attributes from the parent down to the root element.
            for (let i = 0; i < parent.attributes.length; i++) {
                let attr = parent.attributes.item(i);
                if (this._attrIgnoreList.includes(attr.name)) {
                    continue;
                }
                (this._currentRoot as HTMLElement).setAttribute(attr.name, attr.value);

                // this tells the component, if it supports it, that an attribute on it was synced from the parent.
                if (this._currentRoot.attributeSynced) {
                    this._currentRoot.attributeSynced(attr.name, attr.value);
                }
            }

            // setup an observer to watch for attribute changes so we can sync them with the root.
            if (!this._observer) {
                this._observer = new MutationObserver((mutationList, observer) => {
                    for (let i = 0; i < mutationList.length; i++) {
                        if (mutationList[i].type === 'attributes') {
                            let attrName = mutationList[i].attributeName;
                            if (this._attrIgnoreList.includes(attrName)) {
                                continue;
                            }
                            let attrValue = (mutationList[i].target as HTMLElement).getAttribute(attrName);
                            (this._currentRoot as HTMLElement).setAttribute(attrName, attrValue);

                            if (this._currentRoot.attributeSynced) {
                                this._currentRoot.attributeSynced(attrName, attrValue);
                            }
                        }
                        if (mutationList[i].type === 'childList') {
                            mutationList[i].removedNodes.forEach((value: Element, key: number, parent: NodeList) => {
                                if (value.hasAttribute("slot")) {
                                    const slotId = value.getAttribute("slot");
                                    for (let i = 0; i < this._currentRoot.children.length; i++) {
                                        let child: Element = this._currentRoot.children[i];
                                        if (child.tagName === "SLOT" &&
                                            child.hasAttribute("name") &&
                                            child.hasAttribute("slot") &&
                                            child.getAttribute("name") === slotId &&
                                            child.getAttribute("slot") === slotId) {
                                            child.remove();
                                            i--;
                                        }
                                    }

                                    const slotIdx = this.extraSlotNames.indexOf(slotId);
                                    this.extraSlotNames.splice(slotIdx, 1);
                                    this.extraSlotNamesMap.delete(slotId);
                                }
                            });
                            mutationList[i].addedNodes.forEach((value: Element, key: number, parent: NodeList) => {
                                if (value.hasAttribute("slot")) {
                                    const slotId = value.getAttribute("slot");
                                    if (!this.extraSlotNamesMap.has(slotId)) {
                                        let s = document.createElement("slot");
                                        s.setAttribute("name", slotId);
                                        s.setAttribute("slot", slotId);
                                        this._currentRoot.append(s);

                                        this.extraSlotNames.push(slotId);
                                        this.extraSlotNamesMap.add(slotId);
                                    }
                                }
                            });
                        }
                    }
                });
                this._observer.observe(parent, { attributes: true, childList: this.childContentMode });
            } else {
                this._observer.observe(parent, { attributes: true, childList: this.childContentMode });
            }
        }
        
        continueActions(false);

        if (deferAttach) {
            this.element.appendChild(this._currentRoot);
        }
    }

    clearContainer(continueActions: (resumeRequired: boolean) => void) {
        for (let i = this.element.children.length - 1; i >= 0; i--) {
            this.element.children[i].remove();
        }

        if (this._observer) {
            this._observer.disconnect();
        }

        this._currentRoot = null;

        continueActions(false);
    }

    getRootObject(): any {
        return this._currentRoot;
    }

    static isEvent(obj: any): boolean {
        return obj instanceof IgEvent;
    }

}
