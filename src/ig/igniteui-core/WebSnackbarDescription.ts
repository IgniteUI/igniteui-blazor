import { WebBaseAlertLikeDescription } from "./WebBaseAlertLikeDescription";
import { Description } from "./Description";
import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class WebSnackbarDescription extends WebBaseAlertLikeDescription {
	static $t: Type = markType(WebSnackbarDescription, 'WebSnackbarDescription', (<any>WebBaseAlertLikeDescription).$type);
	protected get_type(): string {
		return "WebSnackbar";
	}
	constructor() {
		super();
	}
	private _actionText: string = null;
	get actionText(): string {
		return this._actionText;
	}
	set actionText(value: string) {
		this._actionText = value;
		this.markDirty("ActionText");
	}
	private _action: string = null;
	get actionRef(): string {
		return this._action;
	}
	set actionRef(value: string) {
		this._action = value;
		this.markDirty("ActionRef");
	}
}


