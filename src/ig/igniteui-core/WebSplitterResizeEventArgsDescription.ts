import { Description } from "./Description";
import { WebSplitterResizeEventArgsDetailDescription } from "./WebSplitterResizeEventArgsDetailDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSplitterResizeEventArgsDescription extends Description {
	static $t: Type = markType(WebSplitterResizeEventArgsDescription, 'WebSplitterResizeEventArgsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebSplitterResizeEventArgs";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "SplitterResizeEventArgs";
	constructor() {
		super();
	}
	private _detail: WebSplitterResizeEventArgsDetailDescription = null;
	get detail(): WebSplitterResizeEventArgsDetailDescription {
		return this._detail;
	}
	set detail(value: WebSplitterResizeEventArgsDetailDescription) {
		this._detail = value;
		this.markDirty("Detail");
	}
}


