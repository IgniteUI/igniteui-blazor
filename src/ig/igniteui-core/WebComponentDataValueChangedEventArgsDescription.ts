import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComponentDataValueChangedEventArgsDescription extends Description {
	static $t: Type = markType(WebComponentDataValueChangedEventArgsDescription, 'WebComponentDataValueChangedEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComponentDataValueChangedEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _detail: any = null;
	get detail(): any {
		return this._detail;
	}
	set detail(value: any) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


