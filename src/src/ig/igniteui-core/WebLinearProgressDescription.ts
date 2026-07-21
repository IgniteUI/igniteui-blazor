import { WebProgressBaseDescription } from "./WebProgressBaseDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebLinearProgressDescription extends WebProgressBaseDescription {
	static $t: Type = markType(WebLinearProgressDescription, 'WebLinearProgressDescription', (<any>WebProgressBaseDescription).$type);
	protected get_type(): string {
		return "WebLinearProgress";
	}
	constructor() {
		super();
	}
	private _striped: boolean = false;
	get striped(): boolean {
		return this._striped;
	}
	set striped(value: boolean) {
		this._striped = value;
		this.markDirty("Striped");
	}
	private _labelAlign: string = null;
	get labelAlign(): string {
		return this._labelAlign;
	}
	set labelAlign(value: string) {
		this._labelAlign = value;
		this.markDirty("LabelAlign");
	}
}


