import { Base, IEnumerator$1, IEnumerator$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, fromEnum, Type, markType } from "./type";
import { List$1 } from "./List$1";
import { VersionQueueAction } from "./VersionQueueAction";
import { VersionQueueActionTimeout } from "./VersionQueueActionTimeout";
import { IExecutionContext } from "./IExecutionContext";

/**
 * @hidden 
 */
export class VersionQueueHelper extends Base {
	static $t: Type = markType(VersionQueueHelper, 'VersionQueueHelper');
	private _actions: List$1<VersionQueueAction> = new List$1<VersionQueueAction>((<any>VersionQueueAction).$type, 0);
	private _actionsWithTimeoutLock: any = {};
	private _actionsWithTimeout: List$1<VersionQueueActionTimeout> = new List$1<VersionQueueActionTimeout>((<any>VersionQueueActionTimeout).$type, 0);
	private _executionContext: IExecutionContext = null;
	private _version: number = 0;
	increment(): void {
		if (this._version >= 0x7FFFFFFF - 1) {
			this._version = 0;
		} else {
			this._version++;
		}
		let toExecute: List$1<VersionQueueAction> = new List$1<VersionQueueAction>((<any>VersionQueueAction).$type, 0);
		for (let i = 0; i < this._actions.count; i++) {
			if (this.shouldExecute(this._actions._inner[i].version)) {
				toExecute.add(this._actions._inner[i]);
			}
		}
		for (let i1 = 0; i1 < toExecute.count; i1++) {
			this._actions.remove(toExecute._inner[i1]);
		}
		for (let i2 = 0; i2 < toExecute.count; i2++) {
			toExecute._inner[i2].action();
		}
		let toExecuteTimeout: List$1<VersionQueueActionTimeout> = new List$1<VersionQueueActionTimeout>((<any>VersionQueueActionTimeout).$type, 0);
		for (let i3 = 0; i3 < this._actionsWithTimeout.count; i3++) {
			if (this.shouldExecute(this._actionsWithTimeout._inner[i3].version)) {
				toExecuteTimeout.add(this._actionsWithTimeout._inner[i3]);
			}
		}
		for (let i4 = 0; i4 < toExecuteTimeout.count; i4++) {
			this._actionsWithTimeout.remove(toExecuteTimeout._inner[i4]);
		}
		for (let i5 = 0; i5 < toExecuteTimeout.count; i5++) {
			toExecuteTimeout._inner[i5].action(false);
		}
	}
	getVersion(): number {
		return this._version;
	}
	queue(action: () => void, version: number): void {
		if (this.shouldExecute(version)) {
			action();
			return;
		}
		this._actions.add(new VersionQueueAction(action, version));
	}
	setExecutionContext(executionContext: IExecutionContext): void {
		if (this._executionContext == null && executionContext != null) {
			for (let item of fromEnum<VersionQueueActionTimeout>(this._actionsWithTimeout)) {
				executionContext.executeDelayed(() => this.onActionTimeout(item), item.timeout);
			}
		}
		this._executionContext = executionContext;
	}
	queue1(action: (arg1: boolean) => void, version: number, timeout: number): void {
		if (timeout <= 0) {
			this.queue(() => action(false), version);
			return;
		}
		if (this.shouldExecute(version)) {
			action(false);
			return;
		}
		let versionQueueActionTimeout = new VersionQueueActionTimeout(action, version, timeout);
		this._actionsWithTimeout.add(versionQueueActionTimeout);
		if (this._executionContext != null) {
			this._executionContext.executeDelayed(() => this.onActionTimeout(versionQueueActionTimeout), timeout);
		}
	}
	private onActionTimeout(versionQueueAction: VersionQueueActionTimeout): void {
		if (!this._actionsWithTimeout.contains(versionQueueAction)) {
			return;
		}
		this._actionsWithTimeout.remove(versionQueueAction);
		versionQueueAction.action(true);
	}
	private shouldExecute(version: number): boolean {
		if (this._version > version) {
			return true;
		}
		if (this._version >= 0x7FFFFFFF - 1 && version < 0x7FFFFFFF - 1) {
			return true;
		}
		return false;
	}
}


