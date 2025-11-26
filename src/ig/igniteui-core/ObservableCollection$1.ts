import { List$1 } from "./List$1";
import { INotifyCollectionChanged, INotifyCollectionChanged_$type } from "./INotifyCollectionChanged";
import { INotifyPropertyChanged, INotifyPropertyChanged_$type, IEnumerable$1, IEnumerable$1_$type, Base, PropertyChangedEventArgs, Type, getBoxIfEnum, markType } from "./type";
import { NotifyCollectionChangedEventArgs } from "./NotifyCollectionChangedEventArgs";
import { NotifyCollectionChangedAction } from "./NotifyCollectionChangedAction";

/**
 * @hidden 
 */
export class ObservableCollection$1<T> extends List$1<T> implements INotifyCollectionChanged, INotifyPropertyChanged {
	static $t: Type = markType(ObservableCollection$1, 'ObservableCollection$1', (<any>List$1).$type.specialize(0), [INotifyCollectionChanged_$type, INotifyPropertyChanged_$type]);
	protected $t: Type;
	constructor($t: Type, initNumber: number);
	constructor($t: Type, initNumber: number, source: IEnumerable$1<T>);
	constructor($t: Type, initNumber: number, capacity: number);
	constructor($t: Type, initNumber: number, ..._rest: any[]);
	constructor($t: Type, initNumber: number, ..._rest: any[]) {
		initNumber = (initNumber == void 0) ? 0 : initNumber;
		switch (initNumber) {
			case 0:
			{
				super($t, 0);
				this.$t = $t;
				this.$type = this.$type.specialize(this.$t);
				this.collectionChanged = null;
				this.propertyChanged = null;
			}
			break;

			case 1:
			{
				let source: IEnumerable$1<T> = <IEnumerable$1<T>>_rest[0];
				super($t, 1, source);
				this.$t = $t;
				this.$type = this.$type.specialize(this.$t);
				this.collectionChanged = null;
				this.propertyChanged = null;
			}
			break;

			case 2:
			{
				let capacity: number = <number>_rest[0];
				super($t, 2, capacity);
				this.$t = $t;
				this.$type = this.$type.specialize(this.$t);
				this.collectionChanged = null;
				this.propertyChanged = null;
			}
			break;

		}

	}
	protected setItem(index: number, newItem: T): void {
		let oldItem: T = this._inner[index];
		super.setItem(index, newItem);
		if (this.propertyChanged != null) {
			this.onPropertyChanged(new PropertyChangedEventArgs("Item[]"));
		}
		if (this.collectionChanged != null) {
			let args = new NotifyCollectionChangedEventArgs(2, NotifyCollectionChangedAction.Replace, getBoxIfEnum<T>(this.$t, newItem), getBoxIfEnum<T>(this.$t, oldItem), index);
			this.onCollectionChanged(args);
		}
	}
	protected clearItems(): void {
		super.clearItems();
		if (this.propertyChanged != null) {
			this.onPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.onPropertyChanged(new PropertyChangedEventArgs("Item[]"));
		}
		if (this.collectionChanged != null) {
			let args = new NotifyCollectionChangedEventArgs(0, NotifyCollectionChangedAction.Reset);
			this.onCollectionChanged(args);
		}
	}
	protected insertItem(index: number, newItem: T): void {
		super.insertItem(index, newItem);
		if (this.propertyChanged != null) {
			this.onPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.onPropertyChanged(new PropertyChangedEventArgs("Item[]"));
		}
		if (this.collectionChanged != null) {
			let args = new NotifyCollectionChangedEventArgs(1, NotifyCollectionChangedAction.Add, getBoxIfEnum<T>(this.$t, newItem), index);
			this.onCollectionChanged(args);
		}
	}
	protected addItem(newItem: T): void {
		super.addItem(newItem);
		if (this.propertyChanged != null) {
			this.onPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.onPropertyChanged(new PropertyChangedEventArgs("Item[]"));
		}
		if (this.collectionChanged != null) {
			let args = new NotifyCollectionChangedEventArgs(1, NotifyCollectionChangedAction.Add, getBoxIfEnum<T>(this.$t, newItem), this.count - 1);
			this.onCollectionChanged(args);
		}
	}
	protected removeItem(index: number): void {
		let oldItem: T = this._inner[index];
		super.removeItem(index);
		if (this.propertyChanged != null) {
			this.onPropertyChanged(new PropertyChangedEventArgs("Count"));
			this.onPropertyChanged(new PropertyChangedEventArgs("Item[]"));
		}
		if (this.collectionChanged != null) {
			let args = new NotifyCollectionChangedEventArgs(1, NotifyCollectionChangedAction.Remove, getBoxIfEnum<T>(this.$t, oldItem), index);
			this.onCollectionChanged(args);
		}
	}
	collectionChanged: (sender: any, e: NotifyCollectionChangedEventArgs) => void;
	propertyChanged: (sender: any, e: PropertyChangedEventArgs) => void;
	protected onPropertyChanged(args: PropertyChangedEventArgs): void {
		if (this.propertyChanged != null) {
			this.propertyChanged(this, args);
		}
	}
	protected onCollectionChanged(args: NotifyCollectionChangedEventArgs): void {
		if (this.collectionChanged != null) {
			this.collectionChanged(this, args);
		}
	}
}


