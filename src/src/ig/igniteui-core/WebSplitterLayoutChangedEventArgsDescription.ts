import { Description } from "./Description";
import { WebSplitterLayoutChangedEventArgsDetailDescription } from "./WebSplitterLayoutChangedEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden
 */
export class WebSplitterLayoutChangedEventArgsDescription extends Description {
	static $t: Type = markType(WebSplitterLayoutChangedEventArgsDescription, 'WebSplitterLayoutChangedEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSplitterLayoutChangedEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "SplitterLayoutChangedEventArgs";
	constructor() {
		super();
	}
	private _detail: WebSplitterLayoutChangedEventArgsDetailDescription = null;
	get detail(): WebSplitterLayoutChangedEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebSplitterLayoutChangedEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


