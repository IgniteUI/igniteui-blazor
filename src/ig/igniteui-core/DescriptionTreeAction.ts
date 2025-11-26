import { Base, Type, markType } from "./type";
import { DescriptionTreeActionType } from "./DescriptionTreeActionType";
import { DescriptionTreeNode } from "./DescriptionTreeNode";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";
import { TypeDescriptionPlatform } from "./TypeDescriptionPlatform";
import { stringEndsWith } from "./string";

/**
 * @hidden 
 */
export class DescriptionTreeAction extends Base {
	static $t: Type = markType(DescriptionTreeAction, 'DescriptionTreeAction');
	private _actionType: DescriptionTreeActionType = <DescriptionTreeActionType>0;
	get actionType(): DescriptionTreeActionType {
		return this._actionType;
	}
	set actionType(value: DescriptionTreeActionType) {
		this._actionType = value;
	}
	private _targetNode: DescriptionTreeNode = null;
	get targetNode(): DescriptionTreeNode {
		return this._targetNode;
	}
	set targetNode(value: DescriptionTreeNode) {
		this._targetNode = value;
	}
	private _currentNode: DescriptionTreeNode = null;
	get currentNode(): DescriptionTreeNode {
		return this._currentNode;
	}
	set currentNode(value: DescriptionTreeNode) {
		this._currentNode = value;
	}
	private _propertyMetadata: TypeDescriptionMetadata = null;
	get propertyMetadata(): TypeDescriptionMetadata {
		return this._propertyMetadata;
	}
	set propertyMetadata(value: TypeDescriptionMetadata) {
		this._propertyMetadata = value;
	}
	private _propertyName: string = null;
	get propertyName(): string {
		return this._propertyName;
	}
	set propertyName(value: string) {
		this._propertyName = value;
	}
	getPlatformPropertyName(platform: TypeDescriptionPlatform): string {
		let name = this.propertyName;
		if (this.propertyMetadata != null) {
			name = this.propertyMetadata.getPlatformName(platform);
		} else {
			if (TypeDescriptionMetadata.shouldCamelize(platform)) {
				name = TypeDescriptionMetadata.camelize(this.propertyName);
			}
		}
		if (stringEndsWith(name, "Ref")) {
			name = name.substr(0, name.length - ("Ref").length);
		}
		return name;
	}
	private _oldValue: any = null;
	get oldValue(): any {
		return this._oldValue;
	}
	set oldValue(value: any) {
		this._oldValue = value;
	}
	private _newValue: any = null;
	get newValue(): any {
		return this._newValue;
	}
	set newValue(value: any) {
		this._newValue = value;
	}
	private _oldIndex: number = 0;
	get oldIndex(): number {
		return this._oldIndex;
	}
	set oldIndex(value: number) {
		this._oldIndex = value;
	}
	private _newIndex: number = 0;
	get newIndex(): number {
		return this._newIndex;
	}
	set newIndex(value: number) {
		this._newIndex = value;
	}
}


