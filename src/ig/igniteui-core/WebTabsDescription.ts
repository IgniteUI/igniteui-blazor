import { Description } from "./Description";
import { WebTabDescription } from "./WebTabDescription";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebTabsDescription extends Description {
	static $t: Type = markType(WebTabsDescription, 'WebTabsDescription', (<any>Description).$type);
	protected get_type(): string {
		return "WebTabs";
	}
	get type(): string {
		return this.get_type();
	}
	constructor() {
		super();
	}
	private _tabsCollection: WebTabDescription[] = null;
	get tabsCollection(): WebTabDescription[] {
		return this._tabsCollection;
	}
	set tabsCollection(value: WebTabDescription[]) {
		this._tabsCollection = value;
		this.markDirty("TabsCollection");
	}
	private _alignment: string = null;
	get alignment(): string {
		return this._alignment;
	}
	set alignment(value: string) {
		this._alignment = value;
		this.markDirty("Alignment");
	}
	private _activation: string = null;
	get activation(): string {
		return this._activation;
	}
	set activation(value: string) {
		this._activation = value;
		this.markDirty("Activation");
	}
	private _change: string = null;
	get changeRef(): string {
		return this._change;
	}
	set changeRef(value: string) {
		this._change = value;
		this.markDirty("ChangeRef");
	}
}


