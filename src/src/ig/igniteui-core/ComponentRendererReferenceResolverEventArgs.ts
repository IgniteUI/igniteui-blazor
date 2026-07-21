import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class ComponentRendererReferenceResolverEventArgs extends Base {
	static $t: Type = markType(ComponentRendererReferenceResolverEventArgs, 'ComponentRendererReferenceResolverEventArgs');
	private _found: boolean = false;
	get found(): boolean {
		return this._found;
	}
	set found(value: boolean) {
		this._found = value;
	}
	private _referenceValue: any = null;
	get referenceValue(): any {
		return this._referenceValue;
	}
	set referenceValue(value: any) {
		this._referenceValue = value;
	}
}


