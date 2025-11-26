import { Base, Type, markType } from "./type";
import { DescriptionTreeNode } from "./DescriptionTreeNode";

/**
 * @hidden 
 */
export class DescriptionTreeReconciler_TreeModeData extends Base {
	static $t: Type = markType(DescriptionTreeReconciler_TreeModeData, 'DescriptionTreeReconciler_TreeModeData');
	private _name: string = null;
	get name(): string {
		return this._name;
	}
	set name(value: string) {
		this._name = value;
	}
	private _node: DescriptionTreeNode = null;
	get node(): DescriptionTreeNode {
		return this._node;
	}
	set node(value: DescriptionTreeNode) {
		this._node = value;
	}
	private _index: number = 0;
	get index(): number {
		return this._index;
	}
	set index(value: number) {
		this._index = value;
	}
	private _toRemove: boolean = false;
	get toRemove(): boolean {
		return this._toRemove;
	}
	set toRemove(value: boolean) {
		this._toRemove = value;
	}
	private _targetIndex: number = 0;
	get targetIndex(): number {
		return this._targetIndex;
	}
	set targetIndex(value: number) {
		this._targetIndex = value;
	}
	private _toAdd: boolean = false;
	get toAdd(): boolean {
		return this._toAdd;
	}
	set toAdd(value: boolean) {
		this._toAdd = value;
	}
}


