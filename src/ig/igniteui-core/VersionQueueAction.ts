import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class VersionQueueAction extends Base {
	static $t: Type = markType(VersionQueueAction, 'VersionQueueAction');
	private _version: number = 0;
	private _action: () => void = null;
	constructor(action: () => void, version: number) {
		super();
		this._action = action;
		this._version = version;
	}
	get version(): number {
		return this._version;
	}
	get action(): () => void {
		return this._action;
	}
}


