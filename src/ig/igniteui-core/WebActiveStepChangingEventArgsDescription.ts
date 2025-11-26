import { Description } from "./Description";
import { WebActiveStepChangingEventArgsDetailDescription } from "./WebActiveStepChangingEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebActiveStepChangingEventArgsDescription extends Description {
	static $t: Type = markType(WebActiveStepChangingEventArgsDescription, 'WebActiveStepChangingEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebActiveStepChangingEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ActiveStepChangingEventArgs";
	constructor() {
		super();
	}
	private _detail: WebActiveStepChangingEventArgsDetailDescription = null;
	get detail(): WebActiveStepChangingEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebActiveStepChangingEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


