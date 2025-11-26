import { Base, String_$type, Type, markType } from "./type";
import { Description } from "./Description";
import { List$1 } from "./List$1";

/**
 * @hidden 
 */
export class DescriptionResult extends Base {
	static $t: Type = markType(DescriptionResult, 'DescriptionResult');
	private _warnings: List$1<string> = new List$1<string>(String_$type, 0);
	constructor() {
		super();
	}
	addWarning(warning: string): void {
		this._warnings.add(warning);
	}
	addWarnings(other: DescriptionResult): void {
		for (let i = 0; i < other._warnings.count; i++) {
			this._warnings.add(other._warnings._inner[i]);
		}
	}
	private _result: Description = null;
	get result(): Description {
		return this._result;
	}
	set result(value: Description) {
		this._result = value;
	}
	get warnings(): string[] {
		return this._warnings.toArray();
	}
}


