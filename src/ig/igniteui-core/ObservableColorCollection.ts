import { ObservableCollection$1 } from "./ObservableCollection$1";
import { Color } from "./Color";
import { InterpolationMode } from "./InterpolationMode";
import { NotifyCollectionChangedEventArgs } from "./NotifyCollectionChangedEventArgs";
import { Base, IList$1, IList$1_$type, IEnumerator$1, IEnumerator$1_$type, IEnumerable$1, IEnumerable$1_$type, IEnumerator, IEnumerator_$type, IDisposable, IDisposable_$type, fromEnum, Type, markType } from "./type";
import { List$1 } from "./List$1";
import { NotifyCollectionChangedAction } from "./NotifyCollectionChangedAction";

/**
 * @hidden 
 */
export class ObservableColorCollection extends ObservableCollection$1<Color> {
	static $t: Type = markType(ObservableColorCollection, 'ObservableColorCollection', (<any>ObservableCollection$1).$type.specialize((<any>Color).$type));
	constructor() {
		super((<any>Color).$type, 0);
	}
	get interpolationMode(): InterpolationMode {
		return this._interpolationMode;
	}
	set interpolationMode(value: InterpolationMode) {
		if (this._interpolationMode != value) {
			this._interpolationMode = value;
			this.onCollectionChanged(new NotifyCollectionChangedEventArgs(0, NotifyCollectionChangedAction.Reset));
		}
	}
	private _interpolationMode: InterpolationMode = InterpolationMode.RGB;
	equals(obj: any): boolean {
		if (obj == null) {
			return false;
		}
		let collection: ObservableColorCollection = <ObservableColorCollection>obj;
		if (collection.count != this.count) {
			return false;
		}
		for (let i: number = 0; i < collection.count; i++) {
			if (!collection._inner[i].equals(this._inner[i])) {
				return false;
			}
		}
		return true;
	}
	static from(colorStrings: IList$1<string>): ObservableColorCollection {
		let colors = new ObservableColorCollection();
		for (let str of fromEnum<string>(colorStrings)) {
			let color = new Color();
			color.colorString = str;
			colors.add(color);
		}
		return colors;
	}
}


