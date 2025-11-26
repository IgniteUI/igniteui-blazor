import { Description } from "./Description";
import { WebTreeItemDescription } from "./WebTreeItemDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTreeSelectionEventArgsDetailDescription extends Description {
	static $t: Type = markType(WebTreeSelectionEventArgsDetailDescription, 'WebTreeSelectionEventArgsDetailDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTreeSelectionEventArgsDetail";
	}
	get type(): string {
		return this.get_type();
	}
	private static __marshalByValue: boolean = true;
	private static __marshalByValueAlias: string = "TreeSelectionEventArgsDetail";
	constructor() {
		super();
	}
	private _newSelection: WebTreeItemDescription[] = null;
	get newSelection(): WebTreeItemDescription[] {
		return this._newSelection;
	}
	set newSelection(value: WebTreeItemDescription[]) {
		this._newSelection = value;
		this.markDirty("NewSelection");
	}
}


