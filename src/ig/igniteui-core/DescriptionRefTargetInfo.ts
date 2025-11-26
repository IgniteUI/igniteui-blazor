import { Base, Type, markType } from "./type";

/**
 * @hidden 
 */
export class DescriptionRefTargetInfo extends Base {
	static $t: Type = markType(DescriptionRefTargetInfo, 'DescriptionRefTargetInfo');
	private _target: any = null;
	get target(): any {
		return this._target;
	}
	set target(value: any) {
		this._target = value;
	}
	private _propertyName: string = null;
	get propertyName(): string {
		return this._propertyName;
	}
	set propertyName(value: string) {
		this._propertyName = value;
	}
	private _container: any = null;
	get container(): any {
		return this._container;
	}
	set container(value: any) {
		this._container = value;
	}
}


