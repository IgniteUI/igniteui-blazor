import { EventArgs, IList, IList_$type, Base, Type, markType } from "./type";
import { NotifyCollectionChangedAction } from "./NotifyCollectionChangedAction";
import { List$1 } from "./List$1";

/**
 * @hidden 
 */
export class NotifyCollectionChangedEventArgs extends EventArgs {
	static $t: Type = markType(NotifyCollectionChangedEventArgs, 'NotifyCollectionChangedEventArgs', (<any>EventArgs).$type);
	constructor(initNumber: number, action: NotifyCollectionChangedAction);
	constructor(initNumber: number, action: NotifyCollectionChangedAction, changedItem: any, index: number);
	constructor(initNumber: number, action: NotifyCollectionChangedAction, newItem: any, oldItem: any, index: number);
	constructor(initNumber: number, ..._rest: any[]);
	constructor(initNumber: number, ..._rest: any[]) {
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				let action: NotifyCollectionChangedAction = <NotifyCollectionChangedAction>_rest[0];
				super();
				this._action = <NotifyCollectionChangedAction>0;
				this._newItems = null;
				this._newStartingIndex = 0;
				this._oldItems = null;
				this._oldStartingIndex = 0;
				this._action = action;
				this._oldItems = new List$1<any>((<any>Base).$type, 0);
				this._newItems = new List$1<any>((<any>Base).$type, 0);
			}
			break;

			case 1:
			{
				let action: NotifyCollectionChangedAction = <NotifyCollectionChangedAction>_rest[0];
				let changedItem: any = <any>_rest[1];
				let index: number = <number>_rest[2];
				super();
				this._action = <NotifyCollectionChangedAction>0;
				this._newItems = null;
				this._newStartingIndex = 0;
				this._oldItems = null;
				this._oldStartingIndex = 0;
				this._action = action;
				this._oldItems = new List$1<any>((<any>Base).$type, 0);
				if (this._action == NotifyCollectionChangedAction.Remove || this._action == NotifyCollectionChangedAction.Replace) {
					this._oldItems.add(changedItem);
					this._oldStartingIndex = index;
				}
				if (this._action != NotifyCollectionChangedAction.Remove) {
					this._newItems = ((() => {
						let $ret = new List$1<any>((<any>Base).$type, 0);
						$ret.add1(changedItem);
						return $ret;
					})());
				} else {
					this._newItems = new List$1<any>((<any>Base).$type, 0);
				}
				this._newStartingIndex = index;
			}
			break;

			case 2:
			{
				let action: NotifyCollectionChangedAction = <NotifyCollectionChangedAction>_rest[0];
				let newItem: any = <any>_rest[1];
				let oldItem: any = <any>_rest[2];
				let index: number = <number>_rest[3];
				super();
				this._action = <NotifyCollectionChangedAction>0;
				this._newItems = null;
				this._newStartingIndex = 0;
				this._oldItems = null;
				this._oldStartingIndex = 0;
				this._action = action;
				this._newStartingIndex = index;
				this._oldStartingIndex = index;
				this._newItems = ((() => {
					let $ret = new List$1<any>((<any>Base).$type, 0);
					$ret.add1(newItem);
					return $ret;
				})());
				this._oldItems = ((() => {
					let $ret = new List$1<any>((<any>Base).$type, 0);
					$ret.add1(oldItem);
					return $ret;
				})());
			}
			break;

		}

	}
	private _action: NotifyCollectionChangedAction;
	get action(): NotifyCollectionChangedAction {
		return this._action;
	}
	private _newItems: IList;
	get newItems(): IList {
		return this._newItems;
	}
	private _newStartingIndex: number;
	get newStartingIndex(): number {
		return this._newStartingIndex;
	}
	private _oldItems: IList;
	get oldItems(): IList {
		return this._oldItems;
	}
	private _oldStartingIndex: number;
	get oldStartingIndex(): number {
		return this._oldStartingIndex;
	}
}


