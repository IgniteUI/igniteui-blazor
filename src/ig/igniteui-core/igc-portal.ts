import { IgcHTMLElement } from "./igc-html-element";

export class IgcPortalItemComponent extends IgcHTMLElement {
    private static _nextPortalId: number = 0;

    private _portalId: number = 0;
    public get portalId(): number {
        return this._portalId;
    }

    public constructor() {
        super();
        this._portalId = IgcPortalItemComponent._nextPortalId;
        IgcPortalItemComponent._nextPortalId += 1;
    }

    connectedCallback() {
        if (this.parentElement) {
            if (this.parentElement instanceof IgcPortalEntranceComponent) {
                let entrance = this.parentElement as IgcPortalEntranceComponent;
                entrance.ensurePortalUpdated();
            }
        }
    }


}

export class IgcPortalEntranceComponent extends IgcHTMLElement {
    private _detach: (() => void) | null = null;
    private _observer: MutationObserver | null = null;

    public get portalId(): string {
        return this.getAttribute("portal-id")!;
    }
    public set portalId(value: string) {
        this.setAttribute("portal-id", value);
    }

    public get useShadowMode(): boolean {
        return this.getAttribute("use-shadow-mode") == "true";
    }
    public set useShadowMode(value: boolean) {
        this.setAttribute("use-shadow-mode", value ? "true" : "false");
    }

    public get moveOnceMode(): boolean {
        return this.getAttribute("move-once-mode") == "true";
    }
    public set moveOnceMode(value: boolean) {
        this.setAttribute("move-once-mode", value ? "true" : "false");
    }

    connectedCallback() {
        this._detach = PortalChannel.instance().subscribe(this.portalId, (m) => this.onPortalMessage(m));
        PortalChannel.instance().broadcast(PortalMessage.entranceAttaching(this, this.portalId, this.useShadowMode));
        if (this.useShadowMode) {

        } else {
            this._observer = new MutationObserver((mutations) => {
                if (!this._suspendChangeDetection) {
                    this.ensurePortalUpdated();
                }
            });
            this._observer.observe(this, { childList: true, subtree: true });
        }

        this.ensurePortalUpdated();
    }

    private _suspendChangeDetection = false;

    private _itemContent: Map<number, any> = new Map<number, any>();

    private getPortalContent(): any[] {
        let useMoveOnce = this.moveOnceMode;
        let children = Array.from(this.children);
        let items = children.filter((c) => c instanceof IgcPortalItemComponent);
        if (useMoveOnce) {
            items = children as any;
        }
        let ret = [];
        let seen = new Set<number>();
        for (let i = 0; i < items.length; i++) {
            let currItem = items[i];

            let prevContent: any | null = null;
            if (!useMoveOnce) {
                let portalItem = currItem as IgcPortalItemComponent;
                seen.add(portalItem.portalId);

                if (this._itemContent.has(portalItem.portalId)) {
                    prevContent = this._itemContent.get(portalItem.portalId);
                }
            }

            let itemChild = useMoveOnce ? currItem : (currItem.children.length > 0 ? currItem.children[0] : null);
            if (!useMoveOnce) {
                let portalItem = currItem as IgcPortalItemComponent;
                if (itemChild && itemChild != prevContent) {
                    this._itemContent.set(portalItem.portalId, itemChild);
                } else {
                    itemChild = prevContent;
                }

                if (itemChild) {
                    (itemChild as any).___portalItemId = portalItem.portalId;
                }
            }

            if (itemChild) {
                ret.push(itemChild);
            }
        }

        let toRemove = [];
        for (let key of this._itemContent.keys()) {
            if (!seen.has(key)) {
                toRemove.push(key);
            }
        }
        for (let key of toRemove) {
            this._itemContent.delete(key);
        }
        return ret;
    }

    ensurePortalUpdated() {
        this._suspendChangeDetection = true;
        PortalChannel.instance().broadcast(PortalMessage.entranceSlotContentUpdated(this, this.portalId, this.getPortalContent()));
        this._suspendChangeDetection = false;
    }

    disconnectedCallback() {
        PortalChannel.instance().broadcast(PortalMessage.entranceDetaching(this, this.portalId));
        if (this._detach) {
            this._detach();
        }
        if (this.useShadowMode) {

        } else {
            if (this._observer) {
                this._observer.disconnect();
                this._observer = null;
            }
        }
    }

    private onPortalMessage(m: PortalMessage) {
        if (m.sender === this) {
            return;
        }

        if (m.portalId == this.portalId) {
            switch (m.messageType) {
                case PortalMessageType.ExitAttaching:
                    this.ensurePortalUpdated();
                    break;
            }
        }
    }
}

export class IgcPortalExitComponent extends IgcHTMLElement {
    private _detach: (() => void) | null = null;

    public get portalId(): string {
        return this.getAttribute("portal-id")!;
    }
    public set portalId(value: string) {
        this.setAttribute("portal-id", value);
    }

    public get useShadowMode(): boolean {
        return this.getAttribute("use-shadow-mode") == "true";
    }
    public set useShadowMode(value: boolean) {
        this.setAttribute("use-shadow-mode", value ? "true" : "false");
    }

    public get useParentAsTarget(): boolean {
        return this.getAttribute("use-parent-as-target") == "true";
    }
    public set useParentAsTarget(value: boolean) {
        this.setAttribute("use-parent-as-target", value ? "true" : "false");
    }

    public get moveOnceMode(): boolean {
        return this.getAttribute("move-once-mode") == "true";
    }
    public set moveOnceMode(value: boolean) {
        this.setAttribute("move-once-mode", value ? "true" : "false");
    }

    connectedCallback() {
        this._detach = PortalChannel.instance().subscribe(this.portalId, (m) => this.onPortalMessage(m));
        PortalChannel.instance().broadcast(PortalMessage.exitAttaching(this, this.portalId, this.useShadowMode));
    }

    disconnectedCallback() {
        PortalChannel.instance().broadcast(PortalMessage.exitDetaching(this, this.portalId));
        if (this._detach) {
            this._detach();
        }
    }

    private onPortalMessage(m: PortalMessage) {
        if (m.sender === this) {
            return;
        }

        if (m.portalId == this.portalId) {
            switch (m.messageType) {
                case PortalMessageType.EntranceSlotContentUpdated:
                    this.onSlotContentChanging(m);
                    break;
            }
        }
    }

    private onSlotContentChanging(m: PortalMessage) {
        let useMoveOnce = this.moveOnceMode;
        if (this.useShadowMode) {

        } else {
            let target: HTMLElement = this;
            if (this.useParentAsTarget && this.parentElement) {
                target = this.parentElement
            }

            if (!useMoveOnce) {
                for (let i = target.children.length - 1; i >= 0; i--) {
                    if (target.children[i] === this) {
                        continue;
                    }
                    target.children[i].remove();
                }
            }
            if (m.content) {
                let arr = m.content as any[];
                for (let i = 0; i < arr.length; i++) {
                    target.append(arr[i]);
                }
            }
        }
    }
}


class PortalChannel {
    private static _instance: PortalChannel | null = null;
    public static instance(): PortalChannel {
        if (this._instance == null) {
            this._instance = new PortalChannel();
        }
        return this._instance;
    }

    private _subscribers: Map<string, ((onmessage: PortalMessage) => void)[]> = new Map<string, ((onmessage: PortalMessage) => void)[]>();
    public subscribe(portalId: string, onMessage: (m: PortalMessage) => void): () => void {
        if (!this._subscribers.has(portalId)) {
            this._subscribers.set(portalId, []);
        }
        this._subscribers.get(portalId)!.push(onMessage);
        return () => {
            if (this._subscribers.has(portalId)) {
                let ind = this._subscribers.get(portalId)!.indexOf(onMessage);
                this._subscribers.get(portalId)!.splice(ind, 1);
                if (this._subscribers.get(portalId)!.length == 0) {
                    this._subscribers.delete(portalId);
                }
            }
        };
    }

    public broadcast(m: PortalMessage): void {
        if (!this._subscribers.has(m.portalId)) {
            return;
        }
        let subs = this._subscribers.get(m.portalId)!.slice();

        for (let i = 0; i < subs.length; i++) {
            subs[i](m);
        }
    }
}

enum PortalMessageType {
    EntranceAttaching,
    EntranceDetaching,
    ExitAttaching,
    ExitDetaching,
    EntranceSlotContentUpdated
}

class PortalMessage {

    static entranceAttaching(sender: any, portalId: string, useShadowMode: boolean): PortalMessage {
        let m = new PortalMessage();
        m.messageType = PortalMessageType.EntranceAttaching;
        m.sender = sender;
        m.portalId = portalId;
        m.useShadowMode = useShadowMode;
        return m;
    }
    static entranceDetaching(sender: any, portalId: string): PortalMessage {
        let m = new PortalMessage();
        m.messageType = PortalMessageType.EntranceDetaching;
        m.sender = sender;
        m.portalId = portalId;
        return m;
    }

    static exitAttaching(sender: any, portalId: string, useShadowMode: boolean): PortalMessage {
        let m = new PortalMessage();
        m.messageType = PortalMessageType.ExitAttaching;
        m.sender = sender;
        m.portalId = portalId;
        m.useShadowMode = useShadowMode;
        return m;
    }
    static exitDetaching(sender: any, portalId: string): PortalMessage {
        let m = new PortalMessage();
        m.messageType = PortalMessageType.ExitDetaching;
        m.sender = sender;
        m.portalId = portalId;
        return m;
    }

    static entranceSlotContentUpdated(sender: any, portalId: string, content: any): PortalMessage {
        let m = new PortalMessage();
        m.messageType = PortalMessageType.EntranceSlotContentUpdated;
        m.sender = sender;
        m.portalId = portalId;
        m.content = content;
        return m;
    }

    public useShadowMode: boolean = false;
    public messageType: PortalMessageType = PortalMessageType.EntranceAttaching;
    public portalId: string = "";
    public content: any = null;
    public sender: any = null;
}

export class IgcPortalModule {
    public static register() {
        window.customElements.define("igc-portal-entrance", IgcPortalEntranceComponent);
        window.customElements.define("igc-portal-exit", IgcPortalExitComponent);
        window.customElements.define("igc-portal-item", IgcPortalItemComponent);
    }
}