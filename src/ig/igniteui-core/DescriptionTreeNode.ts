import { Base, typeCast, Array_$type, String_$type, Type, markType } from "./type";
import { List$1 } from "./List$1";
import { DescriptionPropertyValue } from "./DescriptionPropertyValue";
import { Dictionary$2 } from "./Dictionary$2";
import { TypeDescriptionMetadata } from "./TypeDescriptionMetadata";

/**
 * @hidden 
 */
export class DescriptionTreeNode extends Base {
	static $t: Type = markType(DescriptionTreeNode, 'DescriptionTreeNode');
	private static nextId: number = 0;
	private _id: number = 0;
	get id(): number {
		return this._id;
	}
	set id(value: number) {
		this._id = value;
	}
	constructor() {
		super();
		this.id = DescriptionTreeNode.nextId;
		DescriptionTreeNode.nextId++;
		if (DescriptionTreeNode.nextId >= 0x7FFFFFFFFFFFFFFF) {
			DescriptionTreeNode.nextId = 0;
		}
	}
	clone(): DescriptionTreeNode {
		let newNode: DescriptionTreeNode = new DescriptionTreeNode();
		newNode.id = this.id;
		newNode.type = this.type;
		for (let i = 0; i < this._items.count; i++) {
			let pName = this._items._inner[i].propertyName;
			let meta = this._items._inner[i].metadata;
			let value = this._items._inner[i].value;
			if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, value) !== null) {
				value = (<DescriptionTreeNode>value).clone();
			}
			if (typeCast<any[]>(Array_$type, value) !== null) {
				let arr = <any[]>value;
				let newArr: any[] = <any[]>new Array(arr.length);
				for (let j = 0; j < arr.length; j++) {
					if (typeCast<DescriptionTreeNode>((<any>DescriptionTreeNode).$type, arr[j]) !== null) {
						newArr[j] = (<DescriptionTreeNode>arr[j]).clone();
					} else {
						newArr[j] = arr[j];
					}
				}
				value = newArr;
			}
			newNode.addWithMeta(pName, value, meta);
		}
		return newNode;
	}
	private _items: List$1<DescriptionPropertyValue> = new List$1<DescriptionPropertyValue>((<any>DescriptionPropertyValue).$type, 0);
	private _map: Dictionary$2<string, DescriptionPropertyValue> = new Dictionary$2<string, DescriptionPropertyValue>(String_$type, (<any>DescriptionPropertyValue).$type, 0);
	private _type: string = null;
	get type(): string {
		return this._type;
	}
	set type(value: string) {
		this._type = value;
	}
	add(propertyName: string, value: any): void {
		let p: DescriptionPropertyValue = new DescriptionPropertyValue();
		p.propertyName = propertyName;
		p.value = value;
		this._map.addItem(propertyName.toLowerCase(), p);
		this._items.add(p);
	}
	addWithMeta(propertyName: string, value: any, meta: TypeDescriptionMetadata): void {
		let p: DescriptionPropertyValue = new DescriptionPropertyValue();
		p.propertyName = propertyName;
		p.value = value;
		p.metadata = meta;
		this._map.addItem(propertyName.toLowerCase(), p);
		this._items.add(p);
	}
	update(propertyName: string, value: any): void {
		if (this._map.containsKey(propertyName.toLowerCase())) {
			let p = this._map.item(propertyName.toLowerCase());
			p.value = value;
		} else {
			this.add(propertyName, value);
		}
	}
	updateWithMeta(propertyName: string, value: any, meta: TypeDescriptionMetadata): void {
		if (this._map.containsKey(propertyName.toLowerCase())) {
			let p = this._map.item(propertyName.toLowerCase());
			p.value = value;
		} else {
			this.addWithMeta(propertyName, value, meta);
		}
	}
	get(propertyName: string): DescriptionPropertyValue {
		return this._map.item(propertyName.toLowerCase());
	}
	has(propertyName: string): boolean {
		return this._map.containsKey(propertyName.toLowerCase());
	}
	remove(propertyName: string): void {
		if (this._map.containsKey(propertyName.toLowerCase())) {
			let item = this._map.item(propertyName.toLowerCase());
			this._map.removeItem(propertyName.toLowerCase());
			this._items.remove(item);
		}
	}
	clear(): void {
		this._map.clear();
		this._items.clear();
	}
	items(): List$1<DescriptionPropertyValue> {
		return this._items;
	}
}


