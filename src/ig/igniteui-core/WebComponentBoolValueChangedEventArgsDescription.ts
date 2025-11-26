import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebComponentBoolValueChangedEventArgsDescription extends Description {
	static $t: Type = markType(WebComponentBoolValueChangedEventArgsDescription, 'WebComponentBoolValueChangedEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebComponentBoolValueChangedEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ComponentBoolValueChangedEventArgs";
	constructor() {
		super();
	}
	private _detail: boolean = false;
	get detail(): boolean {
		return this._detail;
	}
	set detail(value: boolean) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


