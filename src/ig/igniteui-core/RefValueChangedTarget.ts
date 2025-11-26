import { Base, Type, markType } from "./type";
import { DescriptionRefValueChangedEventArgs } from "./DescriptionRefValueChangedEventArgs";

/**
 * @hidden 
 */
export class RefValueChangedTarget extends Base {
	static $t: Type = markType(RefValueChangedTarget, 'RefValueChangedTarget');
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
	private _handler: (sender: any, args: DescriptionRefValueChangedEventArgs) => void = null;
	get handler(): (sender: any, args: DescriptionRefValueChangedEventArgs) => void {
		return this._handler;
	}
	set handler(value: (sender: any, args: DescriptionRefValueChangedEventArgs) => void) {
		this._handler = value;
	}
	private _container: any = null;
	get container(): any {
		return this._container;
	}
	set container(value: any) {
		this._container = value;
	}
}


