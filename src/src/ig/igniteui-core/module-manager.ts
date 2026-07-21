export class ModuleManager {
    public static register(...modules: any[]) {
        for (let mod of modules) {
            if (mod.register) {
                mod.register();
            }
        }
        RegisterElementHelper.force();
    }
}

export class RegisterElementHelper {
    private static _queue: (() => void)[] = [];
    private static _registeredSet: Set<string> = new Set<string>();
    public static registerElement(tagName: string, cons: Function) {
        RegisterElementHelper._queue.push(() => {
            if (RegisterElementHelper._registeredSet.has(tagName)) {
                return;
            }
            RegisterElementHelper._registeredSet.add(tagName);
            if (window.customElements) {
                window.customElements.define(tagName, cons as any);
            } else {
                (<any>document).registerElement(tagName, cons as any);
            }
        });
        RegisterElementHelper.queueUpdate();
    }

    private static _updateQueued: boolean = false;
    private static _timerId: number = -1;
    private static queueUpdate() {
        this._timerId = window.setTimeout(() => RegisterElementHelper.doUpdate(), 0);
    }

    public static force() {
        if (this._timerId >= 0) {
            window.clearTimeout(this._timerId);
            this._timerId = -1;
        }
        this.doUpdate();
    }

    private static doUpdate() {
        this._timerId = -1;
        this._updateQueued = false;
        while (RegisterElementHelper._queue.length > 0) {
            let ele = RegisterElementHelper._queue.shift();
            ele();
        }
    }
}