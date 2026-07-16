import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebActiveStepChangingEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebActiveStepChangingEventArgsDetailDescription, 'WebActiveStepChangingEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebActiveStepChangingEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "ActiveStepChangingEventArgsDetail";
	constructor() {
		super();
	}
	private _oldIndex: number = 0;
	get oldIndex(): number {
		return this._oldIndex;
	}
	set oldIndex(value: number) {
		this._oldIndex = value;
		this.markDirty("OldIndex");
	}
	private _newIndex: number = 0;
	get newIndex(): number {
		return this._newIndex;
	}
	set newIndex(value: number) {
		this._newIndex = value;
		this.markDirty("NewIndex");
	}
}


