import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebNumberEventArgsDescription extends Description {
	static $t: Type = markType(WebNumberEventArgsDescription, 'WebNumberEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebNumberEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "NumberEventArgs";
	constructor() {
		super();
	}
	private _detail: number = 0;
	get detail(): number {
		return this._detail;
	}
	set detail(value: number) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


