export class IgEvent<TSender, TArgs> {
    private _callbacks: ((sender: TSender, args: TArgs) => void)[] = [];
    emit(sender: TSender, args: TArgs) {
        for (let i = 0; i < this._callbacks.length; i++) {
            this._callbacks[i](sender, args);
        }
    }
    add(callback: (sender: TSender, args: TArgs) => void) {
        this._callbacks.push(callback);
    }
    remove(callback: (sender: TSender, args: TArgs) => void) {
        let ind = this._callbacks.indexOf(callback);
        if (ind >= 0) {
            this._callbacks.splice(ind, 1);
        }
    }
}