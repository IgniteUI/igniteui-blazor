import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebActiveStepChangedEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebActiveStepChangedEventArgsDetailDescription, 'WebActiveStepChangedEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebActiveStepChangedEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ActiveStepChangedEventArgsDetail";
	constructor() {
		super();
	}
	private _index: number = 0;
	get index(): number {
		return this._index;
	}
	set index(value: number) {
		this._index = value;
		this.markDirty("Index");
	}
}


