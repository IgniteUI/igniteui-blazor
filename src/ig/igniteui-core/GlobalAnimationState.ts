import { Base, runOn, Type, markType } from "./type";
import { VersionQueueHelper } from "./VersionQueueHelper";
import { IExecutionContext } from "./IExecutionContext";

/**
 * @hidden 
 */
export class GlobalAnimationState extends Base {
	static $t: Type = markType(GlobalAnimationState, 'GlobalAnimationState');
	private static _instance: GlobalAnimationState = null;
	constructor() {
		super();
	}
	static get instance(): GlobalAnimationState {
		if (GlobalAnimationState._instance == null) {
			GlobalAnimationState._instance = new GlobalAnimationState();
		}
		return GlobalAnimationState._instance;
	}
	private _runningAnimationCount: number = 0;
	private _executionContext: IExecutionContext = null;
	setExecutionContext(executionContext: IExecutionContext): void {
		this._executionContext = executionContext;
		this._animationIdleQueue.setExecutionContext(executionContext);
	}
	private getExecutionContext(): IExecutionContext {
		if (this._executionContext != null) {
			return this._executionContext;
		}
		return null;
	}
	notifyAnimationStart(): void {
		this._runningAnimationCount++;
	}
	notifyAnimationEnd(): void {
		if (this._runningAnimationCount > 0) {
			this._runningAnimationCount--;
		}
		if (this._runningAnimationCount <= 0) {
			this._runningAnimationCount = 0;
			this.queueIdleCheck(0);
		}
	}
	private _idleCheckQueued: boolean = false;
	private queueIdleCheck(checkTimeout: number): void {
		if (this._idleCheckQueued) {
			return;
		}
		let executionContext = this.getExecutionContext();
		if (executionContext == null) {
			this._animationIdleQueue.increment();
			return;
		}
		executionContext.executeDelayed(runOn(this, this.idleCheck), checkTimeout);
		this._idleCheckQueued = true;
	}
	private idleCheck(): void {
		if (this._runningAnimationCount <= 0) {
			this._runningAnimationCount = 0;
			this._animationIdleQueue.increment();
		}
		this._idleCheckQueued = false;
	}
	private _animationIdleQueue: VersionQueueHelper = new VersionQueueHelper();
	queueForAnimationIdle(action: () => void, version: number): void {
		this._animationIdleQueue.queue(action, version);
		this.queueIdleCheck(100);
	}
	queueForAnimationIdleWithTimeout(action: (arg1: boolean) => void, version: number, timeout: number): void {
		this._animationIdleQueue.queue1(action, version, timeout);
		this.queueIdleCheck(100);
	}
	getAnimationIdleVersionNumber(): number {
		return this._animationIdleQueue.getVersion();
	}
	isAnimationActive(): boolean {
		return this._runningAnimationCount > 0;
	}
}


