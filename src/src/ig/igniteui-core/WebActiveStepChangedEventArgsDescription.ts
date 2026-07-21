import { Description } from "./Description";
import { WebActiveStepChangedEventArgsDetailDescription } from "./WebActiveStepChangedEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebActiveStepChangedEventArgsDescription extends Description {
	static $t: Type = markType(WebActiveStepChangedEventArgsDescription, 'WebActiveStepChangedEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebActiveStepChangedEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ActiveStepChangedEventArgs";
	constructor() {
		super();
	}
	private _detail: WebActiveStepChangedEventArgsDetailDescription = null;
	get detail(): WebActiveStepChangedEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebActiveStepChangedEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


