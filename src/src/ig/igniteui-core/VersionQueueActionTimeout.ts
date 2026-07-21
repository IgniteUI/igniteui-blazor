import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class VersionQueueActionTimeout extends Base {
	static $t: Type = markType(VersionQueueActionTimeout, 'VersionQueueActionTimeout');
	private _version: number = 0;
	private _action: (arg1: boolean) => void = null;
	private _timeout: number = 0;
	constructor(action: (arg1: boolean) => void, version: number, timeout: number) {
		super();
		this._action = action;
		this._version = version;
		this._timeout = timeout;
	}
	get version(): number {
		return this._version;
	}
	get action(): (arg1: boolean) => void {
		return this._action;
	}
	get timeout(): number {
		return this._timeout;
	}
}


